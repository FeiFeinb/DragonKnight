using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RPG.Utility
{
    /// <summary>
    /// 判断式扇形检测
    /// </summary>
    public class JudgmentOverlapSectorCheck : BaseOverlapCheck
    {
        public bool isCollide => _isCollide; // 外部获取

        [SerializeField] private float _angle; // 角度
        
        [SerializeField] private float _radius; // 半径
        
        [SerializeField, DisplayOnly] protected bool _isCollide; // 是否碰撞

        [SerializeField] private Collider[] _results = new Collider[5]; // 碰撞体暂存数组

        private void Update()
        {
            Transform trans = transform;
            Vector3 checkVec = trans.position + new Vector3(0, _heightOffSet, 0);
            int resultsValue = Physics.OverlapSphereNonAlloc(checkVec, _radius, _results, _collideLayer.value);
            if (resultsValue <= 0)
            {
                _isCollide = false;
                return;
            }

            _isCollide = Mathf.Abs(Vector3.Angle(_results[0].transform.position - trans.position, trans.forward)) < _angle;
        }

# if UNITY_EDITOR
        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Transform trans = transform;
            Vector3 checkVec = trans.position + new Vector3(0, _heightOffSet, 0);
            Handles.DrawSolidArc(checkVec, trans.up, trans.forward, _angle, _radius);
            Handles.DrawSolidArc(checkVec, trans.up, trans.forward, -_angle, _radius);
        }
#endif
    }
}