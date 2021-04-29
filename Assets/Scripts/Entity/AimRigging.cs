using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using RPG.Utility;
namespace RPG.Entity
{
    [RequireComponent(typeof(OverlabSphereCheck), typeof(OverlabSectorCheck))]
    public class AimRigging : MonoBehaviour
    {
        [SerializeField] private float aimSpeed;                // 朝向速度
        private MultiAimConstraint multiAimConstraint;          // 头部朝向组件
        private OverlabSectorCheck sectorCollidePlayerCheck;    // 扇形区域判断
        private void Start()
        {
            multiAimConstraint = GetComponent<MultiAimConstraint>();
            sectorCollidePlayerCheck = GetComponent<OverlabSectorCheck>();
        }
        private void Update()
        {
            int weightValue = sectorCollidePlayerCheck.IsCollide ? 1 : 0;
            multiAimConstraint.weight = Mathf.Lerp(multiAimConstraint.weight, weightValue, aimSpeed * Time.deltaTime);
        }
    }
}

