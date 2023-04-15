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
        private bool applied;

        public void setApplied(bool setTo)
        { 
            applied = setTo;
        }
        public void toggleApplied()
        {
            applied = !applied;
        }
        public bool getApplied()
        {
            return applied;
        }

    }
    [CustomEditor(typeof(SimpleToggle), true)]
    public class SimpleToggle_Editor : Editor
    {
        private const string Name = "Tools";
        private const string LayerPrefix = "ø";

        public override void OnInspectorGUI ()
        {
            ToggleToolsGUI.InsecptorWindow(this, serializedObject, "assetKey", Create, Remove, ClearApplied);
        }

        private void Create ()
        {
            var selectedObject = (SimpleToggle)target;
            if (selectedObject.getApplied()) return;
            var acc = ToggleToolsUtil.AnimEditor(Name, selectedObject.avatar, selectedObject.assetContainer, selectedObject.assetKey, selectedObject.writeDefaults);
            var fx = acc.CreateSupportingFxLayer($"{LayerPrefix}_{target.name}");
            var toggleOff = fx.NewState($"{LayerPrefix}_{target.name}_Off")
                .WithAnimation(acc.NewClip().Toggling(selectedObject.gameObject, false));
            var toggleOn = fx.NewState($"{LayerPrefix}_{target.name}_On")
                .WithAnimation(acc.NewClip().Toggling(selectedObject.gameObject, true));
            var toggleBool = fx.BoolParameter($"{Name}_{LayerPrefix}_{target.name}");

            toggleOff.TransitionsTo(toggleOn).When(toggleBool.IsTrue());
            toggleOn.TransitionsTo(toggleOff).When(toggleBool.IsFalse());
            selectedObject.toggleApplied();

        }
        private void Remove () 
        {
            var selectedObject = (SimpleToggle)target;
            if (!selectedObject.getApplied()) return;
            var acc = ToggleToolsUtil.AnimEditor(Name, selectedObject.avatar, selectedObject.assetContainer, selectedObject.assetKey, selectedObject.writeDefaults);
            acc.RemoveAllSupportingLayers($"{LayerPrefix}_{target.name}");
            selectedObject.toggleApplied();
        }
        private void ClearApplied()
        {
            var selectedObject = (SimpleToggle)target;
            selectedObject.setApplied(false);
        }
    }
}
#endif