using System;

public class SceneTActions : Singleton<SceneTActions>
{
    public Action OnUnitsTCreated;

    public Action OnUnitTChanged;

    public Action OnWarriorDefeated;

    public Action<UnitT> OnUnitTDefeated;

    public Action<bool> OnGismosSwitch;

    public void OnUnitsCreatedNotify()
    {
        OnUnitsTCreated?.Invoke();

    }

    public void OnUnitsChangedNotify()
    {
        OnUnitTChanged?.Invoke();

    }

    public void OnWarriorDefeatedNotify()
    {
        OnWarriorDefeated?.Invoke();

    }

    public void OnUnitDefeatedNotify(UnitT unitT)
    {
        OnUnitTDefeated?.Invoke(unitT);

    }

    public void OnGismosSwitchNotify(bool value)
    {
        OnGismosSwitch?.Invoke(value);

    }

}
