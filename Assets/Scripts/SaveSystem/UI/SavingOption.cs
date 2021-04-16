using UnityEngine;
using UnityEngine.UI;
using RPG.UI;
namespace RPG.SaveSystem
{
    public class SavingOption : BaseUI
    {
        [SerializeField] private Button saveButton;
        [SerializeField] private Button loadButton;
        [SerializeField] private Button deleteButton;
        public void SetCanLoad(bool _isFilled)
        {
            // 只有当存在存档时 它才可被加载或删除
            loadButton.gameObject.SetActive(_isFilled);
            deleteButton.gameObject.SetActive(_isFilled);
        }
    }
}