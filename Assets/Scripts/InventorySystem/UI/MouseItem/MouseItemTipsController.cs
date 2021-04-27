using System.Text;
using UnityEngine;
using RPG.UI;
using RPG.Module;
using UnityEngine.UI;
namespace RPG.InventorySystem
{
    public class MouseItemTipsController : ToolTipsController
    {
        [SerializeField, Tooltip("启用面板的事件延迟")] private float viewShowDelay = 1.5f;     // 目标计时时间
        private InventorySlot currentInventorySlot;   // 鼠标指向插槽
        private MouseItemTipsView mouseItemTipsView;// UI面板
        private float viewShowTimer;                // 面板显示计时器
        private bool isStartTimer;                  // 是否开启计时

        public static string storePath = "UIView/MouseItemTipsView";   // 路径
        public static MouseItemTipsController controller;
        public override void PreInit()
        {
            base.PreInit();
            MonoEvent.Instance.AddUpdateEvent(TimerCount);
        }
        public void OnEnter(InventorySlot _inventorySlot)
        {
            // 操作对象为非空插槽
            if (!_inventorySlot.isEmpty)
            {
                // 开始计时
                SetControllerState(true, _inventorySlot);
            }
        }
        public void OnEnter(BaseItemObject baseItemObject)
        {

        }
        public void OnExit(InventorySlot _inventorySlot)
        {
            // 操作对象为非空插槽
            if (!_inventorySlot.isEmpty)
            {
                // 结束计时
                SetControllerState(false, null);
                // 退出时立刻隐藏
                Hide();
            }
        }

        public void OnExit(BaseItemObject baseItemObject)
        {

        }
        private string ColorCodeSplicing(string colorStr, string str)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append($"<color={colorStr}>").Append(str).Append("</color>");
            return strBuilder.ToString();
        }
        private string AttributeText(ItemBuff[] itemBuff)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < itemBuff.Length; i++)
            {
                if (itemBuff[i].itemAttributeType == AttributeType.Armor)
                {
                    stringBuilder.Append(itemBuff[i].value).Append("点护甲");
                }
                else
                {
                    stringBuilder.Append(ColorCodeSplicing(ItemTextColor.GetAttributeColorStr(itemBuff[i].itemAttributeType), $"+{itemBuff[i].value} {itemBuff[i].itemAttributeType.ToChinese()}"));
                }
                if (i < itemBuff.Length - 1)
                {
                    stringBuilder.Append("\n");
                }
            }
            return stringBuilder.ToString();
        }

        private void SetControllerState(bool _isStartTimer, InventorySlot _inventorySlot)
        {
            isStartTimer = _isStartTimer;
            viewShowTimer = 0;
            currentInventorySlot = _inventorySlot;
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
            string colorStr = ItemTextColor.GetRareColorStr(currentInventorySlot.itemObject.itemType.itemRare);
            mouseItemTipsView.SetTipName(ColorCodeSplicing(colorStr, currentInventorySlot.itemObject.name));
            // 设置位置
            mouseItemTipsView.SetTipPosition(currentInventorySlot.itemObject.itemType.GetTypeString());
            // 设置装备属性
            string attributeStr = AttributeText(currentInventorySlot.slotData.itemData.itemBuffs);
            mouseItemTipsView.SetTipAttribute(attributeStr);
            // TODO: 设置装备需求

            // TODO: 设置装备售价
        }
    }
}