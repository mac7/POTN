using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace POTN
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            List<string> ranges = new List<string>() { "5Ghz" };
            List<string> bands = new List<string>() { "10MHz", "20MHz", "40MHz" };
            
            foreach (var range in ranges)
            {
                comboBox_range.Items.Add(range);
            }
   
            foreach (var band in bands)
            {
                comboBox_band.Items.Add(band);
            }

            outputBox.Text += "\n\n Now works only for 5GHz range";

        }

        private List<double> PowerCalculation(string range, string band, Int32 gain)
        {
            switch (band)
            {
                case "10MHz":
                    return Powers(gain -= 3); //bug here
                   
                case "20MHz":
                    return Powers(gain); //bug here

                case "40MHz":
                    return Powers(gain += 3); //bug here

                default:
                    return new List<double>() { 0.00001, 0.00001, 0.00001 }; ;
  
            }

        }

        private List<double> Powers (Int32 gain)
        {
            double powerInWatt = Math.Pow(10, (-gain) / 10.0);
            double powerInDBm = 10 * Math.Log10(powerInWatt * 1000);
            double powerInDBW = powerInDBm - 30;
            return new List<double>() { Math.Round(powerInWatt, 4), powerInDBm, powerInDBW };
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = PowerCalculation(comboBox_range.SelectedItem.ToString(), comboBox_band.SelectedItem.ToString(), Convert.ToInt32(textBox_gain.Text));
                StringBuilder output = new StringBuilder();
                output.Append("Maximum transmitter power, Watt: ");
                output.Append(result[0].ToString() + "\n");
                output.Append("Maximum transmitter power, dBm: ");
                output.Append(result[1].ToString() + "\n");
                output.Append("Maximum transmitter power, dBW: ");
                output.Append(result[2].ToString());
                outputBox.Text = output.ToString();
            }
            catch (System.NullReferenceException)
            {

                outputBox.Text = "--- pick all values ---";
            }
            
            
            



            
        
        }
    }
}
