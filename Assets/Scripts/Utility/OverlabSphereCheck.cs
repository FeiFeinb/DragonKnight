using UnityEngine;
using UnityEditor;
namespace RPG.Utility
{
    public class OverlabSphereCheck : MonoBehaviour
    {
        public bool IsCollide => isCollide;
        [SerializeField, DisplayOnly] private bool isCollide;
        [SerializeField] private Vector3 localCenter;
        [SerializeField] private float radius;
        [SerializeField] private LayerMask collideLayer;
        [SerializeField] private Color gizmosColor;
        private Collider[] results = new Collider[5];
        private void Update()
        {
            int resultsValue = Physics.OverlapSphereNonAlloc(transform.position, radius, results, collideLayer.value);
            isCollide = resultsValue > 0;
        }
        private void OnDrawGizmosSelected()
        {

        }
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 0, 0, 0.2f);
            Gizmos.DrawWireSphere(transform.position, radius);
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
