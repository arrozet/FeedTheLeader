// Juanma
//esto es una prueba super ineficiente y no se hará así, es solo para ver si funciona 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompraObjeto1prueba : MonoBehaviour
{
    public void compra()
    {
        if (PointsManager.Instance.RestarPuntos(100))
        {
            PointsManager.Instance.multiplicarMultiplicador(2);
        }
    }

    // Rubén: (testing) para meterte puntos gratis
    public void puntosGratis()
    {
        if (PointsManager.Instance.RestarPuntos(0))
        {
            PointsManager.Instance.SumarPuntos(5000);
        }
    }
    
}
