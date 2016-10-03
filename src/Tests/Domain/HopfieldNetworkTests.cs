using System;
using System.Collections.Generic;
using Domain;
using Domain.Exceptions;
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
    }
}
