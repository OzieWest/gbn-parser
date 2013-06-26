using GetByNameLibrary.Domains;
using GetByNameLibrary.Interfaces;
using GetByNameLibrary.Utilities;
using SimpleLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetByNameLibrary.Controllers
{
	public class UploadController : IUploadController
	{
		List<UploadTask> _tasks;

		FTPUploader _ftpUploader;
		TxtLogger _logger;

		public UploadController()
		{
			_ftpUploader = new FTPUploader();
			_logger = new TxtLogger(@"logs\uploadController.logs");
	
			_tasks = new List<UploadTask>();
		}

		public void AddRequest(UploadTask task)
		{
			_tasks.Add(task);
		}

		protected String UploadTask(UploadTask task)
		{
			return _ftpUploader.Upload(task);
		}

		public RetValue<List<String>> StartUpload()
		{
			var result = new RetValue<List<String>>();
			result.Value = new List<String>();
			try
			{
				_tasks.ForEach(task => { result.Value.Add(this.UploadTask(task)); });
			}
			catch (Exception ex)
			{
				result.Description = ex.Message;
				_logger.AddEntry(ex.ToString(), MessageType.Error);
				_logger.WriteLogs();
			}

			return result;
		}
	}
}
