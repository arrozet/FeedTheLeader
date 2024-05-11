using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersitentVersionText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        // para que nunca muera
        DontDestroyOnLoad(gameObject);
    }
}
