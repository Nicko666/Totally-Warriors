using System;

public class SceneTActions : Singleton<SceneTActions>
{
    public Action OnUnitsCreated;

    public Action<UnitT> OnUnitDefeated;

    public void OnUnitsCreatedNotify()
    {
        OnUnitsCreated?.Invoke();
        UnityEngine.Debug.Log("Units Created");

    }

    public void OnUnitDefeatedNotify(UnitT unitT)
    {
        OnUnitDefeated?.Invoke(unitT);
        UnityEngine.Debug.Log($"Unit {unitT.name} Die");

    }

}
