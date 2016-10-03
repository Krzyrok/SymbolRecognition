namespace Domain
{
    public class Neuron
    {
        public int WeightsSumOutput; // U
        public int ActivationFunctionOutput; // Y

        public void ActivationFunction(int aggregatedFeedbackSignals, int inputSignalBeforeResponse)
        {
            if (aggregatedFeedbackSignals > 0)
            {
                ActivationFunctionOutput = 1;
            }
            else if (aggregatedFeedbackSignals == 0)
            {
                if (inputSignalBeforeResponse > 0)
                    ActivationFunctionOutput = 1;
                else
                    ActivationFunctionOutput = -1;
            }
            else if (aggregatedFeedbackSignals < 0)
            {
                ActivationFunctionOutput = -1;
            }
        }

        // int[,]X - bipolar or binary patterns array
        // int[,] weights - before response
        // TODO: X as one dimensional array
        // TODO: remove threshold because unused
        public void WeightsSum(int[,] X, int[,] weights, int patternNumber, int neuronNumber)
        {
            WeightsSumOutput = X[patternNumber, neuronNumber];

            int weightsDimension = weights.GetLength(0);
            for (var weightIndex = 0; weightIndex < weightsDimension; weightIndex++)
                WeightsSumOutput += weights[neuronNumber, weightIndex] * X[patternNumber, weightIndex];
        }
    }
}