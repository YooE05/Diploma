using UnityEngine;

namespace YooE.Diploma
{
    public sealed class EnemySpawnPoint : MonoBehaviour
    {
        public Vector3 SpawnPosition => transform.position;
        public Quaternion SpawnRotation => transform.rotation;
        
        [field: SerializeField] public EnemyType EnemyType { get; private set; }
    }
}