using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;

namespace Domain
{
    public class HopfieldNetwork
    {
        private int _numberOfNeurons;
        private int _numberOfStoredSymbols;

        private int[,] _weights;

        public int _maxIterations = 20;

        public void Learn(ICollection<BipolarSymbol> symbolsToLearn)
        {
            if (symbolsToLearn == null || !symbolsToLearn.Any())
            {
                throw new NoSymbollsPassedException();
            }

            _numberOfStoredSymbols = symbolsToLearn.Count;
            _numberOfNeurons = BipolarSymbol.RowSize * BipolarSymbol.ColumnSize;

            _weights = new int[_numberOfNeurons, _numberOfNeurons];

        }

        public bool TryRecognise(BipolarSymbol symbolToRecognise)
        {
            return true;
        }
    }
}