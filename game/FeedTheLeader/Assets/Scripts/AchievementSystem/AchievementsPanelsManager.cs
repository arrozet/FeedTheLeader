using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AchievementsPanelsManager : MonoBehaviour
{
    public GameObject[] AchievementPanelSO;
    public AchievementTemplate[] AchievementPanel;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < AchievementManager.Instance.AchievementSO.Length; i++)
        {
            AchievementPanelSO[i].SetActive(true);
        }

        loadPanels();
    }

    
    public void loadPanels() // esto carga los paneles:
                             // realmente lo que tengo es una lista de paneles ocultos (que se activan con el primer for del STart())
                             // esta función asigna a cada panel, el nombre y el objeto de los Scripteable Objects que tenemos
    {
        for (int i = 0; i < AchievementManager.Instance.AchievementSO.Length; i++)
        {
            if (AchievementManager.Instance.AchievementSO[i].unlocked) // se carga su sprite 
            {
                AchievementPanel[i].spriteImage.sprite = AchievementManager.Instance.AchievementSO[i].sprite;
            }
            else // si no está desbloqueado carga el sprite de no desbloqueado
            {
                AchievementPanel[i].spriteImage.sprite = AchievementManager.Instance.AchievementSO[i].NotUnlockedSprite;
            }
        }
    }
}

