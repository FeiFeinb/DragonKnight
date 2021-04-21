using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.UI
{
    public abstract class BaseUIController : BaseUI, IUserInterfacePreInit
    {
        public virtual bool isActive
        {
            get
            {
                // 外部获取面板活跃状态
                return (gameObject == null) ? false : gameObject.activeSelf;
            }
        }
        public abstract void PreInit();
    }
}