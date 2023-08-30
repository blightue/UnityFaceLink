using FaceLink.Player;
using SuiSuiShou.UIEEx;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace FaceLink.Editor.Player
{
    [CustomEditor(typeof(BSPlayerAbstract), true)]
    public class BSPlayerAbstractEditor : UnityEditor.Editor
    {
        protected BSPlayerAbstract PlayerAbstract;

        private void OnEnable()
        {
            PlayerAbstract = target as BSPlayerAbstract;
            PlayerAbstract.InitPlayer();
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            
            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            VisualElement btnGroup = new VisualElement();
            btnGroup.style.flexDirection = FlexDirection.Row;
            btnGroup.style.alignItems = Align.FlexStart;
            root.Add(btnGroup);

            UIELayout.Button(PlayerAbstract.Play, btnGroup).SetText("Play").style.flexGrow = 1;
            UIELayout.Button(PlayerAbstract.Stop, btnGroup).SetText("Stop").style.flexGrow = 1;
            UIELayout.Button(PlayerAbstract.Pause, btnGroup).SetText("Pause").style.flexGrow = 1;

            UIELayout.Button(PlayerAbstract.SetupSKMRs, root).SetText("Setup").style.flexGrow = 1;
            
            return root;
        }
    }
}