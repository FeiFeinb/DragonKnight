using UnityEngine;

namespace RPG.DialogueSystem
{
    [CreateAssetMenu(fileName = "New DialogueEventSO", menuName = "Dialogue System/DialogueEventSO")]
    public abstract class DialogueEventSO : ScriptableObject
    {
        public virtual void Init() {}
        
        public abstract void OnEventTrigger();
    }
}