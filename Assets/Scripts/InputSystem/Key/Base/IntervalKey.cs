using System.Numerics;

namespace RPG.InputSystyem
{
    public abstract class IntervalKey : BaseKey
    {
        public float value => _value;

        /// <summary>
        /// 当前值
        /// </summary>
        protected float _value;

        /// <summary>
        /// 值增长速度
        /// </summary>
        protected float _increaseSpeed = 3.0f;

        /// <summary>
        /// 设置是否启用
        /// </summary>
        /// <param name="paramIsEnable">是否启用</param>
        public override void SetEnable(bool paramIsEnable)
        {
            base.SetEnable(paramIsEnable);
            _value = 0;
        }

        protected IntervalKey(KeyActionType keyActionType) : base(keyActionType)
        {
        }
    }
}