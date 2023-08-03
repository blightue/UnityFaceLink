using System;
using UnityEngine;

namespace FaceLink.Data
{
    [CreateAssetMenu(menuName = "FaceLink/Map/BS Multi-StrengthMap", fileName = "New BSMulti-StrengthMap Asset", order = 2)]
    public class BSMultStrengthMapSO: BSMapSOBase<BSStrength[]>
    {
        
    }

    [Serializable]
    public struct BSStrength
    {
        public string BSName;
        public float Strength;
    }
}