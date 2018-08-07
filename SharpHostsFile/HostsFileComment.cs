using System.Text.RegularExpressions;

namespace SharpHostsFile
{
    public class HostsFileComment : HostsFileEntryBase, IHostsFileEntry
    {
        public static readonly Regex Pattern = new Regex(@"\s#\s*(?<comment>.*)", RegexOptions.Compiled);


        public HostsFileComment(string comment) : this(string.Empty, comment)
        {
        }

        public HostsFileComment(string rawLine, string comment) : base(rawLine)
        {
            RawLine = rawLine;
            Comment = comment;
        }

        public string Comment { get; set; }

        #region Overrides of HostsFileEntryBase

        public override string ToString()
        {
            return Comment;
        }

        #endregion

        #region Implementation of IHostsFileEntry

        public int LineNumber { get; set; }

        public string ToString(bool preserveFormatting)
        {
            return RegexHelper.ReplaceNamedGroup(RawLine, "comment", Comment, Pattern.Match(RawLine));
        }

        #endregion
    }
}