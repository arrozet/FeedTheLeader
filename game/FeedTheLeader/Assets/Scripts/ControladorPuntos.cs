// Autos Juanma

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPuntos : MonoBehaviour
{
    public static ControladorPuntos Instance;
    [SerializeField] public float cantidadPuntos;
    [SerializeField] public float multiplicadorPuntos;// no se que es serializefield
    // Start is called before the first frame update
    public void Awake()
    { 
        // esto es de un tutorial, es para que no destruya el objeto entre escenas
        if(ControladorPuntos.Instance == null)
        {
            ControladorPuntos.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
    public void comienzo()
    {
        if(multiplicadorPuntos == 0)
        {
            multiplicadorPuntos = 1;
        } 
    }
    public void SumarPuntos(float puntos)
    {
        cantidadPuntos += puntos;
    }

    public void multiplicarMultiplicador(float num)
    {
        multiplicadorPuntos*=num;
    }
    public bool RestarPuntos(float num)
    {
        if(cantidadPuntos >= num)
        {
            cantidadPuntos -= num;
            return true;
        } else
        {
            return false;
        }
    }
   
}
