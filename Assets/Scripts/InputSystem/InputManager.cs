using System;
using System.Collections.Generic;
using System.IO;
using Cinemachine;
using RPG.Module;
using UnityEngine;
using LitJson;
using RPG.UI;
using UnityEngine.SceneManagement;

namespace RPG.InputSystyem
{
    public class InputStrTuple
    {
        public InputStrTuple() {}

        public InputStrTuple(string newKeyTypeStr, string newFirstKeyCodeStr, string newSecondKeyCodeStr)
        {
            keyTypeStr = newKeyTypeStr;
            firstKeyCodeStr = newFirstKeyCodeStr;
            secondKeyCodeStr = newSecondKeyCodeStr;
        }
        
        public string keyTypeStr;
        public string firstKeyCodeStr;
        public string secondKeyCodeStr;
    }
    
    
    public class InputManager : BaseSingletonWithMono<InputManager>
    {
        public InputData inputData = new InputData();

        [SerializeField] private CinemachineFreeLook _freeLook;

        [SerializeField] private string _saveDirectory = "PlayerData/";
        [SerializeField] private string _dataFileName = "PlayerKey.json";
        [SerializeField] private string _defaultDataFileName = "DefaultPlayerKey.json";

        public void LoadPlayerJsonToInputData()
        {
            string path = Path.Combine(Application.dataPath, string.Concat(_saveDirectory, _dataFileName));
            if (!File.Exists(path))
            {
                Debug.Log("未找到PlayerKey的Json文件");
                LoadDefaultJsonToInputData();
                return;
            }

            Debug.Log("读取PlayerKey的Json文件");
            LoadJson(path);
        }

        public void LoadDefaultJsonToInputData()
        {
            string path = Path.Combine(Application.dataPath, string.Concat(_saveDirectory, _defaultDataFileName));
            if (!File.Exists(path))
            {
                throw new Exception("未找到键位Default配置文件");
            }

            Debug.Log("读取DefaultJson文件");
            LoadJson(path);
        }

        private void LoadJson(string jsonPath)
        {
            string dataStr = File.ReadAllText(jsonPath);
            // 同步到InputData
            inputData.LoadKeyData(JsonMapper.ToObject<Dictionary<string, InputStrTuple>>(dataStr));
            // TODO: 加载其他类型的键位
        }

        /// <summary>
        /// 写入Default配置文件
        /// </summary>
        /// <param name="data">默认的键位设置</param>
        public void WriteDefaultJsonFromInputData(Dictionary<string, InputStrTuple> data)
        {
            // 从UI上读取信息直接写入Json
            WriteJson(_saveDirectory, _defaultDataFileName, data);
        }

        /// <summary>
        /// 将玩家的键位设置写入Json中
        /// </summary>
        public void WritePlayerJsonFromInputData()
        {
            WriteJson(_saveDirectory, _dataFileName, inputData.GetKeyData());
        }

        private void WriteJson(string saveDirectory, string dataFileName, Dictionary<string, InputStrTuple> data)
        {
            string path = Path.Combine(Application.dataPath, string.Concat(saveDirectory, dataFileName));
            // 若文件夹不存在 则创建文件夹
            if (!Directory.Exists(Path.Combine(Application.dataPath, saveDirectory)))
            {
                Directory.CreateDirectory(Path.Combine(Application.dataPath, saveDirectory));
            }

            FileStream fileStream = File.Open(path, FileMode.Create);
            StreamWriter writer = new StreamWriter(fileStream);

            string jsonStr = JsonMapper.ToJson(data);
            writer.WriteLine(jsonStr);
            writer.Close();
        }

        private void Update()
        {
            inputData.UpdateKey();
        }

        public void CloseMouseInput()
        {
            if (_freeLook)
            {
                _freeLook.enabled = false;
            }
        }

        public void OpenMouseInput()
        {
            if (_freeLook)
            {
                _freeLook.enabled = true;
            }
        }

        public void SeekOrSetMainCamera(CinemachineFreeLook mainCamera = null)
        {
            if (mainCamera == null)
            {
                _freeLook = FindObjectOfType<CinemachineFreeLook>();
                if (_freeLook == null)
                {
                    Debug.Log($"场景: {SceneManager.GetActiveScene().name}中没有CinemachineFreeLook");
                }
            }
            else
            {
                _freeLook = mainCamera;
            }
        }
    }
}