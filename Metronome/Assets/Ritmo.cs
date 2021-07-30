using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ritmo : MonoBehaviour
{
    private int subdivision;
    private int subdivisionBase;
    public bool generarBase;
    private int seedG;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generarRitmo(int seed) {
        
        Random.InitState(seed);
    }
}
