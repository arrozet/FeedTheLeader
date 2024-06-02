//Autor: Edu

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderButton : MonoBehaviour
{

    public Sprite[] leaderSprites;
    public PointsManager pointsManager;
    private Image botonImage;  // Referencia al componente Image del bot�n
    private float salto = 2;

    // Start is called before the first frame update
    void Start()
    {
        pointsManager = GameObject.Find("PointsManager")?.GetComponent<PointsManager>();
        botonImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pointsManager.accumulatedScoreStat < Mathf.Pow(10, 1 * salto))
        {
            botonImage.sprite = leaderSprites[0];
        } else if (pointsManager.accumulatedScoreStat < Mathf.Pow(10, 2 * salto))
        {
            botonImage.sprite = leaderSprites[1];
        } else if (pointsManager.accumulatedScoreStat < Mathf.Pow(10, 3 * salto))
        {
            botonImage.sprite= leaderSprites[2];
        }
        else if (pointsManager.accumulatedScoreStat < Mathf.Pow(10, 4 * salto))
        {
            botonImage.sprite = leaderSprites[3];
        }
        else if (pointsManager.accumulatedScoreStat < Mathf.Pow(10, 5 * salto))
        {
            botonImage.sprite = leaderSprites[4];
        }
        else if (pointsManager.accumulatedScoreStat < Mathf.Pow(10, 6 * salto))
        {
            botonImage.sprite = leaderSprites[5];
        }
        else if (pointsManager.accumulatedScoreStat < Mathf.Pow(10, 7 * salto))
        {
            botonImage.sprite = leaderSprites[6];
        }
        else if (pointsManager.accumulatedScoreStat < Mathf.Pow(10, 8 * salto))
        {
            botonImage.sprite = leaderSprites[7];
        }
    }
}
