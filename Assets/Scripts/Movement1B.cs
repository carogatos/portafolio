using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement1B : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    private Vector3 moveDirection = Vector3.left;

    public bool estadoSemaforo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        estadoSemaforo = GameObject.Find("Cube").GetComponent<Requesting>().semaforo;
        transform.position += speed * Time.deltaTime * moveDirection;


        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDirection, out hit, 5f))
        {
            if (hit.transform.tag == "stop" && estadoSemaforo == true)
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("destroy"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("stop"))
        {
            GameObject.Find("Cube").GetComponent<Requesting>().carCounter++;
        }
    }
}
