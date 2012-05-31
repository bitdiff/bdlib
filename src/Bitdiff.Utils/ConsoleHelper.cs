using System;

namespace Bitdiff.Utils
{
    public class ConsoleHelper
    {
        public void DumpException(Exception exception, bool detailed)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0}: {1}", exception.GetType().Name, exception.Message);
            Console.ResetColor();

            if (!detailed)
                return;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(exception.StackTrace);
            Console.WriteLine();

            Console.ResetColor();

            if (exception.InnerException != null)
                DumpException(exception.InnerException, true);
        }
    }
}