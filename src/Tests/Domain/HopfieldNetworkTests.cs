﻿using System;
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
            IList<BipolarSymbol> symbolsToLearn = digits.Select(SymbolFactory.CreateBipolarFromDigit).ToList();

            // when
            hopfieldNetwork.Learn(symbolsToLearn);

            // then
            Assert.Equal(expectedWeights, hopfieldNetwork.Weights);
        }

        [Fact]
        public void ShouldRecogniseLearnedSymbol()
        {
            // given
            var hopfieldNetwork = new HopfieldNetwork();
            var symbolsToLearn = new List<BipolarSymbol> { SymbolFactory.CreateBipolarFromDigit(1), SymbolFactory.CreateBipolarFromDigit(3) };
            hopfieldNetwork.Learn(symbolsToLearn);

            // when
            bool symbolIsRecognised = hopfieldNetwork.TryRecognise(SymbolFactory.CreateBipolarFromDigit(1));

            // then
            Assert.True(symbolIsRecognised);
            Assert.Equal(0, hopfieldNetwork.IterationsCountOfRecognising);
        }

        [Fact]
        public void ShouldRecogniseTheClosestLearnedSymbol()
        {
            // given
            var hopfieldNetwork = new HopfieldNetwork();
            var symbolsToLearn = new List<BipolarSymbol> { SymbolFactory.CreateBipolarFromDigit(1), SymbolFactory.CreateBipolarFromDigit(3) };
            hopfieldNetwork.Learn(symbolsToLearn);

            // when
            bool symbolIsRecognised = hopfieldNetwork.TryRecognise(SymbolFactory.CreateBipolarFromDigit(2));

            // then
            Assert.True(symbolIsRecognised);
            Assert.Equal(1, hopfieldNetwork.IterationsCountOfRecognising);
            Assert.Equal(SymbolFactory.CreateBipolarFromDigit(3).ConvertToOneDimensionalArray(), hopfieldNetwork.SymbolsOut);
        }

        [Fact]
        public void ShouldNotRecogniseCorrectSymbolWhenTooManyLearnedSymbols()
        {
            // given
            var hopfieldNetwork = new HopfieldNetwork();
            var symbolsToLearn = new List<BipolarSymbol>
            {
                SymbolFactory.CreateBipolarFromDigit(0),
                SymbolFactory.CreateBipolarFromDigit(1),
                SymbolFactory.CreateBipolarFromDigit(2),
                SymbolFactory.CreateBipolarFromDigit(3),
                SymbolFactory.CreateBipolarFromDigit(4),
                SymbolFactory.CreateBipolarFromDigit(9)
            };
            hopfieldNetwork.Learn(symbolsToLearn);

            // when
            bool symbolIsRecognised = hopfieldNetwork.TryRecognise(SymbolFactory.CreateBipolarFromDigit(1));

            // then
            Assert.Equal(3, hopfieldNetwork.IterationsCountOfRecognising);
            Assert.Equal(OutputForUnrecognisedDigitOne(), hopfieldNetwork.SymbolsOut);
            Assert.False(symbolIsRecognised);
        }

        private static int[] OutputForUnrecognisedDigitOne()
        {
            return new[] {-1,-1,1,-1,-1,-1,-1,-1,1,-1,-1,-1,-1,1,1,-1,-1,-1,-1,-1,1,1,-1,-1,-1,1,-1,-1,-1,1,-1,-1,-1,1,-1,-1,-1,1,-1,-1,-1,1,-1,-1,-1,1,-1,-1,-1,1,-1,-1,1,1,-1,-1,-1,1,-1,-1,-1,1,1,1,1,1,1,1,1,1,-1,-1,-1,-1,1,1,1,-1,1,1,1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1};
        }

        [Fact]
        public void ShouldNotRecogniseIncorrectSymbol()
        {
            // given
            var hopfieldNetwork = new HopfieldNetwork();
            var symbolsToLearn = new List<BipolarSymbol>
            {
                SymbolFactory.CreateBipolarFromDigit(1),
                SymbolFactory.CreateBipolarFromDigit(2),
                SymbolFactory.CreateBipolarFromDigit(4)
            };
            hopfieldNetwork.Learn(symbolsToLearn);

            // when
            bool symbolIsRecognised = hopfieldNetwork.TryRecognise(SymbolFactory.CreateBipolarFromDigit(9));

            // then
            Assert.Equal(1, hopfieldNetwork.IterationsCountOfRecognising);
            Assert.Equal(OutputForUnrecognisedDigitNine(), hopfieldNetwork.SymbolsOut);
            Assert.False(symbolIsRecognised);
        }

        private static int[] OutputForUnrecognisedDigitNine()
        {
            return new[] {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,1,-1,-1,-1,-1,-1,1,-1,-1,1,-1,-1,1,-1,-1,-1,1,1,1,-1,1,1,-1,-1,1,-1,-1,-1,1,1,1,1,1,1,1,1,1,-1,-1,-1,1,1,1,1,-1,-1,-1,1,1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1};
        }

        [Fact]
        public void ShouldRecogniseInversedSymbol()
        {
            // given
            var hopfieldNetwork = new HopfieldNetwork();
            var symbolsToLearn = new List<BipolarSymbol>
            {
                SymbolFactory.CreateBipolarFromDigit(1),
                SymbolFactory.CreateBipolarFromDigit(2),
                SymbolFactory.CreateBipolarFromDigit(4)
            };
            hopfieldNetwork.Learn(symbolsToLearn);

            BipolarSymbol inversedNumberTwo = SymbolFactory.CreateBipolarFromDigit(2);
            inversedNumberTwo.Inverse();

            // when
            bool symbolIsRecognised = hopfieldNetwork.TryRecognise(inversedNumberTwo);

            // then
            Assert.Equal(0, hopfieldNetwork.IterationsCountOfRecognising);
            Assert.True(symbolIsRecognised);

            BipolarSymbol expectedRecognisedSymbol = SymbolFactory.CreateBipolarFromDigit(2);
            expectedRecognisedSymbol.Inverse();
            Assert.Equal(expectedRecognisedSymbol.ConvertToOneDimensionalArray(), hopfieldNetwork.SymbolsOut);
        }

        [Fact]
        public void ShouldRecogniseInversedSymbolWithNoises()
        {
            // given
            var hopfieldNetwork = new HopfieldNetwork();
            var symbolsToLearn = new List<BipolarSymbol>
            {
                SymbolFactory.CreateBipolarFromDigit(0),
                SymbolFactory.CreateBipolarFromDigit(1),
                SymbolFactory.CreateBipolarFromDigit(2)
            };
            hopfieldNetwork.Learn(symbolsToLearn);

            // when
            bool symbolIsRecognised = hopfieldNetwork.TryRecognise(IversedNumberOneWithNoises());

            // then
            Assert.Equal(1, hopfieldNetwork.IterationsCountOfRecognising);
            Assert.True(symbolIsRecognised);

            BipolarSymbol expectedRecognisedSymbol = SymbolFactory.CreateBipolarFromDigit(1);
            expectedRecognisedSymbol.Inverse();
            Assert.Equal(expectedRecognisedSymbol.ConvertToOneDimensionalArray(), hopfieldNetwork.SymbolsOut);
        }

        private static BipolarSymbol IversedNumberOneWithNoises()
        {
            return new BipolarSymbol(new[,] {
                {1,1,1,1,0,1,1,1,0,1,0,0},
                {0,1,0,0,1,0,1,1,1,0,0,1},
                {0,1,1,1,0,0,1,0,0,0,1,0},
                {1,0,0,0,0,0,0,0,1,1,0,1},
                {1,0,0,0,0,0,0,0,0,0,1,1},
                {1,1,1,1,1,1,1,1,1,0,1,1},
                {1,1,1,1,1,1,1,1,1,0,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1}
            });
        }
    }
}
