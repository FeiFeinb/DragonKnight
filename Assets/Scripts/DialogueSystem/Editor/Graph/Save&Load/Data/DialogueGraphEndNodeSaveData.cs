using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    [System.Serializable]
    public class DialogueGraphEndNodeSaveData : DialogueGraphBaseNodeSaveData
    {
        public EndDialogueNodeType _endType;

        public DialogueGraphEndNodeSaveData(string guid, string title, Rect rectPos) : base(guid, title, rectPos)
        {
        }
    }
}