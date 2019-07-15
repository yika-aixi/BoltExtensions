//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2018年08月30日-01:35
//Icarus.UnityGameFramework.Bolt

using System;
using Ludiq;
using Ludiq.Bolt;

namespace CabinIcarus.BoltExtensions.Units
{
    [UnitCategory("Icarus/Util")]
    [UnitTitle("Get Now Date Time")]
    [UnitSubtitle("获取当前本地时间")]
    public class GetNowDate : IcUnit
    {
        [Serialize]
        [Inspectable, InspectorLabel("输出格式:")]
        public string _format = "yyyy/MM/dd HH:mm:ss";

        [DoNotSerialize]
        [UnitPortLabel("Result")]
        public ValueOutput _result;

        protected override void Definition()
        {
            base.Definition();
            _result = ValueOutput<string>(nameof(_result));
            Assignment(_enter, _result);
        }

        protected override ControlOutput Enter(Flow flow)
        {
            var date = DateTime.Now;

            var dateStr = date.ToString(_format);

            flow.SetValue(_result, dateStr);


            return _exit;
        }
    }
}