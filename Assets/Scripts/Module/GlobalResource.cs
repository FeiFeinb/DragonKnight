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
        public ItemDataBaseSO itemDataBase;                             // 物品数据库
        public DialogueCharacterInfoDataBaseSO characterInfoDataBase;   // NPC信息数据库
        public QuestDataBaseSO questDataBaseSO;                         // 任务数据库
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
