using Domain;
using Xunit;

namespace Tests.Domain
{
    public class NeuralNetworksTests
    {
        [Fact]
        public void ShouldCreateNeuralNetwork()
        {
            // when
            var neuralNetwork = new NeuralNetwork();

            // then
            Assert.NotNull(neuralNetwork);
        }
    }
}
