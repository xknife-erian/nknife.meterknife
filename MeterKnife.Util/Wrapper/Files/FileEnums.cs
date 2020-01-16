namespace MeterKnife.Util.Wrapper.Files
{
    public enum FileErrorPolicy
    {
        Inform,
        ProvideAlternative
    }

    public enum FileOperationResult
    {
        OK,
        Failed,
        SavedAlternatively
    }
}
