using System.Collections.Generic;
using DialogueSystem.Old.Dialogue.Core;
using UnityEngine;

namespace RPG.DialogueSystem
{
    /// <summary>
    /// 可对话NPC数据库
    /// </summary>
    [CreateAssetMenu(fileName = "New DialogueCharacterInfoDataBaseSO", menuName = "Dialogue System/DialogueCharacterInfoDataBaseSO")]
    public class DialogueCharacterInfoDataBaseSO : ScriptableObject
    {
        public DialogueCharacterInfoSO[] characterInfos;        // 可对话NPC人物信息
        public readonly Dictionary<DialogueCharacterInfoSO, GameObject> sceneCharacterInfoDic = new Dictionary<DialogueCharacterInfoSO, GameObject>();  // 场景中NPC-GameObject字典

        /// <summary>
        /// 更新数据库
        /// </summary>
        public void UpdateDateBase()
        {
            // 仓库序列化时赋值ID
            for (int i = 0; i < characterInfos.Length; i++)
            {
                characterInfos[i].ID = i;
            }
            // 序列化场景NPC记录字典
            foreach (var sceneNPC in FindObjectsOfType<DialogueNPC>())
            {
                sceneCharacterInfoDic.Add(sceneNPC.NPCInfo, sceneNPC.gameObject);
            }
        }
    }
}