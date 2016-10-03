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
            var symbolUsedForLearning = new BipolarSymbol(new int[BipolarSymbol.RowSize, BipolarSymbol.ColumnSize]);
            var symbolsToLearn = new List<BipolarSymbol> { symbolUsedForLearning, new BipolarSymbol(new int[BipolarSymbol.RowSize, BipolarSymbol.ColumnSize]) };
            hopfieldNetwork.Learn(symbolsToLearn);

            // when
            bool symbolIsRecognised = hopfieldNetwork.TryRecognise(symbolUsedForLearning);

            // then
            Assert.True(symbolIsRecognised);
        }
    }
}
