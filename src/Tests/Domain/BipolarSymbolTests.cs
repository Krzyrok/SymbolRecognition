using System.Linq;
using Domain;
using Xunit;
namespace Tests.Domain
{
    public class BipolarSymbolTests
    {
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