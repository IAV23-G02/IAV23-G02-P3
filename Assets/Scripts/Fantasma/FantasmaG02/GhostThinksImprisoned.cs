
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;



/*
 * Condicion de si el fantasma cree que la cantante esta encarcelada
 */
public class GhostThinksImprisoned : Conditional
{
    GameBlackboard blackboard;
    GhostBehaviour ghostBehaviour;

    public override void OnAwake()
    {
        blackboard = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>();
        ghostBehaviour = GetComponent<GhostBehaviour>();
    }

    public override TaskStatus OnUpdate()
    {
        if (ghostBehaviour.thinksSingerIsImprisoned())
            return TaskStatus.Success;

        return TaskStatus.Failure;
    }
}
