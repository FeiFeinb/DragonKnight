using System.Collections.Generic;
using RPG.Module;
using RPG.UI;
using UnityEngine;

namespace RPG.InputSystyem
{
    public class KeySettingView : BaseUIPanel
    {
        public RectTransform container;
        public List<KeySettingPairView> keySettingPairViews;
    }
}
