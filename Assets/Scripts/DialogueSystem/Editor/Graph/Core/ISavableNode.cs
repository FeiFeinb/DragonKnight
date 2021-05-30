namespace RPG.DialogueSystem.Graph
{
    public interface ISavableNode
    {
        DialogueGraphBaseNodeSaveData CreateNodeData();

        void LoadNodeData(DialogueGraphBaseNodeSaveData data);
    }
}