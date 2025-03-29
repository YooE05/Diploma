using UnityEngine;

namespace YooE.Diploma
{
    public class StageInitializer : MonoBehaviour, IStageInitializer
    {
        [SerializeField] protected CharactersTransformInitializer _charactersTransform;
        [SerializeField] private int _dialogueGroupIndex;

        public int GetIndex()
        {
            return _dialogueGroupIndex;
        }

        public virtual void InitGameView()
        {
            _charactersTransform.MoveCharactersToMainPoints();
        }
    }
}