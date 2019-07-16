//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2018年08月02日-01:58
//Icarus.UnityGameFramework.Bolt

using System;
using System.Collections.Generic;
using Bolt;
using CabinIcarus.BoltExtensions.Event;
using Ludiq;
using UnityEngine;

namespace CabinIcarus.BoltExtensions.Units
{
//    [UnitCategory("Icarus/Util/Events")]
    [UnitCategory("Events")]
    [UnitTitle("NewCustomEvent")]
    [TypeIcon(typeof(CustomEvent))]
    [UnitOrder(0)]
    public class NewCustomEventUnit : GameObjectEventUnit<CustomEventArgs>, IEventBaseUnit
    {
        [DoNotSerialize]
        [Inspectable, UnitHeaderInspectable("TableAsset")]
        public EventTableScriptableObject EventTableAsset { get; private set; }

        [Serialize]
        [Inspectable, UnitHeaderInspectable("Events")]
        public EventTable EventTable { get; private set; }

        [DoNotSerialize]
        public ValueInput EventId { get; private set; }
        
        [DoNotSerialize]
        [PortLabel("Event Name")]
        public ValueInput EventName { get; private set; }

        private static readonly string NewhookName = $"New {EventHooks.Custom}";
        protected override string hookName => NewhookName;

        [DoNotSerialize]
        public List<ValueOutput> argumentPorts { get; } = new List<ValueOutput>();

        protected override void Definition()
        {
            base.Definition();

            EventName = ValueInput(nameof(EventName), string.Empty);

            argumentPorts.Clear();
            
            if (EventTable != null)
            {
                EventTable.SelectEvent = EventTable.GetEvent(EventTable.SelectEvent.EventID);
                
                for (var i = 0; i < EventTable.SelectEvent.Args.Count; i++)
                {
                    var arg = EventTable.SelectEvent.Args[i];
                    var argName = arg.ArgName;
                    if (string.IsNullOrWhiteSpace(argName))
                    {
                        argName = $"argument_{i}";
                    }

                    argumentPorts.Add(ValueOutput(arg.ArgType, argName));
                }
            }
        }

        protected override bool ShouldTrigger(Flow flow, CustomEventArgs args)
        {
            return CompareNames(flow, EventName, args.name);
        }

        protected override void AssignArguments(Flow flow, CustomEventArgs args)
        {
            for (var i = 0; i < EventTable.SelectEvent.Args.Count; i++)
            {
                flow.SetValue(argumentPorts[i], args.arguments[i]);
            }
        }

        public static void Trigger(GameObject target, string name, params object[] args)
        {
            EventBus.Trigger(NewhookName, target, new CustomEventArgs(name, args));
        }
    }
}