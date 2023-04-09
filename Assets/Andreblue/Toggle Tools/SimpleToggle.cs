#if UNITY_EDITOR
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using UnityEditor.Animations;
using UnityEditor;

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
        private const string Name = "ToggleTools_Simple";
        private const string LayerPrefix = "Toggle";

        public override void OnInspectorGUI ()
        {
            ToggleTools.InsecptorWindow(this, serializedObject, "assetKey", Create, Remove);
        }

        private void Create ()
        {
            var selectedObject = (SimpleToggle)target;
            var acc = ToggleTools.AnimEditor(Name, selectedObject.avatar, selectedObject.assetContainer, selectedObject.assetKey, selectedObject.writeDefaults);
            var fx = acc.CreateSupportingFxLayer($"{LayerPrefix}_{target.name}");
            var toggleOff = fx.NewState($"{LayerPrefix}_{target.name}_Off")
                .WithAnimation(acc.NewClip().Toggling(selectedObject.gameObject, false));
            var toggleOn = fx.NewState($"{LayerPrefix}_{target.name}_On")
                .WithAnimation(acc.NewClip().Toggling(selectedObject.gameObject, true));
            var toggleBool = fx.BoolParameter($"{Name}_{LayerPrefix}_{target.name}");

            toggleOff.TransitionsTo(toggleOn).When(toggleBool.IsTrue());
            toggleOn.TransitionsTo(toggleOff).When(toggleBool.IsFalse());
        }
        private void Remove () 
        {
            var selectedObject = (SimpleToggle)target;
            var acc = ToggleTools.AnimEditor(Name, selectedObject.avatar, selectedObject.assetContainer, selectedObject.assetKey, selectedObject.writeDefaults);
            acc.RemoveAllSupportingLayers($"{LayerPrefix}_{target.name}");
        }
    }
}
#endif