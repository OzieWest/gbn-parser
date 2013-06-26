using GetByNameLibrary.Controllers;
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
		protected TwitterController target;

		public TwitterControllerSpecification()
		{
			target = new TwitterController();
		}
	}
}
