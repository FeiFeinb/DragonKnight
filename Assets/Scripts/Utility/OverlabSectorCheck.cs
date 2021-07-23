using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace RPG.Utility
{
    public class OverlabSectorCheck : BaseOverlabCheck
    {
        [SerializeField] private float angle;   // 角度
        [SerializeField] private float radius;  // 半径
        private void Update()
        {
            Vector3 checkVec = transform.position + new Vector3(0, heightOffSet, 0);
            int resultsValue = Physics.OverlapSphereNonAlloc(checkVec, radius, results, collideLayer.value);
            if (resultsValue <= 0)
            {
                isCollide = false;
                return;
            }
            isCollide = Mathf.Abs(Vector3.Angle(results[0].transform.position - transform.position, transform.forward)) < angle;
        }
        
        # if UNITY_EDITOR
        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Vector3 checkVec = transform.position + new Vector3(0, heightOffSet, 0);
            Handles.DrawSolidArc(checkVec, transform.up, transform.forward, angle, radius);
            Handles.DrawSolidArc(checkVec, transform.up, transform.forward, -angle, radius);
        }
        #endif
    }
}

