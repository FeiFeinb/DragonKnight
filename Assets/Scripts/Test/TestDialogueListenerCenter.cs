using System;
using System.Collections.Generic;
using RPG.DialogueSystem.Graph;
using RPG.Module;
using UnityEngine;

namespace UnityTemplateProjects.Test
{
    public class TestDialogueListenerCenter : BaseSingletonWithMono<TestDialogueListenerCenter>
    {
        private Dictionary<DialogueGraphSO, Action> dic = new Dictionary<DialogueGraphSO, Action>();

        public void AddListener(DialogueGraphSO matchSO, Action callBack)
        {
            if (dic.ContainsKey(matchSO))
            {
                dic[matchSO] += callBack;
            }
            else
            {
                dic.Add(matchSO, callBack);
            }
        }

        public void RemoveListener(DialogueGraphSO matchSO, Action callBack)
        {
            if (dic.ContainsKey(matchSO))
            {
                dic[matchSO] -= callBack;
                if (dic[matchSO] == null)
                {
                    dic.Remove(matchSO);
                }
            }
        }
        
        public void Raise(DialogueGraphSO matchSO)
        {
            dic[matchSO].Invoke();
        }
    }
}