using System;
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
        
        protected override BSMapSOAbstract BSMapSO { get => _BSMapSO; }


        [SerializeField] private SkinnedMeshRenderer[] _skmrs;
        [SerializeField] private BSSourceAbstract _source;
        [SerializeField] private string[] bsValues;
        [SerializeReference] private BSMapCache _mapCache;
        [SerializeReference] private BSMapSOAbstract _BSMapSO;
        public override void FreshFace(float[] blendshapes)
        {
            bsValues = blendshapes.Select(f => f.ToString("0.000")).ToArray();
        }

        public override void SetupSKMRs()
        {
            _mapCache = BSMapSO.RecordSkMRMapCache(SKMRs);
            Debug.Log("Record Cache");
        }
    }
}