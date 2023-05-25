using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manotazo : MonoBehaviour
{
    public MaquinaEstados MaquinaEstados;
    void Start()
    {
        MaquinaEstados.GetComponent<MaquinaEstados>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MaquinaEstados.Perder();
        }
    }
}
