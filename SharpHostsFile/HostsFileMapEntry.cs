using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace SharpHostsFile
{
    public class HostsFileMapEntry : HostsFileEntryBase, IHostsFileEntry
    {
        public static readonly Regex Pattern = new Regex(@"^\s*(?<address>\S+)\s+(?<hostname>\S+)\s*($|#(?<comment>\S+))", RegexOptions.Compiled);


        public HostsFileMapEntry(IPAddress address, string hostname, string comment = null) : this(string.Empty, address, hostname, comment)
        {
        }

        public HostsFileMapEntry(string rawLine, IPAddress address, string hostname, string comment = null) : base(rawLine)
        {
            RawLine = rawLine;
            Address = address;
            Hostname = hostname;
            Comment = comment;
        }

        public IPAddress Address { get; set; }

        public string Hostname { get; set; }

        public string Comment { get; set; }

        #region Implementation of IHostsFileEntry

        public int LineNumber { get; set; }

        public override string ToString()
        {
            return $"{Address} {Hostname}{(Comment != null ? $" {Comment}" : "")}";
        }

        public string ToString(bool preserveFormatting)
        {
            if (!string.IsNullOrEmpty(RawLine) && preserveFormatting)
            {
                var match = Pattern.Match(RawLine);
                Console.WriteLine(RegexHelper.ReplaceNamedGroup(RawLine, "address", Address.ToString(), match));
                var replaced = RegexHelper.ReplaceNamedGroups(RawLine, new Dictionary<string, string>
                    {
                        {"address", Address.ToString()},
                        {"hostname", Hostname},
                        {"comment", Comment}
                    },
                    Pattern);

                return replaced;
            }
            return ToString();
        }

        #endregion
    }
}