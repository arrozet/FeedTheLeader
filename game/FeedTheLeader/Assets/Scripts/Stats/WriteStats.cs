using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WriteStats : MonoBehaviour
{
    public TMP_Text CurrentPoints;
    public TMP_Text AccumulatedPoints;
    public TMP_Text PointsPerSecond;
    public TMP_Text PointsPerClick;
    public TMP_Text EventsClicked;
    public TMP_Text PlayedTime;

    private double puntosActuales;
    private Tuple<String, String> TuplaPuntosActuales;

    private double puntosAcumulados;
    private Tuple<String, String> TuplaPuntosAcumulados;

    private double puntosPorSegundo;
    private Tuple<String, String> TuplaPuntosPorSegundos;

    private double puntosPorClick;
    private Tuple<String, String> TuplaPuntosPorClick;

    private double eventosClicados;
    private double tiempoJugado;

    public int dias;
    public int horas;
    public int minutos;
    public int segundosRestantes;

    /*Pasos:
     -Creas un  public TMP_Text con el nombre que le quieras dar
     -En el proyecto creas un objeto texto y se lo referencias al public TMP_Text (arrastrar al hueco de unity)
     -En pointsManager creas la variable que necesites para llevar la cuenta
     -Le asignas la variable al texto

     IMPORTANTE:
        El jugador tiene que meterse antes en clickingScreen ya que si no  no estará creado PointsManager y saltará NullReferenceException
        Falta hacer el guardado de las variables
    */
    public void Start()
    {
        
    }

    public void Update()
    {
        if (PointsManager.Instance == null)
        {
            CurrentPoints.text = "Puntos Actuales: No disponibles";
            AccumulatedPoints.text = "Puntos acumulados: No disponibles";
            PointsPerSecond.text = "Puntos por segundo: No disponibles";
            PointsPerClick.text = "Puntos por clic: No disponibles";
            EventsClicked.text = "Eventos aleatorios clicados: No disponibles";
            PlayedTime.text = "Tiempo jugado : No disponible";
        }
        else
        {
            puntosActuales = PointsManager.Instance.getPuntos();
            TuplaPuntosActuales = formatScore(puntosActuales);

            PuntosAcumulados = PointsManager.Instance.getAccumulatedScore();
            TuplaPuntosAcumulados = formatScore(PuntosAcumulados);

            PuntosPorSegundo = PointsManager.Instance.getPointsPerSecond();
            TuplaPuntosPorSegundos = formatScore(PuntosPorSegundo);

            PuntosPorClick = PointsManager.Instance.getScoreUp();
            TuplaPuntosPorClick = formatScore(PuntosPorClick);

            EventosClicados = PointsManager.Instance.getEventsClicked();
            TiempoJugado = Time.time - (PointsManager.Instance.getStartTime());
            ConvertirTiempo(TiempoJugado);

            CurrentPoints.text = "Puntos actuales: " + TuplaPuntosActuales.Item1 + " " + TuplaPuntosActuales.Item2;
            AccumulatedPoints.text = "Puntos acumulados: " + TuplaPuntosAcumulados.Item1 + " " + TuplaPuntosAcumulados.Item2;
            PointsPerSecond.text = "Puntos por segundo: " + TuplaPuntosPorSegundos.Item1 + " " + TuplaPuntosPorSegundos.Item2;
            PointsPerClick.text = "Puntos por click: " + TuplaPuntosPorClick.Item1 + " " + TuplaPuntosPorClick.Item2;
            EventsClicked.text = "Eventos aleatorios clicados: " + EventosClicados;
            PlayedTime.text = "Tiempo jugado: " + dias + " días " + horas + " horas " + minutos + " minutos " + segundosRestantes + " segundos";
        }

    }

    public void ConvertirTiempo(double segundos)
    {
        // Calcula los días, horas, minutos y segundos
        dias = (int)(segundos / 86400); // 86400 segundos en un día
        segundos %= 86400;
        horas = (int)(segundos / 3600); // 3600 segundos en una hora
        segundos %= 3600;
        minutos = (int)(segundos / 60); // 60 segundos en un minuto
        segundos %= 60;
        segundosRestantes = (int)segundos;

    }

    private static readonly string[] Unidades = { "", "millon", "billon", "trillon", "cuatrillon", "quintillon", "sextillon", "septillon", "octillon", "nonillon", "decillon", "undecillon",
        "duodecillon", "tredecillon", "cuatrodecillon", "quindecillon", "sexdecillon", "septendecillon", "octodecillon", "novendecillon", "vigintillon", "unvigintillon", "duovigintillon",
        "trevigintillon", "quattuorvigintillon", "quinvigintillon", "sexvigintillon", "septenvigintillon", "octovigintillon", "novemvigintillon", "trigintillon" };

    public double PuntosActuales { get => puntosActuales; set => puntosActuales = value; }
    public double PuntosAcumulados { get => puntosAcumulados; set => puntosAcumulados = value; }
    public double PuntosPorSegundo { get => puntosPorSegundo; set => puntosPorSegundo = value; }
    public double PuntosPorClick { get => puntosPorClick; set => puntosPorClick = value; }
    public double EventosClicados { get => eventosClicados; set => eventosClicados = value; }
    public double TiempoJugado { get => tiempoJugado; set => tiempoJugado = value; }

    public static Tuple<string, string> formatScore(double numero)
    {
        if (numero < 1000000)
            return Tuple.Create(numero.ToString("N0"), "");

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

        return Tuple.Create(resultado, resultado2);
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

        }

    }

}
