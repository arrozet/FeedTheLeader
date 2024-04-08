// Author: Artur
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetController : MonoBehaviour
{
    public int pointsToAdd = 10; // La cantidad de puntos que se agregar�n al hacer clic en el objetivo

    void Start()
    {
        // Al inicio, desactivamos el objetivo
        gameObject.SetActive(false);
        // Llamamos a una funci�n que iniciar� la aparici�n aleatoria de objetivos
        InvokeRepeating("AppearRandomly", 2f, 45f); // Llama a "AppearRandomly" cada 45 segundos, despu�s de esperar 2 segundos desde el inicio
    }

    void AppearRandomly()
    {
        // Generamos una posici�n aleatoria dentro de la pantalla
        Vector3 randomPosition = new Vector3(Random.Range(10f, 1920f), Random.Range(10f, 1080f), 0f);
        transform.position = randomPosition; // Establecemos la posici�n del objetivo

        gameObject.SetActive(true); // Activamos el objetivo para que aparezca en pantalla
    }

    void OnMouseDown()
    {
        // Esta funci�n se ejecuta cuando se hace clic en el objetivo
        // Incrementamos los puntos
        ControladorPuntos.Instance.SumarPuntos(pointsToAdd); // Llamamos a la funci�n AddPoints del GameManager y le pasamos la cantidad de puntos a agregar
        // Desactivamos el objetivo
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}



    

