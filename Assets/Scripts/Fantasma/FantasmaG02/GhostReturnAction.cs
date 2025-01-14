﻿/*    
   Copyright (C) 2020-2023 Federico Peinado
   http://www.federicopeinado.com
   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).
   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/

using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

/*
 * Accion de ir a la sala de musica, cuando llega devuelve Success
 */

public class GhostReturnAction : Action
{
    NavMeshAgent agent;
    GameObject musicRoom;
    GameBlackboard blackboard;

    public override void OnAwake()
    {
        blackboard = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>();
        musicRoom = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>().musicRoom;
        agent = GetComponent<NavMeshAgent>();
    }

    public override TaskStatus OnUpdate()
    {
        if (agent.enabled)
        {
            agent.SetDestination(musicRoom.transform.position);
        }

        if (Vector3.SqrMagnitude(transform.position - musicRoom.transform.position) < 1.2f)
        {
            agent.SetDestination(transform.position);

            //return TaskStatus.Success;
            //singerNav.enabled = false;
            //singer.transform.SetParent(null);
            //agent.SetDestination(sotanoNorte.transform.position);

            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}