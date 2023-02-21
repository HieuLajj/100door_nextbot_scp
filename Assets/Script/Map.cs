using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Map : MonoBehaviour
{
    public List<NavMeshSurface> surfaces;
    public Transform PosMap;
    public Transform PositionEnemiesChase;
    public Transform positionEndPoint;
    public DoorMain doorMaininMap;
    public int IndexMap;
    public int GamePlay = 0;
    public ActiveSCPMain activeSCPMain;
    public GameObject LastBrick;
    public List<RandomPosition> PositionMap;
    public GameObject KeyObject;
    public SettingLaimRoom SettingLaimRoomm;
    public bool CheckBake = false;

    //gameplay 2
    public ActiveTriggerEnemy ActiveEnemy;
    public chaseActiveCeiling ChaseActiveCeilingFire;
    public TriggerDecor TriggerDecorRoom;
    public void BakeMap(){
        surfaces.ForEach(s=>s.BuildNavMesh());    
    }
    public GameObject[] Hands;
    public void SendPositions(){
        for(int i = 0 ; i < PosMap.childCount; i++){
            RandomPosition randomPosition =  PosMap.GetChild(i).GetComponent<RandomPosition>();
            randomPosition.Send();
        }
    }

    public void SetUpMapGameMode1and2(){
        GameControll.Instance.KeysUI.SetActive(true);
        doorMaininMap.FlagKeyOpen = 1;
        SpawnKey();
    }
    public void SpawnKey(){
        //resetKeytruocdo
        if(KeyObject!=null){
            KeyObject.transform.position = PositionMap[Random.Range(0,PositionMap.Count)].transform.position;
            KeyObject.SetActive(true);
            return;
        }
        for(int i=0; i< GameControll.Instance.Keys.childCount;i++){
            if(!GameControll.Instance.Keys.GetChild(i).gameObject.activeInHierarchy){
                KeyObject =  GameControll.Instance.Keys.GetChild(i).gameObject;
                KeyObject.transform.position = PositionMap[Random.Range(0,PositionMap.Count)].transform.position;
                KeyObject.SetActive(true);
                return;
            }
        }
        KeyObject = Instantiate(GameControll.Instance.KeyPrefabs, PositionMap[Random.Range(0,PositionMap.Count)].transform.position, Quaternion.identity, GameControll.Instance.Keys.transform);
    }
    
    public void ResetMapChase(){
        ChaseActiveCeilingFire.Setup();
        activeSCPMain.flag = 0;
        GameControll.Instance.SCPMain.gameObject.SetActive(false);
        PlayerController.Instance.SpawnEyesInWall.gameObject.SetActive(false);
        TimelineStart.Instance.scpMain.CheckActive = false;
        DisActiveHand();
    }

    public void ActiveHand(){
        for(int i=0 ; i < Hands.Length; i++){
            GameObject g = Hands[i].gameObject;
            Hand hand = g.GetComponent<Hand>();
            hand.AnimatorHand.SetTrigger("starthand");
            hand.RandomAnimationHand();
        }
    }
    public void DisActiveHand(){
        for(int i=0; i< Hands.Length; i++){
            GameObject g = Hands[i].gameObject;
            Hand hand = g.GetComponent<Hand>();
            hand.AnimatorHand.SetTrigger("dishand");
        }
    }
}
