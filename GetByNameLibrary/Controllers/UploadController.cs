using GetByNameLibrary.Domains;
using GetByNameLibrary.Interfaces;
using GetByNameLibrary.Utilities;
using ReturnValues;
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

		public IUploader _ftpUploader;
		public ILogger _logger;

		public UploadController()
		{
			_tasks = new List<UploadTask>();
		}

		public void AddRequest(UploadTask task)
		{
			_tasks.Add(task);
		}

		String UploadTask(UploadTask task)
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
				_logger.Error(ex.ToString());
				_logger.WriteLogs();
			}

			return result;
		}
	}
}
