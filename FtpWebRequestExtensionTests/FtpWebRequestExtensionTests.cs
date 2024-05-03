using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Inspired.Codes.FtpWebRequestExtensionTests;

[TestClass()]
public class FtpWebRequestExtensionTests
{
    [TestMethod()]
    public void GetFilesTest()
    {
        //  uri = "ftp://example.com/%2F/directory" //Go to a forward directory (cd directory)
        //  uri = "ftp://example.com/%2E%2E" //Go to the previously directory (cd ../)

        Uri uri = new("ftp://elefant.local/%2F/homes/IceFtp1/");
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
        request.Credentials = new NetworkCredential("IceFtp1", "87bsohvb");
        //request.Method = "LIST";

        //var result = FtpWebRequestExtension.GetFiles(request);



        //Assert.IsNotNull(result);
        ;
    }
}