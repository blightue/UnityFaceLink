﻿using System;
using System.Collections.Generic;
using System.Linq;
using FaceLink.Data;
using FaceLink.Utility;
using SuiSuiShou.UIEEx;
using SuiSuiShou.UIEEx.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


namespace FaceLink.Editor.Data
{
    public class BSMapAutoGenerator : EditorWindow
    {
        [SerializeField] private SkinnedMeshRenderer skmr;
        [SerializeField] private BSMapSO bsMap;

        [MenuItem("Window/FaceLink/BSMap/AutoGenerator")]
        private static void ShowWindow()
        {
            var window = GetWindow<BSMapAutoGenerator>();
            window.titleContent = new GUIContent("BSMapAutoGenerator");
            window.Show();
        }

        private void CreateGUI()
        {
            ObjectField skmrOF = EditorUIELayout.ObjectField("Skinned MR", rootVisualElement)
                .SetBindingPath(nameof(skmr));
            skmrOF.objectType = typeof(SkinnedMeshRenderer);
            skmrOF.allowSceneObjects = true;

            ObjectField bsMapOF = EditorUIELayout.ObjectField("BS Map", rootVisualElement)
                .SetBindingPath(nameof(bsMap));
            bsMapOF.objectType = typeof(BSMapSO);

            Button button = UIELayout.Button(AutoGenerate, rootVisualElement)
                .SetText("Generate");

            Button buttonWithFilter = UIELayout.Button(() => AutoGenerateWithFilter(s => s.Split('.')[^1]), rootVisualElement)
                .SetText("Generate Remove Prefix");

            rootVisualElement.Bind(new SerializedObject(this));
        }

        private void AutoGenerate()
        {
            string[] skmrBSNames = GetBSNames(skmr);

            string[] targetNames = bsMap.BSMap.Select(kvPair => kvPair.Key).ToArray();

            AutoGenerateMap(skmrBSNames, targetNames);
        }

        private void AutoGenerateWithFilter(Func<string, string> func)
        {
            string[] skmrBSNames = GetBSNames(skmr);

            string[] bsName4Map = skmrBSNames.Select(func).ToArray();

            string[] targetNames = bsMap.BSMap.Select(kvPair => kvPair.Key).ToArray();

            AutoGenerateMap(skmrBSNames, targetNames, BSAutoMap.ComputeBSMap(bsName4Map, targetNames));
        }

        private void AutoGenerateMap(string[] skmrBSNames, string[] targetNames, int[] closestMap = null)
        {
            if (closestMap == null)
                closestMap = BSAutoMap.ComputeBSMap(skmrBSNames, targetNames);

            for (var i = 0; i < closestMap.Length; i++)
            {
                var value = bsMap[closestMap[i]];
                value.Target = skmrBSNames[i];
                bsMap[closestMap[i]] = value;
            }
            Debug.Log($"Set BSNames with length {closestMap.Length}");

            EditorUtility.SetDirty(bsMap);
        }

        private string[] GetBSNames(SkinnedMeshRenderer skmrRenderer)
        {
            int count = skmrRenderer.sharedMesh.blendShapeCount;

            string[] result = new string[count];

            for (int i = 0; i < count; i++)
            {
                result[i] = skmrRenderer.sharedMesh.GetBlendShapeName(i);
            }

            return result;
        }
    }
}