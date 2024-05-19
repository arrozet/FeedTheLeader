
// Juanma: lo he editado para que guarde las variables entre escenas


//Parte de guardado: Edu
//Autor: ?

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ClickingScript : MonoBehaviour
{
    // CLICKER
    public TMP_Text scoreText;
    public TMP_Text scoreText2;

    private float currentScore;
    private float scoreUp;

    // CLICK 
    public GameObject plusObject;
    public TMP_Text plusText;

    void Start()
    {
        PointsManager.Instance.comienzo();
        plusObject.SetActive(false);

    }

    void Update()
    {
        scoreText.text = formatScore(PointsManager.Instance.currentScore).Item1;
        scoreText2.text = formatScore(PointsManager.Instance.currentScore).Item2;

        plusText.text = "+ " + PointsManager.Instance.scoreUp;
    }

    public void click()
    {
        PointsManager.Instance.SumarPuntos(PointsManager.Instance.scoreUp);

        //plusObject.SetActive(false);


        Vector3 clickPosition = Input.mousePosition;
        clickPosition.z = -Camera.main.transform.position.z;
        Vector3 worldClickPosition = Camera.main.ScreenToWorldPoint(clickPosition);

        plusObject.transform.position = new Vector3(worldClickPosition.x, worldClickPosition.y, 0);

        plusObject.SetActive(true);

        Fly();

    }


    IEnumerator Fly()
    {
        for(int i=0; i<=19; i++)
        {
            yield return new WaitForSeconds(0.01f);

            plusObject.transform.position = new Vector3(plusObject.transform.position.x,plusObject.transform.position.y + 2, 0);
        }

        plusObject.SetActive(false);
    }
    

    private static readonly string[] Unidades = { "", "millon", "billon", "trillon", "cuatrillon", "quintillon", "sextillon", "septillon", "octillon", "nonillon", "decillon", "undecillon",
        "duodecillon", "tredecillon", "cuatrodecillon", "quindecillon", "sexdecillon", "septendecillon", "octodecillon", "novendecillon", "vigintillon", "unvigintillon", "duovigintillon",
        "trevigintillon", "quattuorvigintillon", "quinvigintillon", "sexvigintillon", "septenvigintillon", "octovigintillon", "novemvigintillon", "trigintillon" };

    public static Tuple<string,string> formatScore(double numero)
    {
        if (numero < 1000000)
            return Tuple.Create(numero.ToString("N0"),"");

        int unidad = 0;
        while (numero >= 1000000)
        {
            unidad++;
            numero /= 1000000;
        }
        double numerito = Math.Round(numero, 3); // solo necesitamos 3 cifras decimales para mostrar, las demas son prescindibles
        int analizado = AnalizarNumero(numerito);
        string resultado;
        if (analizado == 0)
        {
            //Debug.Log("ME meti en 0");
            resultado = numerito.ToString("N0");
        }
        else if (analizado == 1)
        {
            //Debug.Log("ME meti en 1");
            resultado = numerito.ToString("N1").TrimEnd('0').TrimEnd('.');
        }
        else if (analizado == 2)
        {
            //Debug.Log("ME meti en 2");
            resultado = numerito.ToString("N2").TrimEnd('0').TrimEnd('.');
        }
        else
        {
            //Debug.Log("ME meti en 3");
            resultado = numerito.ToString("N3").TrimEnd('0').TrimEnd('.');
        }
        string resultado2 = "";

        if (unidad > 0)
        {
            int numAux = (int)numero;
            resultado += " ";
            if (numAux == 1 && analizado == 0)
                resultado2 += Unidades[unidad];
            else
                resultado2 += Unidades[unidad] + "es";
        }

        return Tuple.Create(resultado,resultado2);
    }
    public static int AnalizarNumero(double numero)
    {
        
        // esto es la cosa mas guarra que he hecho en mi vida
        string numeroStr = numero.ToString();
        int index = numeroStr.IndexOf(','); // si no hay coma el index sera -1

        if (index == -1)
        {
            // Si no hay parte decimal
            return 0;
        }
        else
        {
            // Si hay parte decimal
            string parteDecimal = numeroStr.Substring(index + 1); // tomas el prmera numero decimal


            if (parteDecimal.Length == 1) // si solo es un numero 
            {
                if (parteDecimal[0].Equals('0')) // si es 0
                {
                    return 0; // significa que no tenemos que representar ningun numero
                }
                else
                {
                    return 1; // si no si tenemos que representarlo
                }
            }
            else if (parteDecimal.Length == 2)
            {
                if (parteDecimal[0].Equals('0') && parteDecimal[1].Equals('0'))
                {
                    return 0;
                }
                else if (parteDecimal[1].Equals('0'))
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
                else if (parteDecimal[1].Equals('0') && parteDecimal[2].Equals('0'))
                {
                    return 1;
                }
                else if (parteDecimal[2].Equals('0'))
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }

                /*
            for (int i = 0; i < parteDecimal.Length && !fin; i++)
            {
                if (parteDecimal[i] != '0')
                {
                    ceros = false;
                    break;
                } else
                {
                    numCeros++;
                }
                if (numCeros == 3)
                {
                    fin = true;
                }
            }

            if (fin)
            {
                return 0;
            } else if (numCeros <= 2 ) 
            {
                
            }




            if (parteDecimal.Length >= 3)
            {
                string ceros = parteDecimal.Substring(0, 2);
                if (ceros.Equals("000"))
                {
                    // Si la parte decimal contiene solo ceros
                    return 0;
                }
            }
            
            else
            {
                // Si la parte decimal no contiene solo ceros
                int cantidadDecimales = parteDecimal.Length;
                return cantidadDecimales;
            }
                */
            
        }

    }
}






