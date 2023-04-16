﻿/*    
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

/*
 * Condicion de si la cantante esta encarcelada
 */

public class ImprisonedCondition : Conditional
{
    GameBlackboard blackboard;
    GhostBehaviour ghostBehaviour;

    public override void OnAwake()
    {
        blackboard = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>();
        ghostBehaviour = GetComponent<GhostBehaviour>();
    }

    public override TaskStatus OnUpdate()
    {
        if (ghostBehaviour.thinksSingerIsImprisoned())
            return TaskStatus.Success;

        return TaskStatus.Failure;
    }
}