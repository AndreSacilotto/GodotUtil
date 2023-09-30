using Util.Classes;

namespace Overleveling;

public class ConditionWithImmunity : IConditionEvent
{
    public event Action? OnActivation, OnDesactivation;

    protected uint conditionCount;
    protected uint immunityCount;

    public ConditionWithImmunity() { }
    public ConditionWithImmunity(uint conditionCount) => this.conditionCount = conditionCount;

    public bool Active => conditionCount != 0u && immunityCount == 0;

    public void Request()
    {
        conditionCount++;
        if (conditionCount == 1u && immunityCount == 0u)
            OnActivation?.Invoke();
    }
    public void RequestImmunity()
    {
        immunityCount++;
        if (conditionCount > 0u)
            OnDesactivation?.Invoke();
    }

    public void Unrequest()
    {
        if (conditionCount == 0u)
            return;

        conditionCount--;

        if (conditionCount == 0u)
            OnDesactivation?.Invoke();
    }
    public void UnrequestImmunity()
    {
        if (immunityCount == 0u)
            return;

        immunityCount--;

        if (immunityCount == 0u && conditionCount != 0u)
            OnActivation?.Invoke();
    }

    public void Reset()
    {
        conditionCount = 0;
        immunityCount = 0;
        OnDesactivation?.Invoke();
    }

    public void Close()
    {
        OnActivation = null;
        OnDesactivation = null;
    }
}


