using GetByNameLibrary.Domains;
using GetByNameLibrary.Interfaces;
using GetByNameLibrary.Stores;
using GetByNameLibrary.Utilities;
using ReturnValues;
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
	public class StoreController : IStoreController
	{
		ILogger _logger;
		ISerializer _serializer;

		List<BaseStoreParser> _stores;

		public StoreController()
		{
			_serializer = new JsonSerializer();
			_logger = new TxtLogger() { FileName = @"logs\" + DateTime.Today.ToShortDateString() + ".logs" };

			_stores = new List<BaseStoreParser>();
			_stores = this.LoadStores().Value;
		}

		public List<AsyncRetValue<Boolean>> AsyncStartParse(Action<AsyncRetValue<Boolean>> method)
		{
			var result = new List<AsyncRetValue<Boolean>>();

			_stores.ForEach((store) =>
			{
				var retValue = new AsyncRetValue<Boolean>();

				retValue = store.AsyncStartParse(method);
				result.Add(retValue);
			});

			return result;
		}

		public AsyncRetValue<Boolean> AsyncParseThis(String parserName, Action<AsyncRetValue<Boolean>> method)
		{
			var result = new AsyncRetValue<Boolean>();
			result.SetProgressRange(0, 1);

			var store = _stores.SingleOrDefault(o => o.FileName == parserName);

			if (store != null)
			{
				var retValue = new AsyncRetValue<Boolean>();
				result = store.AsyncStartParse(method);
			}
			else
			{
				result.Value = false;
				result.Description = String.Format("{0} не найден", parserName);
				result.OnComplete(method);
				result.Complete();
			}

			return result;
		}

		public AsyncRetValue<Boolean> AsyncCompile(Action method)
		{
			var result = new AsyncRetValue<Boolean>();
			result.SetProgressRange(0, 1);

			result.SetWorker(() =>
			{
				try
				{
					var allgameList = this.GetGameEntries(false);
					_serializer.Save<List<GameEntry>>(allgameList, @"completed\games.json");

					var salesList = this.GetGameEntries(true);
					_serializer.Save<List<GameEntry>>(salesList, @"completed\sales.json");

					result.Value = true;
					result.Description = String.Format("All: {0} | sales: {1}", allgameList.Count, salesList.Count);
					result.Complete();
				}
				catch (Exception ex)
				{
					result.AbortProgress(false, ex.Message);

					_logger.Error(ex.ToString());
					_logger.WriteLogs();
				}
			}, "Store compiler");

			result.OnComplete(method);
			result.StartWork();

			return result;
		}

		protected RetValue<List<BaseStoreParser>> LoadStores()
		{
			var result = new RetValue<List<BaseStoreParser>>();
			result.Value = new List<BaseStoreParser>();
			try
			{
				var serializer = new JsonSerializer();

				result.Value.Add(serializer.Load<DirectcodParser>(@"configs\directcod.config"));
				result.Value.Add(serializer.Load<SteamParser>(@"configs\steam.config"));
				result.Value.Add(serializer.Load<YuplayParser>(@"configs\yuplay.config"));
				result.Value.Add(serializer.Load<OriginParser>(@"configs\origin.config"));
				result.Value.Add(serializer.Load<RoxenParser>(@"configs\roxen.config"));
				result.Value.Add(serializer.Load<GamagamaParser>(@"configs\gamagama.config"));
				result.Value.Add(serializer.Load<GamazavrParser>(@"configs\gamazavr.config"));
				result.Value.Add(serializer.Load<IgromagazParser>(@"configs\igromagaz.config"));
				result.Value.Add(serializer.Load<Shop1cParser>(@"configs\shop1c.config"));
			}
			catch (Exception ex)
			{
				result.Description = ex.Message;
				_logger.Error(ex.ToString());
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

			return result.OrderBy(ent => ent.SearchString).ToList();
		}
	}
}
