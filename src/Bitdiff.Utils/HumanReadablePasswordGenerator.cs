using System;
using System.Text;

namespace Bitdiff.Utils
{
    public class HumanReadablePasswordGenerator
    {
        private static readonly char[] Vowels = new[] { 'a', 'e', 'i', 'o', 'u' };
        private static readonly char[] Consonants = new[] { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };
        private static readonly char[] Symbols = new[] { '*', '?', '/', '\\', '%', '$', '#', '@', '!', '~' };
        private static readonly char[] Numbers = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private readonly Random _random;

        public HumanReadablePasswordGenerator()
        {
            _random = new Random();
        }

        public string Generate(int numSyllables, int numNumeric, int numSymbols)
        {
            var password = new StringBuilder();
            
            for (var i = 0; i < numSyllables; i++)
            {
                password.Append(MakeSyllable());

                if (numNumeric > 0 && ((_random.Next() % 2) == 0))
                {
                    password.Append(MakeNumeric());
                    numNumeric--;
                }

                if (numSymbols > 0 && ((_random.Next() % 2) == 0))
                {
                    password.Append(MakeSymbol());
                    numSymbols--;
                }
            }

            while (numNumeric > 0)
            {
                password.Append(MakeNumeric());
                numNumeric--;
            }

            while (numSymbols > 0)
            {
                password.Append(MakeSymbol());
                numSymbols--;
            }

            return password.ToString();
        }

        private char MakeSymbol()
        {
            return Symbols[_random.Next(Symbols.Length)];
        }
        
        private char MakeNumeric()
        {
            return Numbers[_random.Next(Numbers.Length)];
        }
        
        private string MakeSyllable()
        {
            int len = _random.Next(3, 5);

            var syl = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                char c = i == 1 ? Vowels[_random.Next(Vowels.Length)] : Consonants[_random.Next(Consonants.Length)];

                // only first character can be uppercase.
                if (i == 0 && (_random.Next() % 2) == 0)
                    c = Char.ToUpper(c);

                syl.Append(c);
            }

            return syl.ToString();
        }
    }
}