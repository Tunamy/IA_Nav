using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemigo : MonoBehaviour
{

    public Animator anim;
    public Vector3 min, max;
    Vector3 destino;
    
    bool playerDetected = false;

    public float distanciaDeteccion;
    public GameObject jugador;

    void Start()
    {
        DestinoAleatorio();
        StartCoroutine(Patrulla());
        StartCoroutine(Alerta());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestinoAleatorio()
    {
       destino = new Vector3(Random.Range(min.x, max.x), 1, Random.Range(min.z, max.z));
       GetComponent<NavMeshAgent>().SetDestination(destino);
       anim.SetFloat("run", 1);

    }

    IEnumerator Patrulla()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, destino) < 3.5f)
            {
                anim.SetFloat("run", 0);
                yield return new WaitForSeconds(Random.Range(1,3));
                DestinoAleatorio();
            }
            yield return new WaitForSeconds(1);
        }
        
    }

    // deteccion por distancia
    IEnumerator Alerta()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, jugador.transform.position) < distanciaDeteccion)
            {
                StopCoroutine(Patrulla());
                GetComponent<NavMeshAgent>().SetDestination(jugador.transform.position);

            }
            yield return new WaitForSeconds(1);
        }
    }


    //detecion trigger - collider

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerDetected = true;
            StopCoroutine(Patrulla());
            //transform.LookAt(other.transform);
            anim.SetFloat("run", 1);
            GetComponent<NavMeshAgent>().SetDestination(other.transform.position);


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerDetected = false;
            DestinoAleatorio();
            StartCoroutine(Patrulla());


        }
    }
}

