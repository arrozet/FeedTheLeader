// Autor: eloy

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Animations;

public class imageMenuAnimationScript : MonoBehaviour
{
    public float timer = 5.0f;
    public Image imagen;
    public Sprite normal;
    public Sprite smile;
    private bool sonriendo = false;
    private float stamp = 0f;
    public Animator estrella;

    // Start is called before the first frame update
    void Start()
    {
        imagen.sprite = normal;
        sonriendo=false;
        stamp = timer;
    }

    // Update is called once per frame
    void Update()
    {
        stamp -= Time.deltaTime;

        if (stamp <= 0.0f)
        {
            if (sonriendo)
            {
                sonriendo = false;
                imagen.sprite = normal;
            }
            else
            {
                sonriendo = true;
                imagen.sprite = smile;
                estrella.Play("aparece");
            }
            stamp = timer;
        }
    }


        
        
}
