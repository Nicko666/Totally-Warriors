using System.Collections.Generic;
using UnityEngine;

public interface IBehaviorTactical
{
    public void Enable(List<Unit> units);

    public void OnMapClick(Vector3 position);

    public void OnUnitClick(Unit unit);

}
