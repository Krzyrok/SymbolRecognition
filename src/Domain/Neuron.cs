namespace Domain
{
    public class Neuron
    {
        public int CalculateOutput(int[,] neuronsInputs, int[,] weights, int neuronNumber)
        {
            int aggregatedFeedbackSignals = AggregateFeedbackSignals(neuronsInputs, weights, neuronNumber);
            int neuronOutput = CalculateActivationFunction(aggregatedFeedbackSignals, neuronsInputs[0, neuronNumber]);

            return neuronOutput;
        }

        private static int CalculateActivationFunction(int aggregatedFeedbackSignals, int neuronOuputSignalFromPreviousIteration)
        {
            var activationFunctionResult = 1;

            if (aggregatedFeedbackSignals > 0)
            {
                activationFunctionResult = 1;
            }
            else if (aggregatedFeedbackSignals == 0)
            {
                if (neuronOuputSignalFromPreviousIteration > 0)
                    activationFunctionResult = 1;
                else
                    activationFunctionResult = -1;
            }
            else if (aggregatedFeedbackSignals < 0)
            {
                activationFunctionResult = -1;
            }

            return activationFunctionResult;
        }

        // TODO: neuronsInputs as one dimensional array
        // TODO: remove threshold because unused
        private static int AggregateFeedbackSignals(int[,] neuronsInputs, int[,] weights, int neuronNumber)
        {
            int aggregateFeedbackSignals = neuronsInputs[0, neuronNumber]; // TODO: check what if without this step

            int weightsDimension = weights.GetLength(0);
            for (var weightIndex = 0; weightIndex < weightsDimension; weightIndex++)
                aggregateFeedbackSignals += weights[neuronNumber, weightIndex] * neuronsInputs[0, weightIndex];

            return aggregateFeedbackSignals;
        }
    }
}