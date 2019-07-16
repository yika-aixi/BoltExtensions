//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2018年07月29日-11:20
//Icarus.UnityGameFramework.Editor

using System.Collections.Generic;
using System.Linq;
using CabinIcarus.EditorFrame.Utils;
using Ludiq;
using UnityEditor;
using UnityEngine;
using EditorGUIUtility = UnityEditor.Experimental.Networking.PlayerConnection.EditorGUIUtility;

namespace CabinIcarus.BoltExtensions.Event
{
    [Inspector(typeof(EventTable))]
    public class EventableInspector : Inspector
    {
        public EventableInspector(Metadata metadata) : base(metadata)
        {
            _names = new List<string>();
            _ids = new List<int>();
        }

        protected override float GetHeight(float width, GUIContent label)
        {
            return 40;
        }

        protected Metadata Events => metadata["Events"];

        protected Metadata SelectEvent => metadata[nameof(EventTable.SelectEvent)];
        
        protected Metadata SelectEventOfAssetName => metadata[nameof(EventTable.SelectEventOfAssetName)];

        private List<string> _names;
        private List<int> _ids;
        private int _eventArg;
        private int _selectIndex = 0;
        private const string NoTable = "No Table";
        private const string NoEvent = "No Event";
        protected override void OnGUI(Rect position, GUIContent label)
        {
            var table = (EventTable) metadata.value;
            _names.Clear();
            _ids.Clear();
            _eventArg = 0;
            
            if (table == null)
            {
                return;
            }

            if (table.SelectEvent != null)
            {
                _eventArg = table.GetArgCount();
                
                if (table.Events == null)
                {
                    _names.Add(table.SelectEvent.EventName);
                    _ids.Add(table.SelectEvent.EventID);
                    _selectIndex = 0;
                }
                else
                {
                    var nowEvent = table.GetEvent(table.SelectEvent.EventID);
                    
                    _names.AddRange(table.Events.Select(x => x.EventName));
                    _ids.AddRange(table.Events.Select(x => x.EventID));
                    
                    if (nowEvent == null || table.SelectEventOfAssetName != table.TableAssetName)
                    {
                        _names.Insert(0, table.SelectEvent.EventName);
                        _ids.Insert(0, table.SelectEvent.EventID);
                        _selectIndex = 0;
                    }
                    else
                    {
                        _initIndex(table.SelectEvent.EventID);
                    }
                }

            }
            else if (table.Events != null)
            {
                _names.AddRange(table.Events.Select(x => x.EventName));
                _ids.AddRange(table.Events.Select(x => x.EventID));
                _selectIndex = -1;
            }
            else
            {
                _names.Add(NoTable);
                _ids.Add(0);
                _selectIndex = 0;
            }

            if (_names.Count == 0)
            {
                _names.Add(NoEvent);
                _ids.Add(0);
                _selectIndex = 0;
            }
            
            var popRect = new Rect(position.x, position.y, position.width, EditorStyles.popup.fixedHeight);
            
            EditorGUI.BeginChangeCheck();
            
            _selectIndex = EditorGUI.Popup(popRect, _selectIndex, _names.ToArray());

            var change = EditorGUI.EndChangeCheck();
            
            var labelGUIContent = new GUIContent($"Arg Count:{_eventArg}");

            var labelRect = new Rect(popRect.x,popRect.y + popRect.height, 200,20);
            
            EditorGUI.LabelField(labelRect,labelGUIContent);

            if (_selectIndex < 0 || 
                _names[_selectIndex] == NoTable ||
                _names[_selectIndex] == NoEvent)
            {
                return;
            }

            if (_selectIndex == 0)
            {
                //有事件表,但是没有选择过
                if (!string.IsNullOrEmpty(table.SelectEventOfAssetName) && 
                    table.SelectEventOfAssetName != table.TableAssetName && !change)
                {
                    return;
                }
            }
            
            //存在选择
            if (table.SelectEvent != null)
            {
                //资源表是一个
                if (table.SelectEventOfAssetName == table.TableAssetName)
                {
                    //id一样
                    if (_ids[_selectIndex] == table.SelectEvent.EventID)
                    {
                        if (_eventArg == table.SelectEvent.Args.Count)
                        {
                            var args = table.GetArgList();

                            bool isSkip = true;
                    
                            //对比参数列表,如果都一样那就跳过,否则更新
                            for (var i = 0; i < args.Count; i++)
                            {
                                if (args[i] != table.SelectEvent.Args[i])
                                {
                                    isSkip = false;
                                    
                                    break;
                                }
                            }

                            if (isSkip)
                            {
                                return;
                            } 
                        }
                    }
                }
            }

            var eventId = _ids[_selectIndex];

            BeginBlock(metadata, position);
            {
                if (table.Events != null)
                {
                    SelectEvent.value = table.GetEvent(eventId);
                }

                var selectEventName = SelectEvent[nameof(EventEntity.EventName)];
                var selectEventId = SelectEvent[nameof(EventEntity.EventID)];
                selectEventName.value = _names[_selectIndex];
                selectEventId.value = eventId;
                SelectEventOfAssetName.value = table.TableAssetName;

                GUI.changed = true;
            }
            if (EndBlock(metadata))
            {
                metadata.RecordUndo();
            }
        }

        private void _initIndex(int eventEventId)
        {
            for (var i = 0; i < _ids.Count; i++)
            {
                if (_ids[i] == eventEventId)
                {
                    _selectIndex = i;
                }
            }
        }
    }
}