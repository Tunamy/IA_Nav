using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float turnspeed;
    [SerializeField] private float jumpForce;
    public GameObject panel;
    public TextMeshProUGUI tiempotexto;
    public float tiempo;

    Rigidbody rb;
    Vector3 inputsPlayer;
    bool grounded;
    bool puedemoverse = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        panel.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (puedemoverse)
        {
            Move();
            Look();
        }
    }
    void Update()
    {
        GetInpunts();
        Salto();
        if(puedemoverse)
        tiempo += Time.deltaTime;
    }

    void Look() //giro 
    {
        if (inputsPlayer != Vector3.zero)
        {
            var relative = (transform.position + inputsPlayer) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, rot, turnspeed);

        }

    }
    void GetInpunts()
    {

        inputsPlayer = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

    }

    void Move()
    {

        inputsPlayer = Vector3.Normalize(inputsPlayer);
        rb.MovePosition(transform.position + (transform.forward * inputsPlayer.magnitude) * speed * Time.deltaTime);

    }

    void Salto()
    {


        if (Input.GetKeyDown(KeyCode.Space) && grounded ==true)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "NPC")
        {
            puedemoverse = false;
            panel.SetActive(true);

            string formatoTiempo = string.Format("Time: {0:00}:{1:00}", Mathf.FloorToInt(tiempo / 60f), Mathf.FloorToInt(tiempo % 60f));
            tiempotexto.text = formatoTiempo;

            
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            grounded = true;
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = false;
        }
    }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
