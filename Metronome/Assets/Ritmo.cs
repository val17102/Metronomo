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
    public bool seedG;
    private int seedU;

    private int ritmo, subFinal;
    private int[] clave, relleno, claveFinal;

    // Start is called before the first frame update
    void Start()
    {   
        Generar();
        generar.onClick.AddListener(delegate {Generar();});
        usarSeed.onValueChanged.AddListener(delegate {ElegirSeed();});

    }

    public void Generar(){
        txt.text = "";
        ritmo = generarRitmo(0);
        txt.text += "Ritmo "+ritmo+"/4 \n";
        clave = crearClave(ritmo);
        Debug.Log("Clave "+clave);
        txt.text += "Clave: ";
        foreach(var x in clave){
            txt.text += x + " ";
        }
        txt.text += "\n";
        claveFinal = rellenarClave(clave);
        relleno = crearRelleno(clave);
        txt.text += "Clave Principal: ";
        foreach(var x in claveFinal){
            txt.text += x + " ";
        }
        txt.text += "\n";
        txt.text += "Relleno: ";
        foreach(var x in relleno){
            txt.text += x + " ";
        }
    }

    private int generarRitmo(int seed) {
        int SeedI;
        int c;
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
        return (tiempo[c]);

        
    }

    private int[] crearClave(int tiempo){
        int r = 0;
        int subR = Random.Range(0, subdivision.Length);
        txt.text += "Subdivision: "+subdivision[subR];
        txt.text += "\n";
        subFinal = subdivision[subR];
        int subC = tiempo * subdivision[subR];
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
        for (int i= 0; i< clave.Length; i++){
            if (clave[i] == 2) 
                relleno.AddRange(relleno2);
            if (clave[i] == 3)
                relleno.AddRange(relleno3);
        }

        return relleno.ToArray();

    }

    private int[] rellenarClave(int[] clave){
        List<int> claveF = new List<int>();
        for (int i= 0; i< clave.Length; i++){
            if (clave[i] == 2) 
                claveF.AddRange(clave2);
            if (clave[i] == 3)
                claveF.AddRange(clave3);
        }

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
}
