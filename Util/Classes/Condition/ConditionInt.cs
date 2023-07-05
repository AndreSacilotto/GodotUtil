namespace Util.Classes;

public class ConditionInt : IConditionEvent
{
    public event Action? OnActivation, OnDesactivation;

    protected uint activeCount;

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

        if (activeCount == 1u)
            OnDesactivation?.Invoke();

        activeCount--;
    }

    public void UnrequestAll()
    {
        activeCount = 0u;
        OnDesactivation?.Invoke();
    }

    public void Reset()
    {
        UnrequestAll();
        Close();
    }

    public void Close()
    {
        OnActivation = null;
        OnDesactivation = null;
    }

}
