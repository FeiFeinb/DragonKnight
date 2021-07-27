using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace RPG.Utility
{
    /// <summary>
    /// 更新式圆形检测
    /// </summary>
    public class UpdateOverlapSphereCheck : BaseOverlapCheck
    {
        [SerializeField] private float _radius; // 判断半径

        [SerializeField] private List<Collider> _previousFrameColliders = new List<Collider>();

        [SerializeField] private List<Collider> _result = new List<Collider>();
        
        [SerializeField] private Collider[] currentFrameColliders = new Collider[10];
        
        public Action<Collider> onLossCollider;
        
        public Action<Collider> onAddCollider;

        private void Update()
        {
            Vector3 checkVec = transform.position + new Vector3(0, _heightOffSet, 0);
            var size = Physics.OverlapSphereNonAlloc(checkVec, _radius, currentFrameColliders, _collideLayer.value);
            foreach (var lossCollider in _previousFrameColliders.Except(currentFrameColliders))
            {
                onLossCollider?.Invoke(lossCollider);
                _result.Remove(lossCollider);
            }

            foreach (var addCollider in currentFrameColliders.Where(c => c != null).Except(_previousFrameColliders))
            {
                onAddCollider?.Invoke(addCollider);
                _result.Add(addCollider);
            }

            _previousFrameColliders = currentFrameColliders.Where(c => c != null).ToList();
            Array.Clear(currentFrameColliders, 0, currentFrameColliders.Length);
        }

#if UNITY_EDITOR
        protected override void OnDrawGizmosSelected()
        {
            if (!_isDrawLine) return;
            base.OnDrawGizmosSelected();
            Vector3 checkVec = transform.position + new Vector3(0, _heightOffSet, 0);
            Gizmos.DrawSphere(checkVec, _radius);
        }
#endif
    }
}