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
        public static List<DialogueGraphPortSaveData> ToPortData(this IEnumerable<Port> basePorts, DialogueGraphView graphView)
        {
            // foreach (var basePort in basePorts)
            // {
            //     if (!basePort.connected) continue;
            //     Edge connectedEdge = graphView.edges.FirstOrDefault(edge => edge.output == basePort || edge.input == basePort);
            // }
            // return null;
            return basePorts.Select(basePort =>
                {
                    var connectedEdge = graphView.edges.Where(edge => edge.output == basePort || edge.input == basePort).ToList();
                    return new DialogueGraphPortSaveData()
                    {
                        _portName = basePort.portName,
                        _capacity = basePort.capacity,
                        edgesSaveData = connectedEdge.Select(edge => new DialogueGraphEdgeSaveData()
                        {
                            inputNodeUniqueID = (edge.input.node as DialogueGraphBaseNode)?.UniqueID,
                            outputNodeUniqueID = (edge.output.node as DialogueGraphBaseNode)?.UniqueID
                        }).ToList(),
                    };
                }
            ).ToList();
        }
    }
}