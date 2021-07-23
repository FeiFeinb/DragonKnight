using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace RPG.Utility
{
    public class BaseOverlabCheck : MonoBehaviour
    {
        public bool IsCollide => isCollide;                         // 外部获取
        
        [SerializeField, DisplayOnly] protected bool isCollide;     // 是否碰撞
        [SerializeField] protected float heightOffSet;              // 高度偏移
        [SerializeField] protected LayerMask collideLayer;          // 碰撞检测层
        [SerializeField] protected Color gizmosColor;               // 外框颜色
        protected readonly Collider[] results = new Collider[5];    // 碰撞体暂存数组
        
        # if UNITY_EDITOR
        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = gizmosColor;
            Handles.color = gizmosColor;
        }
        #endif
    }
}

