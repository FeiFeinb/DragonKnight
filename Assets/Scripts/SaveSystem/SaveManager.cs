using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;
using UnityEngine;
using RPG.Module;
namespace RPG.SaveSystem
{
    public class SaveManager : BaseSingletonWithMono<SaveManager>
    {
        public int maxSaveFileNum = 6;
        [SerializeField] private string saveName = "MySave";
        [SerializeField] private string saveSuffix = ".save";
        public Dictionary<string, object>[] LoadSaveDics()
        {
            string savePath = GetSavePath();
            // 若不存在存档文件 则新创建字典数组
            if (!File.Exists(savePath))
            {
                return new Dictionary<string, object>[maxSaveFileNum];
            }
            // 存在存档文件 进行读取并反序列化
            IFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Open(savePath, FileMode.Open);
            Dictionary<string, object>[] _saveDics = formatter.Deserialize(fileStream) as Dictionary<string, object>[];
            fileStream.Close();
            return _saveDics;
        }
        public void Save(int _saveIndex, Action<string> saveCallBack)
        {
            // 控制台输出
            string savePath = GetSavePath();
            UnityEngine.Debug.Log($"Save At {savePath} of {_saveIndex}");

            var _saveDics = LoadSaveDics();
            // 将数据写入字典中
            _saveDics[_saveIndex] = CaptureStates();
            // 写入时间信息
            string timeStr = System.DateTime.Now.ToString();
            _saveDics[_saveIndex]["saveTime"] = timeStr;

            // 写入文件
            WriteFile(savePath, _saveDics);
            // 回调
            saveCallBack?.Invoke(timeStr);
        }

        public void Load(int _saveIndex)
        {
            // 控制台输出
            string savePath = GetSavePath();
            UnityEngine.Debug.Log($"Load At {savePath} of {_saveIndex}");

            var _saveDics = LoadSaveDics();
            if (_saveDics[_saveIndex] == null)
            {
                UnityEngine.Debug.LogError("读取的存档为空");
            }
            RestoreStates(_saveDics[_saveIndex]);
        }
        public void Delete(int _saveIndex, Action deleteCallBack)
        {
            // 控制台输出
            string savePath = GetSavePath();
            UnityEngine.Debug.Log($"Delete At {savePath} of {_saveIndex}");

            var _saveDics = LoadSaveDics();
            // 清空记录
            _saveDics[_saveIndex] = null;
            // 写入文件
            WriteFile(savePath, _saveDics);
            // 回调
            deleteCallBack?.Invoke();
        }
        private void WriteFile(string savePath, Dictionary<string, object>[] _saveDics)
        {
            // 将整个字典写入到文件中
            IFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Open(savePath, FileMode.Create);
            formatter.Serialize(fileStream, _saveDics);
            fileStream.Close();
        }

        /// <summary>
        /// 捕获存档数据
        /// </summary>
        /// <returns>存档数据</returns>
        private Dictionary<string, object> CaptureStates()
        {
            Dictionary<string, object> _saveDic = new Dictionary<string, object>();
            // 寻找场景中的SaveEntity
            foreach (SaveEntity saveEntity in FindSaveEntity())
            {
                // 若存在则修改其Value 若不存在则创建新Pair
                _saveDic[saveEntity.GetHashCode().ToString()] = saveEntity.CaptureState();
            }
            return _saveDic;
        }
        /// <summary>
        /// 加载存档数据
        /// </summary>
        /// <param name="saveDic">存档数据</param>
        private void RestoreStates(Dictionary<string, object> saveDic)
        {
            // 寻找场景中的SaveEntity
            foreach (SaveEntity saveEntity in FindSaveEntity())
            {
                // 检查键值是否匹配
                string entityStr = saveEntity.GetHashCode().ToString();
                if (saveDic.ContainsKey(entityStr))
                {
                    // TODO: 检测是应一次性全部Reset 还是应该单个Reset然后Restore
                    saveEntity.ResetStates();
                    saveEntity.RestoreState(saveDic[entityStr]);
                }
            }
        }

        private string GetSavePath()
        {
            // 获取保存路径
            return string.Concat(Path.Combine(Application.persistentDataPath, saveName), saveSuffix);
        }
        private SaveEntity[] FindSaveEntity()
        {
            // 获取可保存实体
            return FindObjectsOfType<SaveEntity>();
        }
    }
}