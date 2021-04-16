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
        public ItemDataBaseObject itemDataBase;
        public DialogueCharacterInfoDataBaseObject characterInfoDataBase;
        private void Awake()
        {
            // 开始游戏时初始化各个数据库
            itemDataBase.UpdateDateBaseID();
            characterInfoDataBase.UpdateDateBaseID();
            DontDestroyOnLoad(gameObject);
        }
    }
}
