using System;
using System.Linq;
using FaceLink.Data;
using UnityEngine;

namespace FaceLink.Player
{
    public class BSStreamPlayer: BSPlayerBase
    {
        public override SkinnedMeshRenderer[] SKMRs => _skmrs;
        public override IBSSource Source => _source;

        [SerializeField] private SkinnedMeshRenderer[] _skmrs;
        [SerializeField] private BSUDPStreamSource _source;
        [SerializeField] private string[] bsValues;
        public override void FreshFace(float[] blendshapes)
        {
            bsValues = blendshapes.Select(f => f.ToString("0.000")).ToArray();
            Debug.Log(blendshapes.Length);
        }

        protected override void SetupSKMRs()
        {
            Debug.Log("InitFace");
        }
    }
}