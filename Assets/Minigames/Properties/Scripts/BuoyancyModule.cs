using System.Collections.Generic;
using UnityEngine;

namespace YooE.Diploma.Properites
{
    public class BuoyancyModule : MonoBehaviour, IInteractionModule
    {
        [SerializeField] private bool _shouldFloat = true;
        [SerializeField] private int _floatCount = 3;
        [SerializeField] private List<ItemProperties> _availableItems = new();

        private readonly List<ItemProperties> _selectedItems = new();

        public void Initialize(IReadOnlyList<ItemProperties> availableItems)
        {
            _availableItems = new List<ItemProperties>(availableItems);
            _selectedItems.Clear();

            PropertiesUIManager.Instance.ShowBuoyancyModuleUI();
        }

        public void OnItemClicked(ItemProperties item)
        {
            if (_selectedItems.Contains(item))
            {
                _selectedItems.Remove(item);
                //Item remove animation
            }
            else
            {
                _selectedItems.Add(item);
                //Item add animation
            }

            var isCorrect = (item.IsBuoyant == _shouldFloat);
            PropertiesUIManager.Instance.MarkItem(item, isCorrect);
        }

        public bool CheckCondition()
        {
            foreach (var item in _selectedItems)
            {
                if (item.IsBuoyant != _shouldFloat)
                {
                    return false;
                }
            }

            return _selectedItems.Count == _floatCount;
        }
    }
}