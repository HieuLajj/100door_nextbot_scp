using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mode3 : MonoBehaviour
{
    public List<Door> listDoor;
    // Start is called before the first frame update
    public void CloseAll(){
        for(int i=0 ; i<listDoor.Count; i++){
            if(listDoor[i].IsOpen){
                listDoor[i].Close();
            }
        }
    }
}
