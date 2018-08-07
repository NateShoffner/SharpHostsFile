namespace SharpHostsFile
{
    public interface IHostsFileEntry
    {
        int LineNumber { get; set; }
        string ToString(bool preserveFormatting);
    }
}