using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace RPG.Utility
{
    public class BaseOverlabCheck : MonoBehaviour
    {
        public Action<List<Collider>> onCollide;
        public bool isCollide => _isCollide;                         // 外部获取
        public Collider[] result => _results;
        
        [SerializeField, DisplayOnly] protected bool _isCollide;     // 是否碰撞
        [SerializeField] protected bool _isDrawLine;              // 是否绘制
        
        [SerializeField] protected float _heightOffSet;              // 高度偏移
        [SerializeField] protected LayerMask _collideLayer;          // 碰撞检测层
        [SerializeField] protected Color _gizmosColor;               // 外框颜色
        [SerializeField] protected Collider[] _results = new Collider[5];    // 碰撞体暂存数组
        # if UNITY_EDITOR
        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = _gizmosColor;
            Handles.color = _gizmosColor;
        }
        #endif
    }
}

