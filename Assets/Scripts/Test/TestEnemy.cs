using System;
using System.ComponentModel.Design.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.QuestSystem;
using RPG.Entity;
using UnityEngine.Events;

public class TestEnemy : BaseEntity/*, IQuestListener*/
{
    private Action onDeath;
    private void Start()
    {
        onDeath += OnQuestTrigger;
    }
    private void OnQuestTrigger()
    {
        PlayerQuestManager.Instance.KillQuestTrigger(EntityID);
    }
    [ContextMenu("Death")]
    public void Death()
    {
        onDeath?.Invoke();
    }
}
