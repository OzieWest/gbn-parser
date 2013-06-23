using GetByNameLibrary.Domains;
using GetByNameLibrary.Stores;
using GetByNameLibrary.Utilities;
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
        protected ISerializer _serializer;

        public List<Store> Stores { get; set; }

        public StoreController()
        {
            _serializer = new SerializerToJson();

            Stores = new List<Store>();

            Stores.Add(new Directcod());
            //Stores.Add(new Steam());
            Stores.Add(new Yuplay());
            //Stores.Add(new Origin());
            //Stores.Add(new Roxen());
            //Stores.Add(new Gamagama());
            //Stores.Add(new Gamazavr());
            //Stores.Add(new Igromagaz());
            //Stores.Add(new Shop1c());
        }

        public List<String> StartParse()
        {
            var result = new List<String>();

            foreach (var store in Stores)
            {
                new Thread(delegate()
                {
                    var answer = String.Format("{0}|{1}|{2}", store.FileName, store.StartParse(), store.GetEntries().Count);
                    result.Add(answer);
                }).Start();
            }

            return result;
        }

        public String CompileGames()
        {
            _serializer.Clear();

            foreach (var store in Stores)
                _serializer.LoadList<GameEntry>(store.FileName);

            var list = _serializer.GetGameEntries(String.Empty, true, false);

            _serializer.SaveList<GameEntry>(list, "games");

            return String.Format("{0} - count - {1}", list.Count());
        }

        public String CompileSales()
        {
            _serializer.Clear();

            foreach (var store in Stores)
                _serializer.LoadList<GameEntry>(store.FileName);

            var list = _serializer.GetGameEntries(String.Empty, true, true);

            _serializer.SaveList<GameEntry>(list, "sales");

            return String.Format("{0} - count - {1}", list.Count());
        }
    }
}
