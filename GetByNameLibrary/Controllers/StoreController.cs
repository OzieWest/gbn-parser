using GetByNameLibrary.Domains;
using GetByNameLibrary.Stores;
using GetByNameLibrary.Utilities;
using SerializeLibra;
using SimpleLogger;
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
		List<BaseStore> _stores;
		TxtLogger _logger;

		public StoreController()
		{
			_serializer = new JsonSerializer();
			_logger = new TxtLogger(@"logs\storeController.logs");

			_stores = new List<BaseStore>();
			_stores = this.LoadStores().Value;
		}

		protected RetValue<List<BaseStore>> LoadStores()
		{
			var result = new RetValue<List<BaseStore>>();
			result.Value = new List<BaseStore>();
			try
			{
				var serializer = new JsonSerializer();

				result.Value.Add(serializer.Load<Directcod>(@"configs\directcod.config"));
				result.Value.Add(serializer.Load<Steam>(@"configs\steam.config"));
				result.Value.Add(serializer.Load<Yuplay>(@"configs\yuplay.config"));
				result.Value.Add(serializer.Load<Origin>(@"configs\origin.config"));
				result.Value.Add(serializer.Load<Roxen>(@"configs\roxen.config"));
				result.Value.Add(serializer.Load<Gamagama>(@"configs\gamagama.config"));
				result.Value.Add(serializer.Load<Gamazavr>(@"configs\gamazavr.config"));
				result.Value.Add(serializer.Load<Igromagaz>(@"configs\igromagaz.config"));
				result.Value.Add(serializer.Load<Shop1c>(@"configs\shop1c.config"));
			}
			catch (Exception ex)
			{
				result.Description = ex.Message;
				_logger.AddEntry(ex.ToString(), MessageType.Error);
				_logger.WriteLogs();
			}

			return result;
		}

		public AnswerStack<String> StartParse()
		{
			var result = new AnswerStack<String>(_stores.Count);

			_stores.ForEach((store) =>
			{
				var thread = new Thread(delegate()
				{
					var answer = String.Format("{0}|{1}|{2}", store.FileName, store.StartParse(), store.GetEntries().Count);
					result.Push(answer);
				}) { Name = store.FileName };
				thread.Start();
			});

			return result;
		}

		public RetValue<Boolean> CompileGames()
		{
			var result = new RetValue<Boolean>();
			try
			{
				var saveList = this.GetGameEntries(false);
				_serializer.Save<List<GameEntry>>(saveList, @"completed\games.json");

				result.Value = true;
				result.Description = String.Format("{0}", saveList.Count);
			}
			catch (Exception ex)
			{
				result.Value = false;
				result.Description = ex.Message;
				_logger.AddEntry(ex.ToString(), MessageType.Error);
				_logger.WriteLogs();
			}

			return result;
		}

		public RetValue<Boolean> CompileSales()
		{
			var result = new RetValue<Boolean>();
			try
			{
				var saveList = this.GetGameEntries(true);

				_serializer.Save<List<GameEntry>>(saveList, @"completed\sales.json");

				result.Value = true;
				result.Description = String.Format("{0}", saveList.Count);
			}
			catch (Exception ex)
			{
				result.Value = false;
				result.Description = ex.Message;
				_logger.AddEntry(ex.ToString(), MessageType.Error);
				_logger.WriteLogs();
			}

			return result;
		}

		protected List<GameEntry> GetGameEntries(Boolean IsSale)
		{
			var result = new List<GameEntry>();
			_stores.ForEach((store) =>
			{
				result.AddRange(_serializer.Load<List<GameEntry>>(@"incompleted\" + store.FileName + @".json"));
			});
			
			if (IsSale)
				result = result.Where(ent => ent.Sale == true).ToList();

			result = result.OrderBy(ent => ent.SearchString).ToList();

			return result;
		}
	}
}
