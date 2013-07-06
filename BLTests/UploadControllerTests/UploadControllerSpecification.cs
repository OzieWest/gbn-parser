using GetByNameLibrary.Controllers;
using GetByNameLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLTests.UploadControllerTests
{
	public class UploadControllerSpecification
	{
		protected IUploadController target;

		public UploadControllerSpecification()
		{
			target = new UploadController();
		}
	}
}
