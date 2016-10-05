namespace Domain
{
    public class BinarySymbol
    {
        public SymbolValues Values { get; set; }

        public BinarySymbol(int[,] symbolValues)
        {
            Values = new SymbolValues(symbolValues, BipolarToBinary);
        }

        private static int BipolarToBinary(int bipolarValue)
        {
            return bipolarValue == 1 ? 1 : 0;
        }

        public int[] ConvertToOneDimensionalArray() => Values.ConvertToOneDimensionalArray();

        public void Inverse() => Values.Inverse(InverseValue);

        private static int InverseValue(int value)
        {
            return value == 1 ? 0 : 1;
        }
    }
}