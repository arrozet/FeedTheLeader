//Author: Juanma

using System;
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
        // a ver en verdad, la cosa esque si lo ponemos cada vez que sume o reste puntos, como va a llegar un punto en el que se cosigan muchisimos, diria que da igual dejarlo as�
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
                             // esta funci�n asigna a cada panel, el nombre y el objeto de los Scripteable Objects que tenemos
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
            PowerUpPanels[i].description.text = powerUpSO[i].desc;
            PowerUpPanels[i].titleText.text = powerUpSO[i].title;
            PowerUpPanels[i].priceText.text = formatScore(powerUpSO[i].price);
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
     //   PointsManager.Instance.scoreUp = 1;
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





    private static readonly string[] Unidades = { "", "millon", "billon", "trillon", "cuatrillon", "quintillon", "sextillon", "septillon", "octillon", "nonillon", "decillon", "undecillon",
        "duodecillon", "tredecillon", "cuatrodecillon", "quindecillon", "sexdecillon", "septendecillon", "octodecillon", "novendecillon", "vigintillon", "unvigintillon", "duovigintillon",
        "trevigintillon", "quattuorvigintillon", "quinvigintillon", "sexvigintillon", "septenvigintillon", "octovigintillon", "novemvigintillon", "trigintillon" };

    public static string formatScore(double numero)
    {
        if (numero < 1000000)
            return numero.ToString("N0");

        int unidad = 0;
        while (numero >= 1000000)
        {
            unidad++;
            numero /= 1000000;
        }
        int analizado = AnalizarNumero(numero);
        string resultado;
        if (analizado == 0)
        {
            //Debug.Log("ME meti en 0");
            resultado = numero.ToString("N0");
        }
        else if (analizado == 1)
        {
            //Debug.Log("ME meti en 1");
            resultado = numero.ToString("N1").TrimEnd('0').TrimEnd('.');
        }
        else if (analizado == 2)
        {
            //Debug.Log("ME meti en 2");
            resultado = numero.ToString("N2").TrimEnd('0').TrimEnd('.');
        }
        else
        {
            //Debug.Log("ME meti en 3");
            resultado = numero.ToString("N3").TrimEnd('0').TrimEnd('.');
        }

        if (unidad > 0)
        {

            int numAux = (int)numero;
            resultado += " ";
            if (numAux == 1 && analizado == 0)
                resultado += Unidades[unidad];
            else
                resultado += Unidades[unidad] + "es";
        }

        return resultado;
    }
    public static int AnalizarNumero(double numero)
    {
        double numerito = Math.Round(numero, 15);
        // esto es la cosa mas guarra que he hecho en mi vida
        string numeroStr = numerito.ToString();
        int index = numeroStr.IndexOf(',');

        if (index == -1)
        {
            // Si no hay parte decimal
            return 0;
        }
        else
        {




            // Si hay parte decimal
            string parteDecimal = numeroStr.Substring(index + 1);


            if (parteDecimal.Length == 1)
            {
                if (parteDecimal[0].Equals('0'))
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else if (parteDecimal.Length == 2)
            {
                if (parteDecimal[0].Equals('0') && parteDecimal[1].Equals('0'))
                {
                    return 0;
                }
                else if (parteDecimal[0].Equals('0'))
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                if (parteDecimal[0].Equals('0') && parteDecimal[1].Equals('0') && parteDecimal[2].Equals('0'))
                {
                    return 0;
                }
                else if (parteDecimal[0].Equals('0') && parteDecimal[1].Equals('0'))
                {
                    return 1;
                }
                else if (parteDecimal[0].Equals('0'))
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }


        }

    }
}
