using System;

namespace Domain
{
    public class BipolarSymbol
    {
        public int[,] Values { get; }

        public BipolarSymbol(int[,] symbolBits)
        {
            if (symbolBits.GetLength(0) != RowSize)
            {
                throw new ArgumentException("Incorrect row size");
            }

            if (symbolBits.GetLength(1) != ColumnSize)
            {
                throw new ArgumentException("Incorrect column size");
            }

            Values = new int[RowSize, ColumnSize];
            for (var row = 0; row < RowSize; row++)
            {
                for (var column = 0; column < ColumnSize; column++)
                {
                    Values[row, column] = BinaryToBipolar(symbolBits[row, column]);
                }
            }
        }

        private static int BinaryToBipolar(int binary)
        {
            return binary == 1 ? 1 : -1;
        }

        public static int ColumnSize => 8;

        public static int RowSize => 12;
    }
}