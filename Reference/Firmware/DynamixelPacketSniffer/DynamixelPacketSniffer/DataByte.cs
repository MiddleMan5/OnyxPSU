using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamixelPacketSniffer {
    class DataByte {

        public byte Value { get; set; }
        public double Time { get; set; }
        public string ParityError { get; set; }
        public string FrameError { get; set; }

        public DataByte(string csv_line) {
            var data = csv_line.Split(',');
            Time = double.Parse(data[0],System.Globalization.NumberStyles.Any);
            Value = byte.Parse(data[1].Substring(2), System.Globalization.NumberStyles.HexNumber);
            ParityError = data[2];
            FrameError = data[3];
        }

        public override string ToString() {
            string s = (Time/1000).ToString() + " \t" + Value.ToString("X");
            return s;
        }

    }
}
