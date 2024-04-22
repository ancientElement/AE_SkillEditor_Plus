using System;
using UnityEngine;
using UnityEngine.Playables;

namespace TempAndTest.PlayableTest
{
    public class TestBehaviour : PlayableBehaviour
    {
    }
    
    public class TestPlayable : MonoBehaviour
    {
        private PlayableGraph grapah;
        private TestBehaviour Behaviour;
        private PlayableOutput outPut;
        private void Start()
        {
            grapah = new PlayableGraph();
            ScriptPlayable<TestBehaviour>.Create(grapah);
            ScriptPlayableOutput.Create(grapah,"scriptable");
        }

        private void Update()
        {
            
        }
    }
}