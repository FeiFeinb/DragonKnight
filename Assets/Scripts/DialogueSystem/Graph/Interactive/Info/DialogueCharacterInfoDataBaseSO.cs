using System.Collections.Generic;
using RPG.Module;
using UnityEngine;

namespace RPG.DialogueSystem.Graph
{
    /// <summary>
    /// 可对话NPC数据库
    /// </summary>
    [CreateAssetMenu(fileName = "New DialogueCharacterInfoDataBaseSO", menuName = "Dialogue System/DialogueCharacterInfoDataBaseSO")]
    public class DialogueCharacterInfoDataBaseSO : DataBaseSO
    {
        public DialogueCharacterInfoSO[] characterInfos;        // 可对话NPC人物信息
        
        public readonly Dictionary<DialogueCharacterInfoSO, GameObject> sceneCharacterInfoDic = new Dictionary<DialogueCharacterInfoSO, GameObject>();  // 场景中NPC-GameObject字典

        /// <summary>
        /// 更新数据库
        /// </summary>
        public override void InitAndLoad()
        {
            // 从场景中查找对话的人物并记录
            // TODO: RunTime的数据库和Static的数据库
            foreach (var sceneNPC in FindObjectsOfType<DialogueNPC>())
            {
                sceneCharacterInfoDic.Add(sceneNPC.npcInfo, sceneNPC.gameObject);
            }
        }

        public void GenerateCharacterID()
        {
            // 仓库序列化时赋值ID
            for (int i = 0; i < characterInfos.Length; i++)
            {
                characterInfos[i].ID = i;
            }
        }
    }
}