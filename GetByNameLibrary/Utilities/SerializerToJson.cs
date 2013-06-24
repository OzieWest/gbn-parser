using GetByNameLibrary.Domains;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace GetByNameLibrary.Utilities
{
	public class SerializerToJson : ISerializer
	{
		public int Count { get; set; }
		public String FolderName { get; set; }

		List<GameEntry> _games;
		List<CoopEntry> _coops;
		List<TwitterEntry> _tweets;
		List<MetaEntry> _metas;

		public SerializerToJson()
		{
			_games = new List<GameEntry>();
			_coops = new List<CoopEntry>();
			_tweets = new List<TwitterEntry>();
			_metas = new List<MetaEntry>();

			FolderName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\" + "query" + "\\");
		}

		public void SaveList<T>(IEnumerable<T> obj, String fileName) where T : class
		{
			var path = FolderName + fileName + ".json";

			using (var streamWriter = new StreamWriter(path))
			{
				var stringBuilder = new StringBuilder();
				new JavaScriptSerializer().Serialize(obj, stringBuilder);
				streamWriter.Write(stringBuilder.ToString());
			}
		}

		public void SaveObject<T>(T obj, String fileName) where T : class
		{
			var path = FolderName + fileName + ".json";

			using (var streamWriter = new StreamWriter(path))
			{
				var stringBuilder = new StringBuilder();
				new JavaScriptSerializer().Serialize(obj, stringBuilder);
				streamWriter.Write(stringBuilder.ToString());
			}
		}

		public T LoadObject<T>(String fileName) where T : class
		{
			var path = FolderName + fileName + ".json";

			var serializer = new JavaScriptSerializer();

			T obj = default(T);
			using (var reader = new StreamReader(path))
			{
				obj = (T)serializer.Deserialize(reader.ReadToEnd(), typeof(T));
			}

			return obj;
		}

		public void LoadList<T>(String fileName) where T : class
		{
			var path = FolderName + fileName + ".json";

			var serializer = new JavaScriptSerializer();

			IEnumerable<T> obj = null;
			using (var reader = new StreamReader(path))
			{
				obj = (List<T>)serializer.Deserialize(reader.ReadToEnd(), typeof(List<T>));
			}

			if (obj is List<GameEntry>)
				_games.AddRange((List<GameEntry>)obj);

			else if (obj is List<CoopEntry>)
				_coops.AddRange((List<CoopEntry>)obj);

			else if (obj is List<TwitterEntry>)
				_tweets.AddRange((List<TwitterEntry>)obj);

			else if (obj is List<MetaEntry>)
				_metas.AddRange((List<MetaEntry>)obj);
		}

		public void Clear()
		{
			_games.Clear();
			_tweets.Clear();
			_coops.Clear();
		}

		public List<GameEntry> GetGameEntries(String searchName, Boolean IsOrdered = false, Boolean IsSale = false)
		{
			if (!String.IsNullOrEmpty(searchName))
				_games = _games.Where(ent => ent.SearchString.Contains(searchName)).ToList();

			if (IsSale)
				_games = _games.Where(ent => ent.Sale == true).ToList();

			if (IsOrdered)
				_games = _games.OrderBy(ent => ent.SearchString).ToList();

			Count = _games.Count();

			return _games;
		}

		public List<CoopEntry> GetCoopEntries(Boolean IsOrdered = true)
		{
			if (IsOrdered)
				_coops = _coops.OrderBy(ent => ent.Name).ToList();

			Count = _coops.Count();

			return _coops;
		}

		public List<TwitterEntry> GetTweetEntries(Boolean IsOrdered = true)
		{
			if (IsOrdered)
				_tweets = _tweets.OrderBy(ent => ent.Date).ToList();

			Count = _tweets.Count();

			return _tweets;
		}

		public List<MetaEntry> GetMetaEntries(Boolean IsOrdered = true)
		{
			if (IsOrdered)
				_metas = _metas.OrderBy(ent => ent.Name).ToList();

			Count = _metas.Count();

			return _metas;
		}
	}
}
