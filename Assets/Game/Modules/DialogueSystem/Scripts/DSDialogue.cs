using UnityEngine;

namespace DS
{
    using ScriptableObjects;

    public class DSDialogue : MonoBehaviour
    {
        /* Dialogue Scriptable Objects */
        public DSDialogueContainerSO DialogueContainer;
        public DSDialogueGroupSO DialogueGroup;
        public DSDialogueSO Dialogue;

        /* Filters */
        public bool GroupedDialogues;
        public  bool StartingDialoguesOnly;

        /* Indexes */
        public  int SelectedDialogueGroupIndex;
        public int SelectedDialogueIndex;
    }
}