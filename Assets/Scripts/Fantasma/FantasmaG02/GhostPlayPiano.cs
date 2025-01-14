using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class GhostPlayPiano : Action
{
    AudioSource pianoSound;


    public override void OnAwake()
    {
        pianoSound= GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>().piano.GetComponent<AudioSource>();
    }

    //Se llama al entrar al nodo
    public override void OnStart()
    {
        if (pianoSound != null && !pianoSound.isPlaying)
        {
            pianoSound.Play();
        }
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        if (pianoSound != null && pianoSound.isPlaying) 
        {
            pianoSound.Stop();
        }
    }
}
