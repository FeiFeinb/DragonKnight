namespace RPG.Interact
{
    public enum InteractType
    {
        /// <summary>
        /// 交互后删除自身
        /// </summary>
        DestroySelf,
        
        /// <summary>
        /// 交互后保持不变
        /// </summary>
        Keep,
        
        /// <summary>
        /// 交互后全部清空
        /// </summary>
        DestroyAll
    }
}