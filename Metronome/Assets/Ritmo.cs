using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Ritmo : MonoBehaviour
{
    [SerializeField]
    Text txt;
    [SerializeField]
    private Button generar;
    [SerializeField]
    private Toggle usarSeed;
    [SerializeField]
    private InputField seedInput;
    private int[] subdivision = {1,2,4};
    private int[] grupos = {2,3};
    private int[] relleno2 = {0,1}, relleno3 = {0,1,1}, clave2 = {1,0}, clave3 = {1,0,0};
    Dictionary<string, int[]> rellenoD = new Dictionary<string, int[]>(){
        {"2-1", new int[] {0,1,1,1,1,1,1,1}},
        {"2-2", new int[] {0,1,1,1}},
        {"2-4", new int[] {0,1}},
        {"3-1", new int[] {0,1,1,1,1,1,1,1,1,1,1,1}},
        {"3-2", new int[] {0,1,1,1,1,1}},
        {"3-4", new int[] {0,1,1}},
    };

    Dictionary<string, int[]> claveD = new Dictionary<string, int[]>(){
        {"2-1", new int[] {1,0,0,0,0,0,0,0}},
        {"2-2", new int[] {1,0,0,0}},
        {"2-4", new int[] {1,0}},
        {"3-1", new int[] {1,0,0,0,0,0,0,0,0,0,0,0}},
        {"3-2", new int[] {1,0,0,0,0,0}},
        {"3-4", new int[] {1,0,0}},
    };
    public bool seedG;
    private int seedU;

    private int ritmo, subFinal;
    private int[] clave, relleno, claveFinal;
    private List<int> claveV2, rellenoV2;
    public int compas = 8;

    // Start is called before the first frame update
    void Start()
    {   
        //Generar("F1");
        //generar.onClick.AddListener(delegate {Generar("F1");});
        usarSeed.onValueChanged.AddListener(delegate {ElegirSeed();});

    }

    public void Generar(string nombre){
        txt.text += nombre+"\n";
        clave = crearClave(ritmo);
        txt.text += "Clave: ";
        foreach(var x in clave){
            txt.text += x + " ";
        }
        txt.text += "\t";
        claveFinal = rellenarClave(clave);
        relleno = crearRelleno(clave);
        txt.text += "Clave Principal: ";
        foreach(var x in claveV2){
            txt.text += x + " ";
        }
        txt.text += "\t";
        txt.text += "Relleno: ";
        foreach(var x in rellenoV2){
            txt.text += x + " ";
        }
    }

    public void iniciarRitmo(int seed) {
        txt.text = "";
        int SeedI;
        int c;
        claveV2 = new List<int>();
        rellenoV2 = new List<int>();
        clave = claveV2.ToArray();
        relleno = clave2.ToArray();
        claveFinal = clave2.ToArray();
        List<int> tiempo = new List<int>(){
            3,4
        };

        if (seedG) {
            SeedI = System.DateTime.Now.Second;
        } else {
            SeedI = int.Parse(seedInput.text);
        }

        Random.InitState(SeedI);

        c = Random.Range(0,tiempo.Count);
        ritmo = tiempo[c];
        int subR = Random.Range(0, subdivision.Length);
        txt.text += "Subdivision: "+subdivision[subR];
        txt.text += "\n";
        subFinal = subdivision[subR];
        txt.text += "Ritmo "+ritmo+"/4 \n";
    }

    private int[] crearClave(int tiempo){
        int r = 0;
        int subC = tiempo * subFinal;
        List<int> agrupaciones = new List<int>();
        while (r <subC){
            r = agrupaciones.Sum();

            int v = grupos[Random.Range(0, grupos.Length)];
            if (v + r <= subC){
                agrupaciones.Add(v);
            }
            if (subC - r == 1){
                agrupaciones.RemoveAt(agrupaciones.Count - 1);
            }
            
        }

        return agrupaciones.ToArray();
    }

    private int[] crearRelleno(int[] clave){
        List<int> relleno = new List<int>();
        rellenoV2 = new List<int>();
        for (int i= 0; i< clave.Length; i++){
            rellenoV2.AddRange(rellenoD[clave[i].ToString()+"-"+subFinal.ToString()]);
        }
        for (int i = 0; i < rellenoV2.Count; i++){
            if (rellenoV2[i] == 1){
                rellenoV2[i] = Random.Range(0,2);
            }
        }
        for (int i = 0; i < compas; i++){
            foreach(var k in rellenoV2){
                relleno.Add(k);
            }
        }
        string s = "";
        foreach(var j in rellenoV2){
            s+= j.ToString();
        }
        Debug.Log("RellenoV2 "+s);
        Debug.Log("Tamano "+s.Length);

        return relleno.ToArray();

    }

    private int[] rellenarClave(int[] clave){
        List<int> claveF = new List<int>();
        claveV2 = new List<int>();
        for (int i= 0; i< clave.Length; i++){
            claveV2.AddRange(claveD[clave[i].ToString()+"-"+subFinal.ToString()]);
        }
        for (int i = 0; i < compas; i++){
            foreach(var k in claveV2){
                claveF.Add(k);
            }
        }
        string s = "";
        foreach(var j in claveV2){
            s+= j.ToString();
        }
        Debug.Log("ClaveV2 "+s);
        Debug.Log("Tamano "+s.Length);

        return claveF.ToArray();
    }

    private void ElegirSeed(){
        if (usarSeed.isOn){
            seedInput.interactable = true;
            seedG = false;
        } else {
            seedInput.interactable = false;
            seedG = true;
        }
    }
    public int getSub(){
        return subFinal;
    }

    public int[] getClave(){
        return claveFinal;
    }

    public int[] getRelleno(){
        return relleno;
    }

    public int getRitmo(){
        return ritmo;
    }
}
