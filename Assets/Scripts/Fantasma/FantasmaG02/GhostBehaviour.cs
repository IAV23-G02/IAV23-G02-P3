using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    [Range(0, 180)]
    // Angulo de vision en horizontal
    public double anguloVistaHorizontal;
    // Distancia maxima de vision
    public double distanciaVista;

    public Transform objetivo;
    public GameBlackboard blackboard;

    //Temporizador que indica cuanto tiempo lleva el fantasma sin ver a la cantante
    private float singerNotSeenTimer;

    private bool cantanteVista;
    private bool thinkSingerImprisoned;



    // Start is called before the first frame update
    void Start()
    {
        cantanteVista = false;
        thinkSingerImprisoned = false;
        singerNotSeenTimer = 1000;
    }

    private void Update()
    {
        singerNotSeenTimer += Time.deltaTime;
        //Si ha visto a la cantante fuera de la celda, cambiamos el booleano.
        if(Scan() && !blackboard.imprisoned && !thinkSingerImprisoned) { 
            thinkSingerImprisoned = false;
        }
    }

    public bool Scan()
    {
        Vector3 playerVector = objetivo.transform.position - transform.position;
        if (Vector3.Angle(playerVector.normalized, transform.forward) < anguloVistaHorizontal * 0.5
            && playerVector.magnitude < distanciaVista)
        {
            singerNotSeenTimer = 0;
            cantanteVista = true;
            return true;
        }
        else
        {
            cantanteVista = false;
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        double halfVisionAngle = anguloVistaHorizontal * 0.5;

        Vector3 p1, p2;

        p1 = PointForAngle((float)halfVisionAngle, (float)distanciaVista, 90);
        p2 = PointForAngle((float)-halfVisionAngle, (float)distanciaVista, 90);

        Gizmos.color = cantanteVista ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + p1);
        Gizmos.DrawLine(transform.position, transform.position + p2);

        Gizmos.DrawRay(transform.position, transform.forward * 3f);
    }

    private Vector3 PointForAngle(float angle, float distance, float orientation)
    {
        return transform.TransformDirection(
            new Vector3(Mathf.Cos((angle + orientation) * Mathf.Deg2Rad), 0, Mathf.Sin((angle + orientation) * Mathf.Deg2Rad))
            * distance);
    }

    /// <summary>
    /// Devuelve el tiempo que lleva el fantasma sin ver a la cantante
    /// </summary>
    /// <returns></returns>
    public float getTimeSinceGhostSawTheSinger()
    {
        return singerNotSeenTimer;
    }

    public bool ISeeTheTarget() {
        return cantanteVista;
    }

    public bool thinksSingerIsImprisoned()
    {
        return thinkSingerImprisoned;
    }

    public void setGhostThinksSingerIsImprisoned(bool b)
    {
        thinkSingerImprisoned = b;
    }
}
