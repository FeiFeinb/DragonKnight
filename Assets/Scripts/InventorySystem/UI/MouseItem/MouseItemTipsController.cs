using System.Timers;
using System.Text;
using UnityEngine;
using RPG.UI;
using RPG.Module;
using UnityEngine.UI;
namespace RPG.InventorySystem
{
    public class ItemToolTipsContent
    {
        public ItemToolTipsContent(BaseItemObject _baseItemObject, ItemBuff[] _itemBuffs)
        {
            baseItemObject = _baseItemObject;
            itemBuffs = _itemBuffs;
        }
        public BaseItemObject baseItemObject;
        public ItemBuff[] itemBuffs;
    }
    public class MouseItemTipsController : ToolTipsController
    {
        public static string storePath = "UIView/MouseItemTipsView";   // 路径
        public static MouseItemTipsController controller;
        
        [SerializeField, Tooltip("启用面板的事件延迟")] private float viewShowDelay = 1.5f;     // 目标计时时间
        private ItemToolTipsContent content;
        private MouseItemTipsView mouseItemTipsView;        // UI面板
        private float viewShowTimer;                        // 面板显示计时器
        private bool isStartTimer;                          // 是否开启计时

        public override void PreInit()
        {
            base.PreInit();
            MonoEvent.Instance.AddUpdateEvent(TimerCount);
        }


        public void OnEnter(ItemToolTipsContent itemToolTipsContent)
        {
            SetControllerState(true, itemToolTipsContent);
        }
        public void OnExit(ItemToolTipsContent itemToolTipsContent)
        {
            // 结束计时
            SetControllerState(false, null);
            // 退出时立刻隐藏
            Hide();
        }


        
        private string AttributeText(ItemBuff[] itemBuff)
        {
            StringBuilder stringBuilder = new StringBuilder();
            // 设置属性值
            for (int i = 0; i < itemBuff.Length; i++)
            {
                // 判断显示数值还是数值范围
                // TODO: 判断此装备的属性对于玩家来说是增益还是减弱（绿字增幅红字减弱）
                string showValue = itemBuff[i].value <= 0 ? $"{itemBuff[i].minValue} - {itemBuff[i].maxValue}" : $"{itemBuff[i].value}";
                if (itemBuff[i].itemAttributeType == AttributeType.Armor)
                {
                    stringBuilder.Append(showValue).Append("点护甲");
                }
                else
                {
                    string colorStr = ItemTextColor.GetAttributeColorStr(itemBuff[i].itemAttributeType);
                    string itemAttributeStr = string.Concat(showValue, itemBuff[i].itemAttributeType.ToChinese());
                    stringBuilder.Append(ItemTextColor.ColorCodeSplicing(colorStr, itemAttributeStr));
                }
                // 在结尾处拼接换行符
                if (i < itemBuff.Length - 1)
                {
                    stringBuilder.Append("\n");
                }
            }
            return stringBuilder.ToString();
        }

        private void SetControllerState(bool _isStartTimer, ItemToolTipsContent _itemToolTipsContent)
        {
            isStartTimer = _isStartTimer;
            viewShowTimer = 0;
            content = _itemToolTipsContent;
        }
        private void TimerCount()
        {
            if (isStartTimer)
            {
                viewShowTimer += Time.deltaTime;
                // 时间达到标准则显示
                if (viewShowTimer >= viewShowDelay)
                {
                    // 开启面板
                    Show();
                }
            }
        }

        public override void UpdateToolTips(GameObject _toolTipsObj)
        {
            if (mouseItemTipsView == null)
            {
                mouseItemTipsView = _toolTipsObj.GetComponent<MouseItemTipsView>();
            }
            // 设置名称
            string colorStr = ItemTextColor.GetRareColorStr(content.baseItemObject.itemType.itemRare);
            mouseItemTipsView.SetTipName(ItemTextColor.ColorCodeSplicing(colorStr, content.baseItemObject.name));
            // 设置位置
            mouseItemTipsView.SetTipPosition(content.baseItemObject.itemType.GetTypeString());
            // 设置装备属性
            string attributeStr = AttributeText(content.itemBuffs);
            mouseItemTipsView.SetTipAttribute(attributeStr);
            // TODO: 设置装备需求

            // 设置装备售价
            mouseItemTipsView.SetTipSellPrice(content.baseItemObject.sellPrice.coinStr);
        }
    }
}