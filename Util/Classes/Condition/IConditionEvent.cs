namespace Util.Classes;

public interface IConditionEvent : IClosable
{
    event Action? OnActivation, OnDesactivation;
    bool Active { get; }
    void Reset();
}
