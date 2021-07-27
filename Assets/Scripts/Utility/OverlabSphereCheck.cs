using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
namespace RPG.Utility
{
    public class OverlabSphereCheck : BaseOverlabCheck
    {
        [SerializeField] private float radius;  // 判断半径

        [SerializeField] private List<Collider> previousFrameColliders = new List<Collider>();

        [SerializeField] private List<Collider> currentResult = new List<Collider>();

        public Action<Collider> onLossCollider;
        public Action<Collider> onAddCollider;
        private void Update()
        {
            Vector3 checkVec = transform.position + new Vector3(0, _heightOffSet, 0);
            var currentFrameColliders = Physics.OverlapSphere(checkVec, radius, _collideLayer.value);
            foreach (var lossCollider in previousFrameColliders.Except(currentFrameColliders))
            {
                onLossCollider?.Invoke(lossCollider);
                currentResult.Remove(lossCollider);
            }

            foreach (var addCollider in currentFrameColliders.Except(previousFrameColliders))
            {
                onAddCollider?.Invoke(addCollider);
                currentResult.Add(addCollider);
            }

            previousFrameColliders = currentFrameColliders.ToList();
        }

#if UNITY_EDITOR
        protected override void OnDrawGizmosSelected()
        {
            if (!_isDrawLine) return;
            base.OnDrawGizmosSelected();
            Vector3 checkVec = transform.position + new Vector3(0, _heightOffSet, 0);
            Gizmos.DrawSphere(checkVec, radius);
        }
        #endif
    }
}
