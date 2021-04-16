using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using RPG.UI;
namespace RPG.SaveSystem
{
    public class SavingSlot : BaseUI, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField, DisplayOnly] private int slotIndex;        // 插槽序号
        [SerializeField] private Image backGround;                  // 插槽背景
        [SerializeField] private SavingOption savingOption;         // 设置页面
        [SerializeField] private Text dateText;                     // 插槽文字
        public void InitSavingSlot(int _slotIndex)
        {
            slotIndex = _slotIndex;
        }
        public void SetSlotFilled(string _timeStr)
        {
            SetDateText(_timeStr);
            SetBackGroundColor(SavingController.controller.fillColor);
            SetOptionState(true);
        }
        public void SetSlotEmpty()
        {
            SetDateText("空");
            SetBackGroundColor(SavingController.controller.defaultColor);
            SetOptionState(false);
        }
        private void SetBackGroundColor(Color _color)
        {
            // 设置插槽背景
            backGround.color = _color;
        }
        private void SetDateText(string _dateStr)
        {
            // 设置插槽文字显示
            dateText.text = _dateStr;
        }
        private void SetOptionState(bool _isFilled)
        {
            // 设置选择操作界面状态
            savingOption.SetCanLoad(_isFilled);
        }

        public void OnClickSave()
        {
            // 点击保存按钮
            SavingController.controller.OnSave(slotIndex);
        }
        public void OnClickLoad()
        {
            // 点击载入按钮
            SavingController.controller.OnLoad(slotIndex);
        }
        public void OnClickDelete()
        {
            // 点击删除按钮
            SavingController.controller.OnDelete(slotIndex);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            // 鼠标移进时显示
            savingOption.Show();
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            // 鼠标移出时隐藏
            savingOption.Hide();
        }
    }
}