using GetByNameLibrary.Interfaces;
using System;
using System.Text.RegularExpressions;

namespace GetByNameLibrary.Utilities
{
	public class Replacer : IReplacer
	{
		public String DelBadChar(ref String str)
		{
			str = str.Replace("в„ў", "")
					 .Replace("вЂ“", "")
					 .Replace("в™", "")
					 .Replace("В®", "")
					 .Replace("вЂ™", "")
					 .Replace("Ђ", "")
					 .Replace("вњ", "")
					 .Replace("вќ", "")
					 .Replace("\0", "")
					 .Replace("™", "")
					 .Replace("®", "");

			str = new Regex(@"&([\s\S]*?);").Replace(str, "");

			return this.DelWithRegex(str);
		}

		public String DelWithRegex(String str)
		{
			return new Regex(@"\W|_").Replace(str, "").ToLower();
		}
	}

}
