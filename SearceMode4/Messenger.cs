using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearceMode4
{
    //internal delegate void MessageHandler(string message);
    internal static class Messenger
    {
        //internal static MessageHandler mes = Message;

        internal static void Message(string message)
        {
            Console.WriteLine(message);
        }
    }
}
