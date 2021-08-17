using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calculadora : MonoBehaviour
{
    [SerializeField]
    Text txt;
    [SerializeField]
    InputField input;
    [SerializeField]
    InputField grado;
    [SerializeField]
    Button button;
    string[] notas = {"Do","Do#","Re","Re#","Mi","Fa","Fa#","Sol","Sol#","La","La#","Si"};
    string[] funcion = {"tonica","subdominante","tonica","subdominante","dominante","tonica","dominante"};
    List<string> scale;
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
    // Start is called before the first frame update
    void Start()
    {
      button.onClick.AddListener(delegate {CalcularEscalaMayor(input.text); CalcularAcordesMayores();});
      Debug.Log(notas.Length); 
      //CalcularEscalaMayor("Do"); 
      //CalcularAcordesMayores();
    }

    void CalcularEscalaMayor(string note){
        txt.text = "Resultado: \n";
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
        foreach (var x in scale){
            Debug.Log(x);
            txt.text += x+"\n";
        }
        
        
        
    }

    void CalcularAcordesMayores(){
        int third1 = 0, third2 = 0, c1 = 0, c2 = 0;
        int third1Q, third2Q;
        string third1QS = "", third2QS = "";
        int g = 0;
        if (grado.text == ""){
            for (int i = 0; i < 7; i++){
                third1 = i + 2;
                third2 = i + 4;
                third1 %= 7;
                third2 %= 7;
                Debug.Log(scale[i]+","+scale[third1]+","+scale[third2]);
                txt.text += scale[i]+","+scale[third1]+","+scale[third2]+" - ";
                c1 = notasD[scale[i]];
                c2 = notasD[scale[third1]];
                if (c2 < c1){
                    c2 = c2+12;
                }
                third1Q = c2-c1;
                c1 = notasD[scale[third1]];
                c2 = notasD[scale[third2]];
                if (c2 < c1){
                    c2 = c2+12;
                }
                third2Q = c2-c1;

                if(third1Q == 3){
                    third1QS = "menor";
                }else if (third1Q == 4){
                    third1QS = "mayor";
                }
                if(third2Q == 3){
                    third2QS = "menor";
                }else if (third2Q == 4){
                    third2QS = "mayor";
                }

                if(third1QS == "mayor" && third2QS == "menor"){
                    Debug.Log("Acorde Mayor");
                    txt.text += "Acorde Mayor \n";
                }
                if(third1QS == "menor" && third2QS == "mayor"){
                    Debug.Log("Acorde Menor");
                    txt.text += "Acorde Menor \n";
                }
                if(third1QS == "menor" && third2QS == "menor"){
                    Debug.Log("Acorde Disminuido");
                    txt.text += "Acorde Disminuido \n";
                }
                if(third1QS == "mayor" && third2QS == "mayor"){
                    Debug.Log("Acorde Aumentado");
                    txt.text += "Acorde Aumentado \n";
                }
            }
        } else {
            g =  int.Parse(grado.text)-1;
            third1 = g + 2;
            third2 = g + 4;
            third1 %= 7;
            third2 %= 7;
            Debug.Log(scale[g]+","+scale[third1]+","+scale[third2]);
            txt.text += scale[g]+","+scale[third1]+","+scale[third2]+" - "+funcion[g]+" ";
            c1 = notasD[scale[g]];
            c2 = notasD[scale[third1]];
            if (c2 < c1){
                c2 = c2+12;
            }
            third1Q = c2-c1;
            c1 = notasD[scale[third1]];
            c2 = notasD[scale[third2]];
            if (c2 < c1){
                c2 = c2+12;
            }
            third2Q = c2-c1;

            if(third1Q == 3){
                third1QS = "menor";
            }else if (third1Q == 4){
                third1QS = "mayor";
            }
            if(third2Q == 3){
                third2QS = "menor";
            }else if (third2Q == 4){
                third2QS = "mayor";
            }

            if(third1QS == "mayor" && third2QS == "menor"){
                Debug.Log("Acorde Mayor");
                txt.text += "Acorde Mayor \n";
            }
            if(third1QS == "menor" && third2QS == "mayor"){
                Debug.Log("Acorde Menor");
                txt.text += "Acorde Menor \n";
            }
            if(third1QS == "menor" && third2QS == "menor"){
                Debug.Log("Acorde Disminuido");
                txt.text += "Acorde Disminuido \n";
            }
            if(third1QS == "mayor" && third2QS == "mayor"){
                Debug.Log("Acorde Aumentado");
                txt.text += "Acorde Aumentado \n";
            }
        }
    }
}
