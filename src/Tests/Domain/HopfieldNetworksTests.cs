using Domain;
using Xunit;

namespace Tests.Domain
{
    public class HopfieldNetworksTests
    {
        [Fact]
        public void ShouldCreateHopfieldNetwork()
        {
            // when
            var hopfieldNetwork = new HopfieldNetwork();

            // then
            Assert.NotNull(hopfieldNetwork);
        }
    }
}
