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
            StoreSymbolValuesForRecognising(symbolToRecognise);
            Recognise();

            return true;
        }

        private void StoreSymbolValuesForRecognising(BipolarSymbol symbolToRecognise)
        {
            _symbolsIn = new int[1, SymbolValues.RowSize * SymbolValues.ColumnSize];
            SymbolsOut = new int[SymbolValues.RowSize * SymbolValues.ColumnSize];

            var digitValueIndex = 0;
            for (var rowIndex = 0; rowIndex < SymbolValues.RowSize; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < SymbolValues.ColumnSize; columnIndex++)
                {
                    _symbolsIn[0, digitValueIndex++] = symbolToRecognise.Values[rowIndex, columnIndex];
                }
            }
        }

        private void Recognise()
        {
            IterationsCountOfRecognising = 0;

            var neuronsInputs = new int[1, NumberOfNeurons]; 
            for (var neuronIndex = 0; neuronIndex < NumberOfNeurons; neuronIndex++)
                neuronsInputs[0, neuronIndex] = _symbolsIn[0, neuronIndex]; 

            var neurons = new Neuron[NumberOfNeurons];
            for (var neuronIndex = 0; neuronIndex < NumberOfNeurons; neuronIndex++)
                neurons[neuronIndex] = new Neuron();

            while (true)
            {
                var neuronsInputsWereChanged = false;

                for (var neuronNumber = 0; neuronNumber < NumberOfNeurons; neuronNumber++)
                {
                    int neuronOutput = neurons[neuronNumber].CalculateOutput(neuronsInputs, Weights, neuronNumber);

                    if (neuronsInputs[0, neuronNumber] != neuronOutput)
                    {
                        neuronsInputs[0, neuronNumber] = neuronOutput;
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
                SymbolsOut[neuronIndex] = neuronsInputs[0, neuronIndex];
        }

        private static void BipolarToBinary(int[,] symbols)
        {
            int rowsCount = symbols.GetLength(0);
            int columnsCount = symbols.GetLength(1);
            for (var row = 0; row < rowsCount; row++)
                for (var column = 0; column < columnsCount; column++)
                {
                    symbols[row, column] = BipolarToBinary(symbols[row, column]);
                }
        }

        private static int BipolarToBinary(int bipolarValue)
        {
            return bipolarValue == 1 ? 1 : 0;
        }
    }
}