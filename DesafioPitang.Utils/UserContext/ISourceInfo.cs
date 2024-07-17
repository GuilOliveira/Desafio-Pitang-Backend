using System.Collections;
using System.Net;

namespace DesafioPitang.Utils.UserContext
{
    public interface ISourceInfo
    {
        Hashtable Data { get; set; }

        IPAddress IP { get; set; }
    }
}
