namespace SharpHostsFile
{
    public class HostsFileWhitespace : HostsFileEntryBase, IHostsFileEntry
    {
        public HostsFileWhitespace(string rawLine) : base(rawLine)
        {
        }

        #region Implementation of IHostsFileEntry

        public int LineNumber { get; set; }

        public string ToString(bool preserveFormatting)
        {
            return RawLine;
        }

        #endregion
    }
}