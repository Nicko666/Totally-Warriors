using System.Collections.Generic;
using UnityEngine;


public class Unit : MonoBehaviour
{
    [field: SerializeField] public List<int> WarriorsHealath { get; private set; }
    [field: SerializeField] public UnitType UnitType;

    public void AddWarriors(int count)
    {
        if (WarriorsHealath.Count + count > UnitType.MaxNumber)
        {
            Debug.LogError($"Too many to add to {gameObject.name}");
        }
        else
        {
            WarriorsHealath.Add(UnitType.MaxHealth);
        }
    }

    public void RemoveWarrior(int health)
    {
        WarriorsHealath.Remove(health);
    }

    public void ResetWarriors(List<int> warriorsHealath)
    {
        WarriorsHealath = warriorsHealath;
    }

}
