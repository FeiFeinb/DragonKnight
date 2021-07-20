using System;
using System.Linq;
using RPG.DialogueSystem.Graph;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueSystem.Editor
{
    public sealed class DialogueGraphEventNode : DialogueGraphBaseNode
    {
        private readonly Foldout _objectFieldFoldout;

        public DialogueGraphEventNode(Vector2 position, DialogueGraphView graphView,
            DialogueGraphEventNodeSaveData eventNodeSaveData = null) : base(position, graphView,
            eventNodeSaveData?.UniqueID)
        {
            title = "Event Node";

            AddInputPort("Parents", Port.Capacity.Single);
            AddOutputPort("Children", Port.Capacity.Single);

            _objectFieldFoldout = AddFoldout("Events:").Item1;
            RefreshExpandedState();
        }

        protected override (Foldout, VisualElement) AddFoldout(string labelStr)
        {
            (Foldout, VisualElement) foldoutPair = base.AddFoldout(labelStr);
            Button addButton = foldoutPair.Item2.Q<Button>(DialogueGraphUSSName.DIALOGUE_NODE_ADD_BUTTON);
            addButton.clickable.clicked += delegate { AddObjectFieldInFoldout(foldoutPair.Item1); };
            return foldoutPair;
        }

        private void AddObjectFieldInFoldout(Foldout foldout, DialogueEventType eventType = DialogueEventType.Others, ScriptableObject objectValue = null)
        {
            foldout.AddEventFieldFromUXML(eventType, objectValue);
        }

        public override bool CanConnectNode(DialogueGraphBaseNode targetNode)
        {
            return true;
        }

        public override DialogueGraphBaseNodeSaveData CreateNodeData()
        {
            DialogueGraphEventNodeSaveData eventNodeSaveData = CreateBaseNodeData<DialogueGraphEventNodeSaveData>();
            eventNodeSaveData.EventFieldsData = _objectFieldFoldout.contentContainer.Query<VisualElement>(DialogueGraphUSSName.DIALOGUE_NODE_EVENT_FIELD)
                .ForEach(eventField => new DialogueGraphEventNodeSaveData.EventTuple()
                {
                    EventType = (DialogueEventType)eventField.Q<EnumField>().value,
                    SO = eventField.Q<ObjectField>().value as ScriptableObject
                }).ToList();
            return eventNodeSaveData;
        }

        public override void LoadNodeData(DialogueGraphBaseNodeSaveData stateInfo)
        {
            DialogueGraphEventNodeSaveData eventNodeSaveData = stateInfo as DialogueGraphEventNodeSaveData;
            eventNodeSaveData.EventFieldsData.ForEach(eventFieldData => AddObjectFieldInFoldout(_objectFieldFoldout, eventFieldData.EventType, eventFieldData.SO));
        }
    }
}