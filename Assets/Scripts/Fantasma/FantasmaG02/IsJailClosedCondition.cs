using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Condicion de si la puerta de la cárcel está cerrada
 */

public class IsJailClosedCondition : Conditional
{
    GameBlackboard blackboard;

    public override void OnAwake()
    {
        blackboard = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>();
    }

    public override TaskStatus OnUpdate()
    {
        if (blackboard.gate)
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }
}
