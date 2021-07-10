using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.DialogueSystem
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

        private void AddObjectFieldInFoldout(Foldout foldout, DialogueEventSO objectValue = null)
        {
            foldout.AddObjectFieldFromUXML<DialogueEventSO>("Event:", objectValue);
        }

        public override bool CanConnectNode(DialogueGraphBaseNode targetNode)
        {
            return true;
        }

        public override DialogueGraphBaseNodeSaveData CreateNodeData()
        {
            DialogueGraphEventNodeSaveData eventNodeSaveData = CreateBaseNodeData<DialogueGraphEventNodeSaveData>();
            eventNodeSaveData.ObjectFields = _objectFieldFoldout.contentContainer.Query<ObjectField>()
                .ForEach(field => field.value as DialogueEventSO).ToList();
            return eventNodeSaveData;
        }

        public override void LoadNodeData(DialogueGraphBaseNodeSaveData stateInfo)
        {
            DialogueGraphEventNodeSaveData eventNodeSaveData = stateInfo as DialogueGraphEventNodeSaveData;
            eventNodeSaveData.ObjectFields.ForEach(objectValue => AddObjectFieldInFoldout(_objectFieldFoldout, objectValue));
        }
    }
}