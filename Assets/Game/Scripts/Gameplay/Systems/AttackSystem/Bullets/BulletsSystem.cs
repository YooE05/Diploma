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

        public void FlyBullet(Vector3 startPosition, int damage, Vector3 velocity, EnemyType enemyType, Color color)
        {
            var bullet = Get();

            bullet.SetUpByArgs(new Args()
            {
                Damage = damage,
                Position = startPosition,
                Velocity = velocity,
                ForWhatEnemyType = enemyType,
                Color = color
            });

            bullet.OnCollisionEntered += ReturnBullet;
            bullet.Show();
        }

        private float _damageMultiplier;

        private void ReturnBullet(Bullet bullet, Collision collision)
        {
            var bulletType = bullet.GetEnemyType();
            bullet.OnCollisionEntered -= ReturnBullet;
            bullet.Hide();
            Return(bullet);

            _damageMultiplier = 1f;
            var hpComponent = collision.gameObject.GetComponentInParent<HitPointsComponent>();
            var enemyView = collision.gameObject.GetComponentInParent<EnemyView>();
            if (!hpComponent) return;

            if (enemyView)
            {
                if (bulletType == enemyView.Type)
                {
                    _damageMultiplier = 2f;
                }
                else if (bulletType != EnemyType.Any)
                {
                    _damageMultiplier = 0.75f;
                }
            }

            hpComponent.TakeDamage(bullet.Damage * _damageMultiplier);
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
        public EnemyType ForWhatEnemyType;
        public Color Color;
    }
}