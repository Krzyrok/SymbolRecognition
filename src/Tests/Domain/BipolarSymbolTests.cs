using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Xunit;
namespace Tests.Domain
{
    public class BipolarSymbolTests
    {

        [Fact]
        public void ShouldRaiseErrorWhenIncorrectSymbolRowLength()
        {
            // given
            var symbolValues = new int[SymbolValues.RowSize + 1, SymbolValues.ColumnSize];

            // when
            Action creatingSymbol = () => new BipolarSymbol(symbolValues);

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
            Action creatingSymbol = () => new BipolarSymbol(symbolValues);

            // then
            var exception = Assert.Throws<ArgumentException>(creatingSymbol);
            Assert.True(exception.Message.Contains("column"));
        }


        [Fact]
        public void ShouldConvertBinaryToBipolarValues()
        {
            // given
            int[,] symbolValues = SymbolValuesWithZeroes();

            // when
            var bipolarSymbol = new BipolarSymbol(symbolValues);

            // then
            Assert.True(bipolarSymbol.ConvertToOneDimensionalArray().All(value => value == -1));
        }

        private static int[,] SymbolValuesWithZeroes()
        {
            return new int[SymbolValues.RowSize, SymbolValues.ColumnSize];
        }


        [Fact]
        public void ShouldDoesNotChangeCorrectValues()
        {
            // given
            var symbolValues = new int[SymbolValues.RowSize, SymbolValues.ColumnSize];
            const int rowWithNegativeValue = 0;
            const int rowWithPositiveValue = 1;
            for (var column = 0; column < SymbolValues.ColumnSize; column++)
            {
                symbolValues[rowWithNegativeValue, column] = -1;
                symbolValues[rowWithPositiveValue, column] = 1;
            }

            // when
            var bipolarSymbol = new BipolarSymbol(symbolValues);

            // then
            for (var column = 0; column < SymbolValues.ColumnSize; column++)
            {
                Assert.Equal(-1, bipolarSymbol.Values[rowWithNegativeValue, column]);
                Assert.Equal(1, bipolarSymbol.Values[rowWithPositiveValue, column]);
            }
        }
    }
}