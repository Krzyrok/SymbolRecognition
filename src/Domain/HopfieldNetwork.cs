using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;

namespace Domain
{
    public class HopfieldNetwork
    {
        private int NumberOfNeurons => _symbolsIn.GetLength(1); // N; TODO: remove comment
        private int NumberOfStoredSymbols => _symbolsIn.GetLength(0); // M; TODO: remove comment

        private int[,] _symbolsIn; // Bipolar
        private int[,] _symbolsOut; // Binary
        private const int MaxIterations = 20; // 

        // TODO: remove getter and refactor UT
        // TODO: maybe Weights could be separate class - class for storing collection
        public int[,] Weights { get; private set; } // W

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
            _symbolsIn = new int[symbolsToLearn.Count, BipolarSymbol.RowSize * BipolarSymbol.ColumnSize];

            var symbolIndex = 0;
            foreach (BipolarSymbol bipolarSymbol in symbolsToLearn)
            {
                var valueIndex = 0;
                for (var rowIndex = 0; rowIndex < BipolarSymbol.RowSize; rowIndex++)
                {
                    for (var columnIndex = 0; columnIndex < BipolarSymbol.ColumnSize; columnIndex++)
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
            _symbolsIn = new int[1, BipolarSymbol.RowSize * BipolarSymbol.ColumnSize];
            _symbolsOut = new int[MaxIterations, BipolarSymbol.RowSize * BipolarSymbol.ColumnSize];

            var digitValueIndex = 0;
            for (var rowIndex = 0; rowIndex < BipolarSymbol.RowSize; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < BipolarSymbol.ColumnSize; columnIndex++)
                {
                    _symbolsIn[0, digitValueIndex++] = symbolToRecognise.Values[rowIndex, columnIndex];
                }
            }
        }

        private void Recognise()
        {
            var iterationsCountOfRecognising = 0;
            var delta = 0;

            // M == NumberOfStoredSymbols, N == NumberOfNeurons; M == 1 bo rozpoznajemy tylko jeden symbol
            var neuronsInputs = new int[1, NumberOfNeurons];  // X
            for (var neuronIndex = 0; neuronIndex < NumberOfNeurons; neuronIndex++)
                neuronsInputs[0, neuronIndex] = _symbolsIn[0, neuronIndex]; // patterns

            var neurons = new Neuron[NumberOfNeurons]; // cells
            for (var neuronIndex = 0; neuronIndex < NumberOfNeurons; neuronIndex++)
                neurons[neuronIndex] = new Neuron();

            while (true)
            {
                var neuronsInputsWereChanged = false;

                for (var neuronNumber = 0; neuronNumber < NumberOfNeurons; neuronNumber++)
                {
                    neurons[neuronNumber].WeightsSum(neuronsInputs, Weights, 0, neuronNumber);  // threshold can be = X[m, i] = symbolValues[0, neuronNumber]
                    neurons[neuronNumber].ActivationFunction(neurons[neuronNumber].WeightsSumOutput, neuronsInputs[0, neuronNumber]);

                    if (neuronsInputs[0, neuronNumber] != neurons[neuronNumber].ActivationFunctionOutput)
                    {
                        neuronsInputs[0, neuronNumber] = neurons[neuronNumber].ActivationFunctionOutput;
                        neuronsInputsWereChanged = true;
                    } 
                }

                if (neuronsInputsWereChanged)
                {
                    iterationsCountOfRecognising++; // TODO: remove because for debugging purposes
                }

                if (!neuronsInputsWereChanged)
                {
                    break;
                }
            }

            BipolarToBinary(neuronsInputs);

            for (var neuronIndex = 0; neuronIndex < NumberOfNeurons; neuronIndex++)
                _symbolsOut[0, neuronIndex] = neuronsInputs[0, neuronIndex];
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