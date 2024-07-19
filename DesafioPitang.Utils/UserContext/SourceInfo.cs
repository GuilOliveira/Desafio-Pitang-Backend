using System.Collections;
using System.Net;

namespace DesafioPitang.Utils.UserContext
{
    public class SourceInfo : ISourceInfo
    {
        public Hashtable Data { get; set; }
        public IPAddress IP { get; set; }
    }
}
