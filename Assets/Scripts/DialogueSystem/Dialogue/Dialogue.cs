using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
namespace RPG.DialogueSystem
{
    [CreateAssetMenu(fileName = "New DialogueObject", menuName = "Dialogue System/Dialogue")]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<DialogueNode> nodes = new List<DialogueNode>();                   // 节点
        private Dictionary<string, DialogueNode> nodeDic = new Dictionary<string, DialogueNode>();      // 节点ID字典
        private Vector2 nodeVerticalOffSet = new Vector2(0, 200);          // 生成节点偏移
        private Vector2 nodeHorizontalOffSet = new Vector2(250, 0);          // 生成节点偏移
        private void OnValidate()
        {
            Debug.Log("重新填充节点ID字典");
            // 清空缓存
            nodeDic.Clear();
            // 重新填充ID字典
            foreach (DialogueNode node in GetNodes())
            {
                node.name = node.Text;
                nodeDic.Add(node.UniqueID, node);
            }
        }
        public DialogueNode GetRootNode()
        {
            // 数组的首位并不一定是根节点
            foreach (DialogueNode node in GetNodes())
            {
                if (node.Parents.Count == 0)
                {
                    return node;
                }
            }
            Debug.LogError("Cant Find Root Node");
            return null;
        }
        public DialogueNode GetNode(string _uniqueID)
        {
            if (nodeDic.ContainsKey(_uniqueID))
            {
                return nodeDic[_uniqueID];
            }
            return null;
        }
        public IEnumerable<DialogueNode> GetNodes()
        {
            return new List<DialogueNode>(nodes);
        }
        public IEnumerable<DialogueNode> GetChildren(DialogueNode parentNode)
        {
            foreach (string child in parentNode.Children)
            {
                if (nodeDic.ContainsKey(child))
                {
                    yield return nodeDic[child];
                }
            }
        }
        public IEnumerable<DialogueNode> GetInteractiveChildren(DialogueNode parentNode)
        {
            // 同时考虑因任务或物品使对话隐藏
            foreach (var child in GetChildren(parentNode))
            {
                if (child.Speaker == DialogueSpeaker.PlayerChoice)
                {
                    yield return child;
                }
            }
        }
        public IEnumerable<DialogueNode> GetNonInteractiveChildren(DialogueNode parentNode)
        {
            // 同时考虑因任务或物品使对话隐藏
            foreach (var child in GetChildren(parentNode))
            {
                if (child.Speaker == DialogueSpeaker.NPC || child.Speaker == DialogueSpeaker.Player)
                {
                    yield return child;
                }
            }
        }
#if UNITY_EDITOR
        public void CreateNode(DialogueNode parentNode)
        {
            // 创建并添加新节点
            DialogueNode newNode = MakeNode(parentNode);
            Undo.RecordObject(this, "Add DialogueNode");
            AddRecord(newNode);
        }

        private DialogueNode MakeNode(DialogueNode parentNode)
        {
            DialogueNode newNode = ScriptableObject.CreateInstance<DialogueNode>();
            if (parentNode != null)
            {
                // 部署节点位置
                Vector2 finalChildNodeOffSet = parentNode.Children == null || parentNode.Children.Count == 0 ?
                    parentNode.sizeRect.position + nodeHorizontalOffSet :
                        nodeDic[parentNode.Children[parentNode.Children.Count - 1]].sizeRect.position + nodeVerticalOffSet;
                // 初始化节点
                newNode.InitDialogueNode(parentNode);
                // 判断是否存在子节点
                newNode.SetRectPosition(finalChildNodeOffSet);
            }
            return newNode;
        }

        public void DeleteNode(DialogueNode deleteNode)
        {
            if (nodeDic.Count == 1)
            {
                Debug.Log("不可删除根节点");
                return;
            }
            Undo.RecordObject(this, "Delete DialogueNode");
            Undo.RecordObject(deleteNode, "Delete DialogueNode");
            List<string> newParents = new List<string>(deleteNode.Parents);
            foreach (string parentId in newParents)
            {
                nodeDic[parentId].UnLink(deleteNode);
            }
            List<string> newChildren = new List<string>(deleteNode.Children);
            foreach (string childID in newChildren)
            {
                deleteNode.UnLink(nodeDic[childID]);
            }
            RemoveRecord(deleteNode);
            Undo.DestroyObjectImmediate(deleteNode);
        }
        private void AddRecord(DialogueNode recordNode)
        {
            nodes.Add(recordNode);
            nodeDic.Add(recordNode.UniqueID, recordNode);
        }
        private void RemoveRecord(DialogueNode recordNode)
        {
            nodes.Remove(recordNode);
            nodeDic.Remove(recordNode.UniqueID);
        }
#endif

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            // TODO: 优化此部分逻辑
            if (nodes.Count == 0)
            {
                AddRecord(MakeNode(null));
            }
            // 场景中存在Dialogue资源
            if (!string.IsNullOrEmpty(AssetDatabase.GetAssetPath(this)))
            {
                // 遍历所有节点
                foreach (DialogueNode node in GetNodes())
                {
                    // 如果有子节点没有被保存
                    if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(node)))
                    {
                        AssetDatabase.AddObjectToAsset(node, this);
                    }
                }
            }
#endif
        }

        public void OnAfterDeserialize()
        {

        }
    }
}