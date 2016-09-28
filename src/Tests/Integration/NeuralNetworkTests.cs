using System.Collections.Generic;
using Domain;
using Xunit;

namespace Tests.Integration
{
    public class NeuralNetworkTests
    {
        [Fact]
        public void ShouldRecogniseLearnedSymbol()
        {
            // given
            var neuralNetwork = new NeuralNetwork();
            var symbolUsedForLearning = new Symbol();
            var symbolsToLearn = new List<Symbol> { symbolUsedForLearning, new Symbol()};
            neuralNetwork.Learn(symbolsToLearn);

            // when
            bool symbolIsRecognised = neuralNetwork.TryRecognise(symbolUsedForLearning);

            // then
            Assert.True(symbolIsRecognised);
        }
    }
}
