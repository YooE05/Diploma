using System;
using System.Collections.Generic;
using UnityEngine;

namespace YooE.Diploma
{
    [CreateAssetMenu(
        fileName = "EnemyPoolsSettings",
        menuName = "Configs/Enemies/New EnemyPoolsSettings"
    )]
    public sealed class EnemyPoolsSettings : ScriptableObject
    {
        [field: SerializeField] public List<EnemyPoolConfigue> EnemyPoolsСonfigs { get; private set; }
    }

    [Serializable]
    public struct EnemyPoolConfigue
    {
        public EnemyType Type;
        public EnemyPoolConfig PoolConfig;
    }
}