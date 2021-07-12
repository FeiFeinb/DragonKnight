using System.Collections.Generic;
using RPG.Module;

namespace RPG.DialogueSystem
{
    public class PlayerDialogueManager : BaseSingletonWithMono<PlayerDialogueManager>
    {
        private Dictionary<string, DialogueGraphBaseNodeSaveData> _cachedNodeData =
            new Dictionary<string, DialogueGraphBaseNodeSaveData>();
        public void StartDialogue(DialogueGraphSO _dialogueSO)
        {
            // 完成数据缓存
            TraverseDialogueGraph(_dialogueSO);
            // 遍历连线数据数组 依次处理所有节点
        }

        private void TraverseDialogueGraph(DialogueGraphSO dialogueSO)
        {
            void CacheDataInDic(DialogueGraphBaseNodeSaveData saveData)
            {
                _cachedNodeData.Add(saveData.UniqueID, saveData);
            }
            dialogueSO.startNodesSaveData.ForEach(CacheDataInDic);
            dialogueSO.endNodesSaveData.ForEach(CacheDataInDic);
            dialogueSO.talkNodesSaveData.ForEach(CacheDataInDic);
            dialogueSO.conditionNodesSaveData.ForEach(CacheDataInDic);
            dialogueSO.eventNodesSaveData.ForEach(CacheDataInDic);
            dialogueSO.choiceNodesSaveData.ForEach(CacheDataInDic);
        }
    }
}