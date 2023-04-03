using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class ImprisonSingerAction : Action
{
    GameObject jailRoom;
    GameBlackboard blackboard;
    NavMeshAgent agent;
    GameObject singer; 
    bool onMyWay = false;

    public override void OnAwake()
    {
        // IMPLEMENTAR
        blackboard = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>();
        jailRoom = blackboard.celda;
        agent = GetComponent<NavMeshAgent>();
        singer = blackboard.singer;
    }

    public override TaskStatus OnUpdate()
    {
        
        if(!onMyWay)
        {
            agent.SetDestination(jailRoom.transform.position);
            onMyWay= true;
        }

        //Si llega a la habitación, deja a la cantante en el suelo
        if (Vector3.SqrMagnitude(transform.position - jailRoom.transform.position) < 1.2f)
        {
            GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>().imprisoned = true;
            agent.SetDestination(transform.position);

            singer.GetComponent<NavMeshAgent>().enabled = false;
            singer.GetComponent<Cantante>().capturada = false;
            singer.transform.SetParent(null);
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}
