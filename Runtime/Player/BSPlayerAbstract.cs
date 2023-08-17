using System;
using FaceLink.Data;
using UnityEngine;

namespace FaceLink.Player
{
    public abstract class BSPlayerAbstract : MonoBehaviour, IBSPlayable
    {
        public abstract SkinnedMeshRenderer[] SKMRs { get; }
        public abstract IBSSource Source { get; }
        public bool IsBSUpdated { get; set; }

        protected abstract BSMapSOAbstract BSMapSO { get; }

        protected virtual void Start()
        {
            InitPlayer();
        }

        protected virtual void Update()
        {
            if(IsBSUpdated) FreshFace();
        }

        public virtual void Play()
        {
            Source.Play();
        }

        public virtual void Stop()
        {
            Source.Stop(); 
        }

        public virtual void Pause()
        {
            Source.Pause();
        }
        
        public abstract void FreshBSData(float[] blendshapes);
        public abstract void FreshFace();
        public abstract void SetupSKMRs();

        public virtual void InitPlayer()
        {
            if(Source != null) Source.RegisterBSChanged(FreshBSData);
        }
        
    }

    public interface IBSPlayable
    {
        SkinnedMeshRenderer[] SKMRs { get; }
        
        IBSSource Source { get; }

        void FreshBSData(float[] blendshapes);
    }
}