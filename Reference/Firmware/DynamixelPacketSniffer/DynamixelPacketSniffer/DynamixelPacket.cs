using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamixelPacketSniffer {
    class DynamixelPacket {

        static Dictionary<Cmd, string> commands = new Dictionary<Cmd, string>();
        public enum Cmd {
            Ping = 0x01,
            ReadData = 0x02,
            WriteData = 0x03,
            RegWrite = 0x04,
            Action = 0x05,
            Reset = 0x06,
            Bootload = 0x08,
            SyncWrite = 0x83,
            SyncRead = 0x84
        }

        public static string CommandToString( byte cmd ) {
            if (commands.Count == 0) {
                commands.Add(Cmd.Ping, "PING");
                commands.Add(Cmd.ReadData, "READ_DATA");
                commands.Add(Cmd.WriteData, "WRITE_DATA");
                commands.Add(Cmd.RegWrite, "REG_WRITE");
                commands.Add(Cmd.Action, "ACTION");
                commands.Add(Cmd.Reset, "RESET");
                commands.Add(Cmd.Bootload, "BOOTLOAD");
                commands.Add(Cmd.SyncWrite, "SYNC_WRITE");
                commands.Add(Cmd.SyncRead, "SYNC_READ");

                commands.Add(0, "NO ERROR");

            }
            return commands[(Cmd)cmd];
        }

        
        
        protected List<DataByte> data = new List<DataByte>();
        public bool IsStatus = false;
        public bool IsValid = false;
        public bool IsComplete = false;
        public DynamixelPacket other; // the status if this one is an instruction (and has a status), and the instruction if this one is a status
        
        public string Time {
            get {
                return (data[0].Time * 1000.0).ToString("F2");
            }
        }
        public string ID {
            get {
                return data[2].Value.ToString();
            }
        }
        public string Command {
            get {
                return CommandToString(data[4].Value);
            }
        }
        public string Data {
            get {
                string res = "";
                
                if ( IsStatus ){
                    // TODO match with the instruction packet
                    foreach( var d in Enumerable.Range(5,data.Count - 6) ){
                        res += " 0x"+data[d].Value.ToString("X2");
                    }
                } else {
                    switch ((Cmd)data[4].Value) {
                        case(Cmd.Ping):
                            break;
                        case(Cmd.ReadData):
                            res += "Addr: 0x"+data[5].Value.ToString("X2");
                            res += " Len: 0x"+data[6].Value.ToString("X2");
                            break;
                        case(Cmd.WriteData):
                        case(Cmd.RegWrite):
                            res += "Addr: 0x"+data[5].Value.ToString("X2") + " Data:";
                            foreach( var d in Enumerable.Range(6,data.Count - 7) ){
                                res += " 0x"+data[d].Value.ToString("X2");
                            }
                            break;
                        case(Cmd.Action):
                            break;
                        case(Cmd.Reset):
                            break;
                        case(Cmd.Bootload):
                            break;
                        case(Cmd.SyncWrite):
                            break;
                        case(Cmd.SyncRead):
                            break;
                    }
                }
                
                return res; 
            }
        }

        public DynamixelPacket() {

        }

        public override string ToString() {
            string res = "";
            if (!IsValid) {
                res += " ";
            }
            else {
                res += "-";
            }

            res += (data[0].Time * 1000.0).ToString("F2") ;
            res += " \tCmd: " + CommandToString(data[4].Value);
            res += "\tID: " + data[2].Value.ToString();
            res += " \tData :";
            foreach( var d in Enumerable.Range(5,data.Count - 6) ){
                res += " 0x"+data[d].Value.ToString("X2");
            }

            return res;
        }


        enum DynamixelParserStates{
            SearchFirst0xFF,
            SearchSecond0xFF,
            GetID,
            GetLength,
            GetInstructionOrError,
            GetParamsOrCrc
        }

        public static List<DynamixelPacket> Parse(List<DataByte> data) {
            List<DynamixelPacket> packets = new List<DynamixelPacket>();

            DynamixelParserStates state = DynamixelParserStates.SearchFirst0xFF;

            DynamixelPacket p = new DynamixelPacket();

            int length = 0;
            int nb_params = 0;
            byte checksum = 0;

            foreach (DataByte d in data) {
                switch (state) {
                    case DynamixelParserStates.SearchFirst0xFF:
                        if (d.Value == 0xFF) {
                            state = DynamixelParserStates.SearchSecond0xFF;
                            p.data.Add(d);
                        }
                        else {
                            // TODO ignored data, store it somewhere to display it too!
                        }
                        break;
                    case DynamixelParserStates.SearchSecond0xFF:
                        if (d.Value == 0xFF) {
                            p.data.Add(d);
                            state = DynamixelParserStates.GetID;
                        }
                        break;
                    case DynamixelParserStates.GetID:
                        if (d.Value == 0xFF) {
                            // TODO get the first 0xFF and store it before removing it.
                            p.data.RemoveAt(0);
                        }
                        else {
                            checksum = d.Value;
                            state = DynamixelParserStates.GetLength;
                        }
                        p.data.Add(d);
                        break;
                    case DynamixelParserStates.GetLength:
                        p.data.Add(d);
                        state = DynamixelParserStates.GetInstructionOrError;
                        length = (int) d.Value;
                        nb_params = 2;
                        checksum += d.Value;
                        break;
                    case DynamixelParserStates.GetInstructionOrError:
                        p.data.Add(d);
                        if (d.Value == 0)
                        {
                            p.IsStatus = true;
                        }
                        state = DynamixelParserStates.GetParamsOrCrc;
                        checksum += d.Value;
                        break;
                    case DynamixelParserStates.GetParamsOrCrc:
                        p.data.Add(d);
                        if (nb_params++ == length) {
                            p.IsComplete = true;

                            //test CRC
                            byte cksm = (byte) (~checksum);
                            byte tmp = d.Value;
                            p.IsValid = (~checksum) == d.Value ;

                            packets.Add(p);
                            p = new DynamixelPacket();
                            state = DynamixelParserStates.SearchFirst0xFF;
                        }
                        else {
                            checksum += d.Value;
                        }
                        break;
                }
            }
            
            // TODO add the last incomplete packet

            return packets;
        }

    }
}
