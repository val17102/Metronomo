using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Metronomo : MonoBehaviour
{
    private IEnumerator coroutine;
    [SerializeField]
    private InputField mainInput;
    [SerializeField]
    private AudioSource beat1;
    [SerializeField]
    private AudioSource beat2;
    [SerializeField]
    private AudioSource beat3;
    
    [SerializeField]
    private Button b24;
    [SerializeField]
    private Button b34;
    [SerializeField]
    private Button b44;
    [SerializeField]
    private Button b54;
    [SerializeField]
    private Button b64;
    [SerializeField]
    private Button b74;
    [SerializeField]
    private Button cor;
    [SerializeField]
    private InputField numerador;
    [SerializeField]
    private InputField status8th;
    private bool corcheas = false;
    private int beatNum = -1;
    private int bpm = 120;
    private int den = 4;
    private float beatDuration = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        b24.onClick.AddListener(delegate {ChangeTime(2);});
        b34.onClick.AddListener(delegate {ChangeTime(3);});
        b44.onClick.AddListener(delegate {ChangeTime(4);});
        b54.onClick.AddListener(delegate {ChangeTime(5);});
        b64.onClick.AddListener(delegate {ChangeTime(6);});
        b74.onClick.AddListener(delegate {ChangeTime(7);});
        cor.onClick.AddListener(delegate {Toggle8th();});
        beatDuration = 60.0f/bpm;
        coroutine = Timing();
        mainInput.text = bpm.ToString();
        mainInput.onValueChanged.AddListener(delegate {ChangeBPM();});
        status8th.text = "8th: Disabled";
        numerador.text = "4/4";
        StartCoroutine(coroutine);
        
    }

    private IEnumerator Timing(){

        Debug.Log("Duracion: "+beatDuration+" BPM: "+bpm);
        beatNum++;
        yield return new WaitForSeconds(beatDuration/2);
        if (corcheas)
        beat3.Play();
        yield return new WaitForSeconds(beatDuration/2);
        if (beatNum % den == 0){
            beatNum = 0;
            beat1.Play();
        }
        else {
            beat2.Play();
        }
        if (corcheas)
        beat3.Play();
        StartCoroutine(Timing());
    }

    private void ChangeTime(int change){
        den = change;
        numerador.text = den+"/4";
    }

    private void ChangeBPM(){
        bpm = int.Parse(mainInput.text);
        beatDuration = 60.0f/bpm;
    }

    private void Toggle8th(){
        corcheas = !corcheas;
        if (corcheas){
            status8th.text = "8th: Enabled";
        } else {
            status8th.text = "8th: Disabled";
        }
    }
}
