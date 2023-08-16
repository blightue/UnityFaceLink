using System;
using System.Collections.Generic;

namespace FaceLink.Data
{
    public abstract class BSMapCacheAbstract
    {
        public abstract IEnumerable<BSSkMRPairsCache> PairsCaches { get; }
    }
    
    [Serializable]
    public class BSMapCache: BSMapCacheAbstract
    {
        public override IEnumerable<BSSkMRPairsCache> PairsCaches => _pairsCaches;
        public BSSkMRPairsCache[] _pairsCaches;
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
    }

    [Serializable]
    public class BSSkMRStrengthPairsCache : BSSkMRPairsCache
    {
        public float[] ARKit2SkMRStrengthMap;
    }
}