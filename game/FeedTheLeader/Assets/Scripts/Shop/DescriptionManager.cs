using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescriptionManager : MonoBehaviour
{
    public GameObject textPanel; // Asigna el panel de texto en el editor
    public bool hasEmerged;


    // M�todo que se llama cuando el rat�n pasa por encima del objeto
    
    public void ActivePanel()
    {
    
           hasEmerged = false; // es imposible que salga si todavia no ha entrado asi que aqui lo inicializamos a false
           StartCoroutine(activar());
        

    }
    private IEnumerator activar()
    {
        yield return new WaitForSeconds(1); // Espera 1 segundo
        if (!hasEmerged ) { // solo lo muestra si no ha salido
            textPanel.SetActive(true); // Muestra el panel de texto
        }
       

    }
    // M�todo que se llama cuando el rat�n se aleja del objeto
    public void DeactivatePanel()
    { //si esta desactivado si lo desactivo no pasa nada
        hasEmerged = true;
        textPanel.SetActive(false);
    }
 
}