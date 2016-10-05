using System;

namespace Domain
{
    public class SymbolValues 
    {
        // TODO: inverse ColumnSize and RowSize - ColumnSize => 8, RowSize => 12; also change factory method: SymbolFactory.CreateFromDigit!!!!
        public static int ColumnSize => 12;

        public static int RowSize => 8;

        public int[,] RawValues { get; }

        public int this[int row, int column]
        {
            get { return RawValues[row, column]; }
            set { RawValues[row, column] = value; }
        }

        public SymbolValues(int[,] values, Func<int, int> convertValue)
        {
            if (values.GetLength(0) != RowSize)
            {
                throw new ArgumentException("Incorrect row size");
            }

            if (values.GetLength(1) != ColumnSize)
            {
                throw new ArgumentException("Incorrect column size");
            }

            RawValues = new int[RowSize, ColumnSize];

            for (var row = 0; row < RowSize; row++)
            {
                for (var column = 0; column < ColumnSize; column++)
                {
                    RawValues[row, column] = convertValue(values[row, column]);
                }
            }
        }
    }
}