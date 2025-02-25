using UnityEngine;

namespace YooE
{
    [CreateAssetMenu(
        fileName = "GameObjectsPoolConfig",
        menuName = "Configs/Pools/New GameObjectsPoolConfig"
    )]
    public class GameObjectsPoolConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public int InitCount { get; private set; }
    }
}