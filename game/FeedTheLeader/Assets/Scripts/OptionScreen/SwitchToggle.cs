//Author: Javi﻿

using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleRectTransform;

    Image backgroundImage; //imagen fondo boton

    Color backgroundActiveColor; //color cuando activo

    Color backgroundDefaultColor; //color cuando inactivo

    Toggle toggle;

    Vector2 handlePosition; //vector que se usa para cambiar la posición del círculo

    void Awake()
    {
        toggle = GetComponent<Toggle>();

        handlePosition = uiHandleRectTransform.anchoredPosition;

        backgroundImage = uiHandleRectTransform.parent.GetComponent<Image>();

        backgroundDefaultColor = backgroundImage.color;


        ColorUtility.TryParseHtmlString("#" + "273BF9", out backgroundActiveColor); 

        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)
            OnSwitch(true);
    }

    private void Update()
    {
        if (toggle.isOn) //actualiza el color dependiendo de si está activado
        {
            backgroundImage.color = backgroundActiveColor;
        }
        else
        {
            backgroundImage.color = backgroundDefaultColor;
        }
    }

    void OnSwitch(bool on)
    {
        uiHandleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition; //mueve la posición del círculo cuando se activa o desactiva
    }
}