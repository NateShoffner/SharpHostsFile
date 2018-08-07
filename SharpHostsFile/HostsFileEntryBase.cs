namespace SharpHostsFile
{
    public abstract class HostsFileEntryBase
    {
        protected HostsFileEntryBase(string rawLine)
        {
            RawLine = rawLine;
        }

        public string RawLine { get; set; }

        #region Overrides of Object

        public override string ToString()
        {
            return RawLine;
        }

        #endregion
    }
}