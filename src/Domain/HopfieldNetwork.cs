using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;

namespace Domain
{
    public class HopfieldNetwork
    {
        private int _numberOfNeurons;
        private int _numberOfStoredSymbols;

        public void Learn(ICollection<Symbol> symbolsToLearn)
        {
            if (symbolsToLearn == null || !symbolsToLearn.Any())
            {
                throw new NoSymbollsPassedException();
            }

            _numberOfNeurons = Symbol.RowSize * Symbol.ColumnSize;
            _numberOfStoredSymbols = symbolsToLearn.Count;
        }

        public bool TryRecognise(Symbol symbolToRecognise)
        {
            return true;
        }
    }
}