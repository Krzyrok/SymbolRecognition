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
            var symbolValues = new int[BipolarSymbol.RowSize + 1, BipolarSymbol.ColumnSize];

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
            var symbolValues = new int[BipolarSymbol.RowSize, BipolarSymbol.ColumnSize + 1];

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
            IEnumerable<int> bipolarSymbolValues = from int value in bipolarSymbol.Values
                                                   select value;
            Assert.True(bipolarSymbolValues.All(value => value == -1));
        }

        private static int[,] SymbolValuesWithZeroes()
        {
            return new int[BipolarSymbol.RowSize, BipolarSymbol.ColumnSize];
        }


        [Fact]
        public void ShouldDoesNotChangeCorrectValues()
        {
            // given
            var symbolValues = new int[BipolarSymbol.RowSize, BipolarSymbol.ColumnSize];
            const int rowWithNegativeValue = 0;
            const int rowWithPositiveValue = 1;
            for (var column = 0; column < BipolarSymbol.ColumnSize; column++)
            {
                symbolValues[rowWithNegativeValue, column] = -1;
                symbolValues[rowWithPositiveValue, column] = 1;
            }

            // when
            var bipolarSymbol = new BipolarSymbol(symbolValues);

            // then
            for (var column = 0; column < BipolarSymbol.ColumnSize; column++)
            {
                Assert.Equal(-1, bipolarSymbol.Values[rowWithNegativeValue, column]);
                Assert.Equal(1, bipolarSymbol.Values[rowWithPositiveValue, column]);
            }
        }
    }
}