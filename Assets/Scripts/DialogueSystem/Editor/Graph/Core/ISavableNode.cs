namespace RPG.DialogueSystem.Graph
{
    public interface ISavableNode<T>
    {
        T CreateNodeData();

        void LoadNodeData(T data);
    }
}