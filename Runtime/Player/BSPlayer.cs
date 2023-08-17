﻿using System;
using System.Linq;
using FaceLink.Data;
using UnityEngine;

namespace FaceLink.Player
{
    public class BSPlayer : BSPlayerAbstract
    {
        public override SkinnedMeshRenderer[] SKMRs => _skmrs;

        public override IBSSource Source
        {
            get => _source;
        }

        protected override BSMapSOAbstract BSMapSO => _BSMapSO;


        [SerializeField] private SkinnedMeshRenderer[] _skmrs;
        [SerializeField] private BSSourceAbstract _source;
        [SerializeField] private float[] bsValues;
        [SerializeReference] private BSMapCacheAbstract _mapCache;
        [SerializeReference] private BSMapSOAbstract _BSMapSO;

        public override void FreshBSData(float[] blendshapes)
        {
            // Debug.Log($"Fresh {blendshapes.Length}");

            // bsValues = blendshapes.Select(f => f.ToString("0.000")).ToArray();
            bsValues = blendshapes;
            IsBSUpdated = true;
        }

        public override void FreshFace()
        {
            _mapCache.EvaluateBS(bsValues, SKMRs);
            IsBSUpdated = false;
        }

        public override void SetupSKMRs()
        {
            _mapCache = BSMapSO.RecordSkMRMapCache(SKMRs);
            Debug.Log($"Record Cache with count {_mapCache.PairsCaches.Count()}");
        }
    }
}