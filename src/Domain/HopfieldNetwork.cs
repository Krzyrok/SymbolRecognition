using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;

namespace Domain
{
    public class HopfieldNetwork
    {
        private static int NumberOfNeurons => SymbolValues.RowSize * SymbolValues.ColumnSize; 
        private int NumberOfStoredSymbols => _learnedSymbols.Count;

        private IList<BipolarSymbol> _learnedSymbols;
        // TODO: remove
        public int[] SymbolsOut { get; private set; } // Binary

        // TODO: remove getter and refactor UT
        // TODO: maybe Weights could be separate class - class for storing collection
        public int[,] Weights { get; private set; }

        // TODO: only for debugging and first tage testing - remove later
        public int IterationsCountOfRecognising { get; private set; }

        public void Learn(IList<BipolarSymbol> symbolsToLearn)
        {
            if (symbolsToLearn == null || !symbolsToLearn.Any())
            {
                throw new NoSymbollsPassedException();
            }
            _learnedSymbols = symbolsToLearn;
            LearnWithHebb(_learnedSymbols);
        }

        // TODO: draw diagram (which neuron with which, what indexes for weights corresponds to them) and refactor this
        private void LearnWithHebb(IList<BipolarSymbol> symbolsToLearn)
        {
            Weights = new int[NumberOfNeurons, NumberOfNeurons];

            for (var weightFirstIndex = 0; weightFirstIndex < NumberOfNeurons; weightFirstIndex++)
            {
                for (var weightSecondIndex = 0; weightSecondIndex <= weightFirstIndex; weightSecondIndex++)
                {
                    Weights[weightFirstIndex, weightSecondIndex] = 0;
                    if (weightSecondIndex < weightFirstIndex)
                    {
                        for (var storedSymbolIndex = 0; storedSymbolIndex < NumberOfStoredSymbols; storedSymbolIndex++)
                        {
                            int[] symbolValues = symbolsToLearn[storedSymbolIndex].ConvertToOneDimensionalArray();
                            Weights[weightFirstIndex, weightSecondIndex] += symbolValues[weightFirstIndex] * symbolValues[weightSecondIndex];
                        }
                    }
                    Weights[weightSecondIndex, weightFirstIndex] = Weights[weightFirstIndex, weightSecondIndex];
                }
            }
        }

        public bool TryRecognise(BipolarSymbol symbolToRecognise)
        {
            return Recognise(symbolToRecognise.ConvertToOneDimensionalArray());
        }

        private bool Recognise(IReadOnlyList<int> symbolValues)
        {
            SymbolsOut = new int[SymbolValues.RowSize * SymbolValues.ColumnSize];

            IterationsCountOfRecognising = 0;

            var neuronsInputs = new int[NumberOfNeurons]; 
            for (var neuronIndex = 0; neuronIndex < NumberOfNeurons; neuronIndex++)
                neuronsInputs[neuronIndex] = symbolValues[neuronIndex]; 

            var neurons = new Neuron[NumberOfNeurons];
            for (var neuronIndex = 0; neuronIndex < NumberOfNeurons; neuronIndex++)
                neurons[neuronIndex] = new Neuron();

            while (true)
            {
                var neuronsInputsWereChanged = false;

                for (var neuronNumber = 0; neuronNumber < NumberOfNeurons; neuronNumber++)
                {
                    int neuronOutput = neurons[neuronNumber].CalculateOutput(neuronsInputs, Weights, neuronNumber);

                    if (neuronsInputs[neuronNumber] != neuronOutput)
                    {
                        neuronsInputs[neuronNumber] = neuronOutput;
                        neuronsInputsWereChanged = true;
                    } 
                }

                if (neuronsInputsWereChanged)
                {
                    IterationsCountOfRecognising++; // TODO: remove because for debugging purposes (and UT - but UT should be changed)
                }

                if (!neuronsInputsWereChanged)
                {
                    break;
                }
            }

            bool symbolIsRecognised = SymbolIsRecognised(neuronsInputs);

            BipolarToBinary(neuronsInputs);

            for (var neuronIndex = 0; neuronIndex < NumberOfNeurons; neuronIndex++)
                SymbolsOut[neuronIndex] = neuronsInputs[neuronIndex];

            return symbolIsRecognised;
        }

        private bool SymbolIsRecognised(IReadOnlyList<int> networkBipolarOutput)
        {
            for (var symbolIndex = 0; symbolIndex < NumberOfStoredSymbols; symbolIndex++)
            {
                int[] valuesOfLearnedSymbol = _learnedSymbols[symbolIndex].ConvertToOneDimensionalArray();
                var symbolIsRecognised = true;
                for (var symbolValueIndex = 0; symbolValueIndex < NumberOfNeurons; symbolValueIndex++)
                {
                    if (valuesOfLearnedSymbol[symbolValueIndex] != networkBipolarOutput[symbolValueIndex])
                    {
                        symbolIsRecognised = false;
                        break;
                    }
                }

                if (symbolIsRecognised)
                {
                    return true;
                }

                symbolIsRecognised = true;
                for (var symbolValueIndex = 0; symbolValueIndex < NumberOfNeurons; symbolValueIndex++)
                {
                    if (valuesOfLearnedSymbol[symbolValueIndex] != BipolarSymbol.InverseValue(networkBipolarOutput[symbolValueIndex]))
                    {
                        symbolIsRecognised = false;
                        break;
                    }
                }

                if (symbolIsRecognised)
                {
                    return true;
                }
            }

            return false;
        }

        private static void BipolarToBinary(IList<int> values)
        {
            for (var index = 0; index < values.Count; index++)
            {
                values[index] = BipolarToBinary(values[index]);
            }
        }

        private static int BipolarToBinary(int bipolarValue)
        {
            return bipolarValue == 1 ? 1 : 0;
        }
    }
}