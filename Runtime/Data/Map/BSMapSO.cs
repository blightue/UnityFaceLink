using System;
using UnityEngine;

namespace FaceLink.Data
{
    [CreateAssetMenu(menuName = "FaceLink/Map/BS Map", fileName = "New BSMap Asset", order = 0)]
    public class BSMapSO : BSMapSOAbstractGeneric<string, BSMapCache>
    {


        public override BSMapCache RecordSkMRMapCache(SkinnedMeshRenderer[] targetSkMRs)
        {
            BSSkMRPairsCache[] result = new BSSkMRPairsCache[targetSkMRs.Length];
            Array.Fill(result, new BSSkMRPairsCache(){ARkit2SkMRIndiceMap = new int[FaceLinkData.ARKITBSCOUNT]});

            for (int i = 0; i < targetSkMRs.Length; i++)
            {
                for (int j = 0; j < _pairs.Length; j++)
                {
                    string skName = _pairs[j].Target;
                    int skBSIndex = targetSkMRs[i].sharedMesh.GetBlendShapeIndex(skName);
                    result[i].ARkit2SkMRIndiceMap[j] = skBSIndex;
                }
            }

            return new BSMapCache() { PairsCaches = result };
        }
    }

}