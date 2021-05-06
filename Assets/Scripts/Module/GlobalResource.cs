using System.Text;
using System.Collections.Generic;
using UnityEngine;
using RPG.InventorySystem;
using RPG.DialogueSystem;
using RPG.QuestSystem;
namespace RPG.Module
{
    public class GlobalResource : BaseSingletonWithMono<GlobalResource>
    {
        [Tooltip("物品数据库")] public ItemDataBaseSO itemDataBase;
        [Tooltip("NPC信息数据库")] public DialogueCharacterInfoDataBaseSO characterInfoDataBase;
        [Tooltip("任务数据库")] public QuestDataBaseSO questDataBaseSO;
        private void Awake()
        {
            // 开始游戏时初始化各个数据库
            itemDataBase.UpdateDateBaseID();
            characterInfoDataBase.UpdateDateBaseID();
            questDataBaseSO.UpdateDataBaseID();
            DontDestroyOnLoad(gameObject);
        }
    }
}
