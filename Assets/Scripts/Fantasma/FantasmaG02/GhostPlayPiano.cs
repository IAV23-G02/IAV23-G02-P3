using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class GhostPlayPiano : Action
{
    AudioSource pianoSound;


    public override void OnAwake()
    {
        pianoSound= GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>().piano.GetComponent<AudioSource>();
        bonkSound = GetComponent<AudioSource>();
    }

    //Se llama al entrar al nodo
    public override void OnStart()
    {
        if (pianoSound != null)
        {
            pianoSound.Play();
        }
    }

    //Se llama cuando la tarea es interrumpida por un conditional abort
    public override void OnConditionalAbort()
    {
        if (pianoSound != null && pianoSound.isPlaying) 
        {
            pianoSound.Stop();
        }
    }
}
