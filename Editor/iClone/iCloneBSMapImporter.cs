using SuiSuiShou.UIEEx;
using SuiSuiShou.UIEEx.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace FaceLink.Editor.iClone
{
    public class iCloneBSMapImporter : EditorWindow
    {
        [SerializeField] private TextAsset bsMapJson;

        private iCloneBSMapModel _bsMap;

        protected iCloneBSMapModel BSMap => _bsMap ??=
            (bsMapJson is null ? null : JsonUtility.FromJson<iCloneBSMapModel>(bsMapJson.text));

        [MenuItem("Window/FaceLink/iClone/BS Map Importer")]
        private static void ShowWindow()
        {
            var window = GetWindow<iCloneBSMapImporter>();
            window.titleContent = new GUIContent("iClone BS Map Importer");
            window.Show();
        }

        private void CreateGUI()
        {
            ObjectField jsonFileField = EditorUIELayout.ObjectField("BS Map json", rootVisualElement)
                .SetBindingPath(nameof(bsMapJson));
            jsonFileField.objectType = typeof(TextAsset);

            rootVisualElement.Bind(new SerializedObject(this));
        }

        public void ResetBSMap()
        {
            _bsMap = null;
        }
    }
}