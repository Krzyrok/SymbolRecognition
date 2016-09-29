using System.Collections.Generic;
using Domain;
using Xunit;

namespace Tests.Integration
{
    public class HopfieldNetworkTests
    {
        [Fact]
        public void ShouldRecogniseLearnedSymbol()
        {
            // given
            var hopfieldNetwork = new HopfieldNetwork();
            var symbolUsedForLearning = new Symbol(new int[Symbol.RowSize, Symbol.ColumnSize]);
            var symbolsToLearn = new List<Symbol> { symbolUsedForLearning, new Symbol(new int[Symbol.RowSize, Symbol.ColumnSize]) };
            hopfieldNetwork.Learn(symbolsToLearn);

            // when
            bool symbolIsRecognised = hopfieldNetwork.TryRecognise(symbolUsedForLearning);

            // then
            Assert.True(symbolIsRecognised);
        }
    }
}
