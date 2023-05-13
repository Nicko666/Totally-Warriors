using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TacticalMap : MonoBehaviour
{
    [field: SerializeField] public Transform[] UpPositions { get; private set; }
    [field: SerializeField] public Transform[] LowPositions { get; private set; }

    public Action<Vector3> MapClick;

    float dragTime = 1.0f;
    float lastDownTime;
    bool drag;

    private void OnMouseUp()
    {
        if (drag)
        {
            drag = false;
            return;
        }
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            MapClick?.Invoke(hit.point);
        }

    }

    private void OnMouseDown()
    {
        lastDownTime = Time.time;
    }

    private void OnMouseDrag()
    {
        if (lastDownTime + dragTime > Time.time) return;
        
        else drag = true;

    }

}
