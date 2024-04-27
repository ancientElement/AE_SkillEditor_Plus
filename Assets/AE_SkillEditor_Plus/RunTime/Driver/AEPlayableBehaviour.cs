using AE_SkillEditor_Plus.RunTime;
using UnityEngine;

namespace AE_SkillEditor_Plus.RunTime.Driver
{
    public class AEPlayableBehaviour
    {
        public AEPlayableStateEnum State { get; protected set; }

        public AEPlayableBehaviour(StandardClip clip)
        {
        }

        public virtual void OnEnter(GameObject context, int currentFrameID)
        {
            State = AEPlayableStateEnum.Running;
            // Debug.LogWarning("OnEnter");
        }

        public virtual void Tick(int currentFrameID, int fps, GameObject context)
        {
            // if (State != AEPlayableStateEnum.Running) return;
            // Debug.Log("OnUpdate  "  + currentFrameID);
        }

        public virtual void OnExit(GameObject context, int currentFrameID)
        {
            State = AEPlayableStateEnum.Exit;
            // Debug.LogWarning("OnExit");
        }
    }
}