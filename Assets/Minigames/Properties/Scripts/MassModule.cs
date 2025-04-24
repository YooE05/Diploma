using System.Collections.Generic;
using UnityEngine;

namespace YooE.Diploma.Properites
{
    public class MassModule : MonoBehaviour, IInteractionModule
    {
        [SerializeField] private float _targetMass = 0f;

        [SerializeField] private List<ItemProperties> _availableItems = new();

        private readonly List<ItemProperties> _selectedItems = new();

        private float _currentMass = 0f;

        public void Initialize(IReadOnlyList<ItemProperties> availableItems)
        {
            _availableItems = new List<ItemProperties>(availableItems);
            _currentMass = 0f;

            PropertiesUIManager.Instance.UpdateMassDisplay(_currentMass, _targetMass);
            PropertiesUIManager.Instance.ShowMassModuleUI();
            _selectedItems.Clear();
        }

        public void OnItemClicked(ItemProperties item)
        {
            if (_selectedItems.Contains(item))
            {
                _selectedItems.Remove(item);
                //Item remove animation
                PropertiesUIManager.Instance.MarkItem(item, false);
                _currentMass -= item.Mass;
            }
            else
            {
                _selectedItems.Add(item);
                //Item add animation
                PropertiesUIManager.Instance.MarkItem(item, true);
                _currentMass += item.Mass;
            }

            PropertiesUIManager.Instance.UpdateMassDisplay(_currentMass, _targetMass);
        }

        public bool CheckCondition()
        {
            var isTargetReached = Mathf.Approximately(_currentMass, _targetMass);
            return isTargetReached;
        }
    }
}