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
                        Capacity = basePort.capacity,
                        EdgesSaveData = connectedEdge.Select(edge => new DialogueGraphEdgeSaveData()
                        {
                            inputNodeUniqueID = (edge.input.node as DialogueGraphBaseNode)?.UniqueID,
                            outputNodeUniqueID = (edge.output.node as DialogueGraphBaseNode)?.UniqueID
                        }).ToList(),
                    };
                }
            ).ToList();
        }

        public static int GetInputPortIndex(this Dictionary<string, DialogueGraphBaseNodeSaveData> baseNodeDic,
            string originUniqueID, string targetUniqueID)
        {
            DialogueGraphBaseNodeSaveData baseNodeSaveData = baseNodeDic[originUniqueID];
            for (int i = 0; i < baseNodeSaveData.InputPortsData.Count; i++)
            {
                if (baseNodeSaveData.InputPortsData[i].EdgesSaveData.Any(dialogueGraphEdgeSaveData => dialogueGraphEdgeSaveData.outputNodeUniqueID == targetUniqueID))
                {
                    return i;
                }
            }

            return -1;
        }

        public static int GetOutputPortIndex(this Dictionary<string, DialogueGraphBaseNodeSaveData> baseNodeDic,
            string originUniqueID, string targetUniqueID)
        {
            DialogueGraphBaseNodeSaveData baseNodeSaveData = baseNodeDic[originUniqueID];
            for (int i = 0; i < baseNodeSaveData.OutputPortsData.Count; i++)
            {
                if (baseNodeSaveData.OutputPortsData[i].EdgesSaveData.Any(dialogueGraphEdgeSaveData => dialogueGraphEdgeSaveData.inputNodeUniqueID == targetUniqueID))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}