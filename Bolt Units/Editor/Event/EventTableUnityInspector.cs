//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2018年07月29日-07:57
//Icarus.UnityGameFramework.Bolt

using Ludiq;
using System;
using System.Collections.Generic;
using System.Linq;
using CabinIcarus.BoltExtensions.Utility;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Object = UnityEngine.Object;
using Type = System.Type;

namespace CabinIcarus.BoltExtensions.Event
{
    [CustomEditor(typeof(EventTableScriptableObject))]
    public class EventTableUnityInspector : Editor
    {
        private EventTableScriptableObject _tableAsset;
        private SerializedProperty _events;
        private string[] _names;
        private int[] _ids;

        private void OnEnable()
        {
            _tableAsset = (EventTableScriptableObject) target;
            _events = serializedObject.FindProperty("_events");
            _argNames = new List<string>();
        }

        private bool _addMode;
        private bool _autoSave;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            {
                var path = AssetDatabase.GetAssetOrScenePath(Selection.activeObject);
                if (oldEventTableUpdateTool.IsCanUpdate(path))
                {
                    if (GUILayout.Button("Update EventTable"))
                    {
                        if (!oldEventTableUpdateTool.Update(path, serializedObject))
                        {
                            throw new Exception("Update EventTable Failure");
                        }
                    }
                }

                serializedObject.Update();
                _names = _tableAsset.GetEventNames().ToArray();
                _ids = _tableAsset.GetEventIDs().ToArray();

                EditorGUILayout.LabelField($"Event Count:{_events.arraySize}");
                _autoSave = EditorGUILayout.Toggle("Auto Save", _autoSave);
                _addMode = EditorGUILayout.Toggle("Auto Add EventID", _addMode);

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUIUtility.labelWidth = 15f;
                    EditorGUILayout.LabelField("Event Name");
                    EditorGUILayout.LabelField("Event ID");
                    EditorGUILayout.LabelField("Event Args");
                    EditorGUIUtility.labelWidth = 0;
                }
                EditorGUILayout.EndHorizontal();
                _addEvent();
                _removeAll();

                _showEventTable();

                if (GUI.changed && _autoSave)
                {
                    AssetDatabase.SaveAssets();
                }
            }
            serializedObject.ApplyModifiedProperties();
        }

        protected void DrawUILine(Color color, int thickness = 2, int padding = 10)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            r.height = thickness;
            r.y += padding / 2;
            r.x -= 2;
            r.width += 6;
            EditorGUI.DrawRect(r, color);
        }

        private void _showEventTable()
        {
            for (var i = 0; i < _events.arraySize; i++)
            {
                EditorGUILayout.BeginVertical("box");
                {
                    var @event = _events.GetArrayElementAtIndex(i);
                    var eventName = @event.FindPropertyRelative("_eventName");
                    var eventID = @event.FindPropertyRelative("_eventId");
                    var eventArgs = @event.FindPropertyRelative("_args");

                    EditorGUILayout.LabelField($"{i}:{eventName.stringValue}");

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PropertyField(eventName, GUIContent.none);
                        EditorGUILayout.PropertyField(eventID, GUIContent.none);
                        var argCount = EditorGUILayout.IntField(eventArgs.arraySize);
                        if (GUI.changed)
                        {
                            eventArgs.arraySize = argCount;
                        }

                        if (GUILayout.Button("Remove"))
                        {
                            _events.DeleteArrayElementAtIndex(i);
                            return;
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    _showArgs(i, eventArgs);

                    if (i < _events.arraySize - 1)
                    {
                        DrawUILine(Color.black);
                    }
                }
                EditorGUILayout.EndVertical();
            }
        }

        private List<string> _argNames;

        private void _showArgs(int index, SerializedProperty eventArgs)
        {
            EditorGUI.indentLevel++;
            {
                if (!_foldout(index, eventArgs.arraySize))
                {
                    return;
                }

                _argNames.Clear();

                for (int i = 0; i < eventArgs.arraySize; i++)
                {
                    var arg = eventArgs.GetArrayElementAtIndex(i);
                    var argName = GetArfNamePropertye(arg);
                    var argTypeStr = arg.FindPropertyRelative("_argTypeStr");
                    var argDesc = arg.FindPropertyRelative("_argDesc");
                    var argNotNull = arg.FindPropertyRelative("_notNull");
                    var isDefault = arg.FindPropertyRelative("_isDefault");
                    var isDefaultUnitySer = arg.FindPropertyRelative("DefaultIsUntiySerialization");
                    
                    EditorGUI.indentLevel++;
                    {
                        if (i != 0)
                        {
                            DrawUILine(Color.white);
                        }

                        if (!_argNames.Exists(x => x == argName.stringValue))
                        {
                            _argNames.Add(argName.stringValue);
                        }
                        else
                        {
                            argName.stringValue = string.Empty;
                        }

                        EditorGUIUtility.labelWidth = 80f;
                        EditorGUILayout.PropertyField(argNotNull, new GUIContent("NotNull:"));
                        EditorGUIUtility.labelWidth = 60f;
                        EditorGUILayout.PropertyField(argName, new GUIContent("Name:"));
                        EditorGUILayout.BeginHorizontal();
                        {
                            GUIStyle fontStyle = new GUIStyle
                            {
                                normal = {textColor = Color.red},
                                fontSize = EditorStyles.label.fontSize
                            };

                            EditorGUILayout.LabelField($"Type:{argTypeStr.stringValue.Split(',').First()}", fontStyle);
                            _selectType(argTypeStr);
                        }
                        EditorGUILayout.EndHorizontal();

                        EditorGUIUtility.labelWidth = 80f;

                        if (!isDefaultUnitySer.boolValue)
                        {
                            Debug.LogWarning("不支持的序列化类型,请参考-> https://docs.unity3d.com/Manual/script-Serialization.html#ClassSerialized");
                            goto end;
                        }

                        EditorGUILayout.PropertyField(isDefault, new GUIContent("Is Default:"));

                        if (isDefault.boolValue)
                        {
                            _drawSetDefault(arg,Type.GetType(argTypeStr.stringValue));
                        }

                        end: 
                        EditorGUIUtility.labelWidth = 60f;
                        EditorGUILayout.PropertyField(argDesc, new GUIContent("Desc:"));
                    }
                    EditorGUI.indentLevel--;
                }
            }
            EditorGUI.indentLevel--;
        }

        private void _drawSetDefault(SerializedProperty arg, Type paraType)
        {
            SerializedProperty @default;
//            EditorGUILayout.PropertyField(@default, new GUIContent("Default:"));
            if (paraType == typeof(int))
            {
                @default = arg.FindPropertyRelative("_intValue");
                
                @default.intValue = EditorGUILayout.IntField("Default:", @default.intValue);
            }
            else if (paraType == typeof(string))
            {
                @default = arg.FindPropertyRelative("_stringValue");
                @default.stringValue = EditorGUILayout.TextField("Default:", @default.stringValue);
            }
            else if (paraType == typeof(float))
            {
                @default = arg.FindPropertyRelative("_floatValue");
                @default.floatValue = EditorGUILayout.FloatField("Default:", @default.floatValue);
            }
            else if (paraType == typeof(double))
            {
                @default = arg.FindPropertyRelative("_doubleValue");
                @default.doubleValue = EditorGUILayout.DoubleField("Default:", @default.doubleValue);
            }
            else if (paraType == typeof(bool))
            {
                @default = arg.FindPropertyRelative("_boolValue");
                @default.boolValue = EditorGUILayout.Toggle("Default:", @default.boolValue);
            }
            else if (paraType == typeof(long))
            {
                @default = arg.FindPropertyRelative("_longValue");
                @default.longValue = EditorGUILayout.LongField("Default:", @default.longValue);  
            }
            else if (paraType == typeof(Quaternion))
            {
                @default = arg.FindPropertyRelative("_quaternionValue");
                @default.quaternionValue = EditorGUILayout.Vector4Field("Default:", @default.quaternionValue.ToVect4()).ToQuaternion();  
            }
            else if (paraType == typeof(Object))
            {
                @default = arg.FindPropertyRelative("_objectValue");
                @default.objectReferenceValue =
                    EditorGUILayout.ObjectField("Default(Non Scene Object)", @default.objectReferenceValue, typeof(Object), false);
            }
            else if (paraType == typeof(Vector2))
            {
                @default = arg.FindPropertyRelative("_vector2Value");
                @default.vector2Value =
                    EditorGUILayout.Vector2Field("Default", @default.vector2Value);
            }
            else if (paraType == typeof(Vector3))
            {
                @default = arg.FindPropertyRelative("_vector3Value");
                @default.vector3Value =
                    EditorGUILayout.Vector3Field("Default", @default.vector3Value);
            }
            else if (paraType == typeof(Vector4))
            {
                @default = arg.FindPropertyRelative("_vector4Value");
                @default.vector4Value =
                    EditorGUILayout.Vector4Field("Default", @default.vector4Value);
            }
            else if (paraType == typeof(Vector2Int))
            {
                @default = arg.FindPropertyRelative("_vector2IntValue");
                @default.vector2IntValue =
                    EditorGUILayout.Vector2IntField("Default", @default.vector2IntValue);
            }
            else if (paraType == typeof(Vector3Int))
            {
                @default = arg.FindPropertyRelative("_vector3IntValue");
                @default.vector3IntValue =
                    EditorGUILayout.Vector3IntField("Default", @default.vector3IntValue);
            }
            else if (paraType == typeof(Color))
            {
                @default = arg.FindPropertyRelative("_colorValue");
                @default.colorValue =
                    EditorGUILayout.ColorField("Default", @default.colorValue);
            }
            else if (paraType == typeof(Rect))
            {
                @default = arg.FindPropertyRelative("_rectValue");
                @default.rectValue =
                    EditorGUILayout.RectField("Default", @default.rectValue);
            }
            else if (paraType == typeof(RectInt))
            {
                @default = arg.FindPropertyRelative("_rectIntValue");
                @default.rectIntValue =
                    EditorGUILayout.RectIntField("Default", @default.rectIntValue);
            }
            else if (paraType == typeof(Bounds))
            {
                @default = arg.FindPropertyRelative("_boundsValue");
                @default.boundsValue =
                    EditorGUILayout.BoundsField("Default", @default.boundsValue);
            }
            else if (paraType == typeof(BoundsInt))
            {
                @default = arg.FindPropertyRelative("_boundsIntValue");
                @default.boundsIntValue =
                    EditorGUILayout.BoundsIntField("Default", @default.boundsIntValue);
            }
            else if (paraType == typeof(Enum))
            {
                @default = arg.FindPropertyRelative("_enumIndexValue");
                @default.enumValueIndex =
                    EditorGUILayout.Popup("Default", @default.enumValueIndex,Enum.GetNames(paraType));
            }
            else if (paraType == typeof(AnimationCurve))
            {
                @default = arg.FindPropertyRelative("_animationCurveValue");
                @default.animationCurveValue =
                    EditorGUILayout.CurveField("Default", @default.animationCurveValue);
            }
        }

        private static SerializedProperty GetArfNamePropertye(SerializedProperty arg)
        {
            return arg.FindPropertyRelative("_argName");
        }

        readonly List<bool> _foldoutState = new List<bool>();

        private bool _foldout(int index, int argCount)
        {
            if (_foldoutState.Count <= index)
            {
                _foldoutState.Add(false);
            }

            _foldoutState[index] = EditorGUILayout.Foldout(_foldoutState[index],
                $"EventArgCount:{argCount}", true);

            return _foldoutState[index];
        }

        private bool _removeConfirm = false;

        private void _removeAll()
        {
            if (_events.arraySize > 0)
            {
                if (_removeConfirm)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("Confirm Remove All Event"))
                        {
                            _events.arraySize = 0;
                            _removeConfirm = false;
                        }

                        if (GUILayout.Button("Cancel Remove All Event"))
                        {
                            _removeConfirm = false;
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }
                else
                {
                    if (GUILayout.Button("Remove All Event"))
                    {
                        _removeConfirm = true;
                    }
                }
            }
        }

        private string _eventName;
        private int _id;
        private int _argCount;

        private void _addEvent()
        {
            EditorGUILayout.BeginHorizontal();
            {
                _eventName = EditorGUILayout.TextField(_eventName, GUILayout.Width(110));
                _id = EditorGUILayout.IntField(_id, GUILayout.Width(110));
                _argCount = EditorGUILayout.IntField(_argCount, GUILayout.Width(50));

                if (GUILayout.Button("ADD"))
                {
                    if (string.IsNullOrWhiteSpace(_eventName))
                    {
                        return;
                    }

                    if (_names.Contains(_eventName))
                    {
                        return;
                    }


                    if (_ids.Contains(_id))
                    {
                        if (!_addMode)
                        {
                            return;
                        }
                        else
                        {
                            var maxID = _ids.OrderBy(x => x).Last();
                            _id = maxID + 1;
                        }
                    }

                    _addElement(_eventName, _id, _argCount);
                    //                    _addElement(_events, _eventName, false);
                    //                    _addElement(_eventIDs, _id);
                    //                    _addElement(_eventArgCount, _argCount);
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void _addElement(string eventName, int eventID, int argCount)
        {
            _events.arraySize++;
            var @event = _events.GetArrayElementAtIndex(_events.arraySize - 1);
            var eventNameSer = @event.FindPropertyRelative("_eventName");
            eventNameSer.stringValue = eventName;
            var eventIDSer = @event.FindPropertyRelative("_eventId");
            eventIDSer.intValue = eventID;
            var eventArgsSer = @event.FindPropertyRelative("_args");
            eventArgsSer.arraySize = argCount;
            _initEventArgs(eventArgsSer);
        }

        /// <summary>
        /// 增加了arraySize后他会复制最后一个元素的值,所以在这里重置一下
        /// </summary>
        /// <param name="eventArgsSer"></param>
        private void _initEventArgs(SerializedProperty eventArgsSer)
        {
            for (var i = 0; i < eventArgsSer.arraySize; i++)
            {
                var arg = eventArgsSer.GetArrayElementAtIndex(i);
                var argName = arg.FindPropertyRelative("_argName");
                var _argTypeStr = arg.FindPropertyRelative("_argTypeStr");
                var _argDesc = arg.FindPropertyRelative("_argDesc");
                var _notNull = arg.FindPropertyRelative("_notNull");

                argName.stringValue = string.Empty;
                _argTypeStr.stringValue = typeof(object).AssemblyQualifiedName;
                _argDesc.stringValue = string.Empty;
                _notNull.boolValue = true;
            }
        }

        private void _selectType(SerializedProperty argTypeStr)
        {
            if (GUILayout.Button("Select Type"))
            {
                var rect = _getFuzzyWindowRect();

                FuzzyWindow.Show(rect,
                    _getOptionTree(_getCurrentType(argTypeStr.stringValue)), (option) =>
                    {
                        argTypeStr.stringValue = ((Type) option.value).AssemblyQualifiedName;

                        serializedObject.ApplyModifiedProperties();

                        FuzzyWindow.instance.Close();
                        InternalEditorUtility.RepaintAllViews();
                    });
            }
        }

        private Rect _getFuzzyWindowRect()
        {
            var eve = UnityEngine.Event.current;

            var rect = new Rect(eve.mousePosition.x - 250, eve.mousePosition.y, 0, 0);

            return rect;
        }

        private Type _getCurrentType(string argStringValue)
        {
            if (string.IsNullOrEmpty(argStringValue))
            {
                return typeof(object);
            }

            return Type.GetType(argStringValue);
        }

        private IFuzzyOptionTree _getOptionTree(object currentSelectType)
        {
            var optionTree = new TypeOptionTree(Codebase.GetTypeSetFromAttribute(Metadata.Root()));
            optionTree.selected.Clear();
            optionTree.selected.Add(currentSelectType);
            return optionTree;
        }
    }
}