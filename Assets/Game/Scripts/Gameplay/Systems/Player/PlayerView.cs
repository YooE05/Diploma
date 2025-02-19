using UnityEngine;

namespace YooE.Diploma
{
    public sealed class PlayerView : MonoBehaviour
    {
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Camera Camera { get; private set; }
        [field: SerializeField] public Transform Visual { get; private set; }
        [field: SerializeField] public WeaponView[] WeaponViews { get; private set; }
    }
}