//Autor: Juanma

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShopManagerScript : MonoBehaviour, IDataPersistence
{
    public static ShopManagerScript Instance;

    public ShopItemScripteableObject[] shopItemsSO;
    public GameObject[] shopPanelsSO;
    public TemplateShop[] shopPanels;
    public Button[] myPurchaseBtns;
    public int AmountObjects;
    // Start is called before the first frame update
    void Start()
    {// muestra solamente los paneles que hay en funci�n de los scripteble objects que hemos creado
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (shopItemsSO[i].unlocked == true) // si no esta desbloqueado el objeto, no lo muestra
            {
                shopPanelsSO[i].SetActive(true);
            }
        }
        desbloquearPaneles();
        AmountObjects = 1;
        loadPanels();
        CheckPurchaseable();
    }

    public void LoadData(GameData data)
    {
        foreach(ShopItemScripteableObject shopItem in shopItemsSO)
        {
            shopItem.LoadData(data);
        }
        first(); // funcion que se tiene que ejecutar para cargar los paneles que deben aparecer por pantalla
    }
    

    public void SaveData(ref GameData data)
    {
        foreach(ShopItemScripteableObject shopItem in shopItemsSO)
        {
            shopItem.SaveData(ref data);
        }
    }
    public void first()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (shopItemsSO[i].unlocked == true) // si no esta desbloqueado el objeto, no lo muestra
            {
                shopPanelsSO[i].SetActive(true);
            }
        }
        desbloquearPaneles();
        AmountObjects = 1;
        loadPanels();
        CheckPurchaseable();
        PowerUpManager.Instance.unlockFOR();
    }

    // Update is called once per frame
    public void Check10Amount()
    {
        if(AmountObjects == 1)
        {
            AmountObjects = 10;
        } else
        {
            AmountObjects = 1;
        }
        loadPanels();
        
    }

    void Update()
    {
        // esto es super poco eficiente porque lo ejecuta todo el rato, voy a buscar la manera de invocarla desde otro script
        CheckPurchaseable();
        // a ver en verdad, la cosa esque si lo ponemos cada vez que sume o reste puntos, como va a llegar un punto en el que se cosigan muchisimos, diria que da igual dejarlo as�
    }
    public void CheckPurchaseable() // esto hace que no se pueda pulsar el boton 
                                    // si no tienes suficiente dinero para comprar el objeto
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (PointsManager.Instance.getPuntos() >= calcularPrecio(i))
            {
                myPurchaseBtns[i].interactable = true;
            }
            else
            {
                myPurchaseBtns[i].interactable = false;
            }
        }
    }

    public void Awake()
    {
        // esto es de un tutorial, es para que no destruya el objeto entre escenas
        if (ShopManagerScript.Instance == null)
        {
            ShopManagerScript.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PurchaseItem(int btnNo) // simplemente resta, y comporueba que boton se ha pulsado para poder restarle el precio y dar el efecto a dicho boton
    {
        if (PointsManager.Instance.getPuntos() >= calcularPrecio(btnNo))
        {
            PointsManager.Instance.RestarPuntos(calcularPrecio(btnNo));
            shopItemsSO[btnNo].amount+=AmountObjects;
            shopItemsSO[btnNo].price = Math.Round(shopItemsSO[btnNo].basePrice * Math.Pow(1.15, shopItemsSO[btnNo].amount)); // voy a usar la formula que utiliza cookie clicker para calcular el precio (aqui no hace falta usar el calcular porque calcula el precio que tendria el objeto numero 11
            PointsManager.Instance.AddPPs(shopItemsSO[btnNo].pointsPerSecond*AmountObjects); // simplemente a�ade los puntos por segundo
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
        VisualGeneratorManager.Instance.seHaComprado(btnNo, AmountObjects);
    }


    public void loadPanels() // esto carga los paneles:
                             // realmente lo que tengo es una lista de paneles ocultos (que se activan con el primer for del STart())
                             // esta funci�n asigna a cada panel, el nombre y el objeto de los Scripteable Objects que tenemos
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (shopItemsSO[i].unlocked == true) // si no esta desbloqueado el objeto, no lo muestra
            {
                shopPanelsSO[i].SetActive(true);
            }
        }

        for (int i = 0; i < shopItemsSO.Length; i++)
        {
                shopPanels[i].titleText.text = shopItemsSO[i].title;
                shopPanels[i].priceText.text = formatScore(calcularPrecio(i));
                shopPanels[i].amountText.text = shopItemsSO[i].amount.ToString();
                shopPanels[i].spriteImage.sprite = shopItemsSO[i].sprite;
                shopPanels[i].description.text = shopItemsSO[i].description;
        }
        PowerUpManager.Instance.unlockFOR();

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
        VisualGeneratorManager.Instance.resetVisualGenerators();
        loadPanels();
    }
    public double calcularPrecio(int btnNo) // esta funcion calcula el precio en funcion de cuantos objetos quieras comprar (si 10 o 1) [metere 100 si esta gente quiere]
    {
        double res = 0;
        for (int i = 0;i<AmountObjects;i++)
        {
            res+=Math.Round(shopItemsSO[btnNo].basePrice * Math.Pow(1.15, shopItemsSO[btnNo].amount+i));
        }
        return res;
        
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
    private void desbloquearPaneles()
    {
        int last = 0;
        for(int i = 0; i < shopItemsSO.Length; i++) {
            if (shopItemsSO[i].amount > 0)
            {
                shopPanelsSO[i].SetActive(true);
                last = i;
            }
        }
        if (last != shopItemsSO.Length) // si no es el �ltimo
        {
            shopPanelsSO[last+1].SetActive(true); // activas el siguiente
        }
    }


}
