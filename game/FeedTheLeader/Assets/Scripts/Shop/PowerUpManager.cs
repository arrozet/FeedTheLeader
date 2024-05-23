//Author: Juanma

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance;

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

    public void Awake()
    {
        // esto es de un tutorial, es para que no destruya el objeto entre escenas
        if (PowerUpManager.Instance == null)
        {
            PowerUpManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
            loadPanels(); // tengo que actualizar los paneleS
        }
    }
    public void loadPanels() // esto carga los paneles:
                             // realmente lo que tengo es una lista de paneles ocultos (que se activan con el primer for del STart())
                             // esta función asigna a cada panel, el nombre y el objeto de los Scripteable Objects que tenemos
    {
        for (int i = 0; i < powerUpSO.Length; i++)
        {
            if (powerUpSO[i].unlocked == true && powerUpSO[i].bought == false) // si no esta desbloqueado el objeto, no lo muestra
            {
                PowerUpPanelsSO[i].SetActive(true);
            }
        }
        for (int i = 0; i < powerUpSO.Length; i++)
        {
            PowerUpPanels[i].spriteImage.sprite = powerUpSO[i].sprite;
            PowerUpPanels[i].frameImage.sprite = powerUpSO[i].frame;
        }
    }
    public void resetPowerUps() // cuidado que pone los puntos por segundo
    {
        for (int i = 0; i < powerUpSO.Length; i++)
        {
            powerUpSO[i].bought = false;
            powerUpSO[i].unlocked = false;
            PowerUpPanelsSO[i].SetActive(false);
        }
        loadPanels();
        PointsManager.Instance.PointsPerSecond = 0;
        PointsManager.Instance.scoreUp = 1;
    }
    public void efecto(int id)
    {
        if (powerUpSO[id].generator == -100) // aumento de la puntuacion de cada click
        {
            PointsManager.Instance.scoreUp = powerUpSO[id].effect;
        }
        else if (powerUpSO[id].generator == -500) // mejora de la prensa cuantica
        {
            PointsManager.Instance.PointsPerSecond *= powerUpSO[id].effect;
            // mejora de presa cuantica 
        }
        else if (powerUpSO[id].generator == -1)
        { // fusion generador 1 y 2
            double antPPerSecond1 = ShopManagerScript.Instance.shopItemsSO[powerUpSO[1].generator].pointsPerSecond; // almacenamos los anteriores puntos por segundos
            double antPPerSecond2 = ShopManagerScript.Instance.shopItemsSO[powerUpSO[2].generator].pointsPerSecond; // almacenamos los anteriores puntos por segundos
            ShopManagerScript.Instance.shopItemsSO[powerUpSO[1].generator].pointsPerSecond *= powerUpSO[1].effect; // aumenta los puntos por segundo del generador
            ShopManagerScript.Instance.shopItemsSO[powerUpSO[2].generator].pointsPerSecond *= powerUpSO[2].effect; // aumenta los puntos por segundo del generador
            PointsManager.Instance.PointsPerSecond -= antPPerSecond1 * ShopManagerScript.Instance.shopItemsSO[powerUpSO[1].generator].amount; // restamos los anteriores puntos por segundo
            PointsManager.Instance.PointsPerSecond -= antPPerSecond1 * ShopManagerScript.Instance.shopItemsSO[powerUpSO[2].generator].amount; // restamos los anteriores puntos por segundo
            PointsManager.Instance.PointsPerSecond += (ShopManagerScript.Instance.shopItemsSO[powerUpSO[1].generator].amount * ShopManagerScript.Instance.shopItemsSO[powerUpSO[1].generator].pointsPerSecond); // metemos los nuevos
            PointsManager.Instance.PointsPerSecond += (ShopManagerScript.Instance.shopItemsSO[powerUpSO[2].generator].amount * ShopManagerScript.Instance.shopItemsSO[powerUpSO[2].generator].pointsPerSecond); // metemos los nuevos
        }
        else
        {
            double antPPerSecond = ShopManagerScript.Instance.shopItemsSO[powerUpSO[id].generator].pointsPerSecond; // almacenamos los anteriores puntos por segundos
            ShopManagerScript.Instance.shopItemsSO[powerUpSO[id].generator].pointsPerSecond *= powerUpSO[id].effect; // aumenta los puntos por segundo del generador
            PointsManager.Instance.PointsPerSecond -= antPPerSecond * ShopManagerScript.Instance.shopItemsSO[powerUpSO[id].generator].amount; // restamos los anteriores puntos por segundo
            PointsManager.Instance.PointsPerSecond += (ShopManagerScript.Instance.shopItemsSO[powerUpSO[id].generator].amount * ShopManagerScript.Instance.shopItemsSO[powerUpSO[id].generator].pointsPerSecond); // metemos los nuevos
        }
    }

    public void unlockFOR()
    {

        for (int i = 0; i < powerUpSO.Length; i++)
        {
            if (powerUpSO[i].bought == false) // si esta comprado no puedes volver a desbloquearlo
            {
                if (powerUpSO[i].generator == -100) // no se que criterio seguir para los clicks
                {

                }
                else if (powerUpSO[i].generator == -500) // no se que criterio seguir para la prensa cuantica
                {

                }
                else if (powerUpSO[i].generator == -1)
                {
                    if (ShopManagerScript.Instance.shopItemsSO[1].amount >= 25 && ShopManagerScript.Instance.shopItemsSO[2].amount >= 25)
                    {
                        powerUpSO[i].unlocked = true;
                    }
                }
                else
                {
                    if (ShopManagerScript.Instance.shopItemsSO[powerUpSO[i].generator].amount >= 5 && ShopManagerScript.Instance.shopItemsSO[powerUpSO[i].generator].amount < 10 && powerUpSO[i].level == 0)
                    {
                        powerUpSO[i].unlocked = true;
                    }
                    else if (ShopManagerScript.Instance.shopItemsSO[powerUpSO[i].generator].amount >= 10 && ShopManagerScript.Instance.shopItemsSO[powerUpSO[i].generator].amount < 25 && powerUpSO[i].level == 1)
                    {
                        powerUpSO[i].unlocked = true;
                    }
                    else if (ShopManagerScript.Instance.shopItemsSO[powerUpSO[i].generator].amount >= 25 && ShopManagerScript.Instance.shopItemsSO[powerUpSO[i].generator].amount < 50 && powerUpSO[i].level == 2)
                    {
                        powerUpSO[i].unlocked = true;
                    }
                    else if (ShopManagerScript.Instance.shopItemsSO[powerUpSO[i].generator].amount >= 50 && ShopManagerScript.Instance.shopItemsSO[powerUpSO[i].generator].amount < 100 && powerUpSO[i].level == 3)
                    {
                        powerUpSO[i].unlocked = true;
                    }
                    else if (ShopManagerScript.Instance.shopItemsSO[powerUpSO[i].generator].amount > 100 && powerUpSO[i].level == 4)
                    {
                        powerUpSO[i].unlocked = true;
                    }
                }
            }
        }
        loadPanels();
    }
}
