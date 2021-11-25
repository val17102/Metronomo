using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forma : MonoBehaviour
{
    private int cantidadFrases;
    private List<int> listadoFrases = new List<int>();
    private int duracion;
    
    private List<int> forma = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generarForma(){ 
        listadoFrases = new List<int>();
        forma = new List<int>();
        cantidadFrases = Random.Range(2,4);
        for (int i = 0; i < cantidadFrases; i++){
            listadoFrases.Add(i+1);
        }
        duracion = Random.Range(cantidadFrases, cantidadFrases+3);
        List<int> temp = new List<int> (listadoFrases);
        for (int i = 0; i < duracion; i++){
            forma.Add(temp[Random.Range(0,temp.Count)]);
            temp = new List<int> (listadoFrases);
            temp.Remove(forma[forma.Count-1]);
        }
        
        string acc ="Frases: ";
        foreach(var i in listadoFrases){
            acc += " "+i;
        }
        Debug.Log(acc);
        Debug.Log("Duracion: "+duracion);
        acc ="Forma: ";
        foreach(var i in forma){
            acc += " "+i;
        }
        Debug.Log(acc);
    }

    public List<int> getForma(){
        return forma;
    }

    public List<int> getFrases(){
        return listadoFrases;
    }

}
