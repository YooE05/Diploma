using UnityEngine;

namespace YooE.Diploma
{
    [CreateAssetMenu(
        fileName = "ShootingConfig",
        menuName = "Configs/Player/New ShootingConfig"
    )]
    public sealed class ShootingConfig : ScriptableObject
    {
        [field: SerializeField] public float ShootingDelay { get; private set; } = 0.3f;
        [field: SerializeField] public float BulletSpeed { get; private set; } = 5f;
        [field: SerializeField] public int Damage { get; private set; } = 5;
        [field: SerializeField] public EnemyType EnemyType { get; private set; } = EnemyType.Any;
        [field: SerializeField] public Color BulletColor { get; private set; } = Color.cyan;
    }
}