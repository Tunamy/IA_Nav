using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimado : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float turnspeed;
    
    private Animator anim;
    
    public float tiempo;

    Rigidbody rb;
    Vector3 inputsPlayer;
    
    bool puedemoverse = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        
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
        if (puedemoverse)
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

        anim.SetFloat("Velocidad", inputsPlayer.magnitude);
    }

    
   

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
