﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetByNameLibrary.Interfaces
{
	public interface ITwitterParser
	{
		void SaveEntries();
		Boolean SendMessage(String msg);
		Boolean GrabTimeLine(int count);
	}
}