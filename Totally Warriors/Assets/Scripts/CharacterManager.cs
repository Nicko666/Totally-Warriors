using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [field: SerializeField] public Character Character { get; private set; }
    [field: SerializeField] public List<Unit> Units { get; private set; }
    [field: SerializeField] public bool Player { get; private set; }
}
