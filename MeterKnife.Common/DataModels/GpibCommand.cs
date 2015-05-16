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
            Interval = 500;
        }

        public bool IsScpi { get; set; }
        public string Content { get; set; }
        public string Command { get; set; }
        public long Interval { get; set; }

        public object Tag { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }
}