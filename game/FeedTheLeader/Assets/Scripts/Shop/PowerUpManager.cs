//Author: Juanma

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    public PowerUpSO[] powerUpSO;
    public GameObject[] PowerUpPanelsSO;
    public PowerUpTemplate[] PowerUpPanels;
    public Button[] myPurchaseBtns;
    void Start()
    {
        for (int i = 0; i < powerUpSO.Length; i++)
        {
            if (powerUpSO[i].unlocked == true && powerUpSO[i].bought == false) // si no esta desbloqueado el objeto, no lo muestra
            {
                PowerUpPanelsSO[i].SetActive(true);
            }
        }

        loadPanels();
        CheckPurchaseable();
    }
    void Update()
    {
        // esto es super poco eficiente porque lo ejecuta todo el rato, voy a buscar la manera de invocarla desde otro script
        CheckPurchaseable();
        // a ver en verdad, la cosa esque si lo ponemos cada vez que sume o reste puntos, como va a llegar un punto en el que se cosigan muchisimos, diria que da igual dejarlo así
    }
    public void CheckPurchaseable() // esto hace que no se pueda pulsar el boton 
                                    // si no tienes suficiente dinero para comprar el objeto
    {
        for (int i = 0; i < powerUpSO.Length; i++)
        {
            if (PointsManager.Instance.getPuntos() >= powerUpSO[i].price)
            {
                myPurchaseBtns[i].interactable = true;
            }
            else
            {
                myPurchaseBtns[i].interactable = false;
            }
        }
    }
    public void PurchaseItem(int btnNo) // simplemente resta, y comporueba que boton se ha pulsado para poder restarle el precio y dar el efecto a dicho boton
    {
        if (PointsManager.Instance.getPuntos() >= powerUpSO[btnNo].price)
        {
            PointsManager.Instance.RestarPuntos(powerUpSO[btnNo].price);
            powerUpSO[btnNo].bought = true; // ya lo ha comprado (no lo vuelve a mostrar)
            efecto(btnNo);
            PowerUpPanelsSO[btnNo].SetActive(false);
            loadPanels(); // tengo que actualizar los paneles
            if (btnNo + 1 != powerUpSO.Length) // si es el ultimo objeto no lo hace sabes (NULLPOINTER)
            {
                if (powerUpSO[btnNo + 1].unlocked == false) // si el siguiente objeto no esta desbloqueado lo desbloquea
                {
                    powerUpSO[btnNo + 1].unlocked = true; // lo desbloquea para cuando vuelvas a abrir el juego
                    PowerUpPanelsSO[btnNo + 1].SetActive(true); // muestra el objeto en pantalla
                }
            }
        }
    }
    public void loadPanels() // esto carga los paneles:
                             // realmente lo que tengo es una lista de paneles ocultos (que se activan con el primer for del STart())
                             // esta función asigna a cada panel, el nombre y el objeto de los Scripteable Objects que tenemos
    {
        for (int i = 0; i < powerUpSO.Length; i++)
        { 
           PowerUpPanels[i].spriteImage.sprite = powerUpSO[i].sprite;
        }
    }
    public void resetPowerUps()
    {
        for (int i = 0; i < powerUpSO.Length; i++)
        {
            powerUpSO[i].bought = false;
            if (i != 0)
            {
                powerUpSO[i].unlocked = false;
                PowerUpPanelsSO[i].SetActive(false);
            } else
            {
                PowerUpPanelsSO[i].SetActive(true);
            }
        }
        loadPanels();
    }
    public void efecto(int id)
    {
        if (id == 0)
        {
            PointsManager.Instance.scoreUp++;
        }
    }
}
