namespace Util;

public class ConditionBool : IConditionEvent
{
    public event Action? OnActivation, OnDesactivation;

    private bool active;
    public bool Active
    {
        get => active;
        set
        {
            if (active == value)
                return;
            active = value;
            if (value)
                OnActivation?.Invoke();
            else
                OnDesactivation?.Invoke();
        }
    }

    public void Reset()
    {
        Active = false;
        Close();
    }

    public void Close()
    {
        OnActivation = null;
        OnDesactivation = null;
    }

}
