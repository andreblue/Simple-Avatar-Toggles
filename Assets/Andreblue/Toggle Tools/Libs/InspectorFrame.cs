#if UNITY_EDITOR
using UnityEditor;

using UnityEngine;
using System;

namespace Andreblue.ToggleTools
{
    public static class ToggleToolsGUI
    {
        //Using a ton of things from https://github.com/hai-vr/av3-animator-as-code/blob/main/Examples/AacExample.cs
        public static void InsecptorWindow(Editor editor, SerializedObject s_Object, string anim_Prefix, Action createFunc, Action removeFunc = null, Action resetApplied = null)
        {
            var prop = s_Object.FindProperty(anim_Prefix);
            if (prop.stringValue.Trim() == String.Empty) 
            { 
                prop.stringValue = GUID.Generate().ToString();
                s_Object.ApplyModifiedProperties();
            }
            editor.DrawDefaultInspector();

            if (GUILayout.Button("Create")) createFunc.Invoke();
            if (removeFunc != null && GUILayout.Button("Remove")) removeFunc.Invoke();
            if (resetApplied != null && GUILayout.Button("Reset Applied")) resetApplied.Invoke();
        }
    }
    
    
}
#endif