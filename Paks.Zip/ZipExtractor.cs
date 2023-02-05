using System.IO.Compression;

namespace Paks.Zip
{
    public class ZipExtractor : IDisposable
    {
        private readonly ZipArchive zip;
        private readonly int entriesTotal;
        private int _entriesExtracted;
        
        public delegate void StatusExtractHandler(object sender, ProgressEventArgs e);
        public event StatusExtractHandler OnExtractStatus;

        private int EntriesExtracted
        {
            get { return _entriesExtracted; }
            set 
            {
                _entriesExtracted = value;
                if (OnExtractStatus != null)
                {
                    ProgressEventArgs args = new ProgressEventArgs(value, entriesTotal);
                    OnExtractStatus(this, args);
                }
            }
        }

        public ZipExtractor(string zipName)
        {
            zip = ZipFile.OpenRead(zipName);
            entriesTotal = zip.Entries.Count;
            _entriesExtracted = 0;
        }

        public ZipExtractor(Stream stream)
        {
            zip = new ZipArchive(stream, ZipArchiveMode.Read, false);
            entriesTotal = zip.Entries.Count;
            _entriesExtracted = 0;
        }

        public void Extract() => Extract(Directory.GetCurrentDirectory());

        public void Extract(string destinationDirectoryName)
        {
            destinationDirectoryName += "/";
            foreach (ZipArchiveEntry item in zip.Entries)
            {
                if (item.FullName[item.FullName.Length - 1] == '/')
                {
                    Directory.CreateDirectory(destinationDirectoryName + item.FullName);
                }
                else
                {
                    item.ExtractToFile(destinationDirectoryName + item.FullName, true);
                }
                EntriesExtracted++;
            }
        }

        public void Dispose()
        {
            zip.Dispose();
        }
    }
}
