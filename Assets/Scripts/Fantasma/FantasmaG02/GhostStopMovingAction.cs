using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class GhostStopMovingAction : Action
{
    NavMeshAgent agent;

    public override void OnAwake()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    public override TaskStatus OnUpdate()
    {
        //Si no tenemos a la cantante en nuestro poder, no la podemos soltar
        agent.SetDestination(transform.position);
        return TaskStatus.Success;
    }
}
