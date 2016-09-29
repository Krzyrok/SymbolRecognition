using System;
using Domain;
using Xunit;
namespace Tests.Domain
{
    public class SymbolTests
    {

        [Fact]
        public void ShouldRaiseErrorWhenIncorrectSymbolRowLength()
        {
            // given
            var symbolValues = new int[Symbol.RowSize + 1, Symbol.ColumnSize];

            // when
            Action creatingSymbol = () => new Symbol(symbolValues);

            // then
            var exception = Assert.Throws<ArgumentException>(creatingSymbol);
            Assert.True(exception.Message.Contains("row"));
        }


        [Fact]
        public void ShouldRaiseErrorWhenIncorrectSymbolColumnLength()
        {
            // given
            var symbolValues = new int[Symbol.RowSize, Symbol.ColumnSize + 1];

            // when
            Action creatingSymbol = () => new Symbol(symbolValues);

            // then
            var exception = Assert.Throws<ArgumentException>(creatingSymbol);
            Assert.True(exception.Message.Contains("column"));
        }

    }
}