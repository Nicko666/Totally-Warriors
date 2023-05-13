using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    List<Unit> units;

    public virtual void Start()
    {

    }

    public virtual void OnAttackedBy(Warrior warrior)
    {
        Debug.Log("AI is attacked");
    }
}
