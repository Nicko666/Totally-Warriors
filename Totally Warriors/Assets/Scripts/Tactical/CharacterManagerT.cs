using System.Collections.Generic;
using UnityEngine;

public class CharacterManagerT : MonoBehaviour
{
    public Character Character { get; private set; }
    [field: SerializeField] public List<UnitT> MyUnits { get; private set; }
    public List<UnitT> EnemyUnits { get; private set; }

    public void Inst(Character character, List<UnitT> myUnits, List<UnitT> enemyUnits)
    {
        Character = character; MyUnits = myUnits; EnemyUnits = enemyUnits;
    }

}
