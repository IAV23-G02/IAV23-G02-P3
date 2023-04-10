using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class GhostDropSinger : Action
{
    NavMeshAgent agent;
    GameObject singer;

    public override void OnAwake()
    {
        // IMPLEMENTAR
        singer = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>().singer;
        agent = GetComponent<NavMeshAgent>();
    }

    public override TaskStatus OnUpdate()
    {
        //Si no tenemos a la cantante en nuestro poder, no la podemos soltar
        if (!singer.GetComponent<Cantante>().capturada)
            return TaskStatus.Failure;

        // IMPLEMENTAR
        agent.SetDestination(transform.position);

        singer.transform.SetParent(null);
        singer.GetComponent<NavMeshAgent>().enabled = true;
        singer.GetComponent<Cantante>().capturada = false;
        singer.GetComponent<NavMeshAgent>().Warp(singer.transform.position);
        return TaskStatus.Success;
    }
}
