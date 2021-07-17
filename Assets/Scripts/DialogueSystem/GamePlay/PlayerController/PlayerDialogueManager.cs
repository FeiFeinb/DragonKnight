using System;
using System.Collections.Generic;
using DialogueSystem.Old.Dialogue.ScriptableObjects;
using RPG.Module;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem.Graph
{
    public class PlayerDialogueManager : BaseSingletonWithMono<PlayerDialogueManager>
    {
        private Dictionary<string, DialogueTreeNode> _cacheDic = new Dictionary<string, DialogueTreeNode>();
        private DialogueTreeNode _rootNode;
        private GameObject _currentObj;

        public void StartDialogue(ConversationalNPC conversationalNpc)
        {
            // 获取根节点(StartNode)
            _rootNode = conversationalNpc.dialogueSO.GetDialogueTree();
            _cacheDic.Clear();
            _rootNode.Traverse(treeNode =>
            {
                string nodeUniqueID = treeNode.BaseNodeSaveData.UniqueID;
                if (!_cacheDic.ContainsKey(nodeUniqueID))
                {
                    _cacheDic.Add(treeNode.BaseNodeSaveData.UniqueID, treeNode);
                }
            });
            
            _currentObj = conversationalNpc.gameObject;
            
            DialogueController.controller.Show();
            HandleTreeNode(_rootNode);
        }

        private void HandleTreeNode(DialogueTreeNode startTreeNode)
        {
            if (startTreeNode != null && startTreeNode.BaseNodeSaveData.HandleData(startTreeNode, _currentObj))
            {
                foreach (DialogueTreeNode childTreeNode in startTreeNode.GetNextNodes())
                {
                    HandleTreeNode(childTreeNode);
                }
            }
        }

        /// <summary>
        /// 从指定的节点开始对话
        /// </summary>
        /// <param name="recordNodeUniqueID">指定的节点</param>
        public void ContinueDialogue(string recordNodeUniqueID)
        {
            DialogueTreeNode recordTreeNode = _cacheDic[recordNodeUniqueID];
            foreach (DialogueTreeNode childTreeNode in recordTreeNode.GetNextNodes())
            {
                HandleTreeNode(childTreeNode);
            }
        }


        public void EndDialogue()
        {
            DialogueController.controller.Hide();
        }
    }
}