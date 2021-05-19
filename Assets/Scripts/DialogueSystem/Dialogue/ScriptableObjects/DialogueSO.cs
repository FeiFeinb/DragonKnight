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
        public Dictionary<string, DialogueNodeSO> NodeDic => nodeDic;
        
        // 只能撤销可序列化的变量
        [SerializeField] private List<DialogueNodeSO> nodes = new List<DialogueNodeSO>();                   // 节点数列
        
        private Dictionary<string, DialogueNodeSO> nodeDic = new Dictionary<string, DialogueNodeSO>();      // 节点ID字典
        
        private Vector2 nodeVerticalOffSet = new Vector2(0, 200);          // 生成节点纵向偏移
        
        private Vector2 nodeHorizontalOffSet = new Vector2(250, 0);          // 生成节点横向偏移
        
        private void OnValidate()
        {
            nodeDic.Clear();
            // 清空缓存
            // 重新填充ID字典
            foreach (DialogueNodeSO node in GetNodes())
            {
                node.name = node.Content;
                nodeDic.Add(node.UniqueID, node);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DialogueNodeSO> GetNodes()
        {
            return nodes;
        }
        
        /// <summary>
        /// 获取根节点
        /// </summary>
        /// <returns></returns>
        public DialogueNodeSO GetRootNode()
        {
            return GetNodes().FirstOrDefault(node => node.Parents.Count == 0);
        }
        
        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="uniqueID">节点ID</param>
        /// <returns></returns>
        public DialogueNodeSO GetNode(string uniqueID)
        {
            return nodeDic.ContainsKey(uniqueID) ? nodeDic[uniqueID] : null;
        }
        
        
        /// <summary>
        /// 获取子节点
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <returns>子节点们</returns>
        public IEnumerable<DialogueNodeSO> GetChildren(DialogueNodeSO parentNode)
        {
            return from child in parentNode.Children where nodeDic.ContainsKey(child) select nodeDic[child];
        }
        
        /// <summary>
        /// 获取可交互子节点
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <returns>可交互子节点们</returns>
        public IEnumerable<DialogueNodeSO> GetInteractiveChildren(DialogueNodeSO parentNode)
        {
            return GetChildren(parentNode).Where(child => child.Speaker == DialogueSpeaker.PlayerChoice);
        }
        
        /// <summary>
        /// 获取不可交互子节点
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <returns>不可交互子节点们</returns>
        public IEnumerable<DialogueNodeSO> GetNonInteractiveChildren(DialogueNodeSO parentNode)
        {
            return GetChildren(parentNode).Where(child => child.Speaker != DialogueSpeaker.PlayerChoice);
        }
        
        
#if UNITY_EDITOR
        /// <summary>
        /// 创建节点
        /// </summary>
        /// <param name="parentNode"></param>
        public void CreateNode(DialogueNodeSO parentNode)
        {
            // 制作节点
            DialogueNodeSO newNode = MakeNode(parentNode);
            Undo.RegisterCreatedObjectUndo(newNode, "Create Node");
            Undo.RecordObject(this, "Add DialogueNode");
            // 添加节点
            // AddRecord(newNode);
            nodes.Add(newNode);
            nodeDic.Add(newNode.UniqueID, newNode);
        }

        /// <summary>
        /// 制作节点
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <returns></returns>
        private DialogueNodeSO MakeNode(DialogueNodeSO parentNode = null)
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
        
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="deleteNode">待删除节点</param>
        public void DeleteNode(DialogueNodeSO deleteNode)
        {
            if (nodeDic.Count == 1)
            {
                Debug.LogError("不可删除根节点");
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
            nodes.Remove(deleteNode);
            nodeDic.Remove(deleteNode.UniqueID);
            Undo.DestroyObjectImmediate(deleteNode);
        }
        
        /// <summary>
        /// 添加节点记录
        /// </summary>
        /// <param name="recordNode">待记录节点</param>
        private void AddRecord(DialogueNodeSO recordNode)
        {
            nodes.Add(recordNode);
            nodeDic.Add(recordNode.UniqueID, recordNode);
        }
        
        /// <summary>
        /// 移除节点记录
        /// </summary>
        /// <param name="recordNode">待移除记录节点</param>
        private void RemoveRecord(DialogueNodeSO recordNode)
        {
            nodes.Remove(recordNode);
            nodeDic.Remove(recordNode.UniqueID);
        }
#endif
        
        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            // // TODO: 优化此部分逻辑
            if (nodes.Count == 0)
            {
                AddRecord(MakeNode());
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
                // 子节点保存了 刷新命名
                else if (node.name != node.Content)
                {
                    node.name = node.Content;
                }
            }
#endif
        }

        public void OnAfterDeserialize() {}
    }
}