//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2019年07月16日-18:20
//Assembly-CSharp

using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CabinIcarus.BoltExtensions.Event
{
    /// <summary>
    /// Unity GUI 序列化支持
    /// </summary>
    public partial class ArgEntity:ISerializationCallbackReceiver
    {
        #region Unity

        // 默认值是否被序列化了
        public bool DefaultIsUntiySerialization;
        
        private Type _unitySerializationTypeASName;

        /// <summary>
        /// Unity 支持的类型
        /// </summary>
        public Type ArgUnitySerializationTypeAsName
        {
            get { return _unitySerializationTypeASName; }
            private set { _unitySerializationTypeASName = value; }
        }

        [SerializeField]
        private int _intValue;
        
        [SerializeField]
        private string _stringValue;
        
        [SerializeField]
        private float _floatValue;
        
        [SerializeField]
        private double _doubleValue;
        
        [SerializeField]
        private bool _boolValue;
        
        [SerializeField]
        private long _longValue;
        
        [SerializeField]
        private Quaternion _quaternionValue;
        
        [SerializeField]
        private Object _objectValue;
        
        [SerializeField]
        private Vector2 _vector2Value;
        
        [SerializeField]
        private Vector3 _vector3Value;
        
        [SerializeField]
        private Vector4 _vector4Value;
        
        [SerializeField]
        private Vector2Int _vector2IntValue;
        
        [SerializeField]
        private Vector3Int _vector3IntValue;
        
        [SerializeField]
        private Color _colorValue;
        
        [SerializeField]
        private Rect _rectValue;
        
        [SerializeField]
        private RectInt _rectIntValue;
        
        [SerializeField]
        private Bounds _boundsValue;
        
        [SerializeField]
        private BoundsInt _boundsIntValue;
        
        [SerializeField]
        private int _enumIndexValue;
        
        [SerializeField]
        private AnimationCurve _animationCurveValue;
        
        public string IntValue
        {
            get
            {
                try
                {
                    var value = (string) _default;
                    return value;
                }
#pragma warning disable 168
                catch (Exception e)
#pragma warning restore 168
                {
                    return "";
                }
            }
            set { _stringValue = value; }
        }
        
        public int GetIntValue()
        {
            try
            {
                var value = (int) _default;
                return value;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                return 0;
            }
        }

        public float GetFloatValue()
        {
            try
            {
                var value = (float) _default;
                return value;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                return 0;
            }
        }

        public double GetDoubleValue()
        {
            try
            {
                var value = (double) _default;
                return value;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                return 0;
            }
        }

        public bool GetBoolValue()
        {
            try
            {
                var value = (bool) _default;
                return value;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                return false;
            }
        }

        public long GetLongValue()
        {
            try
            {
                var value = (long) _default;
                return value;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                return 0;
            }
        }

        public string GetStringValue()
        {
            try
            {
                var value = (string) _default;
                return value;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                return string.Empty;
            }
        }

        public Quaternion GetQuaternionValue()
        {
            try
            {
                var value = (Quaternion) _default;
                return value;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                return Quaternion.identity;
            }
        }

        public Object GetObjectValue()
        {
            try
            {
                var value = (Object) _default;
                return value;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                return null;
            }
        }

        public Vector2 GetVector2Value()
        {
            try
            {
                var value = (Vector2) _default;
                return value;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                return Vector2.zero;
            }
        }

        public Vector3 GetVector3Value()
        {
            try
            {
                var value = (Vector3) _default;
                return value;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                return Vector3.zero;
            }
        }

        public Vector4 GetVector4Value()
        {
            try
            {
                var value = (Vector4) _default;
                return value;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                return Vector4.zero;
            }
        }

        public Vector2Int GetVector2IntValue()
        {
            try
            {
                var value = (Vector2Int) _default;
                return value;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                return Vector2Int.zero;
            }
        }

        public Vector3Int GetVector3IntValue()
        {
            try
            {
                var value = (Vector3Int) _default;
                return value;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                return Vector3Int.zero;
            }
        }

        /// <summary>
        /// 默认值为 0,0,0,0
        /// </summary>
        public Color GetColorValue()
        {
            try
            {
                var value = (Color) _default;
                return value;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                return new Color();
            }
        }

        public Rect GetRectValue()
        {
            try
            {
                var value = (Rect) _default;
                return value;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                return Rect.zero;
            }
        }

        public RectInt GetRectIntValue()
        {
            try
            {
                var value = (RectInt) _default;
                return value;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                return new RectInt();
            }
        }

        public Bounds GetBoundsValue()
        {
            try
            {
                var value = (Bounds) _default;
                return value;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                return new Bounds();
            }
        }

        public BoundsInt GetBoundsIntValue()
        {
            try
            {
                var value = (BoundsInt) _default;
                return value;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                return new BoundsInt();
            }
        }

        /// <summary>
        /// 默认值为-1
        /// </summary>
        public int GetEnumIndexValue()
        {
            try
            {
                var value = (int) _default;
                return value;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                return -1;
            }
        }

        public AnimationCurve GetAnimationCurveValue()
        {
            try
            {
                var value = (AnimationCurve) _default;
                return value;
            }
#pragma warning disable 168
            catch (Exception e)
#pragma warning restore 168
            {
                return new AnimationCurve();
            }
        }

        #endregion

        public void OnBeforeSerialize()
        {
            var paraType = Type.GetType(_argTypeStr);

            if (paraType == null)
            {
                return;
            }
            
            if (paraType == typeof(int))
            {
                _unitySerializationTypeASName = typeof(int);
                DefaultIsUntiySerialization = true;
                _intValue = GetIntValue();
            }
            else if (paraType == typeof(string))
            {
                _unitySerializationTypeASName = typeof(string);
                DefaultIsUntiySerialization = true;
                _stringValue = GetStringValue();
            }
            else if (paraType == typeof(float))
            {
                _unitySerializationTypeASName = typeof(float);
                DefaultIsUntiySerialization = true;
                _floatValue = GetFloatValue();
            }
            else if (paraType == typeof(double))
            {
                _unitySerializationTypeASName = typeof(double);
                DefaultIsUntiySerialization = true;
                _doubleValue = GetDoubleValue();
            }
            else if (paraType == typeof(bool))
            {
                _unitySerializationTypeASName = typeof(bool);
                DefaultIsUntiySerialization = true;
                _boolValue = GetBoolValue();
            }
            else if (paraType == typeof(long))
            {
                _unitySerializationTypeASName = typeof(long);  
                DefaultIsUntiySerialization = true;
                _longValue = GetLongValue();
            }
            else if (paraType == typeof(Quaternion))
            {
                _unitySerializationTypeASName = typeof(Quaternion);  
                DefaultIsUntiySerialization = true;
                _quaternionValue = GetQuaternionValue();
            }
            else if (paraType.IsSubclassOf(typeof(Object)))
            {
                _unitySerializationTypeASName = paraType;
                DefaultIsUntiySerialization = true;
                _objectValue = GetObjectValue();
            }
            else if (paraType == typeof(Vector2))
            {
                _unitySerializationTypeASName = typeof(Vector2);
                DefaultIsUntiySerialization = true;
                _vector2Value = GetVector2Value();
            }
            else if (paraType == typeof(Vector3))
            {
                _unitySerializationTypeASName = typeof(Vector3);
                DefaultIsUntiySerialization = true;
                _vector3Value = GetVector3Value();
            }
            else if (paraType == typeof(Vector4))
            {
                _unitySerializationTypeASName = typeof(Vector4);
                DefaultIsUntiySerialization = true;
                _vector4Value = GetVector4Value();
            }
            else if (paraType == typeof(Vector2Int))
            {
                _unitySerializationTypeASName = typeof(Vector2Int);
                DefaultIsUntiySerialization = true;
                _vector2IntValue = GetVector2IntValue();
            }
            else if (paraType == typeof(Vector3Int))
            {
                _unitySerializationTypeASName = typeof(Vector3Int);
                DefaultIsUntiySerialization = true;
                _vector3IntValue = GetVector3IntValue();
            }
            else if (paraType == typeof(Color))
            {
                _unitySerializationTypeASName = typeof(Color);
                DefaultIsUntiySerialization = true;
                _colorValue = GetColorValue();
            }
            else if (paraType == typeof(Rect))
            {
                _unitySerializationTypeASName = typeof(Rect);
                DefaultIsUntiySerialization = true;
                _rectValue = GetRectValue();
            }
            else if (paraType == typeof(RectInt))
            {
                _unitySerializationTypeASName = typeof(RectInt);
                DefaultIsUntiySerialization = true;
                _rectIntValue = GetRectIntValue();
            }
            else if (paraType == typeof(Bounds))
            {
                _unitySerializationTypeASName = typeof(Bounds);
                DefaultIsUntiySerialization = true;
                _boundsValue = GetBoundsValue();
            }
            else if (paraType == typeof(BoundsInt))
            {
                _unitySerializationTypeASName = typeof(BoundsInt);
                DefaultIsUntiySerialization = true;
                _boundsIntValue = GetBoundsIntValue();
            }
            else if (paraType == typeof(Enum))
            {
                _unitySerializationTypeASName = typeof(Enum);
                DefaultIsUntiySerialization = true;
                _enumIndexValue = GetEnumIndexValue();
            }
            else if (paraType == typeof(AnimationCurve))
            {
                _unitySerializationTypeASName = typeof(AnimationCurve);
                DefaultIsUntiySerialization = true;
                _animationCurveValue = GetAnimationCurveValue();
            }
        }

        public void OnAfterDeserialize()
        {
            var paraType = Type.GetType(_argTypeStr);
            
            if (paraType == typeof(int))
            {
                _default = _intValue;
            }
            else if (paraType == typeof(string))
            {
                _default = _stringValue;
            }
            else if (paraType == typeof(float))
            {
                _default = _floatValue;
            }
            else if (paraType == typeof(double))
            {
                _default = _doubleValue;
            }
            else if (paraType == typeof(bool))
            {
                _default = _boolValue;
            }
            else if (paraType == typeof(long))
            {
                _default = _longValue;  
            }
            else if (paraType == typeof(Quaternion))
            {
                _default = _quaternionValue;  
            }
            else if (paraType == typeof(Object))
            {
                _default = _objectValue;
            }
            else if (paraType == typeof(Vector2))
            {
                _default = _vector2Value;
            }
            else if (paraType == typeof(Vector3))
            {
                _default = _vector3Value;
            }
            else if (paraType == typeof(Vector4))
            {
                _default = _vector4Value;
            }
            else if (paraType == typeof(Vector2Int))
            {
                _default = _vector2IntValue;
            }
            else if (paraType == typeof(Vector3Int))
            {
                _default = _vector3IntValue;
            }
            else if (paraType == typeof(Color))
            {
                _default = _colorValue;
            }
            else if (paraType == typeof(Rect))
            {
                _default = _rectValue;
            }
            else if (paraType == typeof(RectInt))
            {
                _default = _rectIntValue;
            }
            else if (paraType == typeof(Bounds))
            {
                _default = _boundsValue;
            }
            else if (paraType == typeof(BoundsInt))
            {
                _default = _boundsIntValue;
            }
            else if (paraType == typeof(Enum))
            {
                _default = _enumIndexValue;
            }
            else if (paraType == typeof(AnimationCurve))
            {
                _default = _animationCurveValue;
            }
        }
    }
}