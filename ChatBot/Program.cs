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
            // Create Bot
            DiscoBot TestBot = new DiscoBot();

            // Setup AIML
            Bot AI = new Bot();
            AI.loadSettings();
            AI.loadAIMLFromFiles();
            AI.isAcceptingUserInput = false;

            // Setup User
            User myUser = new User("Username", AI);

            AI.isAcceptingUserInput = true;

            // Send and receive message
            while (true)
            {
                Request r = new Request(Console.ReadLine(), myUser, AI);
                Result res = AI.Chat(r);
                Console.WriteLine("Robot: " + res.Output);
            }
        }
    }
}
