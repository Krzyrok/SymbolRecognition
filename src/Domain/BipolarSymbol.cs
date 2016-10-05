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
    }
}