namespace Util.Classes;

public class ConditionInt : IConditionEvent
{
    public event Action? OnActivation, OnDesactivation;

    protected uint activeCount;

    public ConditionInt() { }
    public ConditionInt(uint activeCount) => this.activeCount = activeCount;

    public bool Active => activeCount != 0u;

    public void Request()
    {
        if (activeCount == 0u)
            OnActivation?.Invoke();

        activeCount++;
    }

    public void Unrequest()
    {
        if (activeCount == 0u)
            return;

        activeCount--;

        if (activeCount == 0u)
            OnDesactivation?.Invoke();
    }

    public void Reset()
    {
        activeCount = 0u;
        OnDesactivation?.Invoke();
    }

    public void Close()
    {
        OnActivation = null;
        OnDesactivation = null;
    }

}
