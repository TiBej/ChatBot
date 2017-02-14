using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMLbot;

namespace ChatBot
{
    class Program
    {
        static void Main(string[] args)
        {
            // True if Token isn't valid
            bool valid = true;

            do
            {
                // Get Token
                System.Console.WriteLine("TOKEN: ");
                string Token = Convert.ToString(System.Console.ReadLine());

                try
                {
                    // Create Bot
                    DiscoBot TestBot = new DiscoBot(Token);
                    valid = false;
                }
                catch
                {
                    System.Console.WriteLine("Invalid Token please try again");
                    System.Console.ReadKey();
                    System.Console.Clear();
                    valid = true; // repeat
                }

            } while (valid);
        }
    }
}
