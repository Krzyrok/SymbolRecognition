using System.Collections.Generic;

namespace Domain
{
    public class HopfieldNetwork
    {
        public void Learn(IEnumerable<Symbol> symbolsToLearn)
        {
        }

        public bool TryRecognise(Symbol symbolToRecognise)
        {
            return true;
        }
    }
}