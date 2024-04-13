// Author: Artur
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetController : MonoBehaviour
{
    public int pointsToAdd; // La cantidad de puntos que se agregarán al hacer clic en el objetivo
    private bool clicked = false; // Variable para rastrear si se hizo clic en el objeto    

    void Start()
    {
        // Al inicio, desactivamos el objetivo
        gameObject.SetActive(false);
        // Llamamos a una función que iniciará la aparición aleatoria de objetivos
        InvokeRepeating("AppearRandomly", 2f, 20f); // Llama a "AppearRandomly" cada 45 segundos, después de esperar 10 segundos desde el inicio
    }

    void AppearRandomly()
    {
        // Generamos una posición aleatoria dentro de la pantalla
        Vector3 randomPosition = new Vector3(Random.Range(30f, 1260f), Random.Range(30f, 1050f), 0f);
        transform.position = randomPosition; // Establecemos la posición del objetivo
        clicked = false;

        gameObject.SetActive(true); // Activamos el objetivo para que aparezca en pantalla
                                    
        Invoke("DisappearIfNotClicked", 7f);// Llamamos a la función que hará desaparecer el objetivo si no se hace clic en menos de 7 segundos
    }

    void DisappearIfNotClicked()
    {
        // Si no se hizo clic en el objeto, lo desactivamos
        if (!clicked)
        {
            gameObject.SetActive(false);
        }
    }

}



    

