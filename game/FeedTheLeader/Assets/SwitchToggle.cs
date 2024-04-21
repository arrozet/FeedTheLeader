//Author: Javi﻿

using UnityEngine ;
using UnityEngine.UI ;


public class SwitchToggle : MonoBehaviour {
   public RectTransform uiHandleRectTransform;
   public Color backgroundActiveColor, handleActiveColor;     //seleccionadores de color para toggle activo
   public Color backgroundDefaultColor, handleDefaultColor;   //seleccionadores de color para toggle inactivo
   public float handleOffset;      //desplazamiento del boton
   public Image backgroundImage, handleImage; //imágenes del toggle 

   Toggle toggle ;

   void Awake ( ) {
      toggle = GetComponent <Toggle> ( );

      toggle.onValueChanged.AddListener (OnSwitch);


      if (toggle.isOn)
         OnSwitch (true);
   }

   void OnSwitch (bool on) {
        if (on)
        {
            uiHandleRectTransform.position = new Vector3(uiHandleRectTransform.position.x + handleOffset, 
                                                    uiHandleRectTransform.position.y, uiHandleRectTransform.position.z);
            //backgroundImage.color = backgroundActiveColor;
            //handleImage.color = handleActiveColor;
        }
        else
        {
            uiHandleRectTransform.position = new Vector3(uiHandleRectTransform.position.x - handleOffset,
                                                    uiHandleRectTransform.position.y, uiHandleRectTransform.position.z);
            //backgroundImage.color = backgroundDefaultColor;
            //handleImage.color = handleDefaultColor;
        }

   }

   void OnDestroy ( ) {
      toggle.onValueChanged.RemoveListener (OnSwitch);
   }
}
