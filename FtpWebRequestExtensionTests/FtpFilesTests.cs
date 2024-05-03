using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inspres.Codes.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Inspres.Codes.Web.Tests;

[TestClass()]
public class FtpFilesTests
{
    [TestMethod()]
    public void GetFileNamesTest()
    {
        var ftpFiles = new FtpFiles
        {
            FtpHost = "elefant.local",
            NetworkCredential = new NetworkCredential(userName: "IceFtp1", password: "87bsohvb"),
            SubDirectory = "/homes/IceFtp1"
        };

        var list = ftpFiles.GetFileAndDirectoryList();
        
        Assert.IsNotNull(list);
    }

    [TestMethod()]
    public void GetFileNamesTest2()
    {
        var ftpFiles = new FtpFiles
        {
            FtpHost = "localhost",
            NetworkCredential = new NetworkCredential(userName: "IceFtp1", password: "87bsohvb"),
            SubDirectory = "/homes/IceFtp1"
        };

        var list = ftpFiles.GetFileAndDirectoryList();

        Assert.IsNotNull(list);
    }
}