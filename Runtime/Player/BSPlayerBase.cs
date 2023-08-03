using FaceLink.Data;
using UnityEngine;

namespace FaceLink.Player
{
    public abstract class BSPlayerBase : MonoBehaviour, IBSPlayable
    {

        public abstract SkinnedMeshRenderer[] SKMRs { get; }
        public abstract IBSSource Source { get; }

        protected virtual void Start()
        {
            InitPlayer();
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
        
        public abstract void FreshFace(float[] blendshapes);
        protected abstract void SetupSKMRs();

        public virtual void InitPlayer()
        {
            if(Source != null) Source.RegisterBSChanged(FreshFace);
        }
        
    }

    public interface IBSPlayable
    {
        SkinnedMeshRenderer[] SKMRs { get; }
        
        IBSSource Source { get; }

        void FreshFace(float[] blendshapes);
    }
}