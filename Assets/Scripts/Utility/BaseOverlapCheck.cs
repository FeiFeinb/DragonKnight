using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RPG.Utility
{
    public class BaseOverlapCheck : MonoBehaviour
    {
        [SerializeField] protected bool _isDrawLine; // 是否绘制
        [SerializeField] protected float _heightOffSet; // 高度偏移
        [SerializeField] protected LayerMask _collideLayer; // 碰撞检测层
        [SerializeField] protected Color _gizmosColor; // 外框颜色
        
# if UNITY_EDITOR
        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = _gizmosColor;
            Handles.color = _gizmosColor;
        }
#endif
    }
}