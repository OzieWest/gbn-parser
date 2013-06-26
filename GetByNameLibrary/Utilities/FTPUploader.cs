using GetByNameLibrary.Domains;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GetByNameLibrary.Utilities
{
	public class FTPUploader
	{
		public String Host { get; set; }
		public String Login { get; set; }
		public String Password { get; set; }

		public FTPUploader()
		{
			Host = @"ftp://getbynameru.706.com1.ru/httpdocs";
			Login = "getbyna7";
			Password = "odo4Xigeng";
		}

		public String Upload(UploadTask task)
		{
			FtpWebRequest ftpRequest = null;
			Stream ftpStream = null;
			int bufferSize = 8192;

			ftpRequest = (FtpWebRequest)FtpWebRequest.Create(Host + "/" + task.Remote);
			ftpRequest.Credentials = new NetworkCredential(Login, Password);
			ftpRequest.UseBinary = true;
			ftpRequest.UsePassive = true;
			ftpRequest.KeepAlive = true;
			ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

			ftpStream = ftpRequest.GetRequestStream();
			var localFileStream = new FileStream(task.Local, FileMode.Open);
			var byteBuffer = new byte[bufferSize];
			int bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
			while (bytesSent != 0)
			{
				ftpStream.Write(byteBuffer, 0, bytesSent);
				bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
			}
			localFileStream.Close();
			ftpStream.Close();
			ftpRequest = null;

			return String.Format("{0} | {1}", task.Name);
		}
	}
}
