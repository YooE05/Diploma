using UnityEngine;

namespace YooE.Diploma
{
    [CreateAssetMenu(
        fileName = "EnemyPoolConfig",
        menuName = "Configs/Pools/New EnemyPoolConfig"
    )]
    public sealed class EnemyPoolConfig : GameObjectsPoolConfig
    {
        [field: SerializeField] public EnemyConfig EnemyCharacteristics { get; private set; }
    }
}