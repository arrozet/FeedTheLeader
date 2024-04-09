// Juanma
//esto es una prueba super ineficiente y no se hará así, es solo para ver si funciona 
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

    // Rubén: (testing) para meterte puntos gratis
    public void puntosGratis()
    {
        if (ControladorPuntos.Instance.RestarPuntos(0))
        {
            ControladorPuntos.Instance.SumarPuntos(5000);
        }
    }
    
}
