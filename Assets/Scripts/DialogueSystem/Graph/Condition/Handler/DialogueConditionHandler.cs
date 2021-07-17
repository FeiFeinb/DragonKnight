using System.Collections.Generic;
using RPG.DialogueSystem.Graph;
using RPG.Module;

namespace DialogueSystem.Graph
{
    public class DialogueConditionHandler : BaseSingleton<DialogueConditionHandler>
    {
        public readonly Dictionary<ConditionDialogueNodeType, DialogueBaseConditionHandler> switchDic = new Dictionary<ConditionDialogueNodeType, DialogueBaseConditionHandler>();
        public DialogueConditionHandler()
        {
            switchDic.Add(ConditionDialogueNodeType.HasQuest, new DialogueHasItemConditionHandler());
            switchDic.Add(ConditionDialogueNodeType.CompleteQuest, new DialogueCompleteQuestConditionHandler());
            switchDic.Add(ConditionDialogueNodeType.HasItem, new DialogueHasItemConditionHandler());
            switchDic.Add(ConditionDialogueNodeType.Others, new DialogueOthersConditionHandler());
        }
    }
}