using System;
using System.Collections.Generic;
using System.Linq;
using RPG.DialogueSystem.Graph;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueSystem.Editor.Graph
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
            return basePorts.Where(basePort => basePort.name == "realPort").Select(basePort =>
                {
                    var connectedEdge = graphView.edges.Where(edge => edge.output == basePort || edge.input == basePort)
                        .ToList();
                    return new DialogueGraphPortSaveData()
                    {
                        PortName = basePort.portName,
                        Capacity = basePort.capacity,
                        EdgesSaveData = connectedEdge.Select(edge => new DialogueGraphEdgeSaveData()
                        {
                            InputNodeUniqueID = (edge.input.node as DialogueGraphBaseNode)?.UniqueID,
                            OutputNodeUniqueID = (edge.output.node as DialogueGraphBaseNode)?.UniqueID
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
                if (baseNodeSaveData.InputPortsData[i].EdgesSaveData.Any(dialogueGraphEdgeSaveData => dialogueGraphEdgeSaveData.OutputNodeUniqueID == targetUniqueID))
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
                if (baseNodeSaveData.OutputPortsData[i].EdgesSaveData.Any(dialogueGraphEdgeSaveData => dialogueGraphEdgeSaveData.InputNodeUniqueID == targetUniqueID))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}