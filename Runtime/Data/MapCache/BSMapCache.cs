using System;
using System.Collections.Generic;
using UnityEngine;

namespace FaceLink.Data
{
    public abstract class BSMapCacheAbstract
    {
        public abstract IEnumerable<BSSkMRPairsCache> PairsCaches { get; }
        public abstract void EvaluateBS(float[] bsvalues, SkinnedMeshRenderer[] targets);
    }

    [Serializable]
    public class BSMapCache : BSMapCacheAbstract
    {
        public override IEnumerable<BSSkMRPairsCache> PairsCaches => _pairsCaches;
        public BSSkMRPairsCache[] _pairsCaches;

        public override void EvaluateBS(float[] bsvalues, SkinnedMeshRenderer[] targets)
        {
            if (_pairsCaches.Length != targets.Length) return;
            for (int i = 0; i < _pairsCaches.Length; i++)
            {
                BSSkMRPairsCache cache = _pairsCaches[i];
                SkinnedMeshRenderer renderer = targets[i];
                for (var j = 0; j < bsvalues.Length; j++)
                {
                    int bsIndex = cache.ARkit2SkMRIndiceMap[j];
                    if (bsIndex == -1) continue;
                    renderer.SetBlendShapeWeight(bsIndex, bsvalues[j] * 100f);
                }
            }
        }
    }

    [Serializable]
    public class BSSkMRPairsCache
    {
        public int[] ARkit2SkMRIndiceMap;
    }

    [Serializable]
    public class BSStrengthMapCache : BSMapCacheAbstract
    {
        public override IEnumerable<BSSkMRPairsCache> PairsCaches => _pairsCaches;
        public BSSkMRStrengthPairsCache[] _pairsCaches;

        public override void EvaluateBS(float[] bsvalues, SkinnedMeshRenderer[] targets)
        {
            if (_pairsCaches.Length != targets.Length) return;
            for (int i = 0; i < _pairsCaches.Length; i++)
            {
                BSSkMRStrengthPairsCache cache = _pairsCaches[i];
                SkinnedMeshRenderer renderer = targets[i];
                for (var j = 0; j < bsvalues.Length; j++)
                {
                    int bsIndex = cache.ARkit2SkMRIndiceMap[j];
                    if (bsIndex == -1) continue;
                    float strength = cache.ARKit2SkMRStrengthMap[j];
                    renderer.SetBlendShapeWeight(bsIndex, bsvalues[j] * strength * 100f);
                }
            }
        }
    }

    [Serializable]
    public class BSSkMRStrengthPairsCache : BSSkMRPairsCache
    {
        public float[] ARKit2SkMRStrengthMap;
    }
}