using UnityEngine;

namespace YooE.Diploma
{
    [CreateAssetMenu(
        fileName = "TargetSensorConfig",
        menuName = "Configs/Player/New TargetSensorConfig"
    )]
    public sealed class TargetSensorConfig : ScriptableObject
    {
        [field: SerializeField] public LayerMask LayerMask { get; private set; }
        [field: SerializeField] public int TargetsCapacity { get; private set; }
        [field: SerializeField] public float Radius { get; private set; }
    }
}