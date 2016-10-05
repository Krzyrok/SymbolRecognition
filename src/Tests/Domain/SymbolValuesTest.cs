using System;
using Domain;
using Xunit;

namespace Tests.Domain
{
    public class SymbolValuesTest
    {
        private static int DoNotConvert(int value) => value;

        [Fact]
        public void ShouldRaiseErrorWhenIncorrectSymbolRowLength()
        {
            // given
            var symbolValues = new int[SymbolValues.RowSize + 1, SymbolValues.ColumnSize];

            // when
            Action creatingSymbol = () => new SymbolValues(symbolValues, DoNotConvert);

            // then
            var exception = Assert.Throws<ArgumentException>(creatingSymbol);
            Assert.True(exception.Message.Contains("row"));
        }

        [Fact]
        public void ShouldRaiseErrorWhenIncorrectSymbolColumnLength()
        {
            // given
            var symbolValues = new int[SymbolValues.RowSize, SymbolValues.ColumnSize + 1];

            // when
            Action creatingSymbol = () => new SymbolValues(symbolValues, DoNotConvert);

            // then
            var exception = Assert.Throws<ArgumentException>(creatingSymbol);
            Assert.True(exception.Message.Contains("column"));
        }

        [Fact]
        public void ShouldConvertToOneDimensionalArray()
        {
            // given
            var symbolValues = new int[SymbolValues.RowSize, SymbolValues.ColumnSize];
            symbolValues[0, 1] = 2;
            symbolValues[2, 3] = 3;
            var symbol = new SymbolValues(symbolValues, DoNotConvert);

            // when
            int[] symbolValuesArray = symbol.ConvertToOneDimensionalArray();

            // then
            var expectedArray = new[]
            {
                0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            };
            Assert.Equal(expectedArray, symbolValuesArray);
        }
    }
}