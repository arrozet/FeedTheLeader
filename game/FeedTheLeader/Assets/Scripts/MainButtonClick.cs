<<<<<<< Updated upstream
// Juanma: lo he editado para que guarde las variables entre escenas

=======
//Parte de guardado: Edu
//Autor: ?
>>>>>>> Stashed changes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickingScript : MonoBehaviour, IDataPersistence
{
    // CLICKER
    public TMP_Text scoreText;
<<<<<<< Updated upstream
=======
    private float currentScore;
    private float scoreUp;
>>>>>>> Stashed changes

    public void LoadData(GameData data)
    {
        this.currentScore = data.currentScore;
        this.scoreUp = data.scoreUp;
    }

    public void SaveData(ref GameData data)
    {
        data.currentScore = this.currentScore;
    }

    void Start()
    {
<<<<<<< Updated upstream
        ControladorPuntos.Instance.comienzo();
=======
        
>>>>>>> Stashed changes
    }

    void Update()
    {
        scoreText.text = ControladorPuntos.Instance.cantidadPuntos.ToString();
    }

    public void click()
    {
        ControladorPuntos.Instance.SumarPuntos(ControladorPuntos.Instance.multiplicadorPuntos);
    }
}
