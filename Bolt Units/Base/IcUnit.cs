//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2018年07月28日-06:23
//Icarus.UnityGameFramework.Bolt


using Ludiq;
using Ludiq.Bolt;

namespace CabinIcarus.BoltExtensions
{
    public abstract class IcUnit : Unit
    {
        [DoNotSerialize]
        [UnitPortLabelHidden]
        public ControlInput _enter;
        
        [DoNotSerialize]
        [UnitPortLabelHidden]
        public ControlOutput _exit;

        protected override void Definition()
        {
            _enter = ControlInput(nameof(_enter), Enter);
            _exit = ControlOutput(nameof(_exit));
            Succession(_enter,_exit);
        }

        protected abstract ControlOutput Enter(Flow flow);
    }
}