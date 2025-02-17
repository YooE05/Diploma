using System;
using UnityEngine;

namespace YooE.Diploma
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision> OnCollisionEntered;

        [NonSerialized] public int Damage;
        [SerializeField] private Rigidbody _rigidbody;

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
            SetPosition(args.Position);
            Damage = args.Damage;
            SetVelocity(args.Velocity);
        }

        private void SetVelocity(Vector3 velocity)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.velocity = velocity;
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
    }
}