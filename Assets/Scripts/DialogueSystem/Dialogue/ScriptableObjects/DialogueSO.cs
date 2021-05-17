using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEditor;
namespace RPG.DialogueSystem
{
    /// <summary>
    /// 对话SO
    /// </summary>
    [CreateAssetMenu(fileName = "New DialogueObject", menuName = "Dialogue System/Dialogue")]
    public class DialogueSO : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] private List<DialogueNodeSO> nodes = new List<DialogueNodeSO>();                   // 节点
        
        private readonly Dictionary<string, DialogueNodeSO> nodeDic = new Dictionary<string, DialogueNodeSO>();      // 节点ID字典
        
        private readonly Vector2 nodeVerticalOffSet = new Vector2(0, 200);          // 生成节点偏移
        
        private readonly Vector2 nodeHorizontalOffSet = new Vector2(250, 0);          // 生成节点偏移
        
        private void OnEnable()
        {
            Debug.Log("重新填充节点ID字典");
            // 清空缓存
            nodeDic.Clear();
            // 重新填充ID字典
            foreach (DialogueNodeSO node in GetNodes())
            {
                node.name = node.Content;
                nodeDic.Add(node.UniqueID, node);
            }
        }
        public DialogueNodeSO GetRootNode()
        {
            // 数组的首位并不一定是根节点
            return GetNodes().FirstOrDefault(node => node.Parents.Count == 0);
        }
        public DialogueNodeSO GetNode(string _uniqueID)
        {
            return nodeDic.ContainsKey(_uniqueID) ? nodeDic[_uniqueID] : null;
        }
        
        public IEnumerable<DialogueNodeSO> GetNodes()
        {
            return new List<DialogueNodeSO>(nodes);
        }
        
        public IEnumerable<DialogueNodeSO> GetChildren(DialogueNodeSO parentNode)
        {
            return from child in parentNode.Children where nodeDic.ContainsKey(child) select nodeDic[child];
        }
        
        public IEnumerable<DialogueNodeSO> GetInteractiveChildren(DialogueNodeSO parentNode)
        {
            // 同时考虑因任务或物品使对话隐藏
            return GetChildren(parentNode).Where(child => child.Speaker == DialogueSpeaker.PlayerChoice);
        }
        
        public IEnumerable<DialogueNodeSO> GetNonInteractiveChildren(DialogueNodeSO parentNode)
        {
            // 同时考虑因任务或物品使对话隐藏
            return GetChildren(parentNode).Where(child => child.Speaker != DialogueSpeaker.PlayerChoice);
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
            if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(this))) return;
            // 遍历所有节点
            foreach (DialogueNodeSO node in GetNodes())
            {
                // 如果有子节点没有被保存
                if (string.IsNullOrEmpty(AssetDatabase.GetAssetPath(node)))
                {
                    AssetDatabase.AddObjectToAsset(node, this);
                }
            }
#endif
        }

        public void OnAfterDeserialize()
        {

        }
    }
}