//Autor: Juanma (la cabra)

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicClicking : MonoBehaviour
{
    public GameObject confirmationPanel;
    public GameObject ultimoElementoTienda;

    // boton de prestigiar
    public GameObject cadenaPrestigePanel;

    // Referencia al componente TextMeshProUGUI 
    private TextMeshProUGUI textMeshPro;

    //botón de confirmar del panel
    public GameObject ConfirmButton;

    // Start is called before the first frame update
    void Start()
    {
        // Asegúrate de que textMeshProGameObject no sea null
        if (cadenaPrestigePanel != null)
        {
            // Obtén el componente TextMeshProUGUI del GameObject
            textMeshPro = cadenaPrestigePanel.GetComponent<TextMeshProUGUI>();
        }
    }

    public void Options()
    {
        SceneManager.LoadScene("OptionsScreen");
    }

    public void Stats()
    {
        SceneManager.LoadScene("StatsScreen");
    }

    public void Achievements()
    {
        SceneManager.LoadScene("Achievements");
    }

    public void Back()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void ResetPoints()
    {
        PointsManager.Instance.ResetPoints();
    }

    //Prestigio
    public void ActivePrestigePanel()
    {
        if (ultimoElementoTienda.activeInHierarchy){
            textMeshPro.text = "Estas seguro de que quieres prestigiar?";
            ConfirmButton.SetActive(true);
            confirmationPanel.SetActive(true); // Muestra el panel de confirmar prestigio cuando se llame a esta función
        }
        else
        {
            textMeshPro.text = "NO PUEDES PRESTIGIAR TODAVIA... AVANZA, MUCHACHO";
            ConfirmButton.SetActive(false);
            confirmationPanel.SetActive(true); // Muestra el panel de confirmar prestigio cuando se llame a esta función
        }

    }

    public void ConfirmPrestige()
    {
        confirmationPanel.SetActive(false); // Oculta el panel de confirmar prestigio 
        PointsManager.Instance.Prestige(1.2); //el parametro es el multiplicador 
    }

    public void CancelPrestige()
    {
        confirmationPanel.SetActive(false); // Oculta el panel de confirmar prestigio 
    }
}
