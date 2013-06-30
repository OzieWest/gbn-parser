using GetByNameLibrary.Domains;
using GetByNameLibrary.Utilities;
using ReturnValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetByNameLibrary.Interfaces
{
	public interface IUploadController
	{
		void AddRequest(UploadTask task);

		RetValue<List<String>> StartUpload();
	}
}
