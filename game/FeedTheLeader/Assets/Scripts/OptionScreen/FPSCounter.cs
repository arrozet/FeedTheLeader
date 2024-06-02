//Author: Javi
//Fixed by ROZ
using System;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    float deltaTime = 0.0f;
    public static FPSCounter instance;
    public bool isOn;

    // para crearlo como singleton (solo va a haber 1 solo uno siempre)
    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        // Cargar valor de fpsToggle. No se puede hacer setBool, asi que trabajo con enteros
        int valorFpsToggle = PlayerPrefs.GetInt("FpsToggleValue", 0);
        if (valorFpsToggle == 1)
        {
            isOn = true;
        }
        else
        {
            isOn = false;
        }
    }

    void Update()
    {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        if (isOn)
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            int fontSize = h * 2 / 80;

            Rect rect = new Rect(0, 0, w, fontSize);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = fontSize;
            style.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 0.7f);
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.} fps", fps);
            GUI.Label(rect, text, style);
        }
    }
}