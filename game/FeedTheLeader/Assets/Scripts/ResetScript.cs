using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButtonScript : MonoBehaviour
{
    public void ResetPoints()
    {
        // Llama a la funci�n para resetear los puntos en el GameManager
        ControladorPuntos.Instance.ResetPoints();
    }
}
