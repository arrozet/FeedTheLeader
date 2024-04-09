// Author: Artur
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetController : MonoBehaviour
{
    public int pointsToAdd; // La cantidad de puntos que se agregar�n al hacer clic en el objetivo
    private bool clicked = false; // Variable para rastrear si se hizo clic en el objeto

    void Start()
    {
        // Al inicio, desactivamos el objetivo
        gameObject.SetActive(false);
        // Llamamos a una funci�n que iniciar� la aparici�n aleatoria de objetivos
        InvokeRepeating("AppearRandomly", 2f, 20f); // Llama a "AppearRandomly" cada 45 segundos, despu�s de esperar 10 segundos desde el inicio
    }

    void AppearRandomly()
    {
        // Generamos una posici�n aleatoria dentro de la pantalla
        Vector3 randomPosition = new Vector3(Random.Range(20f, 1900f), Random.Range(20f, 1060f), 0f);
        transform.position = randomPosition; // Establecemos la posici�n del objetivo
        clicked = false;

        gameObject.SetActive(true); // Activamos el objetivo para que aparezca en pantalla
                                    
        Invoke("DisappearIfNotClicked", 7f);// Llamamos a la funci�n que har� desaparecer el objetivo si no se hace clic en menos de 7 segundos
    }

    void DisappearIfNotClicked()
    {
        // Si no se hizo clic en el objeto, lo desactivamos
        if (!clicked)
        {
            gameObject.SetActive(false);
        }
    }

    void OnMouseDown()
    {
        // Esta funci�n se ejecuta cuando se hace clic en el objetivo
        pointsToAdd = Mathf.RoundToInt(ControladorPuntos.Instance.getPuntos()*0.1f); // Calculamos el 10% de los puntos totales y lo pasamos a Int
        if (pointsToAdd < 10)
        {
            pointsToAdd = 10; // M�nimo sumar 10 puntos
        }
        // Incrementamos los puntos
        ControladorPuntos.Instance.SumarPuntos(pointsToAdd); // Llamamos a la funci�n AddPoints del GameManager y le pasamos la cantidad de puntos a agregar
        
        // Desactivamos el objetivo
        gameObject.SetActive(false);
        // Indicamos que se hizo clic en el objeto
        clicked = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}



    

