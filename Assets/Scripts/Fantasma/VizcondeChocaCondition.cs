/*    
   Copyright (C) 2020-2023 Federico Peinado
   http://www.federicopeinado.com
   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).
   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;
using Unity.VisualScripting;

/*
 * Devuelve Success cuando el fantasma choca con el Vizco
 */


public class VizcondeChocaCondition : Conditional
{
    GameObject Vizconde;
    NavMeshAgent agent;

    CapsuleCollider cc;
    bool golpeado;

    public override void OnAwake()
    {
        // IMPLEMENTAR 
        agent = GetComponent<NavMeshAgent>();
        Vizconde = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>().player;
        golpeado = false;
    }

    public override TaskStatus OnUpdate()
    {
        // IMPLEMENTAR
        //Para que solo cuente un golpeo:
        if((Vizconde.transform.position - transform.position).magnitude < 1.2)
        {
            golpeado = true;
            return TaskStatus.Success;
        }
        golpeado = false;
        return TaskStatus.Failure;
    }

}
