using UnityEngine;

namespace YooE.Diploma
{
    [CreateAssetMenu(
        fileName = "PlayerMovementConfig",
        menuName = "Configs/Player/New PlayerMovementConfig"
    )]
    public sealed class PlayerMovementConfig : ScriptableObject
    {
        [field: SerializeField] public float MovingThreshold { get; private set; } = 0.01f;
        [field: SerializeField] public float RunAcceleration { get; private set; } = 50f;
        [field: SerializeField] public float RunSpeed { get; private set; } = 4f;
        [field: SerializeField] public float Drag { get; private set; } = 20f;
        [field: SerializeField] public float YVelocity { get; private set; } = -9.8f;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 4f;
        [field: SerializeField] public float AnimationsBlendSpeed { get; private set; } = 0.2f;
    }
}