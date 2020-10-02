using Serie2.controler;
using Serie2.Model;
using System;

namespace Serie2
{
    /// <summary>
    /// Entry point of the Tick Tack Toe program. Act as a router that only calls the ticktacktoe controller
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point, loop on user input untill "stop"
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ControlerTickTackToe controlerTickTackToe = new ControlerTickTackToe();
            string userInput;
            do
            {
                userInput = Console.ReadLine();
                controlerTickTackToe.ParseInput(userInput);
            } while (!userInput.Contains("stop"));
        }
    }
}
