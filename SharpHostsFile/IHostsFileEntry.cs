namespace SharpHostsFile
{
    /// <summary>
    ///     Represents a hosts file entry.
    /// </summary>
    public interface IHostsFileEntry
    {
        /// <summary>
        ///     Entry line number.
        /// </summary>
        int LineNumber { get; set; }

        /// <summary>
        ///     Returns the string reprsentation of the hosts entry.
        /// </summary>
        /// <param name="preserveFormatting">Preserves formatting, including whitespace of raw entry line.</param>
        /// <returns></returns>
        string ToString(bool preserveFormatting);
    }
}