using System.Runtime.CompilerServices;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.QuestSystem;
using RPG.Entity;
using RPG.InventorySystem;
public class TestGuard : BaseEntity
{
    public void StartQuest(Quest _quest)
    {
        PlayerQuestManager.Instance.AddQuest(_quest);
    }
    public void SubmitQuest(Quest _quest)
    {
        // TODO: 使任务UI显示奖励物品
        // TODO: 完成与NPC对话AddItem
        Debug.Log(string.Concat("完成了任务", _quest.questTitle));
        // TODO: 创建物品生成类
        for (int i = 0; i < _quest.questReward.itemObjAmount; i++)
        {
            ItemData newItem = new ItemData(_quest.questReward.itemObj);
            newItem.itemBuffs.RenerateValues();
            if (!PlayerInventoryManager.Instance.inventoryObject.AddItem(newItem, 1))
            {
                Debug.Log("背包已满");
            }
        }
        // 任务提交完成 移除任务
        PlayerQuestManager.Instance.RemoveQuest(_quest);
    }
}
