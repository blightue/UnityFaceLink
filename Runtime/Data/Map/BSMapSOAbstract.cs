using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FaceLink.Data
{
    public abstract class BSMapSOAbstract<T> : ScriptableObject, IBSPair
    {
        [SerializeField] protected BSPairBase<T>[] _pairs;

        protected Dictionary<string, T> mapCache;
        
        public Dictionary<string, T> BSMap => mapCache ??= ToDictionary();

        protected virtual void Reset()
        {
            ResetPairs();
        }

        public void ResetPairs()
        {
            int length = FaceLinkData.ARKITBSNAMES.Length;
            _pairs = new BSPairBase<T>[length];
            for (int i = 0; i < length; i++)
            {
                _pairs[i] = new BSPairBase<T>()
                {
                    ARKitName = FaceLinkData.ARKITBSNAMES[i]
                };
            }
        }

        protected virtual Dictionary<string, T> ToDictionary()
        {
            return _pairs.ToDictionary(pair => pair.ARKitName, pair => pair.Target);
        }
    }

    public interface IBSPair
    {
        void ResetPairs();
    }

    [Serializable]
    public struct BSPairBase<T>
    {
        public string ARKitName;
        public T Target;
    }
}