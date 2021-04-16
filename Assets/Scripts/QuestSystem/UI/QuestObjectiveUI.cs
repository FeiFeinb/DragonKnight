using System.Text;
using UnityEngine;
using UnityEngine.UI;
namespace RPG.QuestSystem
{
    public class QuestObjectiveUI : MonoBehaviour
    {
        [SerializeField] private Text descriptionText;  // 任务描述
        [SerializeField] private Text progressText;     // 任务进度

        public void SetQuestObjectiveSidebar(int questProgress, int objectiveTarget, string questDescription)
        {
            // 设置任务描述
            descriptionText.text = questDescription;
            // 设置任务进度
            progressText.text = ProgressTextConcat(questProgress.ToString(), objectiveTarget.ToString());
        }
        public void SetQuestObjectiveSidebar(string questDescription)
        {
            // 设置任务描述
            descriptionText.text = questDescription;
        }
        private string ProgressTextConcat(string progressStr, string targetStr)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(progressStr);
            builder.Append(" / ");
            builder.Append(targetStr);
            return builder.ToString();
        }
    }
}