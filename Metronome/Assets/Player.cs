using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private AudioSource melodyNote;
    [SerializeField]
    private Forma forma;
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
    [SerializeField]
    Text txt, nota, form, fActual;
    private float beatDuration = 60.0f/80.0f;
    private int subdivision = 1;
    private int[] clave, relleno;
    private int transpose = -6;
    private List<int> melodia = new List<int>();
    private List<int> chordDuration = new List<int>();
    private List<int> finalRythm = new List<int>();
    private List<int> finalChord = new List<int>();
    private List<int> finalFiller = new List<int>();
    private List<int> finalMelody = new List<int>();
    private List<string> frases = new List<string>();
    private List<string> finalFrases = new List<string>();
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

    Dictionary<string, List<int>> rythmDict = new Dictionary<string, List<int>>();
    Dictionary<string, List<int>> fillerDict = new Dictionary<string, List<int>>();
    Dictionary<string, List<int>> chordDict = new Dictionary<string, List<int>>();
    Dictionary<string, List<int>> melodyDict = new Dictionary<string, List<int>>();
    Dictionary<string, List<string>> fraseDict = new Dictionary<string, List<string>>();   
    void Start()
    {
        chords1.volume = 0.1f;
        chords2.volume = 0.1f;
        chords3.volume = 0.1f;
        beat1.volume = 0.1f;
        beat2.volume = 0.05f;
        beat3.volume = 0.1f;
        start.onClick.AddListener(delegate {Comenzar();});
        stop.onClick.AddListener(delegate {Detener();});
        generar.onClick.AddListener(delegate {generarForma();});
        bpm.text = "100";
        escala.text = "Do";
        generarForma();
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
        melodyNote.Stop();
        StopAllCoroutines();
    }
    private IEnumerator Timing(){
        List<int> ritmo = new List<int> {0,4,8,12,16};  
        string [] temp;
        //Debug.Log("Duracion: "+beatDuration+" BPM: "+bpm);
        for (var i = 0; i < finalRythm.Count; i++){
            yield return new WaitForSeconds(beatDuration/4);
            if (finalRythm[i] == 1){
                beat1.Play();
            }
            if (i%4 == 0){
                beat2.Play();
            }
            /*if (ritmo.Contains(i)){
                beat2.Play();
            }*/
            if (finalFiller[i] == 1){
                beat3.Play();
            }
            if (finalChord[i] != 0){
                temp = calculadora2.CalcularAcordesMayores(finalChord[i]);
                setNote1(temp[0]);
                setNote2(temp[1]);
                setNote3(temp[2]);
                acordeActual.text = "Acorde Actual: "+temp[0]+" "+temp[1]+" "+temp[2];
            }
            if (finalMelody[i] != 0){
                setNoteM(calculadora2.GetNoteScale(finalMelody[i]));
                melodyNote.Stop();
                melodyNote.Play();
                nota.text = "Nota Actual: " + calculadora2.GetNoteScale(finalMelody[i]);

            }
            fActual.text = "Frase Actual: "+finalFrases[i];
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

    private void setNoteM(string note){
        int value;
        value = notasD[note];
        melodyNote.pitch = Mathf.Pow(2, (value+transpose)/12.0f);
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

    private void generarForma(){
        List<int> tempForm;
        List<int> tempPhrase;
        txt.text = "";
        form.text = "";
        fActual.text = "";
        int length;
        ritmoS.iniciarRitmo(0);
        //Iniciar for
        forma.generarForma();
        tempForm = forma.getForma();
        tempPhrase = forma.getFrases();
        string acc = "";
        foreach (var x in tempPhrase){
            acc += "F"+x+" ";
        }
        acc += "\n";
        form.text+="Frases: "+acc;
        acc = "";
        foreach (var x in tempForm){
            acc += "F"+x;
        }
        acc += "\n";
        form.text+="Forma: "+acc;
        finalChord = new List<int>();
        finalRythm = new List<int>();
        finalFiller = new List<int>();
        finalMelody = new List<int>();
        frases = new List<string>();
        foreach(var j in tempPhrase){
            length = Random.Range(3,8);
            Debug.Log("Duracion de "+length);
            ritmoS.compas = length;
            acordes.compasNum = length;
            ritmoS.Generar("\nF"+j); 
            acordes.generador(ritmoS.getRitmo()); 
            generarLista();
            subdivision = ritmoS.getSub();
            clave = ritmoS.getClave();
            relleno = ritmoS.getRelleno();
            string temp = "";
            foreach(var i in clave){
                frases.Add("F"+j);
            }
            rythmDict["F"+j] = new List<int>(clave);
            fillerDict["F"+j] = new List<int>(relleno);
            chordDict["F"+j] = new List<int>(chordDuration);  
            generarMelodia();
            melodyDict["F"+j] = new List<int>(melodia);
            fraseDict["F"+j] = new List<string>(frases);
        }
        foreach(var x in tempForm){
            finalChord.AddRange(chordDict["F"+x]);
            finalFiller.AddRange(fillerDict["F"+x]);
            finalRythm.AddRange(rythmDict["F"+x]);
            finalMelody.AddRange(melodyDict["F"+x]);
            finalFrases.AddRange(fraseDict["F"+x]);
        }
        //Debug.Log("CLAVE "+temp);
        //Debug.Log("Tamano clave "+clave.Length);
    }

    private void generarMelodia(){
        int[] notes = {1,2,3,4,5,6,7};
        melodia = new List<int>();
        bool flag = false;
        int [] posibSub = {2,4,6}; 
        int subNota = 0;
        int[] posib;
        List<int> posib2 = new List<int>();
        int tRand;
        foreach(var i in chordDuration){
            if (subNota == 0){
                if (i != 0){
                    posib = calculadora2.CalcularPosiblesNotas(i);
                    tRand = posib[Random.Range(0,posib.Length)];
                    melodia.Add(tRand);
                    posib2 = new List<int>();
                    
                    foreach(var k in notes){
                        if (Mathf.Abs(posib[0]-k) != 1){
                            posib2.Add(k);
                        }
                    }
                    
                } else {
                    string h = "";
                    foreach(var p in posib2){
                        h += p;
                    }
                    Debug.Log(h);
                    melodia.Add(posib2[Random.Range(0,posib2.Count)]);
                }
                subNota = posibSub[Random.Range(0,posibSub.Length)];


            } else {
                melodia.Add(0);
            }
            subNota -= 1;

        }
        string acc = "";
        foreach(var i in melodia){
            acc += i;
        }
        Debug.Log("Melodia frase: "+acc);
    }
}
