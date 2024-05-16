//Autor: Juanma

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShopManagerScript : MonoBehaviour, IDataPersistence
{
    public ShopItemScripteableObject[] shopItemsSO;
    public GameObject[] shopPanelsSO;
    public TemplateShop[] shopPanels;
    public Button[] myPurchaseBtns;
    // Start is called before the first frame update
    void Start()
    {// muestra solamente los paneles que hay en función de los scripteble objects que hemos creado
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (shopItemsSO[i].unlocked == true) // si no esta desbloqueado el objeto, no lo muestra
            {
                shopPanelsSO[i].SetActive(true);
            }
        }

        loadPanels();
        CheckPurchaseable();
    }

    public void LoadData(GameData data)
    {
        foreach(ShopItemScripteableObject shopItem in shopItemsSO)
        {
            shopItem.LoadData(data);
        }
    }

    public void SaveData(ref GameData data)
    {
        foreach(ShopItemScripteableObject shopItem in shopItemsSO)
        {
            shopItem.SaveData(ref data);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // esto es super poco eficiente porque lo ejecuta todo el rato, voy a buscar la manera de invocarla desde otro script
        CheckPurchaseable();
        // a ver en verdad, la cosa esque si lo ponemos cada vez que sume o reste puntos, como va a llegar un punto en el que se cosigan muchisimos, diria que da igual dejarlo así
    }
    public void CheckPurchaseable() // esto hace que no se pueda pulsar el boton 
                                    // si no tienes suficiente dinero para comprar el objeto
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (PointsManager.Instance.getPuntos() >= shopItemsSO[i].price)
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
        if (PointsManager.Instance.getPuntos() >= shopItemsSO[btnNo].price)
        {
            PointsManager.Instance.RestarPuntos(shopItemsSO[btnNo].price);
            shopItemsSO[btnNo].amount++;
            shopItemsSO[btnNo].price = Math.Round(shopItemsSO[btnNo].basePrice * Math.Pow(1.15, shopItemsSO[btnNo].amount)); // voy a usar la formula que utiliza cookie clicker para calcular el precio 
            PointsManager.Instance.AddPPs(shopItemsSO[btnNo].pointsPerSecond); // simplemente añade los puntos por segundo
            loadPanels(); // tengo que actualizar los paneles
            if (btnNo + 1 != shopItemsSO.Length) // si es el ultimo objeto no lo hace sabes (NULLPOINTER)
            {
                if (shopItemsSO[btnNo + 1].unlocked == false) // si el siguiente objeto no esta desbloqueado lo desbloquea
                {
                    shopItemsSO[btnNo + 1].unlocked = true; // lo desbloquea para cuando vuelvas a abrir el juego
                    shopPanelsSO[btnNo + 1].SetActive(true); // muestra el objeto en pantalla
                }
            }
        }
    }


    public void loadPanels() // esto carga los paneles:
                             // realmente lo que tengo es una lista de paneles ocultos (que se activan con el primer for del STart())
                             // esta función asigna a cada panel, el nombre y el objeto de los Scripteable Objects que tenemos
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
                shopPanels[i].titleText.text = shopItemsSO[i].title;
                shopPanels[i].priceText.text = formatScore(shopItemsSO[i].price);
                shopPanels[i].amountText.text = shopItemsSO[i].amount.ToString();
                shopPanels[i].spriteImage.sprite = shopItemsSO[i].sprite;           
        }
    }
    public void resetItems()
    {
        for(int i = 0; i<shopItemsSO.Length; i++) {
            shopItemsSO[i].amount = 0;
            shopItemsSO[i].price = shopItemsSO[i].basePrice;
            if (i != 0)
            {
                shopItemsSO[i].unlocked = false;
                shopPanelsSO[i].SetActive(false);
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

        if (unidad > 0) { 
        
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
