// Juanma
//esto es una prueba super ineficiente y no se har� as�, es solo para ver si funciona 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompraObjeto1prueba : MonoBehaviour
{
    public void compra()
    {
        if (ControladorPuntos.Instance.RestarPuntos(100))
        {
            ControladorPuntos.Instance.multiplicarMultiplicador(2);
        }
    }
}