using UnityEngine;

namespace YooE.Diploma
{
    [CreateAssetMenu(
        fileName = "EnemyConfig",
        menuName = "Configs/Enemies/New EnemyConfig"
    )]
    public sealed class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public TargetSensorConfig WaitPlayerSensorConfig { get; private set; }
        [field: SerializeField] public TargetSensorConfig FollowPlayerSensorConfig { get; private set; }
        [field: SerializeField] public TargetSensorConfig AttackRangeSensorConfig { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; } = 4;
        [field: SerializeField] public float MovementSpeed { get; private set; } = 3;
        [field: SerializeField] public int Damage { get; private set; } = 1;
        [field: SerializeField] public int HitPoints { get; private set; } = 10;
    }
}