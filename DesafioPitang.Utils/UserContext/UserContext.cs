using System.Collections;

namespace DesafioPitang.Utils.UserContext
{
    public class UserContext : IUserContext
    {
        public UserContext() { }
        public DateTime StartDateTime { get; set; }
        public ISourceInfo SourceInfo { get; set; }
        public Guid RequestId { get; set; }
        public Hashtable AdditionalData { get; set; }
        public Hashtable UnhandledExceptions { get; set; } = new Hashtable();
        public string Status { get; set; }
    }
}
