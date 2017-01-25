using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace DynamixelPacketSniffer {
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window {
        public Window1() {
            InitializeComponent();
        }


        
        List<DynamixelPacket> packets = new List<DynamixelPacket>();
        string[] headers = { "" };

        private void UpdateGUI() {
            lvPackets.Items.Clear();
            foreach (var p in packets) {
                
                lvPackets.Items.Add(p);
                // TODO change row bg color based on IsStatus 

            }
        }

        private void LoadFile(string filename) {
            string[] lines;
            try
            {
                lines = File.ReadAllLines(filename);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("The file could not be found, nOOb", "n00bException");
                return;
            }

            if (lines.Length < 2) {
                MessageBox.Show("The file is empty, nOOb >_>", "n00bException");
                return;
            }
            
            headers = lines[0].Split(',');
            List<DataByte> dbs = new List<DataByte>();
            foreach (string l in lines.Skip(1)) {
                if (l.Length == 0) {
                    continue;
                }
                DataByte db = new DataByte(l);
                dbs.Add(db);
            }

            packets = DynamixelPacket.Parse(dbs);
        }



        private void bLoad_Click(object sender, RoutedEventArgs e) {
            LoadFile(tbFileName.Text);
            UpdateGUI();
        }

        private void LastNameCM_Click(object sender, RoutedEventArgs e) {

        }

    }
}
