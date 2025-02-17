using UnityEngine;

[CreateAssetMenu(
    fileName = "GameObjectsPoolConfig",
    menuName = "Configs/Pools/New GameObjectsPoolConfig"
)]
public sealed class GameObjectsPoolConfig : ScriptableObject
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public int InitCount { get; private set; }
}