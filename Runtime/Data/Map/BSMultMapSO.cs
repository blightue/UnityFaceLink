﻿using System;
using UnityEngine;

namespace FaceLink.Data
{
    [CreateAssetMenu(fileName = "New BSMulti-Map Asset", menuName = "FaceLink/Map/BS Multi-Map", order = 1)]
    public class BSMultMapSO : BSMapSOAbstractGeneric<string[]>
    {
        public override BSMapCacheAbstract RecordSkMRMapCache(SkinnedMeshRenderer[] targetSkMRs)
        {
            BSSkMRPairsCache[] result = new BSSkMRPairsCache[targetSkMRs.Length];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = new BSSkMRPairsCache()
                {
                    ARkit2SkMRIndiceMap = new int[FaceLinkData.ARKITBSCOUNT],
                };
            }

            for (int i = 0; i < targetSkMRs.Length; i++)
            {
                for (int j = 0; j < _pairs.Length; j++)
                {
                    string[] bsStrengths = _pairs[j].Target;
                    foreach (string target in bsStrengths)
                    {
                        int skBSIndex = targetSkMRs[i].sharedMesh.GetBlendShapeIndex(target);
                        result[i].ARkit2SkMRIndiceMap[j] = skBSIndex;
                    }
                }
            }

            return new BSMapCache() { _pairsCaches = result };
        }
    }
}