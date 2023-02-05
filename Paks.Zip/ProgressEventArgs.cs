namespace Paks.Zip
{
    public class ProgressEventArgs : EventArgs
    {
        public int EntriesExtracted { get; private set; }
        public int EntriesTotal { get; private set; }

        internal ProgressEventArgs(int entriesExtracted, int entriesTotal)
        {
            EntriesExtracted = entriesExtracted;
            EntriesTotal = entriesTotal;
        }
    }
}