using System;
using UnityEngine;

namespace YooE.Diploma
{
    public abstract class DeathObserver
    {
        public event Action OnDeathStart;

        private HitPointsComponent _hitPointsComponent;

        protected virtual void Init(HitPointsComponent hitPointsComponent)
        {
            _hitPointsComponent = hitPointsComponent;
            hitPointsComponent.OnHpEmpty += StartDeathProcess;
        }

        protected virtual void StartDeathProcess(GameObject obj)
        {
            _hitPointsComponent.OnHpEmpty -= StartDeathProcess;
            OnDeathStart?.Invoke();
        }
    }
}