using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using UnityEditor;

/// <summary>
/// Devuelve Succes si el fantasma ha visto a la cantante dentro del rango de tiempo especificado. Failure en otro caso.
/// </summary>
public class GhostRecentlySawSinger : Conditional
{
    public float tiempoMaximo;

    GhostBehaviour ghostBehaviour;

    public override void OnAwake()
    {
        ghostBehaviour= GetComponent<GhostBehaviour>();
    }

    public override TaskStatus OnUpdate()
    {
        if(ghostBehaviour.getTimeSinceGhostSawTheSinger() < tiempoMaximo)
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }
}
