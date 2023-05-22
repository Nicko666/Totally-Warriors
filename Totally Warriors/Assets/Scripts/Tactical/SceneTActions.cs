using System;

public class SceneTActions : Singleton<SceneTActions>
{
    public Action OnUnitsCreated;

    public Action OnUnitsChanged;

    public Action<UnitT> OnUnitDefeated;

    public void OnUnitsCreatedNotify()
    {
        OnUnitsCreated?.Invoke();

    }

    public void OnUnitsChangedNotify()
    {
        OnUnitsChanged?.Invoke();

    }

    public void OnUnitDefeatedNotify(UnitT unitT)
    {
        OnUnitDefeated?.Invoke(unitT);

    }

}
