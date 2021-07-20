namespace RPG.DialogueSystem.Graph
{
    public enum DialogueEventType
    {
        /// <summary>
        /// 接受任务
        /// </summary>
        AcceptQuest,
        
        /// <summary>
        /// 推进任务
        /// </summary>
        ProgressQuest,
        
        /// <summary>
        /// 提交任务
        /// </summary>
        SubmitQuest,
        
        /// <summary>
        /// 其他事件
        /// </summary>
        Others
    }
}