using FaceLink.Data;
using SuiSuiShou.UIEEx;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace FaceLink.Editor.Data
{
    [CustomEditor(typeof(BSMapSOBase<>), true)]
    public class BSMapSOBaseEditor : UnityEditor.Editor
    {
        protected IBSPair targetSO;

        private void OnEnable()
        {
            targetSO = target as IBSPair;
        }


        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            
            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            return root;
        }
    }
}