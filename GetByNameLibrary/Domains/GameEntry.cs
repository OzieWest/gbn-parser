using System;
namespace GetByNameLibrary.Domains
{
    [Serializable]
    public class GameEntry
    {
        public String SearchString { get; set; }
        public String Title { get; set; }
        public String GameUrl { get; set; }
        public String StoreUrl { get; set; }
        public String Cost { get; set; }
        public Boolean Sale { get; set; }
    }
}
