using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using System.IO;
using System.Configuration;

namespace Mimeparse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = ConfigurationManager.AppSettings["mboxLocation"]; // e.g.  @"C:\temp\notifications\Notifications.mbox";

            FileStream stm = new FileStream(filePath, FileMode.Open);
            MimeKit.MimeParser parser = new MimeParser(stm, MimeFormat.Mbox);
            int ctr = 0;
            string csv = String.Empty;
            csv += $"name, email" + Environment.NewLine;
            try
            {
                while (!parser.IsEndOfStream)
                {
                    var message = parser.ParseMessage();
                    string line = message.From[0].ToString();

                    string name = "";
                    string email = "";

                    System.Net.Mail.MailAddress addr = new System.Net.Mail.MailAddress(line);
                    name = addr.DisplayName;
                    email = addr.Address;
                    var newline = $"{name},{email}" + Environment.NewLine;
                    csv += newline;
                    Console.WriteLine(name + ", " + email);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            string csvvLocation = ConfigurationManager.AppSettings["csvLocation"];           
            File.WriteAllText(csvvLocation, csv);

        }
    }
}
