using System.Collections.Generic;
using UnityEngine;

public class MapPointer : MonoBehaviour
{
    [SerializeField]
    private List<Unit> units;

    private void Update()
    {
        foreach (var unit in PlayerManager.Instance.SelectedUnits)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.transform.tag == "Map")
                    {
                        unit.SetUnitTarget(hit.point);
                    }
                        
                }
            }
        }
    }

}
