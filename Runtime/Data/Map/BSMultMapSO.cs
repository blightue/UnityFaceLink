using System;
using UnityEngine;

namespace FaceLink.Data
{
    [CreateAssetMenu(fileName = "New BSMulti-Map Asset", menuName = "FaceLink/Map/BS Multi-Map", order = 1)]
    public class BSMultMapSO : BSMapSOAbstract<string[], BSMapCache>
    {
        public override BSMapCache RecordSkMRMapCache(SkinnedMeshRenderer[] targetSkMRs)
        {
            BSSkMRPairsCache[] result = new BSSkMRPairsCache[targetSkMRs.Length];
            Array.Fill(result, new BSSkMRPairsCache()
            {
                ARkit2SkMRIndiceMap = new int[FaceLinkData.ARKITBSCOUNT],
            });

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

            return new BSMapCache() { PairsCaches = result };
        }
    }
}