﻿using System;
using UnityEngine;

namespace FaceLink.Data
{
    [CreateAssetMenu(menuName = "FaceLink/Map/BS Multi-StrengthMap", fileName = "New BSMulti-StrengthMap Asset", order = 2)]
    public class BSMultStrengthMapSO: BSMapSOAbstract<BSStrength[], BSStrengthMapCache>
    {
        public override BSStrengthMapCache RecordSkMRMapCache(SkinnedMeshRenderer[] targetSkMRs)
        {
            BSSkMRStrengthPairsCache[] result = new BSSkMRStrengthPairsCache[targetSkMRs.Length];
            Array.Fill(result, new BSSkMRStrengthPairsCache()
            {
                ARkit2SkMRIndiceMap = new int[FaceLinkData.ARKITBSCOUNT],
                ARKit2SkMRStrengthMap = new float[FaceLinkData.ARKITBSCOUNT],
            });

            for (int i = 0; i < targetSkMRs.Length; i++)
            {
                for (int j = 0; j < _pairs.Length; j++)
                {
                    BSStrength[] bsStrengths = _pairs[j].Target;
                    foreach (BSStrength bss in bsStrengths)
                    {
                        int skBSIndex = targetSkMRs[i].sharedMesh.GetBlendShapeIndex(bss.BSName);
                        result[i].ARkit2SkMRIndiceMap[j] = skBSIndex;
                        result[i].ARKit2SkMRStrengthMap[j] = bss.Strength;
                    }
                }
            }

            return new BSStrengthMapCache() { PairsCaches = result };
        }
    }

    [Serializable]
    public struct BSStrength
    {
        public string BSName;
        public float Strength;
    }
}