using System;
using System.Collections.Generic;

namespace FaceLink.Editor.iClone
{
    [Serializable]
    public class iCloneBSMapModel
    {
        public string device;
        public string[] expression;
        public Mapping mapping;
        public string type;
        public string version;
    }

    [Serializable]
    public class Mapping
    {
        public float[] browInnerUp;
        public float[] browDown_L;
        public float[] browDown_R;
        public float[] browOuterUp_L;
        public float[] browOuterUp_R;
        public float[] eyeLookUp_L;
        public float[] eyeLookUp_R;
        public float[] eyeLookDown_L;
        public float[] eyeLookDown_R;
        public float[] eyeLookOut_L;
        public float[] eyeLookIn_L;
        public float[] eyeLookIn_R;
        public float[] eyeLookOut_R;
        public float[] eyeBlink_L;
        public float[] eyeBlink_R;
        public float[] eyeSquint_L;
        public float[] eyeSquint_R;
        public float[] eyeWide_L;
        public float[] eyeWide_R;
        public float[] cheekPuff;
        public float[] cheekSquint_L;
        public float[] cheekSquint_R;
        public float[] noseSneer_L;
        public float[] noseSneer_R;
        public float[] jawOpen;
        public float[] jawForward;
        public float[] jawLeft;
        public float[] jawRight;
        public float[] mouthFunnel;
        public float[] mouthPucker;
        public float[] mouthLeft;
        public float[] mouthRight;
        public float[] mouthRollUpper;
        public float[] mouthRollLower;
        public float[] mouthShrugUpper;
        public float[] mouthShrugLower;
        public float[] mouthClose;
        public float[] mouthSmile_L;
        public float[] mouthSmile_R;
        public float[] mouthFrown_L;
        public float[] mouthFrown_R;
        public float[] mouthDimple_L;
        public float[] mouthDimple_R;
        public float[] mouthUpperUp_L;
        public float[] mouthUpperUp_R;
        public float[] mouthLowerDown_L;
        public float[] mouthLowerDown_R;
        public float[] mouthPress_L;
        public float[] mouthPress_R;
        public float[] mouthStretch_L;
        public float[] mouthStretch_R;
        public float[] tongueOut;
        public float[] head_Up;
        public float[] head_Down;
        public float[] head_Left;
        public float[] head_Right;
        public float[] head_LeftTilt;
        public float[] head_RightTilt;
    }
}