using System;
using UnityEngine;

public class TacticalMap : MonoBehaviour
{
    [field: SerializeField] public Transform[] UpPositions { get; private set; }
    [field: SerializeField] public Transform[] DownPositions { get; private set; }

    public Action<Vector3> MapClick;

    private void OnMouseUp()
    {        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            MapClick(hit.point);
        }

    }    

}
