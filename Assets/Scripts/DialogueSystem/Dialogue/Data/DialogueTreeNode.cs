using System.Collections.Generic;

namespace RPG.DialogueSystem
{
    public class DialogueTreeNode
    {
        public DialogueGraphBaseNodeSaveData BaseNodeSaveData;
        public List<DialogueTreeNode> childrenNode = new List<DialogueTreeNode>();
    }
}