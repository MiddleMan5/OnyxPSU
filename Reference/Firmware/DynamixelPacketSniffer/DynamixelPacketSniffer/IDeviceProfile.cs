using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamixelPacketSniffer {
    interface IDeviceProfile {
        // profile to analyse a type of device

        string getName();
        string getAddressName(byte addr);
        string formatData(byte addr, List<DataByte> data);

    }
}
