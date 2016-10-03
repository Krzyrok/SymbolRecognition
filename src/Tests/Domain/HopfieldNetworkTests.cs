using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Domain;
using Domain.Exceptions;
using Tests.Domain.Helpers;
using Xunit;

namespace Tests.Domain
{
    public class HopfieldNetworkTests
    {
        [Fact]
        public void ShouldCreateHopfieldNetwork()
        {
            // when
            var hopfieldNetwork = new HopfieldNetwork();

            // then
            Assert.NotNull(hopfieldNetwork);
        }

        public static IEnumerable<object[]> EmptySymbols = new List<object[]>
        {
            new object[] { null },
            new object[] { new List<BipolarSymbol>() }
        };

        [Theory]
        [MemberData(nameof(EmptySymbols))]
        public void ShouldRaiseErrorWhenLearningWithoutSymbols(List<BipolarSymbol> emptySymbols)
        {
            // given
            var hopfieldNetwork = new HopfieldNetwork();

            // when
            Action networkLearning = () => hopfieldNetwork.Learn(emptySymbols);

            // then
            Assert.Throws<NoSymbollsPassedException>(networkLearning);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        [InlineData(9)]
        public void ShouldCorrectlyLearnOneDigit(int digit)
        {
            // given
            int[,] expectedWeights = HopfieldNetworkWeightsFactory.WeightsForHebbianLearningOfDigit(digit);
            var hopfieldNetwork = new HopfieldNetwork();
            var symbolsToLearn = new Collection<BipolarSymbol> { SymbolFactory.CreateFromDigit(digit) };

            // when
            hopfieldNetwork.Learn(symbolsToLearn);

            // then
            Assert.Equal(expectedWeights, hopfieldNetwork.Weights);

        }

        [Fact]
        public void ShouldCorrectlyLearnFewDigits()
        {
            // given

            // when

            // then
        }
    }
}
