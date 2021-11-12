using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Calculadora2 calculadora2;
    [SerializeField]
    private Ritmo ritmoS; 
    [SerializeField]
    private Compas acordes;
    [SerializeField]
    private AudioSource chords1;
    [SerializeField]
    private AudioSource chords2;
    [SerializeField]
    private AudioSource chords3;
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
    [SerializeField]
    private InputField escala;
    [SerializeField]
    private Text acordeActual;
    private float beatDuration = 60.0f/80.0f;
    private int subdivision = 1;
    private int[] clave, relleno;
    private int transpose = -6;
    private List<int> chordDuration = new List<int>();
    Dictionary<string, int> notasD = new Dictionary<string, int>(){
        {"Do", 0},
        {"Do#", 1},
        {"Re", 2},
        {"Re#", 3},
        {"Mi", 4},
        {"Fa", 5},
        {"Fa#", 6},
        {"Sol", 7},
        {"Sol#", 8},
        {"La", 9},
        {"La#", 10},
        {"Si", 11}
    };
    void Start()
    {
        start.onClick.AddListener(delegate {Comenzar();});
        stop.onClick.AddListener(delegate {Detener();});
        generar.onClick.AddListener(delegate {ritmoS.Generar(); acordes.generador(ritmoS.getRitmo()); generarLista();});
        bpm.text = "100";
        escala.text = "Do";
        ritmoS.Generar(); acordes.generador(ritmoS.getRitmo()); generarLista();
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
        escala.interactable = false;
        subdivision = ritmoS.getSub();
        clave = ritmoS.getClave();
        relleno = ritmoS.getRelleno();
        string temp = "";
        foreach(var i in clave){
            temp+=clave[i];
        }
        Debug.Log("CLAVE "+temp);
        Debug.Log("Tamano clave "+clave.Length);
        string esc = escala.text;
        calculadora2.CalcularEscalaMayor(esc);
        chords1.Play();
        chords2.Play();
        chords3.Play();
        StartCoroutine(Timing());
    }

    void Detener(){
        generar.interactable = true;
        start.interactable = true;
        stop.interactable = false;
        bpm.interactable = true;
        escala.interactable = true;
        chords1.Stop();
        chords2.Stop();
        chords3.Stop();
        StopAllCoroutines();
    }
    private IEnumerator Timing(){
        List<int> ritmo = new List<int> {0,4,8,12,16};  
        string [] temp;
        //Debug.Log("Duracion: "+beatDuration+" BPM: "+bpm);
        for (var i = 0; i < clave.Length; i++){
            yield return new WaitForSeconds(beatDuration/4);
            if (clave[i] == 1){
                beat1.Play();
            }
            if (i%4 == 0){
                beat2.Play();
            }
            /*if (ritmo.Contains(i)){
                beat2.Play();
            }*/
            if (relleno[i] == 1){
                beat3.Play();
            }
            if (chordDuration[i] != 0){
                temp = calculadora2.CalcularAcordesMayores(chordDuration[i]);
                setNote1(temp[0]);
                setNote2(temp[1]);
                setNote3(temp[2]);
                acordeActual.text = "Acorde Actual: "+temp[0]+" "+temp[1]+" "+temp[2];
            }

        }
        StartCoroutine(Timing());
    }

    private void setNote1(string note){
        int value;
        value = notasD[note];
        chords1.pitch = Mathf.Pow(2, (value+transpose)/12.0f);
    }

    private void setNote2(string note){
        int value;
        value = notasD[note];
        chords2.pitch = Mathf.Pow(2, (value+transpose)/12.0f);
    }

    private void setNote3(string note){
        int value;
        value = notasD[note];
        chords3.pitch = Mathf.Pow(2, (value+transpose)/12.0f);
    }

    private void generarLista(){
        clave = ritmoS.getClave();
        chordDuration = new List<int>();
        int sub = 0;
        int pos = 0;
        foreach(var i in acordes.durationList){
            sub = (int)(i /0.25f);
            chordDuration.Add(acordes.noteList[pos]+1);
            for(int j = 0; j < sub-1; j++){
                chordDuration.Add(0);
            }
            pos +=1;
        }
        string tex = "";
        foreach(var x in chordDuration){
            tex+=x.ToString();
        }
        Debug.Log("Acordes "+tex);
        Debug.Log("Tamano acordes "+tex.Length);
        Debug.Log("Tamano ritmo "+clave.Length);
    }
}
