using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Ritmo ritmoS;
    [SerializeField]
    private AudioSource beat1;
    [SerializeField]
    private AudioSource beat2;
    [SerializeField]
    private AudioSource beat3;
    [SerializeField]
    private Button start, generar, stop;
    [SerializeField]
    private InputField bpm;
    private float beatDuration = 60.0f/80.0f;
    private int subdivision = 1;
    private int[] clave, relleno;
    void Start()
    {
        start.onClick.AddListener(delegate {Comenzar();});
        stop.onClick.AddListener(delegate {Detener();});
        bpm.text = "100";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Comenzar(){
        beatDuration = 60.0f/ int.Parse(bpm.text);
        generar.interactable = false;
        start.interactable = false;
        stop.interactable = true;
        bpm.interactable = false;
        subdivision = ritmoS.getSub();
        clave = ritmoS.getClave();
        relleno = ritmoS.getRelleno();
        StartCoroutine(Timing());
    }

    void Detener(){
        generar.interactable = true;
        start.interactable = true;
        stop.interactable = false;
        bpm.interactable = true;
        StopAllCoroutines();
    }
    private IEnumerator Timing(){
        List<int> ritmo = new List<int> {0,4,8,12,16};  
        //Debug.Log("Duracion: "+beatDuration+" BPM: "+bpm);
        for (var i = 0; i < clave.Length; i++){
            yield return new WaitForSeconds(beatDuration/2);
            if (clave[i] == 1){
                beat1.Play();
            }
            if (ritmo.Contains(i)){
                beat2.Play();
            }
            if (relleno[i] == 1){
                beat3.Play();
            }
        }
        StartCoroutine(Timing());
    }
}
