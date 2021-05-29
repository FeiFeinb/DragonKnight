using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    [System.Serializable]
    public class DialogueGraphStartNodeSaveData : DialogueGraphBaseNodeSaveData
    {
        public DialogueGraphStartNodeSaveData(string guid, string title, Rect rectPos) : base(guid, title, rectPos)
        {
        }
    }
}