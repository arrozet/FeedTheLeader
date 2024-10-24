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
        //Por alguna razon ahora 4f equivale a la pos 432 y 2 a la 216. Cada numero a�ade 108 pixeles
        Vector3 randomPosition = new Vector3(Random.Range(-8f, 4f), Random.Range(-4f, 4f), 100f);
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

}



    

