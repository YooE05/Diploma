using System.Collections.Generic;
using UnityEngine;

namespace YooE.Diploma.Properites
{
    public class VolumeModule : MonoBehaviour, IInteractionModule
    {
        [SerializeField]
        private float _targetVolume = 0f;

        [SerializeField]
        private List<ItemProperties> _availableItems = new();

        private readonly List<ItemProperties> _selectedItems = new();
        private float _displacedVolume = 0f;

        public void Initialize(IReadOnlyList<ItemProperties> availableItems)
        {
            _availableItems = new List<ItemProperties>(availableItems);
            _displacedVolume = 0f;
            
            PropertiesUIManager.Instance.UpdateVolumeDisplay(_displacedVolume, _targetVolume);
            PropertiesUIManager.Instance.ShowVolumeModuleUI();
            _selectedItems.Clear();
        }

        public void OnItemClicked(ItemProperties item)
        {
            if (_selectedItems.Contains(item))
            {
                _selectedItems.Remove(item);
                //Item remove animation
                PropertiesUIManager.Instance.MarkItem(item, false);
                _displacedVolume -= item.Volume;
            }
            else
            {
                _selectedItems.Add(item);
                //Item add animation
                PropertiesUIManager.Instance.MarkItem(item, true);
                _displacedVolume += item.Volume;
            }

            PropertiesUIManager.Instance.UpdateVolumeDisplay(_displacedVolume, _targetVolume);
        }

        public bool CheckCondition()
        {
            var isTargetReached = Mathf.Approximately(_displacedVolume, _targetVolume);
            return isTargetReached;
        }
    }
}