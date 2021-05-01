using UnityEngine;
namespace RPG.QuestSystem
{
    [System.Serializable]
    public class KillQuestObjective : QuestObjective
    {
        public string EntityID => entityID;
        [Tooltip("击杀实体ID"), SerializeField] private string entityID;
    }
}