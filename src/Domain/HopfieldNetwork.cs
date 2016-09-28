using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;

namespace Domain
{
    public class HopfieldNetwork
    {


        public void Learn(IEnumerable<Symbol> symbolsToLearn)
        {
            if (symbolsToLearn == null || !symbolsToLearn.Any())
            {
                throw new NoSymbollsPassedException();
            }
        }

        public bool TryRecognise(Symbol symbolToRecognise)
        {
            return true;
        }
    }
}