﻿using System;
using UnityEngine;

namespace FaceLink.Data
{
    public abstract class BSSourceBase: ScriptableObject, IBSSource
    {
        protected Action<float[]> OnBSChanged;

        public abstract void Play();
        public abstract void Stop();
        public abstract void Pause();

        public virtual bool RegisterBSChanged(Action<float[]> onBSChangedAction)
        {
            OnBSChanged += onBSChangedAction;
            return true;
        }
    }
    
    public interface IBSSource
    {
        void Play();
        void Stop();
        void Pause();

        bool RegisterBSChanged(Action<float[]> onBSChangedAction);
    }
}