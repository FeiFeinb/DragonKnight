using System;
using RPG.Module;
using UnityEngine;

namespace RPG.InputSystyem
{
    public class InputManager : BaseSingletonWithMono<InputManager>
    {
        public InputData inputData = new InputData();

        private void Start()
        {
            
        }

        private void Update()
        {
            inputData.UpdateKey();
        }
    }
}