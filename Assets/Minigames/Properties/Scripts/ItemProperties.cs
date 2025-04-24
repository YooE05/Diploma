using UnityEngine;

namespace YooE.Diploma.Properites
{
    [CreateAssetMenu(menuName = "MiniGame/ItemProperties")]
    public class ItemProperties : ScriptableObject
    {
        [field: SerializeReference] public string ItemName { get; private set; }
        [field: SerializeReference] public float Mass { get; private set; }
        [field: SerializeReference] public float Volume { get; private set; }
        [field: SerializeReference] public bool IsBuoyant { get; private set; }
        [field: SerializeReference] public float MeltingPoint { get; private set; }
    }
}