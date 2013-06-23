using GetByNameLibrary.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetByNameLibrary.Utilities
{
	public interface ISerializer
	{
		void SaveList<T>(IEnumerable<T> obj, String fileName) where T : class;
		void LoadList<T>(String fileName) where T : class;

        void SaveObject<T>(T obj, String fileName) where T : class;
		T LoadObject<T>(String fileName) where T : class;

		void Clear();

		List<GameEntry> GetGameEntries(String searchName, Boolean IsOrdered = false, Boolean IsSale = false);
		List<CoopEntry> GetCoopEntries(Boolean IsOrdered = true);
		List<TwitterEntry> GetTweetEntries(Boolean IsOrdered = true);
	}
}
