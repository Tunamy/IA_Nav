using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MaquinaEstados : MonoBehaviour
{
    public Transform ruta;
    public Transform player;
    public int indiceHijos = 0;
    public Material materialAlerta , materialNormal;
    public GameObject skin;
     Animator anim;

    NavMeshAgent agent;

    Vector3 destino;

    bool patrullando;
    bool atacando = false;


    float tiempoENAlerta, tiempoAsalvo;
    public TextMeshProUGUI tiempoAlertaText, tiempoAsalvoText;
    bool alerta;

    public GameObject panelWin, panelLose, botones;

    public Collider brazocollider;

    
    void Start()
    {
        Time.timeScale = 1;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        Patrullar();

        panelLose.SetActive(false);
        panelWin.SetActive(false);
        botones.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Vector3.Distance(transform.position, destino) < 6f && patrullando == true )
        {
            patrullando = false;
            StartCoroutine(Patrulla());
        }

        if(alerta == false)
        {
            tiempoAsalvo += Time.deltaTime;

            string formatoTiempo1 = string.Format("Safe: {0:00}:{1:00}", Mathf.FloorToInt(tiempoAsalvo / 60f), Mathf.FloorToInt(tiempoAsalvo % 60f));
            tiempoAsalvoText.text = formatoTiempo1;
            if (tiempoAsalvo>= 30)
            {
                panelWin.SetActive(true);
                botones.SetActive(true);
                tiempoAsalvo = 30;
                Time.timeScale = 0;
            }
        }
        else
        {
            tiempoENAlerta += Time.deltaTime;

            string formatoTiempo = string.Format("Alert: {0:00}:{1:00}", Mathf.FloorToInt(tiempoENAlerta / 60f), Mathf.FloorToInt(tiempoENAlerta % 60f));
            tiempoAlertaText.text = formatoTiempo;

            if (tiempoENAlerta >= 30)
            {
                panelLose.SetActive(true);
                botones.SetActive(true);
                tiempoENAlerta = 30;
                Time.timeScale = 0;
            }
        }

        


    }

    private void OnDrawGizmos()
    {
        

        // Dibujar el ángulo de visión en el Editor
        Vector3 visionDir = Quaternion.Euler(0, -70 * 0.5f, 0) * transform.forward;
        Handles.color = Color.yellow;
        Handles.DrawSolidArc(transform.position, Vector3.up, visionDir, 70, 37);
    }

    IEnumerator Atack()
    {
        agent.enabled = false;
        anim.SetTrigger("atack");
        yield return new WaitForSeconds(2);

        anim.SetFloat("run", 1);
        agent.enabled = true;
        atacando = false;
        brazocollider.enabled = false;
        
    }

    IEnumerator Patrulla()
    {
        
        anim.SetFloat("run", 0);
        indiceHijos++;

        indiceHijos = indiceHijos % ruta.childCount; // hace lo de abajo

        yield return new WaitForSeconds(Random.Range(1, 3));

        destino = ruta.GetChild(indiceHijos).position;
        GetComponent<NavMeshAgent>().SetDestination(destino);
        anim.SetFloat("run", 1);
        patrullando = true;

    }

    public void Patrullar()
    {
        skin.GetComponent<SkinnedMeshRenderer>().material = materialNormal;
        patrullando = true;
        
        destino = ruta.GetChild(indiceHijos).position;
        GetComponent<NavMeshAgent>().SetDestination(destino);
        anim.SetFloat("run", 1);
  
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            Vector3 directionToPlayer = player.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToPlayer);

            float distancia = Vector3.Distance(transform.position, player.transform.position);
            Debug.Log(distancia);

            if (angle <= 70)
            {
                alerta = true;
                skin.GetComponent<SkinnedMeshRenderer>().material = materialAlerta;
                agent.SetDestination(player.transform.position);
                agent.speed = 25;

                if (distancia <= 8 && atacando == false)
                {
                    
                    atacando = true;
                    StartCoroutine(Atack());
                }
            }
            else
            {
                alerta = false;
                skin.GetComponent<SkinnedMeshRenderer>().material = materialNormal;
                Patrullar();
                agent.speed = 15;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            agent.speed = 15;
            Patrullar();


        }
    }

    

    public void Salir()
    {
        Application.Quit();
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void Perder()
    {
        panelLose.SetActive(true);
        botones.SetActive(true);

        Time.timeScale = 0;
    }

    public void EnableColider()
    {
        brazocollider.enabled = true;
    }
}
