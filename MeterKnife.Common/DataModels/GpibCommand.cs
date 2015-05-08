namespace MeterKnife.Common.DataModels
{
    /// <summary>
    /// 
    /// </summary>
    public class GpibCommand
    {
        public GpibCommand()
            : this(true)
        {
        }

        public GpibCommand(bool isScpi)
        {
            IsScpi = isScpi;
        }

        public bool IsScpi { get; set; }
        public string Command { get; set; }
        public long Interval { get; set; }
    }
}