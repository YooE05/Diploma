using System.Collections.Generic;
using DS.ScriptableObjects;
using UnityEngine;

namespace YooE.DialogueSystem
{
    [CreateAssetMenu(
        fileName = "DialogueGroupsSequenceConfig",
        menuName = "Configs/DialogueSystem/New DialogueGroupsSequenceConfig"
    )]
    public class DialogueGroupsSequenceConfig : ScriptableObject
    {
        [field: SerializeField] public List<DSDialogueGroupSO> Groups;
    }
}