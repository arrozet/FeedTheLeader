//Author: Javi

using UnityEngine;
using UnityEngine.UI;

public class FpsLimiter : MonoBehaviour
{
    public Slider fpsSlider;
    private int[] fpsValues = { 30, 60, 120, -1 }; // -1 para ilimitado

    void Start()
    {
        // Configura el slider
        fpsSlider.minValue = 0;
        fpsSlider.maxValue = fpsValues.Length - 1;
        fpsSlider.wholeNumbers = true;
        fpsSlider.onValueChanged.AddListener(OnSliderValueChanged);
        fpsSlider.value = fpsSlider.maxValue;
    }

    void OnSliderValueChanged(float value)
    {
        int fpsLimit = fpsValues[(int)value];
        if (fpsLimit == -1)
        {
            // Desactiva la limitación de FPS
            Application.targetFrameRate = -1;
        }
        else
        {
            // Limita los FPS
            Application.targetFrameRate = fpsLimit;
        }
    }
}

