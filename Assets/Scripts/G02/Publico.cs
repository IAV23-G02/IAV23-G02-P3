using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

/*
 * Se encarga de controlar si el público debería huir o quedarse en el patio de butacas
 */

public class Publico : MonoBehaviour
{
    //int lucesEncendidas = 2;
    bool miLuzEncendida = true;
    bool sentado = true;

    bool escondido = false;

    GameObject luzAsociada;
    [SerializeField] GameObject escondite;
    [SerializeField] GameObject butaca;

    [SerializeField] ControlPalanca foco; 
    
    [SerializeField]
    AudioSource grito;

    NavMeshAgent fantasma;

    private void Start()
    {
        sentado = true;

        GetComponent<NavMeshAgent>().SetDestination(butaca.transform.position);

        foco.focoCaido.AddListener(apagaLuz);
        foco.focoLevantado.AddListener(enciendeLuz);
        fantasma = GameObject.FindGameObjectWithTag("Blackboard").GetComponent<GameBlackboard>().fantasma.GetComponent<NavMeshAgent>();
        //No funciona
        //float coste = fantasma.GetAreaCost(3);
        //fantasma.SetAreaCost(3, coste + 10);
           
    }

    public void LateUpdate()
    {
        // para que rote hacia donde se mueve
        if (GetComponent<NavMeshAgent>().velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(GetComponent<NavMeshAgent>().velocity.normalized);
        }
        else if(miLuzEncendida)  //para que al llegar a su butaca miren hacia delante(el escenario)
            transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public bool getLuces()
    {
        return sentado;
    }

    public void apagaLuz()
    {
        GetComponent<Collider>().enabled = false;

        miLuzEncendida = false;
        sentado = false;

        GetComponent<NavMeshAgent>().SetDestination(escondite.transform.position);

        //No funciona
        //float coste = fantasma.GetAreaCost(3);
        //fantasma.SetAreaCost(3, coste - 10);

        if (!grito.isPlaying)
            grito.Play();
    }

    //se llama cuando el fantasma o el vizconde desactivan o activan las luces
    public void enciendeLuz()
    {
        GetComponent<Collider>().enabled = true;

        miLuzEncendida = true;
        sentado = true;

        GetComponent<NavMeshAgent>().SetDestination(butaca.transform.position);

        if (grito.isPlaying)
            grito.Stop();

        //No funciona
        //float coste = fantasma.GetAreaCost(3);
        //fantasma.SetAreaCost(3, coste + 10);

        // lucesEncendidas++;
        //sentado = lucesEncendidas == 2;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //Le cae el foco en la cabeza
    //    if (collision.gameObject.tag == "Foco")
    //    {
    //        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
    //        if(rb.velocity.y != 0)
    //        {
    //            //Dejamos inconsciente al personaje
    //            gameObject.GetComponent<CapsuleCollider>().enabled = false;
    //            gameObject.GetComponent<NavMeshAgent>().enabled = false;
    //        }
    //    }
    //}

}
