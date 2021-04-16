using System.Collections.Generic;
using UnityEngine;
using RPG.Module;
namespace RPG.InventorySystem
{
    public class EquipmentCombiner
    {
        public Dictionary<int, EquipmentBindInfo> bindInfoDic = new Dictionary<int, EquipmentBindInfo>();   // 储存装备外观
        public Dictionary<int, Transform> rootBoneDic = new Dictionary<int, Transform>();                   // 储存主角骨骼节点
        public EquipmentCombiner(Transform loadObj, Transform _rootTrans)
        {
            // TODO: 抽象出装备外观资源管理类 在游戏开始时统一加载
            // ResourcesManager.Instance.LoadAsync<GameObject>(loadPath, (loadObj) => { InitBindInfo(loadObj.transform); });
            InitBindInfo(loadObj);
            InitRootBoneDic(_rootTrans);
        }
        public void Combine(Transform originTrans, EquipmentBindInfo bindInfo)
        {
            var skm = originTrans.GetComponent<SkinnedMeshRenderer>();
            skm.material = bindInfo.sharedMaterial;
            skm.rootBone = rootBoneDic[bindInfo.rootBoneName.GetHashCode()];
            skm.sharedMesh = bindInfo.sharedMesh;
            // 骨骼绑定
            Transform[] bones = new Transform[bindInfo.bonesName.Length];
            for (int i = 0; i < bones.Length; i++)
            {
                bones[i] = rootBoneDic[bindInfo.bonesName[i].GetHashCode()];
            }
            skm.bones = bones;
        }
        private void InitBindInfo(Transform _bindTrans)
        {
            // 访问所有外观 不加载隐藏物体
            var skms = _bindTrans.GetComponentsInChildren<SkinnedMeshRenderer>(true);
            // 完成字典的填充
            foreach (var skm in skms)
            {
                // 填充装备外观数据库
                bindInfoDic.Add(skm.transform.name.GetHashCode(), new EquipmentBindInfo(skm));
            }
        }
        private void InitRootBoneDic(Transform _rootTrans)
        {
            // 逐层遍历Transform 储存所有骨骼节点
            foreach (Transform child in _rootTrans)
            {
                rootBoneDic.Add(child.name.GetHashCode(), child);
                InitRootBoneDic(child);
            }
        }
    }
}