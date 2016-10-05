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
    }
}