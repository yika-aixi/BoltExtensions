//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2018年07月30日-05:25
//Icarus.UnityGameFramework.Editor

using System.Linq;
using CabinIcarus.BoltExtensions.Event;
using Ludiq;
using Ludiq.Bolt;
using UnityEngine;

[assembly: RegisterEditor(typeof(IEventBaseUnit), typeof(IEventBaseUnitEditor))]

namespace CabinIcarus.BoltExtensions.Event
{
    public class IEventBaseUnitEditor : UnitEditor
    {
        protected MemberAccessor TableScriptableObject => accessor[nameof(IEventBaseUnit.EventTableAsset)];
        protected MemberAccessor Table => accessor[nameof(IEventBaseUnit.EventTable)];

        protected MemberAccessor EventId => accessor[nameof(IEventBaseUnit.EventId)];

        protected MemberAccessor EventName => accessor[nameof(IEventBaseUnit.EventName)];

        public IEventBaseUnitEditor(Accessor accessor) : base(accessor)
        {
        }

        protected override void OnGUI(Rect position, GUIContent label)
        {
            base.OnGUI(position, label);

            BeginBlock(accessor, position);
            {
                if (Table.value == null)
                {
                    Table.value = new EventTable();
                    return;
                }
                var table = (EventTable)Table.value;

                if (TableScriptableObject.value != null)
                {
                    var tableAsset = ((EventTableScriptableObject)
                        TableScriptableObject.value);

                    EventEntity[] entities = new EventEntity[tableAsset.Table.Events.Count];
                    tableAsset.Table.Events.CopyTo(entities, 0);
                    table.Events = entities.ToList();
                    var tableAssetName = Table[nameof(EventTable.TableAssetName)];
                    tableAssetName.value = tableAsset.name;
                }

                if (EventId.value != null)
                {
                    var idInput = (ValueInput)EventId.value;

                    if (table.SelectEvent != null)
                    {
                        idInput.Default(table.SelectEvent.EventID);
                    }
                }

                if (EventName.value != null)
                {
                    var nameInput = (ValueInput)EventName.value;
                    if (table.SelectEvent != null)
                    {
                        nameInput.Default(table.SelectEvent.EventName);
                    }
                }
            }
            if (EndBlock())
            {
                accessor.RecordUndo();
            }
        }
    }
}