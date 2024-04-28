using AE_SkillEditor_Plus.RunTime.Driver;
using UnityEngine;

namespace AE_SkillEditor_Plus.RunTime.BuiltTracks.Effect
{
    public class AEEffectBehaviour : AEPlayableBehaviour
    {
        public AEEffectClip Clip;
        public GameObject temp;

        public AEEffectBehaviour(StandardClip clip) : base(clip)
        {
            Clip = clip as AEEffectClip;
        }

        public override void OnEnter(GameObject context, int currentFrameID)
        {
            base.OnEnter(context, currentFrameID);
            if (Clip.Prefab == null) return;
            if (context == null) return;
            var position = context.transform.TransformPoint(Clip.Position);
            var rotation = Quaternion.Euler(Clip.Rotarion);
            temp = GameObject.Instantiate(Clip.Prefab, position, rotation);
        }

        public override void Tick(int currentFrameID, int fps, GameObject context)
        {
            base.Tick(currentFrameID, fps, context);
            if (context == null) return;
            var position = context.transform.TransformPoint(Clip.Position);
            temp.transform.position = position;
        }

        public override void OnExit(GameObject context, int currentFrameID)
        {
            base.OnExit(context, currentFrameID);
            if (temp != null) GameObject.DestroyImmediate(temp);
        }
    }
}