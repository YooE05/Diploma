using System;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision> OnCollisionEntered;

        [NonSerialized] public int Damage;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MeshRenderer _meshRenderer;

        private EnemyType _enemyType;
        // private Vector2 _lastVelocity = Vector2.zero;

        private void OnCollisionEnter(Collision collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }

        /*public void OnPause()
        {
            _lastVelocity = _rigidbody.velocity;
            _rigidbody.velocity = Vector2.zero;
        }

        public void OnResume()
        {
            _rigidbody.velocity = _lastVelocity;
        }*/

        public void SetUpByArgs(Args args)
        {
            _enemyType = args.ForWhatEnemyType;
            _meshRenderer.material.color = args.Color;

            SetPosition(args.Position);
            Damage = args.Damage;
            SetVelocity(args.Velocity);
        }

        private const float Epsilon = 0.001f;
        private const float EpsilonSq = Epsilon * Epsilon;

        private void SetVelocity(Vector3 velocity)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.velocity = velocity;

            if (velocity is { x: 0f, y: 0f, z: 0f })
                return;

            var target = Quaternion.LookRotation(velocity.normalized, Vector3.up);
            transform.rotation = target;
        }

        private void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public EnemyType GetEnemyType()
        {
            return _enemyType;
        }
    }
}