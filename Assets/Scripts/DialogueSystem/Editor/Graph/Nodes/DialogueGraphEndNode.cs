using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace RPG.DialogueSystem.Graph
{
    public sealed class DialogueGraphEndNode : DialogueGraphBaseNode
    {
        private readonly EnumField _endTypeEnumField;      // 节点结束类型EnumField

        public DialogueGraphEndNode(Vector2 position,
            DialogueGraphView graphView, DialogueGraphEndNodeSaveData endNodeSaveData = null) : base(position, graphView, endNodeSaveData?.UniqueID)
        {
            title = "End Node";
            AddInputPort("Parents", Port.Capacity.Multi);
            
            _endTypeEnumField = CreateEnumField(EndDialogueNodeType.End, "EndType:");
            extensionContainer.Add(_endTypeEnumField);
            
            RefreshExpandedState();
        }

        public override bool CanConnectNode(DialogueGraphBaseNode targetNode)
        {
            return true;
        }

        public override DialogueGraphBaseNodeSaveData CreateNodeData()
        {
            DialogueGraphEndNodeSaveData endNodeSavaData = CreateBaseNodeData<DialogueGraphEndNodeSaveData>();
            endNodeSavaData.EndType = (EndDialogueNodeType) _endTypeEnumField.value;
            return endNodeSavaData;
        }

        public override void LoadNodeData(DialogueGraphBaseNodeSaveData stateInfo)
        {
            _endTypeEnumField.value = (stateInfo as DialogueGraphEndNodeSaveData).EndType;
        }
    }
}