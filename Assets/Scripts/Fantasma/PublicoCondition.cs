/*    
   Copyright (C) 2020-2023 Federico Peinado
   http://www.federicopeinado.com
   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).
   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/

using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicoCondition : Conditional
{
    GameBlackboard blackboard;

    [SerializeField] bool publicoWest;
    [SerializeField] bool publicoEast;

    public override void OnAwake()
    {
        blackboard = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>();
        publicoWest = blackboard.westLever.GetComponent<ControlPalanca>().caido;
        publicoEast = blackboard.eastLever.GetComponent<ControlPalanca>().caido;
    }

    public override TaskStatus OnUpdate()
    {
        if(publicoEast && publicoWest)
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }
}
