using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.InventorySystem
{
    [System.Serializable]
    public class ModifiedInt
    {
        public int ModifiedValue => modifiedValue;
        public int BaseValue => baseValue;
        [SerializeField] private int baseValue;                         // 基础值
        [SerializeField, DisplayOnly] private int modifiedValue;        // 应用值
        private Action modifiedAction;                                  // 属性变化回调
        private List<IModifier> modifiers = new List<IModifier>();      // 构成某个属性的接口数列
        public void InitModifiedInt()
        {
            // 初始化
            modifiedValue = baseValue;
            modifiedAction?.Invoke();
        }
        public void ResetBaseValue(int _baseValue)
        {
            // TODO: 根据类型获取baseValue 然后应用到modifiedValue中
            // 不同角色会有不同的baseValue 在创建角色的时候 或 在读取存档的时候 会调用这个函数
            // 重置BaseValue 然后 再InitModifiedInt 进行属性的初始化
            baseValue = _baseValue;
        }
        public void AddModifier(IModifier _modifier)
        {
            // 属性增加
            _modifier.AddValue(ref modifiedValue);
            // 添加至数列中
            modifiers.Add(_modifier);
            // 调用回调
            modifiedAction?.Invoke();
        }
        public void RemoveModifier(IModifier _modifier)
        {
            // 属性减少
            _modifier.SubValue(ref modifiedValue);
            // 从数列中移出
            modifiers.Remove(_modifier);
            // 调用回调
            modifiedAction?.Invoke();
        }
        public void AddModifiedCallBack(Action _modifiedAction)
        {
            // 添加回调
            modifiedAction += _modifiedAction;
        }
        public void RemoveModifiedCallBack(Action _modifiedAction)
        {
            // 移出回调
            modifiedAction -= _modifiedAction;
        }
    }
}
