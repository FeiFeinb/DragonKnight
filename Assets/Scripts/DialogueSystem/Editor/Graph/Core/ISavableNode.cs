namespace RPG.DialogueSystem.Graph
{
    /// <summary>
    /// 可保存节点接口
    /// </summary>
    public interface ISavableNode
    {
        /// <summary>
        /// 创建节点保存信息
        /// </summary>
        /// <returns>节点保存信息</returns>
        DialogueGraphBaseNodeSaveData CreateNodeData();

        /// <summary>
        /// 加载节点保存信息
        /// </summary>
        /// <param name="data">节点保存信息</param>
        void LoadNodeData(DialogueGraphBaseNodeSaveData data);
    }
}