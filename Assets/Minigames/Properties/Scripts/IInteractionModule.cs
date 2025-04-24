using System.Collections.Generic;

namespace YooE.Diploma.Properites
{
    public interface IInteractionModule
    {
        void Initialize(IReadOnlyList<ItemProperties> availableItems);
        void OnItemClicked(ItemProperties item);
        bool CheckCondition();
    }
}