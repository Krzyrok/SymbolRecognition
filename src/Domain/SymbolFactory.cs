using System;

namespace Domain
{
    public static class SymbolFactory
    {
        public static Symbol CreateFromDigit(int digit)
        {
            switch (digit)
            {
                case 0:
                    return new Symbol(Digit0);
                case 1:
                    return new Symbol(Digit1);
                case 2:
                    return new Symbol(Digit2);
                case 3:
                    return new Symbol(Digit3);
                case 4:
                    return new Symbol(Digit4);
                default:
                    throw new ArgumentException("Incorrect digit");
            }
        }

        private static readonly int[,] Digit0 =
        {
            {0,0,1,1,1,1,1,1,1,0,0,0},
            {0,1,1,1,1,1,1,1,1,1,0,0},
            {0,1,0,0,0,1,1,0,0,1,0,0},
            {0,1,0,0,1,1,0,0,0,1,0,0},
            {0,1,0,1,1,0,0,0,0,1,0,0},
            {0,1,1,1,1,1,1,1,1,1,0,0},
            {0,0,1,1,1,1,1,1,1,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0}
        };

        private static readonly int[,] Digit1 =
        {
            {0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,1,0,0,0,0,0,1,0,0},
            {0,0,1,1,0,0,0,0,0,1,0,0},
            {0,1,1,1,1,1,1,1,1,1,0,0},
            {0,1,1,1,1,1,1,1,1,1,0,0},
            {0,0,0,0,0,0,0,0,0,1,0,0},
            {0,0,0,0,0,0,0,0,0,1,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0}
        };

        private static readonly int[,] Digit2 =
        {
            {0,0,1,0,0,0,0,0,1,1,0,0},
            {0,1,1,0,0,0,0,1,1,1,0,0},
            {0,1,0,0,0,0,1,1,0,1,0,0},
            {0,1,0,0,0,1,1,0,0,1,0,0},
            {0,1,0,0,1,1,0,0,0,1,0,0},
            {0,1,1,1,1,0,0,0,1,1,0,0},
            {0,0,1,1,0,0,0,0,1,1,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0}
        };

        private static readonly int[,] Digit3 =
        {
            {0,0,1,0,0,0,0,0,1,0,0,0},
            {0,1,1,0,0,0,0,0,1,1,0,0},
            {0,1,0,0,0,1,0,0,0,1,0,0},
            {0,1,0,0,0,1,0,0,0,1,0,0},
            {0,1,0,0,0,1,0,0,0,1,0,0},
            {0,1,1,1,1,1,1,1,1,1,0,0},
            {0,0,1,1,1,0,1,1,1,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0}
        };

        private static readonly int[,] Digit4 =
        {
            {0,0,0,0,0,1,1,0,0,0,0,0},
            {0,0,0,0,1,1,1,0,0,0,0,0},
            {0,0,0,1,1,0,1,0,0,0,0,0},
            {0,0,1,1,0,0,1,0,0,1,0,0},
            {0,1,1,1,1,1,1,1,1,1,0,0},
            {0,1,1,1,1,1,1,1,1,1,0,0},
            {0,0,0,0,0,0,1,0,0,1,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0}
        };
    }
}