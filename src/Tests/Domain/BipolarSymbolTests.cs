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
            // when
            var bipolarSymbol = new BipolarSymbol(SymbolValuesWithZeroes());

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
            const int rowWithNegativeValue = 0;
            const int rowWithPositiveValue = 1;

            // when
            var bipolarSymbol = new BipolarSymbol(SymbolValuesWithVariedValues(rowWithNegativeValue, rowWithPositiveValue));

            // then
            for (var column = 0; column < SymbolValues.ColumnSize; column++)
            {
                Assert.Equal(-1, bipolarSymbol.Values[rowWithNegativeValue, column]);
                Assert.Equal(1, bipolarSymbol.Values[rowWithPositiveValue, column]);
            }
        }

        private static int[,] SymbolValuesWithVariedValues(int rowWithNegativeValue, int rowWithPositiveValue)
        {
            var symbolValues = new int[SymbolValues.RowSize, SymbolValues.ColumnSize];
            for (var column = 0; column < SymbolValues.ColumnSize; column++)
            {
                symbolValues[rowWithNegativeValue, column] = -1;
                symbolValues[rowWithPositiveValue, column] = 1;
            }

            return symbolValues;
        }

        [Fact]
        public void ShouldInverseValues()
        {
            // given
            const int rowWithOriginallyNegativeValue = 0;
            const int rowWithOriginallyPositiveValue = 1;
            var bipolarSymbol = new BipolarSymbol(SymbolValuesWithVariedValues(rowWithOriginallyNegativeValue, rowWithOriginallyPositiveValue));

            // when
            bipolarSymbol.Inverse();

            // then
            for (var column = 0; column < SymbolValues.ColumnSize; column++)
            {
                Assert.Equal(1, bipolarSymbol.Values[rowWithOriginallyNegativeValue, column]);
                Assert.Equal(-1, bipolarSymbol.Values[rowWithOriginallyPositiveValue, column]);
            }
        }
    }
}