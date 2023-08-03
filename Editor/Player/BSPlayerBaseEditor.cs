using System;
using FaceLink.Player;
using SuiSuiShou.UIEEx;
using SuiSuiShou.UIEEx.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.Player
{
    [CustomEditor(typeof(BSPlayerBase), true)]
    public class BSPlayerBaseEditor : UnityEditor.Editor
    {
        protected BSPlayerBase _playerBase;

        private void OnEnable()
        {
            _playerBase = target as BSPlayerBase;
            _playerBase.InitPlayer();
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            
            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            VisualElement btnGroup = new VisualElement();
            btnGroup.style.flexDirection = FlexDirection.Row;
            btnGroup.style.alignItems = Align.FlexStart;
            root.Add(btnGroup);

            UIELayout.Button(_playerBase.Play, btnGroup).SetText("Play").style.flexGrow = 1;
            UIELayout.Button(_playerBase.Stop, btnGroup).SetText("Stop").style.flexGrow = 1;
            UIELayout.Button(_playerBase.Pause, btnGroup).SetText("Pause").style.flexGrow = 1;
            
            return root;
        }
    }
}