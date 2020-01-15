namespace SerialKnife.Common
{
    /// <summary>
    /// 串口操作类类型，实现了两种串口操作类，
    /// 一种是通过.net实现，一种是通过windows api实现
    /// </summary>
    public enum SerialType
    {
        WinApi,
        DotNet
    }
}