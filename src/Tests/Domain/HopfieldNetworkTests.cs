using System;
using System.Collections.Generic;
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

        public static IEnumerable<object[]> DigitsToLearn = new List<object[]>
        {
            new object[] { new List<int> { 0 } },
            new object[] { new List<int> { 3 } },
            new object[] { new List<int> { 9 } },
            new object[] { new List<int> { 0, 3, 9 } }
        };

        [Theory]
        [MemberData(nameof(DigitsToLearn))]
        public void ShouldCorrectlyLearnDigits(List<int> digits)
        {
            // given
            int[,] expectedWeights = HopfieldNetworkWeightsFactory.WeightsForHebbianLearningOfDigits(digits);
            var hopfieldNetwork = new HopfieldNetwork();
            ICollection<BipolarSymbol> symbolsToLearn = digits.Select(SymbolFactory.CreateFromDigit).ToList();

            // when
            hopfieldNetwork.Learn(symbolsToLearn);

            // then
            Assert.Equal(expectedWeights, hopfieldNetwork.Weights);
        }
    }
}
