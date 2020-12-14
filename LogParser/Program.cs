using System;

namespace LogParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Logic logic = new Logic();
            logic.Log(@"Sample_Input.csv");
        }
    }
}
