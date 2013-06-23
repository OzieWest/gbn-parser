using System;
namespace GetByNameLibrary.Domains
{
    [Serializable]
    public class CoopEntry
    {
        public String Name { get; set; }
        public String Offline { get; set; }
        public String Online { get; set; }
        public String Lan { get; set; }
        public String CoopMode { get; set; }
        public String CoopComp { get; set; }
    }
}
