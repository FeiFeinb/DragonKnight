using UnityEngine;
using UnityEditor;
namespace RPG.Utility
{
    public class OverlabSphereCheck : BaseOverlabCheck
    {
        [SerializeField] private float radius;  // 判断半径
        private void Update()
        {
            Vector3 checkVec = transform.position + new Vector3(0, heightOffSet, 0);
            int resultsValue = Physics.OverlapSphereNonAlloc(checkVec, radius, results, collideLayer.value);
            isCollide = resultsValue > 0;
        }
        #if UNITY_EDITOR
        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Vector3 checkVec = transform.position + new Vector3(0, heightOffSet, 0);
            Gizmos.DrawSphere(checkVec, radius);
        }
        #endif
    }
}
