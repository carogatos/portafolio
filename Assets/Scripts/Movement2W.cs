using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2W : MonoBehaviour
{
    public bool estadoSemaforo;

    private float speed = 10;
    private Vector3 moveDirection = Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject.Find("Cube").GetComponent<Requesting>().semaforo;

    }

    // Update is called once per frame
    void Update()
    {
        estadoSemaforo = GameObject.Find("Cube").GetComponent<Requesting>().semaforo;
        //print(estadoSemaforo);
        transform.position += speed * Time.deltaTime * moveDirection;

        RaycastHit hit;
        if(Physics.Raycast(transform.position,moveDirection,out hit, 5f))
        {
            if (hit.transform.tag == "stop" && estadoSemaforo == false)
            {
                speed = 0;
            }
            else if (hit.transform.tag == "car")
            {
                
                speed = 0;
            }
            else
            {
                
                speed = 10;
            }



        }
        else
        {
            speed = 10;
        }
    }

    /*public void Semaforo(bool estado)
    {
        estadoSemaforo = estado;
    }*/


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("destroy"))
        {
            Destroy(gameObject);
        }else if (other.CompareTag("stop"))
        {
            GameObject.Find("Cube").GetComponent<Requesting>().carCounter++;
        }
    }
}
