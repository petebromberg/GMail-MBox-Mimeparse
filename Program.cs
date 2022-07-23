using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using System.IO;

namespace Mimeparse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\temp\notifications\Notifications.mbox";

            FileStream stm = new FileStream(filePath, FileMode.Open);
            MimeKit.MimeParser parser = new MimeParser(stm, MimeFormat.Mbox);

            
            try
            {
                while (!parser.IsEndOfStream)
                {
                    
                    var message = parser.ParseMessage();
                    // use substring to get out email betweeen "< and >"]
                    /// sample: From: Kiran Bingi <bkiran@futransolutions.com>
                    string line = message.From[0].ToString();
                    //  if (line.Contains(","))
                    string name = "";
                    string email = "";
                    try
                    {
                        if(line.Contains("<"))
                        name = line.Substring(0, line.IndexOf("<"));
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.ToString());
                    };
                    if(line.Contains("<"))
                     email = line.Substring(line.IndexOf("<")+1, line.IndexOf(">") -1);
                    Console.WriteLine( name + "," +email);
                  // else
                      //  Console.WriteLine(line);
                   // add to csv file here
                  
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
