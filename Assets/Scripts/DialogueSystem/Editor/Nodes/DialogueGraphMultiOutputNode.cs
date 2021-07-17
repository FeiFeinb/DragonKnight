using System;
using System.Collections.Generic;
using System.Linq;
using RPG.DialogueSystem.Graph;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueSystem.Editor
{
    public abstract class DialogueGraphMultiOutputNode : DialogueGraphBaseNode
    {
        protected readonly List<VisualElement> collapsibleElements = new List<VisualElement>();     // 可折叠的组件

        protected DialogueGraphMultiOutputNode(Vector2 position, DialogueGraphView graphView, string uniqueID = null) :
            base(position, graphView, uniqueID)
        {
        }

        public override bool expanded
        {
            set
            {
                bool isDisplay = value;
                base.expanded = isDisplay;
                // 使折叠时按钮能隐藏
                collapsibleElements.ForEach(element => element.style.display = isDisplay
                    ? new StyleEnum<DisplayStyle>(DisplayStyle.Flex)
                    : new StyleEnum<DisplayStyle>(DisplayStyle.None));
                RefreshExpandedState();
            }
        }

        /// <summary>
        /// 初始化Port并创建新增Port的Button
        /// </summary>
        /// <param name="baseNodeSaveData">节点数据</param>
        /// <param name="outputPortStr">输出Port标签</param>
        protected void GenerateOutputPort(DialogueGraphBaseNodeSaveData baseNodeSaveData, string outputPortStr)
        {
            if (baseNodeSaveData == null)
            {
                AddOutputPort(outputPortStr, Port.Capacity.Single);
            }
            else
            {
                foreach (DialogueGraphPortSaveData dialogueGraphPortSaveData in baseNodeSaveData.OutputPortsData)
                {
                    AddOutputPort(dialogueGraphPortSaveData.PortName, dialogueGraphPortSaveData.Capacity.SwitchType());
                }
            }
            
            // 创建添加端口Button
            Button addPortButton = CreateButton("+", delegate
            {
                AddOutputPort(outputPortStr, Port.Capacity.Single);
            });
            collapsibleElements.Add(addPortButton);
            topContainer.Insert(1, addPortButton);
        }

        protected override Port AddInputPort(string portStr, Port.Capacity capacity, Type portType = null)
        {
            Port newInputPort = base.AddInputPort(portStr, capacity, portType);
            // 使其在输出节点拓展时位置能够居中
            newInputPort.style.flexGrow = 1;
            return newInputPort;
        }

        protected override Port AddOutputPort(string portName, Port.Capacity capacity, Type portType = null)
        {
            Port outputPort = base.AddOutputPort(portName, capacity, portType);
            // 创建删除端口Button
            Button deleteButton = CreateButton("X", delegate
            {
                // 不允许清空所有端口
                if (_outputBasePorts.Count <= 1) return;
                // 移除端口间连线
                foreach (var edge in _graphView.edges.Where(edge => edge.output == outputPort))
                {
                    edge.input.Disconnect(edge);
                    edge.output.Disconnect(edge);
                    _graphView.RemoveElement(edge);
                }

                // 移除输出节点记录
                _outputBasePorts.Remove(outputPort);
                // 从节点中移除端口
                outputContainer.Remove(outputPort);
                RefreshPorts();
            });
            collapsibleElements.Add(deleteButton);
            outputPort.contentContainer.Add(deleteButton);

            RefreshPorts();
            return outputPort;
        }
    }
}