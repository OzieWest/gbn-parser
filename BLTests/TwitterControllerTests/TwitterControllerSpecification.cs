using GetByNameLibrary.Controllers;
using GetByNameLibrary.Interfaces;
using GetByNameLibrary.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLTests.TwitterControllerTests
{
	public class TwitterControllerSpecification
	{
		protected ICompile target;

		public TwitterControllerSpecification()
		{
			target = new TwitterController();
		}
	}
}
