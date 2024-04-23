using System;
using AE_SkillEditor_Plus.RunTime.Driver;

namespace AE_SkillEditor_Plus.RunTime.Interface
{
    public interface IEditorBehaviour
    {
        public AEPlayableBehaviour CreateEditorBehaviour(StandardClip clip);
    }
}