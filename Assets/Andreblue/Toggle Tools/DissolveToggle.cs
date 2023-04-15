#if UNITY_EDITOR
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using UnityEditor.Animations;
using UnityEditor;


namespace Andreblue.ToggleTools
{
    public class DissolveToggle : MonoBehaviour
    {
        public VRCAvatarDescriptor avatar;
        public AnimatorController assetContainer;
        public string assetKey;
        public bool writeDefaults;
        public float dissolveTime;

    }
    [CustomEditor(typeof(DissolveToggle), true)]
    public class DissolveToggle_Editor : Editor
    {
        private const string Name = "ToggleTools_Dissolve";
        private const string LayerPrefix = "Dissolve";

        public override void OnInspectorGUI()
        {
            ToggleToolsGUI.InsecptorWindow(this, serializedObject, "assetKey", Create, Remove);
        }

        private void Create()
        {
            var selectedObject = (DissolveToggle)target;
            var acc = ToggleToolsUtil.AnimEditor(Name, selectedObject.avatar, selectedObject.assetContainer, selectedObject.assetKey, selectedObject.writeDefaults);
            GameObject meshToEdit = selectedObject.gameObject;

        }
        private void Remove()
        {
            /*var selectedObject = (SimpleToggle)target;
            var acc = ToggleToolsUtil.AnimEditor(Name, selectedObject.avatar, selectedObject.assetContainer, selectedObject.assetKey, selectedObject.writeDefaults);
            acc.RemoveAllSupportingLayers($"{LayerPrefix}_{target.name}");*/
        }
    }
}
#endif