using System;
using System.Collections.Generic;

namespace RPG.DialogueSystem.Graph
{
    public class DialogueTreeNode
    {
        public class TreeNodePair
        {
            public TreeNodePair(DialogueTreeNode node, bool canThrough)
            {
                Node = node;
                CanThrough = canThrough;
            }

            public void SetCanThrough(bool canThrough)
            {
                CanThrough = canThrough;
            }
            
            public readonly DialogueTreeNode Node;
            public bool CanThrough;
        }

        public DialogueTreeNode(string dialogueGraphSOUniqueID, DialogueGraphBaseNodeSaveData baseNodeSaveData)
        {
            DialogueGraphSOUniqueID = dialogueGraphSOUniqueID;
            BaseNodeSaveData = baseNodeSaveData;
        }
        
        public string DialogueGraphSOUniqueID;
        public DialogueGraphBaseNodeSaveData BaseNodeSaveData;
        public readonly List<TreeNodePair> childrenNodePair = new List<TreeNodePair>();
        
        public IEnumerable<DialogueTreeNode> GetNextNodes()
        {
            foreach (var pair in childrenNodePair)
            {
                if (pair.CanThrough)
                {
                    yield return pair.Node;
                }
            }
        }

        // 遍历树
        public void Traverse(Action<DialogueTreeNode> callBack)
        {
            foreach (TreeNodePair childPair in childrenNodePair)
            {
                callBack(this);
                childPair.Node?.Traverse(callBack);
            }
        }
    }
}