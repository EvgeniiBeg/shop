using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace D_Framework
{
    public static class Utilities
    {
        #region Static Functions

        #if UNITY_EDITOR
        #region Editor

        public static string GetAssetDirectory(UnityEngine.Object asset)
        {
            string assetPath = UnityEditor.AssetDatabase.GetAssetPath(asset);
            char separator = '/';
            string[] splitPath = assetPath.Split(separator);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < splitPath.Length - 1; i++)
            {
                sb.Append(splitPath[i]);

                if (i != splitPath.Length - 2)
                    sb.Append(separator);
            }

            return sb.ToString();
        }

        public static T[] GetAsArray<T>(this SerializedProperty prop)
        {
            if (prop == null) throw new System.ArgumentNullException("prop");
            if (!prop.isArray) throw new System.ArgumentException("SerializedProperty does not represent an Array.", "prop");

            var arr = new T[prop.arraySize];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = GetPropertyValue<T>(prop.GetArrayElementAtIndex(i));
            }
            return arr;
        }

        public static void SetAsArray<T>(this SerializedProperty prop, T[] arr)
        {
            if (prop == null) throw new System.ArgumentNullException("prop");
            if (!prop.isArray) throw new System.ArgumentException("SerializedProperty does not represent an Array.", "prop");

            int sz = arr != null ? arr.Length : 0;
            prop.arraySize = sz;
            for (int i = 0; i < sz; i++)
            {
                prop.GetArrayElementAtIndex(i).SetPropertyValue(arr[i]);
            }
        }

        public static void SetPropertyValue(this SerializedProperty prop, object value)
        {
            if (prop == null) throw new System.ArgumentNullException("prop");

            switch (prop.propertyType)
            {
                case SerializedPropertyType.Integer:
                    prop.intValue = ConvertUtil.ToInt(value);
                    break;
                case SerializedPropertyType.Boolean:
                    prop.boolValue = ConvertUtil.ToBool(value);
                    break;
                case SerializedPropertyType.Float:
                    prop.floatValue = ConvertUtil.ToSingle(value);
                    break;
                case SerializedPropertyType.String:
                    prop.stringValue = ConvertUtil.ToString(value);
                    break;
                case SerializedPropertyType.Color:
                    prop.colorValue = ConvertUtil.ToColor(value);
                    break;
                case SerializedPropertyType.ObjectReference:
                    prop.objectReferenceValue = value as UnityEngine.Object;
                    break;
                case SerializedPropertyType.LayerMask:
                    prop.intValue = (value is LayerMask) ? ((LayerMask)value).value : ConvertUtil.ToInt(value);
                    break;
                case SerializedPropertyType.Enum:
                    //prop.enumValueIndex = ConvertUtil.ToInt(value);
                    prop.SetEnumValue(value);
                    break;
                case SerializedPropertyType.Vector2:
                    prop.vector2Value = ConvertUtil.ToVector2(value);
                    break;
                case SerializedPropertyType.Vector3:
                    prop.vector3Value = ConvertUtil.ToVector3(value);
                    break;
                case SerializedPropertyType.Vector4:
                    prop.vector4Value = ConvertUtil.ToVector4(value);
                    break;
                case SerializedPropertyType.Rect:
                    prop.rectValue = (Rect)value;
                    break;
                case SerializedPropertyType.ArraySize:
                    prop.arraySize = ConvertUtil.ToInt(value);
                    break;
                case SerializedPropertyType.Character:
                    prop.intValue = ConvertUtil.ToInt(value);
                    break;
                case SerializedPropertyType.AnimationCurve:
                    prop.animationCurveValue = value as AnimationCurve;
                    break;
                case SerializedPropertyType.Bounds:
                    prop.boundsValue = (Bounds)value;
                    break;
                case SerializedPropertyType.Gradient:
                    throw new System.InvalidOperationException("Can not handle Gradient types.");
            }
        }

        public static T GetPropertyValue<T>(this SerializedProperty prop)
        {
            var obj = GetPropertyValue(prop);
            if (obj is T) return (T)obj;

            var tp = typeof(T);
            try
            {
                return (T)System.Convert.ChangeType(obj, tp);
            }
            catch (System.Exception)
            {
                return default(T);
            }
        }

        public static void SetEnumValue<T>(this SerializedProperty prop, T value) where T : struct
        {
            if (prop == null) throw new System.ArgumentNullException("prop");
            if (prop.propertyType != SerializedPropertyType.Enum) throw new System.ArgumentException("SerializedProperty is not an enum type.", "prop");

            prop.intValue = ConvertUtil.ToInt(value);
        }

        public static void SetEnumValue(this SerializedProperty prop, System.Enum value)
        {
            if (prop == null) throw new System.ArgumentNullException("prop");
            if (prop.propertyType != SerializedPropertyType.Enum) throw new System.ArgumentException("SerializedProperty is not an enum type.", "prop");

            if (value == null)
            {
                prop.enumValueIndex = 0;
                return;
            }

            prop.intValue = ConvertUtil.ToInt(value);
        }

        public static void SetEnumValue(this SerializedProperty prop, object value)
        {
            if (prop == null) throw new System.ArgumentNullException("prop");
            if (prop.propertyType != SerializedPropertyType.Enum) throw new System.ArgumentException("SerializedProperty is not an enum type.", "prop");

            if (value == null)
            {
                prop.enumValueIndex = 0;
                return;
            }

            prop.intValue = ConvertUtil.ToInt(value);
        }

        public static object GetPropertyValue(this SerializedProperty prop)
        {
            if (prop == null) throw new System.ArgumentNullException("prop");

            switch (prop.propertyType)
            {
                case SerializedPropertyType.Integer:
                    return prop.intValue;
                case SerializedPropertyType.Boolean:
                    return prop.boolValue;
                case SerializedPropertyType.Float:
                    return prop.floatValue;
                case SerializedPropertyType.String:
                    return prop.stringValue;
                case SerializedPropertyType.Color:
                    return prop.colorValue;
                case SerializedPropertyType.ObjectReference:
                    return prop.objectReferenceValue;
                case SerializedPropertyType.LayerMask:
                    return (LayerMask)prop.intValue;
                case SerializedPropertyType.Enum:
                    return prop.enumValueIndex;
                case SerializedPropertyType.Vector2:
                    return prop.vector2Value;
                case SerializedPropertyType.Vector3:
                    return prop.vector3Value;
                case SerializedPropertyType.Vector4:
                    return prop.vector4Value;
                case SerializedPropertyType.Rect:
                    return prop.rectValue;
                case SerializedPropertyType.ArraySize:
                    return prop.arraySize;
                case SerializedPropertyType.Character:
                    return (char)prop.intValue;
                case SerializedPropertyType.AnimationCurve:
                    return prop.animationCurveValue;
                case SerializedPropertyType.Bounds:
                    return prop.boundsValue;
                case SerializedPropertyType.Gradient:
                    throw new System.InvalidOperationException("Can not handle Gradient types.");
            }

            return null;
        }
        #endregion
        #endif

        public static void CastRayAtMousePosition(Action<RaycastHit> rayAction, LayerMask targetMask = default)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, targetMask))
            {
                rayAction(hitInfo);
            }
        }

        public static Transform RecursiveFindChild(Transform parent, string childName)
        {
            foreach (Transform child in parent)
            {
                if (child.name == childName)
                {
                    return child;
                }
                else
                {
                    Transform found = RecursiveFindChild(child, childName);
                    if (found != null)
                    {
                        return found;
                    }
                }
            }
            return null;
        }

        public static List<Transform> GetChildren(this Transform parent)
        {
            List<Transform> childrenList = new List<Transform>();

            for (int i = 0; i < parent.childCount; i++)
            {
                childrenList.Add(parent.GetChild(i));
            }

            return childrenList;
        }

        #endregion

        #region Extension Methods

        public static void EnableForces(this Rigidbody rb)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        public static void DisableForces(this Rigidbody rb)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        public static double NextDouble(this System.Random rng, double minValue, double maxValue)
        {
            return rng.NextDouble() * (maxValue - minValue) + minValue;
        }
        #endregion

        #region Kotlin-like Functions

        public static void TODO() => throw new NotImplementedException();
        public static void TODO(string message) => throw new NotImplementedException(message);


        public static void With<T>(this T obj, Action<T> action)
        {
            action(obj);
        }

        public static T Apply<T>(this T obj, Action<T> action)
        {
            action(obj);
            return obj;
        }

        #endregion
    }
}