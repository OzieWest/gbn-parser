﻿using GetByNameLibrary.Domains;
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

		List<BaseStore> _stores;

		public StoreController()
		{
			_serializer = new JsonSerializer();
			_logger = new TxtLogger() { FileName = @"logs\storeController.logs" };

			_stores = new List<BaseStore>();
			_stores = this.LoadStores().Value;
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

		public RetValue<Boolean> Compile()
		{
			var result = new RetValue<Boolean>();
			try
			{
				var allgameList = this.GetGameEntries(false);
				_serializer.Save<List<GameEntry>>(allgameList, @"completed\games.json");

				var salesList = this.GetGameEntries(true);
				_serializer.Save<List<GameEntry>>(salesList, @"completed\sales.json");

				result.Value = true;
				result.Description = String.Format("All: {0} | sales: {1}", allgameList.Count, salesList.Count);
			}
			catch (Exception ex)
			{
				result.Value = false;
				result.Description = ex.Message;
				_logger.Error(ex.ToString());
				_logger.WriteLogs();
			}

			return result;
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
