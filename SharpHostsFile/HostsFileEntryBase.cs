namespace SharpHostsFile
{
    /// <summary>
    ///     Provides the abstract base class for a hosts file entry.
    /// </summary>
    public abstract class HostsFileEntryBase
    {
        /// <summary>
        ///     Initializes a new instance of the SharpHostsFile.HostsFileEntryBase class.
        /// </summary>
        /// <param name="rawLine"></param>
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