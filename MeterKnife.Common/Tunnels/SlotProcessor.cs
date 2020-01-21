using NKnife.Jobs;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Util.Protocol.Generic;
using NKnife.MeterKnife.Util.Tunnel;
using NKnife.MeterKnife.Util.Tunnel.Filters;
using NKnife.MeterKnife.Util.Tunnel.Generic;

namespace NKnife.MeterKnife.Common.Tunnels
{
    /// <summary>
    ///     插槽的处理部件
    /// </summary>
    public class SlotProcessor
    {
        private const string FAMILY_NAME = "care";

        public SlotProcessor(IDataConnector connector, BytesProtocolFamily family, BytesCodec codec, BytesProtocolFilter filter)
        {
            Connector = connector;
            Family = family;
            Family.FamilyName = FAMILY_NAME;
            Codec = codec;
            Codec.CodecName = FAMILY_NAME;
            Filter = filter;
            Filter.Bind(Codec, Family);
            JobManager = new JobManager {Pool = new CareCommandPool {IsOverall = true}};
        }

        public JobManager JobManager { get; set; }

        public IDataConnector Connector { get; set; }

        public BytesCodec Codec { get; set; }

        public BytesProtocolFamily Family { get; set; }

        public BytesProtocolFilter Filter { get; set; }
    }
}