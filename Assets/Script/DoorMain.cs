using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DoorMain : MonoBehaviour
{
    // Start is called before the first frame update
    public int IndexDoor;
    public int FlagKeyOpen = 0;
    public TMP_Text tMP_Text;
    public Door DoorMainforMap;
    public int IndexRoomDoorNumber;
    public int FakeDoor = 0;
    public void SetDoor(int h){
        IndexDoor = h;
        IndexRoomDoorNumber = GameControll.Instance.FlagRoom * 4 + h;
        tMP_Text.text = IndexRoomDoorNumber+"";

        GameControll.Instance.SavePlayerMain.coin+=(GameControll.Instance.FlagRoom+5);
    }
    public void ResetMap(){
        if(DoorMainforMap!=null){
            DoorMainforMap.ResetDooor();
        }
    }

}
