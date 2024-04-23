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

        public virtual void OnEnter()
        {
            State = AEPlayableStateEnum.Running;
            // Debug.LogWarning("OnEnter");
        }

        public virtual void Tick(int currentFrameID, int fps)
        {
            if (State != AEPlayableStateEnum.Running) return;
            // Debug.Log("OnUpdate  "  + currentFrameID);
        }

        public virtual void OnExit()
        {
            State = AEPlayableStateEnum.Exit;
            // Debug.LogWarning("OnExit");
        }
    }
}