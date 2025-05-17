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
        [field: SerializeField] public float Damage { get; private set; } = 1;
        [field: SerializeField] public float HitPoints { get; private set; } = 10;

        public EnemyConfig GetCloneWithNewValues(EnemyWaveData newWaveDataData)
        {
            var clone = Instantiate(this);
            clone.MovementSpeed = newWaveDataData.MovementSpeed;
            clone.Damage = newWaveDataData.Damage;
            clone.HitPoints = newWaveDataData.HitPoints;

            return clone;
        }
    }

    public sealed class EnemyWaveData
    {
        public float MovementSpeed { get; }
        public float Damage { get; }
        public float HitPoints { get; }

        public EnemyWaveData(float newSpeed, float newDamage, float newHp)
        {
            MovementSpeed = newSpeed;
            Damage = newDamage;
            HitPoints = newHp;
        }
    }
}