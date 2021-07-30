using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using RPG.Utility;

namespace RPG.Character
{
    [RequireComponent(typeof(JudgmentOverlapSectorCheck))]
    public class AimRigging : MonoBehaviour
    {
        [SerializeField] private float aimSpeed; // 朝向速度
        [SerializeField] private MultiAimConstraint multiAimConstraint; // 头部朝向组件
        [SerializeField] private JudgmentOverlapSectorCheck sectorCollidePlayerCheck; // 扇形区域判断

        private void Update()
        {
            int weightValue = sectorCollidePlayerCheck.isCollide ? 1 : 0;
            multiAimConstraint.weight = Mathf.Lerp(multiAimConstraint.weight, weightValue, aimSpeed * Time.deltaTime);
        }
    }
}