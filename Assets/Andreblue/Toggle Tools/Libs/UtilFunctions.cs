#if UNITY_EDITOR
using UnityEditor;

using UnityEngine;
using System;
using AnimatorAsCode.V0;
using VRC.SDK3.Avatars.Components;
using AnimatorController = UnityEditor.Animations.AnimatorController;

namespace Andreblue.ToggleTools
{
    public class ToggleToolsUtil
    {
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