using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;

namespace Domain
{
    public class HopfieldNetwork
    {
        private int NumberOfNeurons => _symbolsIn.GetLength(1); 
        private int NumberOfStoredSymbols => _symbolsIn.GetLength(0);

        // TODO: remove
        private int[,] _symbolsIn; // Bipolar
        // TODO: remove
        public int[] SymbolsOut { get; private set; } // Binary

        // TODO: remove getter and refactor UT
        // TODO: maybe Weights could be separate class - class for storing collection
        public int[,] Weights { get; private set; }

        // TODO: only for debugging and first tage testing - remove later
        public int IterationsCountOfRecognising { get; private set; }

        public void Learn(ICollection<BipolarSymbol> symbolsToLearn)
        {
            if (symbolsToLearn == null || !symbolsToLearn.Any())
            {
                throw new NoSymbollsPassedException();
            }

            StoreSymbolsValuesForLearning(symbolsToLearn);
            LearnWithHebb();
        }

        private void StoreSymbolsValuesForLearning(ICollection<BipolarSymbol> symbolsToLearn)
        {
            // TODO: change this to storing just Symbols (instead of raw values)
            _symbolsIn = new int[symbolsToLearn.Count, SymbolValues.RowSize * SymbolValues.ColumnSize];
            var symbolIndex = 0;
            foreach (BipolarSymbol bipolarSymbol in symbolsToLearn)
            {
                var valueIndex = 0;
                for (var rowIndex = 0; rowIndex < SymbolValues.RowSize; rowIndex++)
                {
                    for (var columnIndex = 0; columnIndex < SymbolValues.ColumnSize; columnIndex++)
                    {
                        _symbolsIn[symbolIndex, valueIndex++] = bipolarSymbol.Values[rowIndex, columnIndex];
                    }
                }

                symbolIndex++;
            }
        }

        // TODO: draw diagram (which neuron with which, what indexes for weights corresponds to them) and refactor this
        private void LearnWithHebb()
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
                            Weights[weightFirstIndex, weightSecondIndex] += _symbolsIn[storedSymbolIndex, weightFirstIndex] * _symbolsIn[storedSymbolIndex, weightSecondIndex];
                        }
                    }
                    Weights[weightSecondIndex, weightFirstIndex] = Weights[weightFirstIndex, weightSecondIndex];
                }
            }
        }

        public bool TryRecognise(BipolarSymbol symbolToRecognise)
        {
            Recognise(symbolToRecognise.ConvertToOneDimensionalArray());

            return true;
        }

        private void Recognise(IReadOnlyList<int> symbolValues)
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

            BipolarToBinary(neuronsInputs);

            for (var neuronIndex = 0; neuronIndex < NumberOfNeurons; neuronIndex++)
                SymbolsOut[neuronIndex] = neuronsInputs[neuronIndex];
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