namespace Domain
{
    public class BipolarSymbol
    {
        public SymbolValues Values { get; }

        public BipolarSymbol(int[,] symbolBits)
        {
            Values = new SymbolValues(symbolBits, BinaryToBipolar);
        }

        private static int BinaryToBipolar(int binary)
        {
            return binary == 1 ? 1 : -1;
        }

        public int[] ConvertToOneDimensionalArray() => Values.ConvertToOneDimensionalArray();

        public void Inverse() => Values.Inverse(InverseValue);

        // TODO: make as private
        public static int InverseValue(int value)
        {
            return value == 1 ? -1 : 1;
        }
    }
}