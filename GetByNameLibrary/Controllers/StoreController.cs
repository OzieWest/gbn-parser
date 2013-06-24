using GetByNameLibrary.Domains;
using GetByNameLibrary.Stores;
using GetByNameLibrary.Utilities;
using SerializeLibra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace GetByNameLibrary.Controllers
{
	public class StoreController
	{
		JsonSerializer _serializer;

		List<Store> _stores;

		public StoreController()
		{
			_serializer = new JsonSerializer();
			_stores = this.LoadStores();
		}

		protected List<Store> LoadStores()
		{
			var serializer = new JsonSerializer();

			var result = new List<Store>();
			result.Add(serializer.Load<Directcod>(@"configs\directcod.config"));
			result.Add(serializer.Load<Steam>(@"configs\steam.config"));
			result.Add(serializer.Load<Yuplay>(@"configs\yuplay.config"));
			result.Add(serializer.Load<Origin>(@"configs\origin.config"));
			result.Add(serializer.Load<Roxen>(@"configs\roxen.config"));
			result.Add(serializer.Load<Gamagama>(@"configs\gamagama.config"));
			result.Add(serializer.Load<Gamazavr>(@"configs\gamazavr.config"));
			result.Add(serializer.Load<Igromagaz>(@"configs\igromagaz.config"));
			result.Add(serializer.Load<Shop1c>(@"configs\shop1c.config"));

			return result;
		}

		public List<String> StartParse()
		{
			var result = new List<String>();

			foreach (var store in _stores)
			{
				var thread = new Thread(delegate()
				{
					var answer = String.Format("{0}|{1}|{2}", store.FileName, store.StartParse(), store.GetEntries().Count);
					result.Add(answer);
				}) { Name = store.FileName };
				thread.Start();
			}

			return result;
		}

		public int CompileGames()
		{
			var list = this.GetGameEntries(String.Empty, true, false);

			_serializer.Save<List<GameEntry>>(list, @"query\games.json");

			return list.Count;
		}

		public int CompileSales()
		{
			var list = this.GetGameEntries(String.Empty, true, true);

			_serializer.Save<List<GameEntry>>(list, @"query\sales.json");

			return list.Count;
		}

		public List<GameEntry> GetGameEntries(String searchName, Boolean IsOrdered = false, Boolean IsSale = false)
		{
			var games = new List<GameEntry>();

			_stores.ForEach((store) =>
			{
				games.AddRange(_serializer.Load<List<GameEntry>>(@"query\" + store.FileName + @".json"));
			});

			if (!String.IsNullOrEmpty(searchName))
				games = games.Where(ent => ent.SearchString.Contains(searchName)).ToList();

			if (IsSale)
				games = games.Where(ent => ent.Sale == true).ToList();

			if (IsOrdered)
				games = games.OrderBy(ent => ent.SearchString).ToList();

			return games;
		}
	}
}
