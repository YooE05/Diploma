using UnityEngine;

namespace YooE.Diploma
{
    public sealed class PlayerShooterView : PlayerView
    {
        [field: SerializeField] public WeaponView[] WeaponViews { get; private set; }
        [field: SerializeField] public HitPointsComponent HitPointsComponent { get; private set; }
    }
}