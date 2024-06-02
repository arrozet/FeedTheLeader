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

    public AchievementManager achievementManager;

    // Start is called before the first frame update
    void Start()
    {
        achievementManager = GameObject.Find("AchievementManager")?.GetComponent<AchievementManager>();
        // Asegúrate de que textMeshProGameObject no sea null
        if (cadenaPrestigePanel != null)
        {
            // Obtén el componente TextMeshProUGUI del GameObject
            textMeshPro = cadenaPrestigePanel.GetComponent<TextMeshProUGUI>();
        }
    }

    public void Options()
    {
        ScreenManagerScript.Instance.LoadScene("OptionsScreen");
    }

    public void Stats()
    {
        ScreenManagerScript.Instance.LoadScene("StatsScreen");
    }

    public void Achievements()
    {
        ScreenManagerScript.Instance.LoadScene("Achievements");
    }

    public void Back()
    {
        ScreenManagerScript.Instance.GoBack();
    }

    public void ResetPoints()
    {
        PointsManager.Instance.ResetPoints();
    }

    //Prestigio
    public void ActivePrestigePanel()
    {
        if (ultimoElementoTienda.activeInHierarchy){
            textMeshPro.text = "¿Estás seguro de que quieres prestigiar?";
            ConfirmButton.SetActive(true);
            confirmationPanel.SetActive(true); // Muestra el panel de confirmar prestigio cuando se llame a esta función
        }
        else
        {
            textMeshPro.text = "Todavía no puedes prestigiar, sigue avanzando. Quizá el líder quiere que compres todas las propiedades...";
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

    public void ResetAchievements()
    {
        foreach (Achievement ach in achievementManager.achievements)
        {
            ach.unlocked = false;
        }
    }
}
