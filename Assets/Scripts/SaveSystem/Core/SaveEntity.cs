using System.Collections.Generic;
using UnityEngine;
namespace RPG.SaveSystem
{
    public class SaveEntity : MonoBehaviour
    {
        private Dictionary<string, ISaveable> iSaveableDic = new Dictionary<string, ISaveable>();   // 储存该类所在物体可供保存的信息
        private void Start()
        {
            // 对字典进行初始化
            var iSaveables = GetComponentsInChildren<ISaveable>();
            foreach (ISaveable iSaveable in iSaveables)
            {
                iSaveableDic.Add(iSaveable.GetHashCode().ToString(), iSaveable);
            }
        }
        public object CaptureState()
        {
            // 储存SaveEntity挂在的物体上所有需要保存的信息
            Dictionary<string, object> saveDic = new Dictionary<string, object>();
            foreach (var iSaveavlePair in iSaveableDic)
            {
                object newState = iSaveavlePair.Value.CreateState();
                if (newState != null)
                {
                    saveDic.Add(iSaveavlePair.Key, newState);
                }
            }
            return saveDic;
        }
        public void RestoreState(object stateInfo)
        {
            // 读取本物体上需要保存的信息
            Dictionary<string, object> saveDic = stateInfo as Dictionary<string, object>;
            if (saveDic == null)
            {
                Debug.LogError("Cant Restore State -- SaveEntity");
                return;
            }
            // 信息重新写入
            foreach (var iSaveablePair in iSaveableDic)
            {
                // 键值是否存在
                if (saveDic.ContainsKey(iSaveablePair.Key))
                {
                    iSaveablePair.Value.LoadState(saveDic[iSaveablePair.Key]);
                }
            }
        }
        public void ResetStates()
        {
            foreach (var iSaveablePair in iSaveableDic)
            {
                iSaveablePair.Value.ResetState();
            }
        }
    }
}