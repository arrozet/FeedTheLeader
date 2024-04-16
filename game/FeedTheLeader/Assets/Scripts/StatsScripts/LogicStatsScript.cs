//Author: Jorge
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicStatsScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VolverAMenuInicio() // vuelve a la escena de menú inicio
    {
        SceneManager.LoadScene("StartScreen");
    }
}
