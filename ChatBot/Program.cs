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
            // Get Token
            System.Console.WriteLine("TOKEN: ");
            string Token = Convert.ToString(System.Console.ReadLine());

            // Create Bot
            DiscoBot TestBot = new DiscoBot(Token);
        }
    }
}
