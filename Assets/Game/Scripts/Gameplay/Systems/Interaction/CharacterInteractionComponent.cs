using UnityEngine;
using YooE.DialogueSystem;
using Zenject;

namespace YooE.Diploma.Interaction
{
    public sealed class CharacterInteractionComponent : InteractionComponent
    {
        [SerializeField] private CharacterDialogueComponent _character;

        [Inject] private PlayerMotionController _playerMotionController;
        [Inject] private PlayerInteraction _playerInteraction;

        public override void Interact()
        {
            base.Interact();
            _character.StartCurrentDialogueGroup().Forget();
            _playerMotionController.DisableMotion();
            _playerInteraction.DisableInteraction();
        }
    }
}