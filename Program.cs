using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using System.IO;
using System.Configuration;
using System.Windows.Forms;

namespace Mimeparse
{
   
    internal class Program 
  
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args"></param>
        // <created>pbromberg,10/10/2022</created>
        // <changed>pbromberg,10/10/2022</changed>
         [STAThread]
        static void Main(string[] args)
        {
            string filePath = ConfigurationManager.AppSettings["mboxLocation"]; // e.g.  @"C:\temp\notifications\Notifications.mbox";
                                                                                // filePath = @"C\temp\notfications\notfications.mbox";
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "mbox files (*.mbox)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                  
                   
                }
            }
            
            FileStream stm = new FileStream(filePath, FileMode.Open);
            MimeKit.MimeParser parser = new MimeParser(stm, MimeFormat.Mbox);
            int ctr = 0;
            string csv = String.Empty;
            csv += $"date, name, email" + Environment.NewLine;
            int numDaysBack = Convert.ToInt32(ConfigurationManager.AppSettings["numDaysBack"]);
            try
            {
                while (!parser.IsEndOfStream )
                {
                    var message = parser.ParseMessage();
                    if (message.Date > DateTime.Now.AddDays(numDaysBack * -1))
                    {
                        var headers = message.Headers;
                        string line = message.From[0].ToString();

                        string name = "";
                        string email = "";
                        string date = message.Date.ToString();

                        System.Net.Mail.MailAddress addr = new System.Net.Mail.MailAddress(line);
                        name = addr.DisplayName;
                        email = addr.Address;
                        date = DateTime.Now.ToString();
                        if (String.IsNullOrEmpty(name))
                            name = email;
                        var newline = $"{date},{name},{email}" + Environment.NewLine;
                        csv += newline;
                        Console.WriteLine(date + ", " + name + ", " + email);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            string csvLocation = ConfigurationManager.AppSettings["csvLocation"];           
            File.WriteAllText(csvLocation, csv);
            Console.WriteLine("Done with export- any key to quit");
            Console.ReadKey();


        }
    }
}
