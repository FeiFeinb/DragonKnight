using System;
using System.Collections.Generic;
using System.Linq;
using RPG.DialogueSystem.Graph;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueSystem.Editor
{
    public static class DialogueGraphExtensionMethod
    {
        /// <summary>
        /// 记录端口组为可保存的端口数据数列
        /// </summary>
        /// <param name="basePorts">端口组</param>
        /// <param name="graphView">端口组所在节点编辑器图</param>
        /// <returns>端口数据数列</returns>
        public static List<DialogueGraphPortSaveData> ToPortData(this IEnumerable<Port> basePorts,
            DialogueGraphView graphView)
        {
            return basePorts.Select(basePort =>
                {
                    var connectedEdge = graphView.edges.Where(edge => edge.output == basePort || edge.input == basePort)
                        .ToList();
                    return new DialogueGraphPortSaveData()
                    {
                        PortName = basePort.portName,
                        Capacity = basePort.capacity.SwitchType(),
                        EdgesSaveData = connectedEdge.Select(edge => new DialogueGraphEdgeSaveData()
                        {
                            InputNodeUniqueID = edge.input.GetDialogueNode()?.UniqueID,
                            OutputNodeUniqueID = edge.output.GetDialogueNode()?.UniqueID
                        }).ToList(),
                    };
                }
            ).ToList();
        }

        /// <summary>
        /// 清空图中连线与节点
        /// </summary>
        /// <param name="graphView">对画图</param>
        public static void ClearEdgesAndNodes(this DialogueGraphView graphView)
        {
            // 清空连线
            foreach (Edge edge in graphView.edges)
            {
                graphView.RemoveElement(edge);
            }

            // 清空节点
            foreach (Node selectViewPort in graphView.nodes)
            {
                graphView.RemoveElement(selectViewPort);
            }
        }

        /// <summary>
        /// 计算得到输入端口索引
        /// </summary>
        /// <param name="baseNodeDic">节点字典</param>
        /// <param name="originUniqueID">源节点ID</param>
        /// <param name="targetUniqueID">目标节点ID</param>
        /// <returns>端口索引</returns>
        public static int GetInputPortIndex(this Dictionary<string, DialogueGraphBaseNodeSaveData> baseNodeDic,
            string originUniqueID, string targetUniqueID)
        {
            DialogueGraphBaseNodeSaveData baseNodeSaveData = baseNodeDic[originUniqueID];
            // 遍历节点
            for (int i = 0; i < baseNodeSaveData.InputPortsData.Count; i++)
            {
                // 找到输入节点的第一个匹配的输出节点
                if (baseNodeSaveData.InputPortsData[i].EdgesSaveData.Any(dialogueGraphEdgeSaveData =>
                    dialogueGraphEdgeSaveData.OutputNodeUniqueID == targetUniqueID))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 计算得到输出端口索引
        /// </summary>
        /// <param name="baseNodeDic">节点字典</param>
        /// <param name="originUniqueID">源节点ID</param>
        /// <param name="targetUniqueID">目标节点ID</param>
        /// <returns>端口索引</returns>
        public static int GetOutputPortIndex(this Dictionary<string, DialogueGraphBaseNodeSaveData> baseNodeDic,
            string originUniqueID, string targetUniqueID)
        {
            DialogueGraphBaseNodeSaveData baseNodeSaveData = baseNodeDic[originUniqueID];
            // 遍历节点
            for (int i = 0; i < baseNodeSaveData.OutputPortsData.Count; i++)
            {
                // 找到输出节点的第一个匹配的输入节点
                if (baseNodeSaveData.OutputPortsData[i].EdgesSaveData.Any(dialogueGraphEdgeSaveData =>
                    dialogueGraphEdgeSaveData.InputNodeUniqueID == targetUniqueID))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 从UXML中创建ObjectField
        /// </summary>
        /// <param name="foldout">源类</param>
        /// <param name="labelStr">ObjectField中的标签</param>
        /// <param name="objectValue">记录的数据</param>
        /// <typeparam name="T">记录的数据类型</typeparam>
        /// <returns>创建的ObjectField</returns>
        public static ObjectField AddObjectFieldFromUXML<T>(this Foldout foldout, string labelStr, T objectValue = null)
            where T : ScriptableObject
        {
            // TODO: 建立VisualTreeAsset资源创建类
            VisualTreeAsset buttonPairTreeAsset =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(DialogueGraphAssetsPath
                    .DialogueGraphNodeObjectFieldButtonPair);
            VisualElement objectFieldButtonPair = buttonPairTreeAsset.Instantiate();

            ObjectField objectField =
                objectFieldButtonPair.Q<ObjectField>(DialogueGraphUSSName.DIALOGUE_NODE_OBJECT_FIELD);
            objectField.objectType = typeof(T);
            objectField.value = objectValue;
            objectField.label = labelStr;
            objectField.labelElement.name = DialogueGraphUSSName.DIALOGUE_NODE_LABEL;

            Button deleteButton = objectFieldButtonPair.Q<Button>(DialogueGraphUSSName.DIALOGUE_NODE_DELETE_BUTTON);
            // TODO: 设置按钮样式
            deleteButton.clickable.clicked += () => foldout.contentContainer.Remove(objectFieldButtonPair);

            foldout.contentContainer.Add(objectFieldButtonPair);
            return objectField;
        }

        /// <summary>
        /// 保存图中节点
        /// </summary>
        /// <param name="_selectSO">选中的SO</param>
        /// <param name="graphView">节点图</param>
        public static void Save(this DialogueGraphSO _selectSO, DialogueGraphView graphView)
        {
            // List<SaveT> CaptureNodesData<T, SaveT>(UQueryState<Node> graphNodes) where T : DialogueGraphBaseNode
            // {
            //     return graphNodes.Where(node => node is T).Cast<T>().Select(node => node.CreateNodeData()).Cast<SaveT>()
            //         .ToList();
            // }

            Dictionary<string, DialogueGraphBaseNodeSaveData> cachedData = new Dictionary<string, DialogueGraphBaseNodeSaveData>();
            
            // 储存节点数据
            _selectSO.Clear();
            IEnumerable<DialogueGraphBaseNode> baseNodes = graphView.nodes.Cast<DialogueGraphBaseNode>();
            foreach (DialogueGraphBaseNode baseNode in baseNodes)
            {
                DialogueGraphBaseNodeSaveData baseNodeSaveData = baseNode.CreateNodeData();
                cachedData.Add(baseNodeSaveData.UniqueID, baseNodeSaveData);
                switch (baseNode)
                {
                    case DialogueGraphStartNode _:
                        _selectSO.startNodesSaveData.Add(baseNodeSaveData as DialogueGraphStartNodeSaveData);
                        break;
                    case DialogueGraphEndNode _:
                        _selectSO.endNodesSaveData.Add(baseNodeSaveData as DialogueGraphEndNodeSaveData);
                        break;
                    case DialogueGraphTalkNode _:
                        _selectSO.talkNodesSaveData.Add(baseNodeSaveData as DialogueGraphTalkNodeSaveData);
                        break;
                    case DialogueGraphEventNode _:
                        _selectSO.eventNodesSaveData.Add(baseNodeSaveData as DialogueGraphEventNodeSaveData);
                        break;
                    case DialogueGraphConditionNode _:
                        _selectSO.conditionNodesSaveData.Add(baseNodeSaveData as DialogueGraphConditionNodeSaveData);
                        break;
                    case DialogueGraphChoiceNode _:
                        _selectSO.choiceNodesSaveData.Add(baseNodeSaveData as DialogueGraphChoiceNodeSaveData);
                        break;
                }
            }
            
            // 构建节点连线数组
            foreach (Edge edge in graphView.edges.Where(edge => edge.input != null))
            {
                _selectSO.edgesSaveData.Add(new DialogueGraphEdgeSaveData
                {
                    OutputNodeUniqueID = ((DialogueGraphBaseNode) edge.output.node).UniqueID,
                    InputNodeUniqueID = ((DialogueGraphBaseNode) edge.input.node).UniqueID
                });
            }

            // 脏标记
            EditorUtility.SetDirty(_selectSO);
        }

        /// <summary>
        /// 加载对话节点
        /// </summary>
        /// <param name="_selectSO">选中的SO</param>
        /// <param name="graphView">目标对话图</param>
        public static void Load(this DialogueGraphSO _selectSO, DialogueGraphView graphView)
        {
            // 清空
            graphView.ClearEdgesAndNodes();

            var nodeDataDic = _selectSO.GetCachedDictionary();

            void LoadNode<T>(IEnumerable<DialogueGraphBaseNodeSaveData> nodesSaveData) where T : DialogueGraphBaseNode
            {
                foreach (DialogueGraphBaseNodeSaveData dialogueGraphBaseNodeSaveData in nodesSaveData)
                {
                    // 生成节点
                    T dialogueNode = (T) Activator.CreateInstance(typeof(T),
                        dialogueGraphBaseNodeSaveData.RectPos.position, graphView, dialogueGraphBaseNodeSaveData);
                    // 加载节点数据
                    dialogueNode.LoadNodeData(dialogueGraphBaseNodeSaveData);
                    graphView.AddElement(dialogueNode);
                }
            }

            LoadNode<DialogueGraphStartNode>(_selectSO.startNodesSaveData);
            LoadNode<DialogueGraphEndNode>(_selectSO.endNodesSaveData);
            LoadNode<DialogueGraphTalkNode>(_selectSO.talkNodesSaveData);
            LoadNode<DialogueGraphConditionNode>(_selectSO.conditionNodesSaveData);
            LoadNode<DialogueGraphEventNode>(_selectSO.eventNodesSaveData);
            LoadNode<DialogueGraphChoiceNode>(_selectSO.choiceNodesSaveData);

            // originNodeUniqueID - List<originPortIndex, targetNodeUniqueID, targetPortIndex>
            var edgeDataDic = new Dictionary<string, List<Tuple<int, string, int>>>();
            
            foreach (DialogueGraphEdgeSaveData edgeSaveData in _selectSO.edgesSaveData)
            {
                string originNodeUniqueID = edgeSaveData.OutputNodeUniqueID;
                string targetNodeUniqueID = edgeSaveData.InputNodeUniqueID;
                int outputPortIndex = nodeDataDic.GetOutputPortIndex(originNodeUniqueID, targetNodeUniqueID);
                int inputPortIndex = nodeDataDic.GetInputPortIndex(targetNodeUniqueID, originNodeUniqueID);
                // 缓存连线数据进字典中
                if (edgeDataDic.ContainsKey(originNodeUniqueID))
                {
                    edgeDataDic[originNodeUniqueID]
                        .Add(new Tuple<int, string, int>(outputPortIndex, targetNodeUniqueID, inputPortIndex));
                }
                else
                {
                    edgeDataDic.Add(originNodeUniqueID, new List<Tuple<int, string, int>>
                    {
                        new Tuple<int, string, int>(outputPortIndex, targetNodeUniqueID, inputPortIndex)
                    });
                }
            }

            // 连线
            List<DialogueGraphBaseNode> baseNodes = graphView.nodes.Cast<DialogueGraphBaseNode>().ToList();
            foreach (var originNode in baseNodes.Where(originNode => edgeDataDic.ContainsKey(originNode.UniqueID)))
            {
                foreach (var (originPortIndex, targetNodeUniqueID, targetPortIndex) in edgeDataDic[
                    originNode.UniqueID])
                {
                    DialogueGraphBaseNode targetNode =
                        baseNodes.FirstOrDefault(node => node.UniqueID == targetNodeUniqueID);
                    if (targetNode == null) continue;
                    Edge edge = originNode.OutPutBasePorts.ToList()[originPortIndex]
                        .ConnectTo(targetNode.InputBasePorts[targetPortIndex]);
                    graphView.AddElement(edge);
                }
            }
        }

        /// <summary>
        /// 在Unity自带的Port类型与自己创建的Port类型进行转换
        /// </summary>
        /// <param name="editorCapacity">Unity自带类型</param>
        /// <returns>自己创建类型</returns>
        /// <exception cref="Exception">类型不匹配异常</exception>
        public static DialogueGraphPortSaveData.PortCapacity SwitchType(this Port.Capacity editorCapacity)
        {
            return editorCapacity switch
            {
                Port.Capacity.Multi => DialogueGraphPortSaveData.PortCapacity.Multi,
                Port.Capacity.Single => DialogueGraphPortSaveData.PortCapacity.Single,
                _ => throw new Exception("Port Type Switch Failed")
            };
        }

        /// <summary>
        /// 在自己创建的Port类型和Unity自带的Port类型进行转换
        /// </summary>
        /// <param name="myCapacity">自己创建类型</param>
        /// <returns>Unity自带类型</returns>
        /// <exception cref="Exception">类型不匹配异常</exception>
        public static Port.Capacity SwitchType(this DialogueGraphPortSaveData.PortCapacity myCapacity)
        {
            return myCapacity switch
            {
                DialogueGraphPortSaveData.PortCapacity.Multi => Port.Capacity.Multi,
                DialogueGraphPortSaveData.PortCapacity.Single => Port.Capacity.Single,
                _ => throw new Exception("Port Type Switch Failed")
            };
        }

        public static DialogueGraphBaseNode GetDialogueNode(this Port port)
        {
            return port.node as DialogueGraphBaseNode;
        }
    }
}