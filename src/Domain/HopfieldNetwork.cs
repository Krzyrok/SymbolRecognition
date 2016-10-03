﻿using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;

namespace Domain
{
    public class HopfieldNetwork
    {
        private int NumberOfNeurons => _symbolsIn.GetLength(1); // N; TODO: remove comment
        private int NumberOfStoredSymbols => _symbolsIn.GetLength(0); // M; TODO: remove comment

        private int[,] _symbolsIn;

        // TODO: remove getter and refactor UT
        public int[,] Weights { get; private set; }

        public void Learn(ICollection<BipolarSymbol> symbolsToLearn)
        {
            if (symbolsToLearn == null || !symbolsToLearn.Any())
            {
                throw new NoSymbollsPassedException();
            }

            StoreSymbolsValues(symbolsToLearn);
            LearnWithHebb();

        }

        private void StoreSymbolsValues(ICollection<BipolarSymbol> symbolsToLearn)
        {
            _symbolsIn = new int[symbolsToLearn.Count, BipolarSymbol.RowSize * BipolarSymbol.ColumnSize];

            var symbolIndex = 0;
            foreach (BipolarSymbol bipolarSymbol in symbolsToLearn)
            {
                var valueIndex = 0;
                for (var rowIndex = 0; rowIndex < BipolarSymbol.RowSize; rowIndex++)
                    for (var columnIndex = 0; columnIndex < BipolarSymbol.ColumnSize; columnIndex++)
                    {
                        _symbolsIn[symbolIndex, valueIndex++] = bipolarSymbol.Values[rowIndex, columnIndex];
                    }

                symbolIndex++;
            }
        }

        // TODO: draw diagram and refactor this
        private void LearnWithHebb()
        {
            Weights = new int[NumberOfNeurons, NumberOfNeurons];

            for (var i = 0; i < NumberOfNeurons; i++)
            {
                for (var j = 0; j <= i; j++)
                {
                    Weights[i, j] = 0;
                    if (j < i)
                    {
                        for (var m = 0; m < NumberOfStoredSymbols; m++)
                        {
                            Weights[i, j] += _symbolsIn[m, i] * _symbolsIn[m, j];
                        }
                    }
                    Weights[j, i] = Weights[i, j];
                }
            }
        }

        public bool TryRecognise(BipolarSymbol symbolToRecognise)
        {
            return true;
        }
    }
}