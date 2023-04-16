using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using UnityEditor;

//Devuelve �xito si el fantasma est� viendo a la cantante 
public class GhostSeeSinger : Conditional
{
    public float tiempoMaximo;

    GhostBehaviour ghostBehaviour;

    public override void OnAwake()
    {
        ghostBehaviour = GetComponent<GhostBehaviour>();
    }

    public override TaskStatus OnUpdate()
    {
        if (ghostBehaviour.ISeeTheTarget())
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }
}
