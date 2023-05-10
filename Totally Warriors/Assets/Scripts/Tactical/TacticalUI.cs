using System.Collections.Generic;
using UnityEngine;

public class TacticalUI : MonoBehaviour
{
    [SerializeField] UnitsCanvas _upUnitsCanvas;
    [SerializeField] UnitsCanvas _downUnitsCanvas;


    public void SetCanvace(List<Unit> units1, List<Unit> units2) 
    {
        _upUnitsCanvas.SetDeck(units2);
        _downUnitsCanvas.SetDeck(units1);
    }

}
