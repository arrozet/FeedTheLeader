
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

    private float currentScore;
    private float scoreUp;

    void Start()
    {
        PointsManager.Instance.comienzo();

    }

    void Update()
    {
        scoreText.text = formatScore(PointsManager.Instance.currentScore);
    }

    public void click()
    {
        PointsManager.Instance.SumarPuntos(PointsManager.Instance.scoreUp);
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
            Debug.Log("ME meti en 0");
            resultado = numero.ToString("N0");
        }
        else if (analizado == 1)
        {
            Debug.Log("ME meti en 1");
            resultado = numero.ToString("N1").TrimEnd('0').TrimEnd('.');
        }
        else if (analizado == 2)
        {
            Debug.Log("ME meti en 2");
            resultado = numero.ToString("N2").TrimEnd('0').TrimEnd('.');
        }
        else
        {
            Debug.Log("ME meti en 3");
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







