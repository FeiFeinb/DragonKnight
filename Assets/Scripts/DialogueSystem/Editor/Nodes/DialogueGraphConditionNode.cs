using RPG.DialogueSystem.Graph;
using RPG.InventorySystem;
using RPG.QuestSystem;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;

namespace DialogueSystem.Editor
{
    public sealed class DialogueGraphConditionNode : DialogueGraphBaseNode
    {
        private readonly EnumField _conditionField;
        private readonly ObjectField _determineField;
        public DialogueGraphConditionNode(Vector2 position, DialogueGraphView graphView, DialogueGraphConditionNodeSaveData conditionNodeSaveData = null) : base(position, graphView, conditionNodeSaveData?.UniqueID)
        {
            title = "Condition Node";
            
            AddInputPort("Source", Port.Capacity.Single);
            AddOutputPort("True", Port.Capacity.Single);
            AddOutputPort("False", Port.Capacity.Single);
            
            // 初值设定为是否拥有任务
            _conditionField = CreateEnumField(DialogueConditionType.HasQuest, "ConditionType", evt => SwitchObjectFieldType((DialogueConditionType)evt.newValue, (DialogueConditionType)evt.previousValue));
            extensionContainer.Add(_conditionField);

            _determineField = CreateObjectField<QuestSO>("Quest:");
            extensionContainer.Add(_determineField);
            
            RefreshExpandedState();
        }

        public override bool CanConnectNode(DialogueGraphBaseNode targetNode)
        {
            return !(targetNode is DialogueGraphEndNode);
        }

        public override DialogueGraphBaseNodeSaveData CreateNodeData()
        {
            DialogueGraphConditionNodeSaveData conditionNodeSaveData = CreateBaseNodeData<DialogueGraphConditionNodeSaveData>();
            conditionNodeSaveData.dialogueConditionType = (DialogueConditionType) _conditionField.value;
            conditionNodeSaveData.SourceSO = _determineField.value as ScriptableObject;
            return conditionNodeSaveData;
        }

        public override void LoadNodeData(DialogueGraphBaseNodeSaveData stateInfo)
        {
            DialogueGraphConditionNodeSaveData conditionNodeSaveData = stateInfo as DialogueGraphConditionNodeSaveData;
            _conditionField.value = conditionNodeSaveData.dialogueConditionType;
            SwitchObjectFieldType(conditionNodeSaveData.dialogueConditionType, DialogueConditionType.Others);
            _determineField.value = conditionNodeSaveData.SourceSO;
        }

        private void SwitchObjectFieldType(DialogueConditionType nodeType, DialogueConditionType oldType)
        {
            switch (nodeType)
            {
                case DialogueConditionType.CompleteQuest:
                case DialogueConditionType.HasQuest:
                    if (oldType != DialogueConditionType.CompleteQuest && oldType != DialogueConditionType.HasQuest)
                    {
                        _determineField.value = null;
                        _determineField.objectType = typeof(QuestSO);
                        _determineField.label = "Quest:";
                    }
                    break;
                case DialogueConditionType.HasItem:
                    _determineField.value = null;
                    _determineField.objectType = typeof(BaseItemObject);
                    _determineField.label = "Item:";
                    break;
                default:
                    _determineField.value = null;
                    _determineField.objectType = typeof(DialogueConditionSO);
                    _determineField.label = "Others:";
                    break;
            }
        }
    }
}

