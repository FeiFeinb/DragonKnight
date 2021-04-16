using UnityEngine;
namespace RPG.InventorySystem
{
    public class EquipmentBindInfo
    {
        public int rootBoneName;
        public int[] bonesName;
        public Mesh sharedMesh;
        public Material sharedMaterial;
        public EquipmentBindInfo(SkinnedMeshRenderer skinnedMeshRenderer)
        {
            // 网格
            sharedMesh = skinnedMeshRenderer.sharedMesh;
            // 材质
            sharedMaterial = skinnedMeshRenderer.sharedMaterial;
            // 根节点
            rootBoneName = skinnedMeshRenderer.rootBone.name.GetHashCode();
            // 骨骼
            bonesName = new int[skinnedMeshRenderer.bones.Length];
            for (int i = 0; i < skinnedMeshRenderer.bones.Length; i++)
            {
                bonesName[i] = skinnedMeshRenderer.bones[i].name.GetHashCode();
            }
        }
    }
}