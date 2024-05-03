using System.Net;
using static System.Net.WebRequestMethods;

namespace Inspres.Codes.Web;


public class FtpFiles
{
    public string FtpHost { get; set; }
    public string SubDirectory { get; set; }
    public NetworkCredential NetworkCredential { get; set; }

    /// <summary>
    /// with subdirectory value: 
    /// ftp://ftphost.net/%2F/subdirectory <br/>
    /// without subdirectory value: 
    /// ftp://ftphost.net/
    /// </summary>
    public string UriWithSubDirectory => GetFtpPath();

    private string GetFtpPath()
    {
        string basePath = "ftp://" + FtpHost;
        if (string.IsNullOrWhiteSpace(SubDirectory))
            return basePath;

        string subDirectory = SubDirectory.Trim().Trim('/');
        return basePath.Trim().TrimEnd('/') + "/%2F/" + subDirectory + "/";
    }

    public IEnumerable<string> GetFileAndDirectoryList()
    {
        var fileAndDirectoryList = NewMethod();
        var files = NewMethod2(fileAndDirectoryList);
        return files;
    }

    /// <summary>
    /// Field Explanation
    /// – : normal file
    /// d : directory
    /// s : socket file
    /// l : link file
    /// </summary>
    /// <param name="fileAndDirectoryNames"></param>
    /// <returns></returns>
    private IEnumerable<string> NewMethod2(IEnumerable<string> fileAndDirectoryNames)
    {
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(UriWithSubDirectory);
        request.Credentials = NetworkCredential;
        request.Method = Ftp.ListDirectoryDetails;
        var response = (FtpWebResponse)request.GetResponse();
        var reader = new StreamReader(response.GetResponseStream());
        List<string> lines = new();
        while (!reader.EndOfStream)
        {
            string filename = reader.ReadLine();
            lines.Add(filename);
        }

        IEnumerable<string> files = lines.Where(l => l.StartsWith('-'));

        IEnumerable<string> filtered = fileAndDirectoryNames
                                         .Where(fd => files
                                                        .Any(f => f.EndsWith(fd, StringComparison.InvariantCultureIgnoreCase)));
        return filtered;
    }

    private IEnumerable<string> NewMethod()
    {
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(UriWithSubDirectory);
        request.Credentials = NetworkCredential;
        request.Method = Ftp.ListDirectory;
        var response = (FtpWebResponse)request.GetResponse();
        var reader = new StreamReader(response.GetResponseStream());
        List<string> files = new();
        while (!reader.EndOfStream)
        {
            string filename = reader.ReadLine();
            files.Add(filename);
        }
        return files;
    }

}
