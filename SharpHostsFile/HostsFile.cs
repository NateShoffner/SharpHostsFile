using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace SharpHostsFile
{
    public class HostsFile : IList<IHostsFileEntry>
    {
        private readonly List<IHostsFileEntry> _entries = new List<IHostsFileEntry>();

        public IReadOnlyCollection<IHostsFileEntry> Entries => _entries.AsReadOnly();

        public void Load(string fileName)
        {
            _entries.Clear();

            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));

            if (!File.Exists(fileName))
                throw new FileNotFoundException($"Hosts file not found at {fileName}");

            var counter = 0;

            using (var sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var type = GetHostsFileEntryType(line);

                    IHostsFileEntry entry = null;

                    if (type != null)
                    {
                        if (type == typeof(HostsFileWhitespace))
                            entry = new HostsFileWhitespace(line) {LineNumber = counter};

                        if (type == typeof(HostsFileComment))
                            entry = new HostsFileComment(line, line) {LineNumber = counter};

                        if (type == typeof(HostsFileMapEntry))
                        {
                            var match = HostsFileMapEntry.Pattern.Match(line);

                            if (match.Success)
                                entry = new HostsFileMapEntry(line,
                                    IPAddress.Parse(match.Groups["address"].Value),
                                    match.Groups["hostname"].Value,
                                    match.Groups["comment"].Value) {LineNumber = counter};
                        }
                    }

                    if (entry != null)
                        _entries.Add(entry);

                    counter++;
                }
            }
        }

        public void Save(string fileName, bool preserveFormatting = true)
        {
            if (fileName == null)
                throw new ArgumentNullException(nameof(fileName));

            using (var file = new StreamWriter(File.Open(fileName, FileMode.OpenOrCreate)))
            {
                foreach (var entry in _entries)
                {
                    file.WriteLine(entry.ToString(preserveFormatting));
                }
            }
        }

        public IHostsFileEntry Find(Predicate<IHostsFileEntry> match)
        {
            return _entries.Find(match);
        }

        public List<IHostsFileEntry> FindAll(Predicate<IHostsFileEntry> match)
        {
            return _entries.FindAll(match);
        }

        public static string GetDefaultHostsFilePath()
        {
            var hostsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "drivers\\etc\\hosts");

            if (!File.Exists(hostsPath))
                throw new FileNotFoundException($"Hosts file not found at {hostsPath}");
            return hostsPath;
        }

        private static Type GetHostsFileEntryType(string line)
        {
            if (string.IsNullOrEmpty(line) || line.Trim().Length == 0)
                return typeof(HostsFileWhitespace);
            return line.TrimStart().StartsWith("#")
                ? typeof(HostsFileComment)
                : typeof(HostsFileMapEntry);
        }

        #region Implementation of IEnumerable

        public IEnumerator<IHostsFileEntry> GetEnumerator()
        {
            return _entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<IHostsFileEntry>

        public void Add(IHostsFileEntry entry)
        {
            _entries.Add(entry);
        }

        public void Clear()
        {
            _entries.Clear();
        }

        public bool Contains(IHostsFileEntry entry)
        {
            return _entries.Contains(entry);
        }

        public void CopyTo(IHostsFileEntry[] array, int arrayIndex)
        {
            _entries.CopyTo(array, arrayIndex);
        }

        public bool Remove(IHostsFileEntry entry)
        {
            return _entries.Remove(entry);
        }

        public int Count => _entries.Count;

        public bool IsReadOnly => false;

        #endregion

        #region Implementation of IList<IHostsFileEntry>

        public int IndexOf(IHostsFileEntry entry)
        {
            return _entries.IndexOf(entry);
        }

        public void Insert(int index, IHostsFileEntry entry)
        {
            _entries.Insert(index, entry);
        }

        public void RemoveAt(int index)
        {
            _entries.RemoveAt(index);
        }

        public IHostsFileEntry this[int index]
        {
            get => _entries[index];
            set => _entries[index] = value;
        }

        #endregion
    }
}