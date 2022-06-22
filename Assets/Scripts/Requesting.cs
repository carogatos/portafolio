using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class Requesting : MonoBehaviour
{
    [SerializeField] private GameObject carro1;
    [SerializeField] private GameObject carro2;
    [SerializeField] private Vector3[] posicionCarros = new Vector3[2];


    [SerializeField] private int numCarros = 2;
    private int randomTime;
    private int randomCar;

    private int counter=0;
    public bool semaforo = true;

    private Movement2W carroBlanco;

    [SerializeField] private Material colorRojo;
    [SerializeField] private Material colorVerde;

    [SerializeField] private GameObject semaforo1;
    [SerializeField] private GameObject semaforo2;

    private float timePassed;
    public int carCounter=0;

    [SerializeField] private Text timeRun;
    [SerializeField] private Text carPassed;
    //Text ourComponent;




    void Start()
    {
        //borrar
        posicionCarros = new Vector3[2];
        StartCoroutine(GetText());

        // test de parsing
        string json = "{" +
    "\"data\": [" +
        "{\"x\":0, \"y\":1, \"z\":2}," +
        "{\"x\":3, \"y\":4, \"z\":5}," +
        "{\"x\":6, \"y\":7, \"z\":8}" +
    "]" +
"}";
        Data posiciones = JsonUtility.FromJson<Data>(json);

        StartCoroutine(Create());
        StartCoroutine(Semaforo());


    }

    IEnumerator Semaforo()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            semaforo1.GetComponent<MeshRenderer>().material = colorRojo;
            semaforo2.GetComponent<MeshRenderer>().material = colorVerde;
            semaforo = false;
            //semaforo1.GetComponent<MeshRenderer>().material = colorRojo;
            yield return new WaitForSeconds(10);
            semaforo1.GetComponent<MeshRenderer>().material = colorVerde;
            semaforo2.GetComponent<MeshRenderer>().material = colorRojo;
            semaforo = true;
        }

    }


    IEnumerator Create()
    {
        for (int i = 0; i < numCarros; i++)
        {
            randomTime = UnityEngine.Random.Range(1, 6);
            randomCar = UnityEngine.Random.Range(0, 2);
            yield return new WaitForSeconds(randomTime);
            if(randomCar == 0)
            {
                Instantiate(carro2, posicionCarros[1], Quaternion.Euler(0f, 0f, 0f));
            }
            else
            {
                //Instantiate(carro2, posicionCarros[1], Quaternion.Euler(0f, 0f, 0f));
                Instantiate(carro1, posicionCarros[0], Quaternion.Euler(0f, -90f, 0f));
            }
            
        }
    }

    void Update()
    {
        carPassed.text = "Passed cars: " + carCounter/2 + "/" + numCarros;
        if(carCounter/2 != numCarros)
        {
            timeRun.text = "Time run: " + String.Format("{0:0.00}", Time.time);
        }
    }


    IEnumerator GetText()
    {
        float inicio = Time.time;

        print("haciendo request");
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:8080/");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
        }

        Data posiciones = JsonUtility.FromJson<Data>(www.downloadHandler.text);

        foreach (Position p in posiciones.data)
        {
            posicionCarros[counter] = new Vector3(p.x, p.y, p.z);
            //Debug.Log(p.x + ", " + p.y + ", " + p.z);
            counter++;
        }



        float total = Time.time - inicio;
        print("tomo: " + total);
    }
}