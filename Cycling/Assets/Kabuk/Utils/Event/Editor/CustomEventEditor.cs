using UnityEngine;
using UnityEditor;
using System.Reflection;
namespace Kabuk
{

    [CustomEditor(typeof(CustomEvent<>), true)]
    public class CustomEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Fire"))
            {
                MethodInfo info = target.GetType().GetMethod("Fire", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                info.Invoke(target, new object[] { });
            }
        }
    }

    [CustomEditor(typeof(CustomEvent<,>), true)]
    public class CustomEventWithParameterEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Fire"))
            {
                MethodInfo methodInfo = target.GetType().GetMethod("Fire", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                FieldInfo fieldInfo = target.GetType().GetField("parameters", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
                object o = fieldInfo.GetValue(target);
                methodInfo.Invoke(target, new object[] { o });
            }
        }
    }

}
