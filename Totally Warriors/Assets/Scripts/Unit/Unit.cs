using System.Collections.Generic;
using UnityEngine;


public class Unit : MonoBehaviour
{
    [field: SerializeField] public List<int> WarriorsHealth { get; private set; }
    [field: SerializeField] public UnitType UnitType;

    public void AddWarriors(int count)
    {
        if (WarriorsHealth.Count + count > UnitType.MaxNumber)
        {
            Debug.LogError($"Too many to add to {gameObject.name}");
        }
        else
        {
            WarriorsHealth.Add(UnitType.MaxHealth);
        }
    }

    public void RemoveWarrior(int health)
    {
        WarriorsHealth.Remove(health);
    }

    public void ResetWarriors(List<int> warriorsHealath)
    {
        WarriorsHealth = warriorsHealath;
    }

}
