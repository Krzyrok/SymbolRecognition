namespace Domain
{
    public class Symbol
    {
        public Symbol()
        {
            RowSize = 8;
            ColumnSize = 12;
        }

        public int ColumnSize { get; }

        public int RowSize { get; }
    }
}