using System;
using System.Collections.Generic;
using RPG.Module;

namespace UnityTemplateProjects.Test
{
    public class TestDialogueListenerCenter : BaseSingletonWithMono<TestDialogueListenerCenter>
    {

        private Dictionary<string, Action> test = new Dictionary<string, Action>();
        
        
        
        public void AddListener(string matchID, Action callBack)
        {
            test.Add(matchID, callBack);
        }
        
        public void Raise(string matchID)
        {
            test[matchID].Invoke();
        }
    }
}