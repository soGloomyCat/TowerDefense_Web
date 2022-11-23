using System.Collections;
using System.Collections.Generic;
using TowerDefense.Daniel;
using UnityEngine;

public class Holder : MonoBehaviour
{
    [field: SerializeField] public Transform TrashBox { get; private set; }
    [field: SerializeField] public Transform MeteoriteSpawnPoint { get; private set; }
    [field: SerializeField] public CastleTargets CurrentCastleTarget { get; private set; }
}
