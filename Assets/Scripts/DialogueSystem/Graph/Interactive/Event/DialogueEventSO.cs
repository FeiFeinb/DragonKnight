using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    public abstract class DialogueEventSO : ScriptableObject
    {
        public virtual void Init(GameObject obj) {}
        
        public abstract void RaiseEvent();
    }
}