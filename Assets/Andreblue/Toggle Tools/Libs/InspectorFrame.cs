#if UNITY_EDITOR
using UnityEditor;

using UnityEngine;
using System;
using AnimatorAsCode.V0;
using VRC.SDK3.Avatars.Components;
using AnimatorController = UnityEditor.Animations.AnimatorController;

namespace Andreblue.ToggleTools
{
    public static class ToggleTools
    {
        //Using a ton of things from https://github.com/hai-vr/av3-animator-as-code/blob/main/Examples/AacExample.cs
        public static void InsecptorWindow(Editor editor, SerializedObject s_Object, string anim_Prefix, Action createFunc, Action removeFunc = null)
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
        }
        public static AacFlBase AnimEditor(string systemName, VRCAvatarDescriptor avatar, AnimatorController assetContainer, string assetKey, bool writeDefaults = false)
        {
            var animCode = AacV0.Create(new AacConfiguration
            {
                SystemName = systemName,
                AvatarDescriptor = avatar,
                AnimatorRoot = avatar.transform,
                DefaultValueRoot = avatar.transform,
                AssetContainer = assetContainer,
                AssetKey = assetKey,
                DefaultsProvider = new AacDefaultsProvider(writeDefaults: writeDefaults)

            });
            animCode.ClearPreviousAssets();

            return animCode;
        }
    }
    
    
}
#endif