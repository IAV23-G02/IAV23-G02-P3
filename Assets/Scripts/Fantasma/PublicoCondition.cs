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

    ControlPalanca palancaEast;
    ControlPalanca palancaWest;

    public override void OnAwake()
    {
        blackboard = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>();
        palancaEast = blackboard.eastLever.GetComponentInChildren<ControlPalanca>();
        palancaWest = blackboard.westLever.GetComponentInChildren<ControlPalanca>();
    }

    public override TaskStatus OnUpdate()
    {
        publicoEast = palancaEast.caido;
        publicoWest = palancaWest.caido;

        if(!(publicoEast && publicoWest))
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }
}
