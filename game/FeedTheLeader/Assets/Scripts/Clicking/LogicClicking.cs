//Autor: Juanma (la cabra)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicClicking : MonoBehaviour
{
    public GameObject confirmationPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Back()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void ResetPoints()
    {
        // Llama a la funci�n para resetear los puntos en el GameManager
        PointsManager.Instance.ResetPoints();
    }

    //Prestigio
    public void ActivePrestigePanel()
    {
        confirmationPanel.SetActive(true); // Muestra el panel de confirmar prestigio cuando se llame a esta función
    }

    public void ConfirmPrestige()
    {
        confirmationPanel.SetActive(false); // Oculta el panel de confirmar prestigio 
        PointsManager.Instance.Prestige(CalculateMultiplier());
    }

    public void CancelPrestige()
    {
        confirmationPanel.SetActive(false); // Oculta el panel de confirmar prestigio 
    }

    public double CalculateMultiplier() //aqúi debería calcularse el multiplicador de prestigio
    {
        return 2;
    }
}
