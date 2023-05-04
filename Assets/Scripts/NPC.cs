using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public Transform ruta;
    public Transform jugador;
    int indiceHijos;
    Vector3 destino;

    
    public float speedIncreasePerSecond = 0.1f;
    public NavMeshAgent agent;


    public Vector3 min, max;

    private void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        //destino = ruta.GetChild(indiceHijos).position;
        //destino = DestinoAleatorio();
        //GetComponent<NavMeshAgent>().SetDestination(destino);


        
    }

    

    void Update()
    {
        UpdateSpeed();

        destino = jugador.position;
        GetComponent<NavMeshAgent>().SetDestination(destino);

        //MOVIMIENTO POR CLICK

        //if (Input.GetButtonDown("Fire1"))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit = new RaycastHit();

        //    if(Physics.Raycast(ray, out hit, 1000))
        //    {

        //        GetComponent<NavMeshAgent>().SetDestination(hit.point);

        //    }
        //}

        //MOVIMIENTO DE PATRULLA

        //if(Vector3.Distance(transform.position,destino) < 1f) // distancia entre el agente y el punto menos que 1
        //{
        //    indiceHijos++;

        //    indiceHijos = indiceHijos % ruta.childCount; // hace lo de abajo

        //    //if(indiceHijos >= ruta.childCount)
        //    //{
        //    //    indiceHijos = 0;
        //    //}

        //    destino = ruta.GetChild(indiceHijos).position;
        //    GetComponent<NavMeshAgent>().SetDestination(destino);
        //}

        // MOVIMIENTO ALEATORIO

        //if (Vector3.Distance(transform.position, destino) < 1f) // distancia entre el agente y el punto menos que 1
        //{
        //    indiceHijos = Random.Range(0, ruta.childCount);

        //    destino = ruta.GetChild(indiceHijos).position;
        //    GetComponent<NavMeshAgent>().SetDestination(destino);
        //}

        //MOVIMIENTO AREA DESINO ALEATORIO
        //if (Vector3.Distance(transform.position, destino) < 1f)
        //{
        //    destino = DestinoAleatorio();
        //    GetComponent<NavMeshAgent>().SetDestination(destino);
        //}

    }
    private void UpdateSpeed()
    {
        float currentSpeed = agent.speed;
        float newSpeed = currentSpeed + speedIncreasePerSecond * Time.deltaTime;
        if(newSpeed >= 4.5f)
            newSpeed = 4.5f;
        agent.speed = newSpeed;
        
    }
    Vector3 DestinoAleatorio()
    {
        return new Vector3(Random.Range(min.x, max.x), 0, Random.Range(min.z, max.z));
    }
}
