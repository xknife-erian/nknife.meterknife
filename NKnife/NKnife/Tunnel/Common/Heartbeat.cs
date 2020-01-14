using System.Text;

namespace NKnife.Tunnel.Common
{
    public class Heartbeat
    {
        public Heartbeat()
            : this("LocalHeart","RemoteHeart")
        {
        }


        public Heartbeat(string localHeartDescription,string remoteHeartDescription)
        {
            LocalHeartDescription = localHeartDescription;
            RemoteHeartDescription = remoteHeartDescription;
            RequestToRemote = Encoding.Default.GetBytes(string.Format("[[beat request from {0} to {1}.]]",LocalHeartDescription, RemoteHeartDescription));
            ReplyToRemote = Encoding.Default.GetBytes(string.Format("[[beat reply from {0} to {1}.]]", LocalHeartDescription, RemoteHeartDescription));
            RequestFromRemote = Encoding.Default.GetBytes(string.Format("[[beat request from {0} to {1}.]]", RemoteHeartDescription,LocalHeartDescription));
            ReplyFromRemote = Encoding.Default.GetBytes(string.Format("[[beat reply from {0} to {1}.]]", RemoteHeartDescription, LocalHeartDescription));
        }
        public string Name { get; set; }
        public string LocalHeartDescription { get; private set; }
        public string RemoteHeartDescription { get; private set; }

        public byte[] RequestToRemote { get; set; }
        public byte[] ReplyToRemote { get; set; }
        public byte[] RequestFromRemote { get; set; }
        public byte[] ReplyFromRemote { get; set; }
    }
}
