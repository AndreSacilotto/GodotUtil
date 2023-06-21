namespace Util;

/// <summary>Support closing/disposing things that cause memory holding, not to be confused with memory leaks</summary>
public interface IClosable
{
    /// <summary>Close and/or free memory holders</summary>
    void Close();
}