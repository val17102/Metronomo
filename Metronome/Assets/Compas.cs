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
    private List<float> durationList = new List<float>();
    void Start()
    {
        generarCompas(tempRitmo);
    }

    public void generarCompas(int ritmo){
        float[] sub;
        if (ritmo == 3){
            sub = new float[] {0.25f,0.5f,1,2,3,6};
        } else
        if (ritmo == 4){
            sub = new float[] {0.25f,0.5f,2,4,8};
        }else{
            sub = new float[0];
        }
        float totalNotes = compasNum * ritmo;
        float sum = 0;
        int numCom = 0;
        float temp1; 
        int temp2;

        while (sum != totalNotes){
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
        Debug.Log(p);
        Debug.Log(durationList.Sum());
    }
}
