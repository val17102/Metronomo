using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculadora2 : MonoBehaviour
{
    // Start is called before the first frame update
    public List<string> scale;
    private string[] notas = {"Do","Do#","Re","Re#","Mi","Fa","Fa#","Sol","Sol#","La","La#","Si"};
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string[] CalcularAcordesMayores(int grado){
        int third1 = 0, third2 = 0;
        int g =  grado - 1;
        third1 = g + 2;
        third2 = g + 4;
        third1 %= 7;
        third2 %= 7;
        //Debug.Log(scale[g]+","+scale[third1]+","+scale[third2]);
        return new string[] {scale[g],scale[third1],scale[third2]};
    }

    public int[] CalcularPosiblesNotas(int grado){
        int third1 = 0, third2 = 0;
        int g =  grado - 1;
        third1 = g + 2;
        third2 = g + 4;
        third1 %= 7;
        third2 %= 7;
        //Debug.Log(scale[g]+","+scale[third1]+","+scale[third2]);
        return new int[] {g,third1,third2};
    }

    public string GetNoteScale(int grado){
        return scale[grado];
    }

    public void CalcularEscalaMayor(string note){
        scale = new List<string>();
        int currentNote = notasD[note];
        int c = 0;
        scale.Add(note);
        for (int i = 0; i < 7; i++)
        {
            c++;
            if (c == 3 || c == 7){
                currentNote += 1;
            }
            else 
            {
                currentNote += 2;
            }
            currentNote = currentNote % 12;
            scale.Add(notas[currentNote]);
        }
        /*
        foreach (var x in scale){
            Debug.Log(x);
        }*/
    }
}
