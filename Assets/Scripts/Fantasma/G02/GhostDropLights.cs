using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using UnityEditor.Experimental.GraphView;

public class GhostDropLights : Action
{
    NavMeshAgent agent;
    GameBlackboard blackboard;

    public override void OnAwake()
    {
        // IMPLEMENTAR
        blackboard = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>();
        agent = GetComponent<NavMeshAgent>();
    }

    public override TaskStatus OnUpdate()
    {
        // IMPLEMENTAR
        agent.SetDestination(blackboard.westLever.transform.position);
        if(agent.remainingDistance < 0.5 )
            return TaskStatus.Success;

        return TaskStatus.Running;
    }
}
