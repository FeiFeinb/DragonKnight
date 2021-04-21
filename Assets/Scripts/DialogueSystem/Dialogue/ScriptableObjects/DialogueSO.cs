using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
namespace RPG.DialogueSystem
{
    [CreateAssetMenu(fileName = "New DialogueObject", menuName = "Dialogue System/Dialogue")]
    public class DialogueSO : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<DialogueNodeSO> nodes = new List<DialogueNodeSO>();                   // 节点
        private Dictionary<string, DialogueNodeSO> nodeDic = new Dictionary<string, DialogueNodeSO>();      // 节点ID字典
        private Vector2 nodeVerticalOffSet = new Vector2(0, 200);          // 生成节点偏移
        private Vector2 nodeHorizontalOffSet = new Vector2(250, 0);          // 生成节点偏移
        private void OnValidate()
        {
            Debug.Log("重新填充节点ID字典");
            // 清空缓存
            nodeDic.Clear();
            // 重新填充ID字典
            foreach (DialogueNodeSO node in GetNodes())
            {
                node.name = node.Text;
                nodeDic.Add(node.UniqueID, node);
            }
        }
        public DialogueNodeSO GetRootNode()
        {
            // 数组的首位并不一定是根节点
            foreach (DialogueNodeSO node in GetNodes())
            {
                if (node.Parents.Count == 0)
                {
                    return node;
                }
            }
            Debug.LogError("Cant Find Root Node");
            return null;
        }
        public DialogueNodeSO GetNode(string _uniqueID)
        {
            if (nodeDic.ContainsKey(_uniqueID))
            {
                return nodeDic[_uniqueID];
            }
            return null;
        }
        public IEnumerable<DialogueNodeSO> GetNodes()
        {
            return new List<DialogueNodeSO>(nodes);
        }
        public IEnumerable<DialogueNodeSO> GetChildren(DialogueNodeSO parentNode)
        {
            foreach (string child in parentNode.Children)
            {
                if (nodeDic.ContainsKey(child))
                {
                    yield return nodeDic[child];
                }
            }
        }
        public IEnumerable<DialogueNodeSO> GetInteractiveChildren(DialogueNodeSO parentNode)
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
        public IEnumerable<DialogueNodeSO> GetNonInteractiveChildren(DialogueNodeSO parentNode)
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
        public void CreateNode(DialogueNodeSO parentNode)
        {
            // 创建并添加新节点
            DialogueNodeSO newNode = MakeNode(parentNode);
            Undo.RecordObject(this, "Add DialogueNode");
            AddRecord(newNode);
        }

        private DialogueNodeSO MakeNode(DialogueNodeSO parentNode)
        {
            DialogueNodeSO newNode = ScriptableObject.CreateInstance<DialogueNodeSO>();
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

        public void DeleteNode(DialogueNodeSO deleteNode)
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
        private void AddRecord(DialogueNodeSO recordNode)
        {
            nodes.Add(recordNode);
            nodeDic.Add(recordNode.UniqueID, recordNode);
        }
        private void RemoveRecord(DialogueNodeSO recordNode)
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
                foreach (DialogueNodeSO node in GetNodes())
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