using System;

namespace FaceLink.Data
{
    [Serializable]
    public class BSMapCache
    {
        public BSSkMRPairsCache[] PairsCaches;
    }

    [Serializable]
    public class BSSkMRPairsCache
    {
        public int[] ARkit2SkMRIndiceMap;
    }

    [Serializable]
    public class BSStrengthMapCache : BSMapCache
    {
        public new BSSkMRStrengthPairsCache[] PairsCaches;
    }

    [Serializable]
    public class BSSkMRStrengthPairsCache : BSSkMRPairsCache
    {
        public float[] ARKit2SkMRStrengthMap;
    }
}