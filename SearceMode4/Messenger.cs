using System;

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