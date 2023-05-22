using System;

public class SceneTActions : Singleton<SceneTActions>
{
    public Action OnUnitsTCreated;

    public Action OnUnitTChanged;

    public Action OnWarriorDefeated;

    public Action<UnitT> OnUnitTDefeated;

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

}
