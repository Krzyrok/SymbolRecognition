using System;

namespace Domain
{
    public class Symbol
    {
        private readonly int[,] value;

        public Symbol(int[,] symbolBits)
        {
            if (symbolBits.GetLength(0) != RowSize)
            {
                throw new ArgumentException("Incorrect row size");
            }

            if (symbolBits.GetLength(1) != ColumnSize)
            {
                throw new ArgumentException("Incorrect column size");
            }

            value = new int[RowSize, ColumnSize];
            for (var row = 0; row < RowSize; row++)
            {
                for (var column = 0; column < ColumnSize; column++)
                {
                    value[row, column] = symbolBits[row, column];
                }
            }
        }

        public static int ColumnSize => 8;

        public static int RowSize => 12;
    }
}