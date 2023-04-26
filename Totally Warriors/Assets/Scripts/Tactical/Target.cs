using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Action<Transform> OnChangePosition;
    private Vector3 _lastPose;

    private void Start()
    {
        _lastPose = transform.position;

    }

    private void Update()
    {
        if (transform.position != _lastPose)
        {
            if (OnChangePosition != null)
            {
                OnChangePosition.Invoke(transform);
            }
        }

        _lastPose = transform.position;

    }

}
