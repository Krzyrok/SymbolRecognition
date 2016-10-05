using System;

namespace Domain
{
    public class SymbolValues 
    {
        // TODO: inverse ColumnSize and RowSize - ColumnSize => 8, RowSize => 12; also change factory method: SymbolFactory.CreateFromDigit!!!!
        public static int ColumnSize => 12;

        public static int RowSize => 8;

        private readonly int[,] _rawValues;

        public int this[int row, int column] => _rawValues[row, column];

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

            _rawValues = new int[RowSize, ColumnSize];

            for (var row = 0; row < RowSize; row++)
            {
                for (var column = 0; column < ColumnSize; column++)
                {
                    _rawValues[row, column] = convertValue(values[row, column]);
                }
            }
        }

        public int[] ConvertToOneDimensionalArray()
        {
            var values = new int[RowSize * ColumnSize];
            var valueIndex = 0;
            for (var row = 0; row < RowSize; row++)
            {
                for (var column = 0; column < ColumnSize; column++)
                {
                    values[valueIndex++] = this[row, column];
                }
            }

            return values;
        }

        public void Inverse(Func<int, int> inverseValue)
        {
            for (var row = 0; row < RowSize; row++)
            {
                for (var column = 0; column < ColumnSize; column++)
                {
                    _rawValues[row, column] = inverseValue(_rawValues[row, column]);
                }
            }
        }
    }
}