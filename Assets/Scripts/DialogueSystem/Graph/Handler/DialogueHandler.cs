using System.Collections.Generic;
using DialogueSystem.Graph.Events;
using RPG.DialogueSystem.Graph;
using RPG.Module;

namespace DialogueSystem.Graph
{
    public class DialogueHandler : BaseSingleton<DialogueHandler>
    {
        public readonly Dictionary<DialogueConditionType, IDialogueConditionHandler> ConditionHandlers = new Dictionary<DialogueConditionType, IDialogueConditionHandler>();
        
        public readonly Dictionary<DialogueEventType, IDialogueEventHandler> EventHandlers =
            new Dictionary<DialogueEventType, IDialogueEventHandler>();
        
        public DialogueHandler()
        {
            ConditionHandlers.Add(DialogueConditionType.HasQuest, new DialogueHasQuestConditionHandler());
            ConditionHandlers.Add(DialogueConditionType.CompleteQuest, new DialogueCompleteQuestConditionHandler());
            ConditionHandlers.Add(DialogueConditionType.HasItem, new DialogueHasItemConditionHandler());
            ConditionHandlers.Add(DialogueConditionType.Others, new DialogueOtherConditionConditionHandler());

            EventHandlers.Add(DialogueEventType.AcceptQuest, new DialogueAcceptQuestConditionHandler());
            EventHandlers.Add(DialogueEventType.ProgressQuest, new DialogueProgressQuestConditionHandler());
            EventHandlers.Add(DialogueEventType.SubmitQuest, new DialogueSubmitQuestConditionHandler());
            EventHandlers.Add(DialogueEventType.Others, new DialogueOtherEventsConditionHandler());
        }
    }
}