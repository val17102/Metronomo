using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Compas : MonoBehaviour
{
    [SerializeField]
    private int compasNum = 8;
    public int tempRitmo = 4;
    // Start is called before the first frame update
    public List<float> durationList = new List<float>();
    public List<int> noteList = new List<int>();
    private float[] probabilidadFun = {0.6f, 0.3f, 0.1f};
    private int[][] cordFun = {new int[] {0,2,5}, new int[] {1,3}, new int[] {4,6}};
    string[] funcion = {"tonica","subdominante","tonica","subdominante","dominante","tonica","dominante"};
    void Start()
    {
        //generarCompas(tempRitmo);
        //generarFuncion();
    }

    public void generador(int tRitmo){
        durationList = new List<float>();
        noteList = new List<int>();
        generarCompas(tRitmo);
        generarFuncion();
    }

    public void generarCompas(int ritmo){
        float[] sub;
        if (ritmo == 3){
            sub = new float[] {1,2,3};
        } else
        if (ritmo == 4){
            sub = new float[] {1,2,3,4};
        }else{
            sub = new float[0];
        }
        float totalNotes = compasNum * ritmo;
        float sum = 0;
        int numCom = 0;
        float temp1; 
        int temp2;

        while (sum != totalNotes){
            Debug.Log(".");
            sum = durationList.Sum();
            numCom = (int) sum / ritmo;

            if (sum < totalNotes){
                float r = sub[Random.Range(0, sub.Length)];
                while ((r > ritmo && (sum % ritmo) != 0)){
                    r = sub[Random.Range(0, sub.Length)];
                }
                temp1 = (r+sum) % ritmo;
                temp2 = (int) (r+sum) / ritmo;
                if (r+sum <= totalNotes){
                    if (temp1 != 0 && temp2 != numCom)
                    {

                    }else
                    if ((temp1 != 0 && temp2 == numCom) || temp1 == 0){
                        durationList.Add(r);
                    }
                }
            }
        }
        string p = "";
        foreach(var l in durationList){
            p+=l.ToString()+" ";
        }
        Debug.Log("Duration");
        Debug.Log(p);
        Debug.Log(durationList.Sum());
    }

    public void generarFuncion(){
        float r;
        foreach(var x in durationList){
            r = Random.Range(0.0f, 1.0f);
            if (r >=0.0f && r < probabilidadFun[0]){
                noteList.Add(cordFun[0][Random.Range(0,cordFun[0].Length)]);
                probabilidadFun = new float[] {0.4f, 0.4f, 0.2f};
                
            }else
            if (r >= probabilidadFun[0] && r < (probabilidadFun[0]+probabilidadFun[1])){
                noteList.Add(cordFun[1][Random.Range(0,cordFun[1].Length)]);
                probabilidadFun = new float[] {0.4f, 0.4f, 0.2f};
            }else
            if (r >= (probabilidadFun[0]+probabilidadFun[1]) && r < (probabilidadFun[0]+probabilidadFun[1]+probabilidadFun[2])){
                noteList.Add(cordFun[2][Random.Range(0,cordFun[2].Length)]);
                probabilidadFun = new float[] {0.6f, 0.3f, 0.1f};
            }
        }

        string temp = "";
        string temp2 = "";
        foreach(var y in noteList){
            temp += funcion[y]+ " ";
            temp2 += y.ToString()+" ";
        }
        Debug.Log("Notas");
        Debug.Log(temp);
        Debug.Log(temp2);
    }
}
