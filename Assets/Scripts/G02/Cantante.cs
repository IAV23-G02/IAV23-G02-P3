using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Cantante : MonoBehaviour
{
    // Segundos que estara cantando
    public double tiempoDeCanto;
    // Segundo en el que comezo a cantar
    private double tiempoComienzoCanto;
    // Segundos que esta descanasando
    public double tiempoDeDescanso;
    // Segundo en el que comezo a descansar
    private double tiempoComienzoDescanso;
    // Si esta capturada
    public bool capturada = false;

    [Range(0, 180)]
    // Angulo de vision en horizontal
    public double anguloVistaHorizontal;
    // Distancia maxima de vision
    public double distanciaVista;
    // Objetivo al que ver"
    public Transform objetivo;

    // Segundos que puede estar merodeando
    public double tiempoDeMerodeo = 7;
    // Segundo en el que comezo a merodear
    public double tiempoComienzoMerodeo = 0;
    // Distancia de merodeo
    public int distanciaDeMerodeo = 16;
    // Si canta o no
    public bool cantando = false;

    // Componente cacheado NavMeshAgent
    private NavMeshAgent agente;

    // Objetivos de su itinerario
    public Transform Escenario;
    public Transform Bambalinas;

    // La blackboard
    public GameBlackboard bb;

    //para seguir al fantasma o al vizconde
    public GameObject fantasma;
    public GameObject vizconde;

    //Variable de uso exclusivo para el Gizmos
    bool jugadorVisto = false;

    public void Awake()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    public void Start()
    {
        agente.updateRotation = false;
    }

    private void Update()
    {
        tiempoComienzoMerodeo+= Time.deltaTime;
        Scan();
    }

    public void LateUpdate()
    {
        if (agente.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(agente.velocity.normalized);
        }
    }

    // Comienza a cantar, reseteando el temporizador
    public void Cantar()
    {
        tiempoComienzoCanto = 0;
        cantando = true;
    }

    // Comprueba si tiene que dejar de cantar
    public bool TerminaCantar()
    {
        // IMPLEMENTAR
        nuevoObjetivo(Bambalinas.gameObject);
        cantando = false;
        return true;
    }

    // Comienza a descansar, reseteando el temporizador
    public void Descansar()
    {
   
    }

    // Comprueba si tiene que dejar de descansar
    public bool TerminaDescansar()
    {
        // IMPLEMENTAR
        nuevoObjetivo(Escenario.gameObject);
        return true;
    }

    // Comprueba si se encuentra en la celda
    public bool EstaEnCelda()
    {
        // IMPLEMENTAR
        return true;
    }

    // Comprueba si esta en un sitio desde el cual sabe llegar al escenario
    public bool ConozcoEsteSitio()
    {
        
        // IMPLEMENTAR
        return true;
    }

    

    //Mira si ve al vizconde con un angulo de vision y una distancia maxima
    public bool Scan()
    {
        // IMPLEMENTAR
        Vector3 playerVector = vizconde.transform.position - transform.position;    
        if(Vector3.Angle(playerVector.normalized, -transform.forward) < anguloVistaHorizontal * 0.5
            && playerVector.magnitude < distanciaVista)
        {
            jugadorVisto = true;
            return true;
        }
        else
        {
            jugadorVisto = false;
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        double halfVisionAngle = anguloVistaHorizontal * 0.5;

        Vector3 p1, p2;

        p1 = PointForAngle((float)halfVisionAngle, (float)distanciaVista, -90);
        p2 = PointForAngle((float)-halfVisionAngle, (float)distanciaVista, -90);

        Gizmos.color = jugadorVisto? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + p1);
        Gizmos.DrawLine(transform.position, transform.position + p2);

        Gizmos.DrawRay(transform.position, -transform.forward * 3f);
    }

    private Vector3 PointForAngle(float angle, float distance, float orientation)
    {
        return transform.TransformDirection( 
            new Vector3(Mathf.Cos((angle + orientation) * Mathf.Deg2Rad), 0, Mathf.Sin((angle + orientation) * Mathf.Deg2Rad))
            * distance );          
    }

    // Genera una posicion aleatoria a cierta distancia dentro de las areas permitidas
    private Vector3 RandomNavSphere(float distance) 
    {
        
        Vector2 merRadius = Random.insideUnitCircle.normalized;
        merRadius *= distance;
        return transform.position + new Vector3(merRadius.x, transform.position.y, merRadius.y);
    }

    // Genera un nuevo punto de merodeo cada vez que agota su tiempo de merodeo actual
    public void IntentaMerodear()
    {
        if(tiempoComienzoMerodeo > tiempoDeMerodeo)
        {
            float distance = Random.Range(0, (float)distanciaDeMerodeo);
            bool merodeoExitoso = false;
            //Número de intentos en el que busca un sitio al que merodear
            for (int i = 0; i < 20 && !merodeoExitoso; i++)
            {
                merodeoExitoso = agente.SetDestination(RandomNavSphere(distance));
            }
            ResetMerodeoTimer();
        }
    }
    public void ResetMerodeoTimer()
    {
        tiempoComienzoMerodeo = 0;
    }

    public bool GetCapturada()
    {
        return capturada;
    }

    public void setCapturada(bool cap)
    {
        capturada= cap;
    }

    public GameObject sigueFantasma()
    {
        // IMPLEMENTAR
        return null;
    }

    public void sigueVizconde()
    {
        agente.SetDestination(vizconde.transform.position);
    }

    private void nuevoObjetivo(GameObject obj)
    {
        agente.SetDestination(obj.transform.position);
    }

}
