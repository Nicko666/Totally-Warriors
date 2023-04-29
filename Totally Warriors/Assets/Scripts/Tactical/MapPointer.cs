using System.Collections.Generic;
using UnityEngine;

public class MapPointer : MonoBehaviour
{
    [SerializeField]
    private List<Unit> units;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.tag == "Map")
                {
                    foreach (var unit in PlayerManager.Instance.SelectedUnits)
                    {
                        unit.SetPosition(hit.point);
                    }

                }
            }
        }
    }

}
