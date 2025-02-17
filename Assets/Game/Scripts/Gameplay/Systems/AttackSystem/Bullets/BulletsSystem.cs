using UnityEngine;

namespace YooE.Diploma
{
    public sealed class BulletsSystem : Pool<Bullet>
    {
        public BulletsSystem(GameObjectsPoolConfig config, Transform initParent)
        {
            _initParent = initParent;
            _prefab = config.Prefab.GetComponent<Bullet>();
            _initCount = config.InitCount;

            OnAddObject += AddBulletActions;
        }

        public void FlyBullet(Vector3 startPosition, int damage, Vector3 velocity)
        {
            var bullet = Get();

            bullet.SetUpByArgs(new Args()
            {
                Damage = damage,
                Position = startPosition,
                Velocity = velocity
            });

            bullet.OnCollisionEntered += ReturnBullet;
            bullet.Show();
        }

        private void ReturnBullet(Bullet bullet, Collision collision2D)
        {
            bullet.OnCollisionEntered -= ReturnBullet;
            bullet.Hide();
            Return(bullet);
            //TODO: do damage to enemy
        }

        private void AddBulletActions(Bullet newBullet)
        {
            newBullet.Hide();
        }
    }

    public struct Args
    {
        public Vector3 Position;
        public Vector3 Velocity;
        public int Damage;
    }
}