using AE_SkillEditor_Plus.RunTime;
using AE_SkillEditor_Plus.RunTime.Driver;
using UnityEngine;

namespace AE_SkillEditor_Plus.Example.BuiltTracks.Effect
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
            //设置随机种子
            var particleSystems = Clip.Prefab.GetComponentsInChildren<ParticleSystem>();
            foreach (var item in particleSystems)
            {
                item.randomSeed = (uint)Clip.SeedRandom;
            }

            temp = GameObject.Instantiate(Clip.Prefab, position, rotation);
        }

        public override void Tick(int currentFrameID, int fps, GameObject context)
        {
            // Debug.Log(Clip.Prefab?.name);
            base.Tick(currentFrameID, fps, context);
            if (context == null) return;
            if (temp == null) return;
            if (Clip.Follow)
            {
                var position = context.transform.TransformPoint(Clip.Position);
                temp.transform.position = position;
            }

            ParticleSystem[] particleSystems = temp.GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < particleSystems.Length; i++)
            {
                ParticleSystem.MainModule ps = particleSystems[i].main;
                ps.loop = false; //禁止循环
                int simulateFrame = currentFrameID - Clip.StartID;
                particleSystems[i].Simulate((float)simulateFrame / fps, true);
            }
        }

        public override void OnExit(GameObject context, int currentFrameID)
        {
            base.OnExit(context, currentFrameID);
#if UNITY_EDITOR
            if (temp != null) GameObject.DestroyImmediate(temp);
#else
            if (temp != null) GameObject.Destroy(temp);
#endif
        }
    }
}