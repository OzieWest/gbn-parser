using GetByNameLibrary.Controllers;
using GetByNameLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLTests.MetacriticControllerTests
{
	public class MetacriticControllerSpecification
	{
		protected ICompile target;

		public MetacriticControllerSpecification()
		{
			target = new MetacriticController();
		}
	}
}
