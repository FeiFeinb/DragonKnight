using UnityEngine;
namespace RPG.QuestSystem
{
    [System.Serializable]
    public class KillQuestObjective : QuestObjective
    {
        public string EntityID => entityID;
        [SerializeField] private string entityID;
    }
}