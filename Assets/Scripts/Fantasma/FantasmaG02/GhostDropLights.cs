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

    [SerializeField] bool publicoWest;
    [SerializeField] bool publicoEast;

    ControlPalanca palancaEast;
    ControlPalanca palancaWest;

    public override void OnAwake()
    {
        // IMPLEMENTAR
        blackboard = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>();
        agent = GetComponent<NavMeshAgent>();
        palancaEast = blackboard.eastLever.GetComponentInChildren<ControlPalanca>();
        palancaWest = blackboard.westLever.GetComponentInChildren<ControlPalanca>();
    }

    public override TaskStatus OnUpdate()
    {
        //Hay que tener en cuenta también qué focos están activos
        publicoEast = palancaEast.caido;
        publicoWest = palancaWest.caido;
        // IMPLEMENTAR
        agent.SetDestination(selectPalanca());
        if(publicoWest && publicoEast)
            return TaskStatus.Success;

        return TaskStatus.Running;
    }

    //pequela máquina de estados ad hoc que ahorra nodos al behavior tree
    private Vector3 selectPalanca()
    {
        //Si están las dos palancas activadas
        if(!publicoWest && !publicoEast)
        {
            //escogemos la palanca más cercana
            if ((palancaWest.transform.position - agent.transform.position).magnitude > (palancaEast.transform.position - agent.transform.position).magnitude)
            {
                return palancaEast.transform.position;
            }
            else
                return palancaWest.transform.position;
        }
        else if(!publicoWest)
            return palancaWest.transform.position;
        else 
            return palancaEast.transform.position;
    }
}
