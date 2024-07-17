using System.Collections;

namespace DesafioPitang.Utils.UserContext
{
    public interface IUserContext
    {
        DateTime StartDateTime { get; set; }
        ISourceInfo SourceInfo { get; set; }
        Guid RequestId { get; set; }
        Hashtable AdditionalData { get; set; }
        Hashtable UnhandledExceptions { get; set; }
        public string Status { get; set; }
    }
}
