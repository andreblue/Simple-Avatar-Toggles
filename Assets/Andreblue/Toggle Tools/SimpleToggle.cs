#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using VRC.SDK3.Avatars.Components;
using UnityEditor.Animations;
using UnityEditor;
using AnimatorAsCode.V0;

namespace Andreblue.ToggleTools
{
    public class SimpleToggle : MonoBehaviour
    {
        public VRCAvatarDescriptor avatar;
        public AnimatorController assetContainer;
        public string assetKey;
        public bool writeDefaults;

    }
    [CustomEditor(typeof(SimpleToggle), true)]
    public class SimpleToggle_Editor : Editor
    {
        private const string Name = "SP";

        public override void OnInspectorGUI ()
        {
            ToggleTools.InsecptorWindow(this, serializedObject, "assetKey", Create, Remove);
        }

        private void Create ()
        {
            var selectedObject = (SimpleToggle)target;
            var acc = ToggleTools.AnimEditor(Name, selectedObject.avatar, selectedObject.assetContainer, selectedObject.assetKey, selectedObject.writeDefaults);
            var fx = acc.CreateSupportingFxLayer($"Toggle_{target.name}");//acc.CreateMainFxLayer();
            var toggleOff = fx.NewState($"Toggle_{target.name}_Off")
            .WithAnimation(acc.NewClip().Toggling(selectedObject.gameObject, false));
            var toggleOn = fx.NewState($"Toggle_{target.name}_On")
            .WithAnimation(acc.NewClip().Toggling(selectedObject.gameObject, true));
            var toggleBool = fx.BoolParameter($"{Name}_Toggle_{target.name}");

            toggleOff.TransitionsTo(toggleOn).When(toggleBool.IsTrue());
            toggleOn.TransitionsTo(toggleOff).When(toggleBool.IsFalse());
        }
        private void Remove () 
        {
            var selectedObject = (SimpleToggle)target;
            var acc = ToggleTools.AnimEditor(Name, selectedObject.avatar, selectedObject.assetContainer, selectedObject.assetKey, selectedObject.writeDefaults);
            acc.RemoveAllSupportingLayers($"Toggle_{target.name}");
        }
    }
}
#endif