using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using UnityEngine.UI;
public class GameControll : MonoBehaviour
{
    public VideoClip defaultVideo;
    public bool CheckStart = false;
    public bool CheckStartPlayer = false;
    public GameObject Player;
    public GameObject EnemyPrefabs;
    public GameObject KeyPrefabs;
    public GameObject LightPrefab;

    [SerializeField] private GameObject SCPuiMod4forPlayer;
    public GameObject BotPrefabs;
    public GameObject Camera2;
    
    public int Mod = 0;
    public int KeyInt = 0;
    public string PathVideo = "";
    public VideoClip linkVideo;
    public Sprite PathImageSprite ;
    public AudioClip audioClip;
    public GameObject MapMain;
    public GameObject map4;
    public List<RandomPosition> PositionMap;
    public List<int> PreRandom;
    public Transform Enemies;
    public  Transform Bots;
    public Transform SCPEnemys;
    public Transform Booms;
    public Transform Keys;
    public Transform Maps;
    public Transform Lights;
    public Transform Eyes;
    public GameObject EyePreb;
    public Transform Maps100door;
    public Transform SPCEnemies;
    private Rigidbody _rigidbodyPlayer;
    public Vector3 ThrowPlayer = new Vector3(0,100,0);
    public Timer timer;
    [SerializeField] private TextMeshProUGUI textCoin;
    private Mode3 _mode3;
    public SkinnedMeshRenderer SkinnedPlayer;
    public SwitchCamera switchCamera;
    public RigidbodyFirstPersonController RigidbodyFirstPersonPlayer;
    private int enemyInt;
    private int botInt;
    private Transform nearestEnemy;
    public List<GameObject> TargetforEnemy; 
    private TextAsset m_textAsset;
    public List<string> m_namesList;

    //mod4
    public bool CheckActiveSCP = false;
    public int IntBoom;
    public int IntSCP;
    public int flagButtonBoom = 0;
    public bool CheckActiveBotMode4 = true;
    public bool CheckInvisible = false;
    public List<GameObject> MapTest;
    public List<Map> MapInGame;
    public int IndexRoomLastest;
    public Vector3 positionPre;
    public int FlagRoom=0;
    public int FlagInitRoom = 0;
    public GameObject SCPMain;
    public List<int> RoomActived;
    public List<int> RoomDisActived;
    public GameObject KeysUI;
    private static GameControll instance;
    public static GameControll Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<GameControll>();
            }
            return instance;
        }
        private set{
            instance = value;
        }
    }
    public SavePlayer SavePlayerMain;
    public Button btnSave;
    public Button btnDeleteSave;
    public GameObject Arrow;
    private void Awake() {
        CallDataSave();
    }

    private void Start() {
        _rigidbodyPlayer = Player.GetComponent<Rigidbody>();
        SetGameConfig();
    }
    private void Update() {
        if(CheckStartPlayer){
            if(Mod == 4 && CheckActiveSCP){
                Effect.Instance.Changevignette(FindNearst2());
            }else{
                Effect.Instance.Changevignette(FindNearst());
            }
        }else{
            Effect.Instance.Changevignette(0);
        }
    }
    public void CallDataSave(){
        SavePlayerMain = PlayerPrefsExtra.GetObject("HieulajjNextdoor",new SavePlayer());
    }
    public void WinGame(){
        Camera2.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y+1.5f,  Player.transform.position.z+4.5f);
        SwitchCamera.Instance.cameraPositionChange(1);
        RigidbodyFirstPersonPlayer.m_RigidBody.velocity = Vector3.zero;
        RigidbodyFirstPersonPlayer.AnimatorPlayer.SetTrigger("win");
        UIManager.Instance.UICanvasInGame.SetActive(false);
        CheckStart = false;
        CheckStartPlayer = false;
        Invoke("ResetGameCall",3);
    }
    void ResetGameCall(){
        UIManager.Instance.UICanvasReStartGame.SetActive(true);
        UIManager.Instance.TextEffectDied.TextMesh.text = "Win";
        UIManager.Instance.TextEffectDied.StartZoom();
        UIManager.Instance.UICanvasInGame.SetActive(false);
        GameControll.Instance.DisableChildEnemyPlayerKeys();
    }

    public void PlayGame(){
        InstantiateMap();
        RestartGame();
    }
    private void SetGameConfig()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
    }

    public void RestartGame(){
        switch(Mod){
            case 4:
                SetupGame(0,0,4);
                break;
            case 5:
                SetupGame(0,0,0);
                break;
            default:
                SetupGame(1,0,Random.Range(0,5));
                break;
        }
    }
    private void SetupGame(int enemyInt, int KeyInt, int BotInt){
        switchCamera.cameraPositionChange(0);
        this.enemyInt = enemyInt;
        this.botInt = BotInt;
        if(Mod!=5){
            MapMain.SetActive(true);
        }else{
            Mix();
        }
        //player
        Player.SetActive(true);
        PlayerController.Instance.ResetPlayerController();

        //add target
        TargetforEnemy.Add(Player);

        int amountKey = SpawnPooledObjectKeys(KeyInt, null);
        for(int i=0;i < amountKey; i++){
            GameObject key = GetKeys();
            key.transform.position = GetIndex().transform.position;
            key.SetActive(true);
        }
        CheckStartPlayer =true;

        switch(Mod){
            case 4:
                PlayerController.Instance.fps.uIPlayerManager.InActiveKey();
                SCPuiMod4forPlayer.SetActive(true);
                ModeSCPUI.Instance.ResetUI();
                break;
            case 5:
                SCPuiMod4forPlayer.SetActive(false);
                break;
            default:
                break;
        }
        SpawnBot();
        // if(Mod == 3) {
        //     _mode3.CloseAll();
        //     return;
        // }
        CheckStart = true;
    }
    private void SpawnEnemy(){
        int amountEnemy = SpawnPooledObjectEmemies(enemyInt);
        for(int i=0 ; i< amountEnemy; i++){
            RandomPosition randomPosition = GetIndex();
            GameObject enemyGO = GetChildEnemy();
            Enemy enemy = enemyGO.GetComponent<Enemy>();
            enemy.SetupPosition(randomPosition.transform.position);
            enemyGO.SetActive(true);
            enemy.Setup();
            if(Mod == 3){
                randomPosition.enemyPos = enemy;
                enemy.audioSource.mute = true;
            }
        }
    }

    //map2
    public void SpawnEnemyInDoor(Vector3 positionEnemy){
        for(int i=0; i< Enemies.childCount; i++){
            GameObject g = Enemies.GetChild(i).gameObject;
            if(!g.activeInHierarchy){
                Enemy enemyy = g.GetComponent<Enemy>();
                enemyy.SetupPosition(positionEnemy);
                g.SetActive(true);
                enemyy.Setup();
                return;
            }
        }
        GameObject e =  Instantiate(EnemyPrefabs, positionEnemy, Quaternion.identity, Enemies);
        Enemy enemy = e.GetComponent<Enemy>();
        enemy.Setup();
    }

    public void InstantiateMap(){
        if(PositionMap.Count!=0){
            PositionMap.Clear();
        }
        // if(PositionEnemy.Count!=0){
        //     PositionEnemy.Clear();
        // }
        Map map;
        switch(Mod){
            case 4:
                MapMain = FindMap("map4(Clone)");
                if(MapMain == null){
                    MapMain =  Instantiate(map4, new Vector3(0,0,0), Quaternion.identity, Maps);
                    map = MapMain.GetComponent<Map>();
                    map.BakeMap();
                    map.SendPositions();
                    return;
                }
                MapMain.SetActive(true);
                map = MapMain.GetComponent<Map>();
                map.SendPositions();
                break;
            // case 5:
            //     Mix();
            //     break;
            default:
                break;
        }
            //map.SendPositionChase();
    }
    public void Mix(){
        int [] arr = ShuffleArray.Shuffle(new[]{0,1,2,3,4});
        if(Maps100door.childCount == 0){
            for(int i=0; i<5 ;i++){
            GameObject map1 = Instantiate(MapTest[arr[i]], positionPre, Quaternion.identity, Maps100door);
            Map map2 = map1.GetComponent<Map>();
            // if(map2.GamePlay == 5){
            //     map2.BakeMap();
            // }
            map2.doorMaininMap.SetDoor(i);
            positionPre = map2.positionEndPoint.position;
            MapInGame.Add(map2);       
            if(i==0){
                map2.SendPositions();
                map2.LastBrick.SetActive(true);
                SetupGamePlayMap(map2);
            }
            if(i>=2){
                map1.SetActive(false);
            }else{
                map2.SettingLaimRoomm.SetupLightRoom();
                // if(map2.GamePlay == 1){
                //     map2.SetUpMapGameMode1and2();
                // }   
            }
            }
        }else{
            for(int i=0; i<5 ;i++){
                GameObject g = Maps100door.GetChild(arr[i]).gameObject;
                Map map2 = g.GetComponent<Map>();
                map2.doorMaininMap.SetDoor(i);
                g.transform.position = positionPre;
                // if(map2.GamePlay == 1){
                //     map2.SetUpMapGameMode1and2();
                // }
                positionPre = map2.positionEndPoint.position;
                MapInGame.Add(map2);
                if(i==0){
                    map2.SendPositions();
                    map2.LastBrick.SetActive(true);
                    SetupGamePlayMap(map2);
                }
                if(i<2){
                    g.SetActive(true);
                    map2.SettingLaimRoomm.SetupLightRoom();
                    map2.doorMaininMap.ResetMap();
                    // if(map2.GamePlay == 1){
                    //     map2.SetUpMapGameMode1and2();
                    // }   
                }
            }
        }
    }
    public void MixNext(){
        FlagRoom++;
        FlagInitRoom++;
        if(FlagRoom%2==0){
            FlagInitRoom = 0;
        }
        IndexRoomLastest =  MapInGame[MapInGame.Count-1].transform.GetSiblingIndex();
        Map roomlast = MapInGame[MapInGame.Count-1];
        if(roomlast.GamePlay==5){
            roomlast.ResetMapChase();
        }
        if(roomlast.GamePlay==1){
            if(roomlast.TriggerDecorRoom!=null){
                roomlast.TriggerDecorRoom.ResetActive();
            }
        }
        // if(roomlast.GamePlay == 1){
        //    //roomlast.doorMaininMap.FlagKeyOpen = 1;
        //     roomlast.SetUpMapGameMode1and2();
        // }
        roomlast.LastBrick.SetActive(true);
        roomlast.doorMaininMap.SetDoor(0);
        roomlast.SettingLaimRoomm.SetupLightRoom();
        MapInGame.Clear();
        MapInGame.Add(roomlast);
        RoomActived.Clear();
        RoomDisActived.Clear();

        if(Maps100door.childCount < 10){
            MixNextNext(FlagRoom);
            return;
        }
        //int [] arr = ShuffleArray.Shuffle(new[]{0,1,2,3,4});


        int jumpZoomIndex = 5 * FlagInitRoom;
        int [] arr2 = ShuffleArray.Shuffle(new[]{0,1,2,3,4});
        int [] arr = new int[arr2.Length];
        for(int i = 0 ; i < arr2.Length; i++){
            arr[i] = arr2[i]+jumpZoomIndex;
        }
        
        int flagOverIndexLastRoom = 0;
        for(int i=0; i<5 ;i++){
            int flag = arr[i];
            if(flag==IndexRoomLastest){
                flagOverIndexLastRoom = 1;
            }
            else{
            GameObject g = Maps100door.GetChild(flag).gameObject;
            Map map2 = g.GetComponent<Map>();
            map2.LastBrick.SetActive(false);
            if(map2.GamePlay==5){
                map2.ResetMapChase();
            }
            if(map2.GamePlay==1){
                if(map2.TriggerDecorRoom!=null){
                    map2.TriggerDecorRoom.ResetActive();
                }
            }

            if(flagOverIndexLastRoom==0){
                map2.doorMaininMap.SetDoor(i+1);
            }else{
                map2.doorMaininMap.SetDoor(i);
            }
            g.transform.position = positionPre;
            positionPre = map2.positionEndPoint.position;
            MapInGame.Add(map2);
            if(i<1){
                g.SetActive(true);
                map2.doorMaininMap.ResetMap();
                map2.SettingLaimRoomm.SetupLightRoom();
            }else{
                g.SetActive(false);
                map2.SettingLaimRoomm.ResetLightRoom();
            }
            }
        }
    }

    public void MixNextNext(int flagRoom){
        int jumpZoomIndex = 5 * (FlagRoom);
        int [] arr2 = ShuffleArray.Shuffle(new[]{0,1,2,3,4});
        int [] arr = new int[arr2.Length];
        for(int i = 0 ; i < arr2.Length; i++){
            arr[i] = arr2[i]+jumpZoomIndex;
        }
        for(int i=0; i<5 ;i++){
            GameObject map1 = Instantiate(MapTest[arr[i]], positionPre, Quaternion.identity, Maps100door);
            Map map2 = map1.GetComponent<Map>();
            map2.doorMaininMap.SetDoor(i+1);
            positionPre = map2.positionEndPoint.position;
            MapInGame.Add(map2);       
            if(i<1){
                map1.SetActive(true);
                map2.doorMaininMap.ResetMap();
                map2.SettingLaimRoomm.SetupLightRoom();
            }else{
                map1.SetActive(false);
                map2.SettingLaimRoomm.ResetLightRoom();
            }
        }
    }
    public RandomPosition GetIndex(){
        int Index;
        do
        { 
            Index = Random.Range(0,PositionMap.Count);
        } while (PreRandom.Contains(Index));
        PreRandom.Add(Index);
        return PositionMap[Index];
    }
    public void DisplayEnemy(){
        for(int i = 0 ; i < Enemies.transform.childCount; i++){
            GameObject g = Enemies.transform.GetChild(i).gameObject;
            if(g.activeInHierarchy){
                Enemy enemy = g.GetComponent<Enemy>();
                enemy.checkVisionPlayer = false;
                enemy.TargetChase = null;
                if(Mod == 3){
                    enemy.Play = false;
                    enemy.audioSource.mute = false;
                }
                g.SetActive(false);
            }
        }
    }
    public void DisableChildEnemyPlayerKeys(){
        PlayerPrefsExtra.SetObject("HieulajjNextdoor",SavePlayerMain);
        KeysUI.SetActive(false);
        //enemy
        DisplayEnemy();
        //bot
        for(int i = 0 ; i < Bots.transform.childCount; i++){
            GameObject g = Bots.transform.GetChild(i).gameObject;
            if(g.activeInHierarchy){
                g.SetActive(false);
            }
        }
        //keys
        for(int i = 0; i < Keys.transform.childCount; i++){
            GameObject g = Keys.transform.GetChild(i).gameObject;
            if( g.activeInHierarchy ){
                g.SetActive(false);
            }
        }
        //lights
        for(int i = 0; i < Lights.transform.childCount; i++){
            GameObject g = Lights.transform.GetChild(i).gameObject;
            if( g.activeInHierarchy ){
                g.SetActive(false);
            }
        }
        //reset position
        PreRandom.Clear();
        // reset Target
        TargetforEnemy.Clear();

        switch(Mod){
            case 4:
                MapMain.SetActive(false);
                if(Booms.transform.childCount!=0){
                    ModeSCPUI.Instance.boomWait.DestroyBoom();
                }
                for(int i = 0 ; i < Booms.transform.childCount; i++){
                    GameObject g = Booms.transform.GetChild(i).gameObject;
                    if(g.activeInHierarchy){
                        g.SetActive(false);
                    }
                }
                for(int i = 0 ; i < SCPEnemys.transform.childCount; i++){
                    GameObject g = SCPEnemys.transform.GetChild(i).gameObject;
                    if(g.activeInHierarchy){
                        g.SetActive(false);
                    }
                }
                CheckActiveBotMode4 = true;
                ResetMod4();
                break;
            case 5:
                ResetMode5();
                break;
        }

        PlayerStatus playerStatus = Player.GetComponent<PlayerStatus>();
        if(playerStatus.UIPlayer.arrow.gameObject.activeInHierarchy){
            playerStatus.UIPlayer.arrow.gameObject.SetActive(false);
            playerStatus.UIPlayer.findKeysBtn.interactable = true;
        }
        //player
        Player.SetActive(false);
        //key
        GameControll.Instance.KeyInt = 0;
        UIManager.Instance.TextCoin.text = "0";

        // ResetMod4();
        FlagRoom = 0;
        FlagInitRoom = 0;
        TimelineStart.Instance.ResetStop();

        //reset5()
        if(AudioManager.Instance.LongSource.isPlaying){
            AudioManager.Instance.LongSource.Stop();
        }

    }
    // keys
    public GameObject GetKeys(){
        for(int i = 0 ; i < Keys.transform.childCount; i++){
            GameObject g = Keys.transform.GetChild(i).gameObject;
            if(!g.activeInHierarchy){
                return g;
            }
        }
        return null;
    }
    
    public GameObject GetChildEnemy(){
        for(int i = 0 ; i < Enemies.transform.childCount; i++){
            GameObject g = Enemies.transform.GetChild(i).gameObject;
            if(!g.activeInHierarchy){
                return g;
            }
        }
        return null;
    }
    //MOD4
    public void ChangChaseEnemyMod4(){
        for(int i = 0 ; i < SCPEnemys.transform.childCount; i++){
            GameObject g = SCPEnemys.transform.GetChild(i).gameObject;
            if(g.activeInHierarchy){
                SCPEnemy sCPEnemy = g.GetComponent<SCPEnemy>();
                sCPEnemy.ChooseTarget();
            }
        }
    } 
    public void ResetMod4(){
        //mode 4
        IntBoom = 0;
        IntSCP = 0;
        flagButtonBoom = 0;
        CheckActiveSCP = false;
        CheckInvisible = false;
    }
    public void ResetBoomVsSCP(){
        flagButtonBoom = 0;
        IntBoom = 0;
        CheckActiveSCP = false;
        CheckDisActiveEnemySCP();
        //enemy null target
        for(int i = 0 ; i < SCPEnemys.transform.childCount; i++){
            GameObject g = SCPEnemys.transform.GetChild(i).gameObject;
            if(g.activeInHierarchy){
                SCPEnemy sCPEnemy = g.GetComponent<SCPEnemy>();
                sCPEnemy.TargetChase = null;
            }
        }
    }

    public GameObject GetChildBot(){
        for(int i = 0 ; i < Bots.transform.childCount; i++){
            GameObject g = Bots.transform.GetChild(i).gameObject;
            if(!g.activeInHierarchy){
                return g;
            }
        }
        return null;
    }
    //spawn enemy
    public int SpawnPooledObjectEmemies(int amount){

        int h = amount - Enemies.transform.childCount;
        if(h > 0){
            for(int i=0; i<h; i++){
                RandomPosition randomPosition = GetIndex();
                GameObject g = Instantiate(EnemyPrefabs, randomPosition.transform.position, Quaternion.identity, Enemies.transform);
                Enemy enemy = g.GetComponent<Enemy>();
                enemy.Setup();
                if(Mod == 3){
                    randomPosition.enemyPos = g.GetComponent<Enemy>();
                    enemy.audioSource.mute = true;
                }
            }
            return(amount - h);
        }else{
            return amount;
        }
    }
    private void SpawnBot(){
        int amountBot = SpawnPooledObjectBot(botInt);
        for(int i=0 ; i< amountBot; i++){
            int index =  Random.Range(0, PositionMap.Count);
            GameObject botGO = GetChildBot();
            Bot bot = botGO.GetComponent<Bot>();
            bot.ResetBot();
            bot.SetUpBot();
            bot.SetupPosition(PositionMap[index].transform.position);
            botGO.SetActive(true);
            TargetforEnemy.Add(botGO);
        }
    }

    public Transform RandomPositionBot(){
        return PositionMap[Random.Range(0, PositionMap.Count)].transform;
    }
 
    //spawn bot
    public int SpawnPooledObjectBot(int amount){
        int h = amount - Bots.transform.childCount;
        if(h>0){
            for(int i=0; i<h; i++){
                int index =  Random.Range(0, PositionMap.Count);
                //PositionMap

                GameObject g = Instantiate(BotPrefabs, PositionMap[index].transform.position, Quaternion.identity, Bots.transform);
                Bot bot = g.GetComponent<Bot>();
                bot.SetUpBot();
                TargetforEnemy.Add(g);
            }
            return ( amount - h );
        }else{
            return amount;
        }
    }


    //spawn key
    public int SpawnPooledObjectKeys(int amount, RandomPosition randomPosition){

        int h = amount - Keys.transform.childCount;
        if(h > 0){
            for(int i=0; i<h; i++){
                if(randomPosition == null){
                    randomPosition = GetIndex();
                }
                GameObject g = Instantiate(KeyPrefabs, randomPosition.transform.position, Quaternion.identity, Keys.transform);
            }
            return(amount - h);
        }else{
            return amount;
        }
    }

    public GameObject InstanceLight(){
        for(int i=0 ;i < Lights.childCount; i++){
            GameObject g = Lights.GetChild(i).gameObject;
            if(!g.activeInHierarchy){
                g.SetActive(true);
                LightPoint lightPoint = g.GetComponent<LightPoint>();
                lightPoint.animator.SetFloat("Light",0);
                return g;
            }
        }
        GameObject light = Instantiate(LightPrefab,Lights);
        return light;
    }

    // find Map
    public GameObject FindMap(string namemap){
        for(int i = 0 ; i < Maps.childCount; i++){
            if(Maps.GetChild(i).name == namemap){
                return Maps.GetChild(i).gameObject;
            }
        }
        return null;
    }
    private float FindNearst(){
        float minimumDistance = Mathf.Infinity;
        for(int i = 0 ; i < Enemies.childCount; i++){
            GameObject g = Enemies.transform.GetChild(i).gameObject;
            if(g.activeInHierarchy){
                float distance = Vector3.Distance(Player.transform.position, g.transform.position);
                if (distance < minimumDistance)
                {
                    minimumDistance = distance;
                    nearestEnemy = g.transform;
                }
            }
        }
        return NoiseSign(minimumDistance);
    }

    private float FindNearst2(){
        float minimumDistance = Mathf.Infinity;
        for(int i = 0 ; i < SCPEnemys.childCount; i++){
            GameObject g = SCPEnemys.transform.GetChild(i).gameObject;
            if(g.activeInHierarchy){
                float distance = Vector3.Distance(Player.transform.position, g.transform.position);
                if (distance < minimumDistance)
                {
                    minimumDistance = distance;
                    nearestEnemy = g.transform;
                }
            }
        }
        return NoiseSign(minimumDistance);
    }


    private float NoiseSign(float distance){
        float a = 100 - distance * 1.5f; 
        if(a<=0) return 0;
        return (a/100f);
    }
    
    public void GameActiveMapGo(int h){
        // if(h>=0 && h<MapInGame.Count-1){
        //     MapInGame[h].GetComponent<Map>().LastBrick.SetActive(true);   
        // }

        if(RoomActived.Contains(h)){
            return;
        }
        RoomActived.Add(h);
        // if(h==MapInGame.Count-1){
        //     // MixNext();
        //     Debug.Log("chieyn");
        //     return;
        // }
        
        int a= h-1;

        if(a<0) a=0;
          if(a!=h){
            Map map = MapInGame[a].GetComponent<Map>();
            if(map.GamePlay==5){
                map.ResetMapChase();
            }
            if(map.GamePlay==1){
                if(map.TriggerDecorRoom!=null){
                    map.TriggerDecorRoom.ResetActive();
                }
            }
            MapInGame[a].gameObject.SetActive(false);
            if(a==0){
                map.LastBrick.SetActive(false);
            }
            MapInGame[h].GetComponent<Map>().LastBrick.SetActive(true);
            map.SettingLaimRoomm.ResetLightRoom();
            }
        int b = h+1;
        if(b>=MapInGame.Count){
           //return;
        }else{

            MapInGame[b].gameObject.SetActive(true);
            Map map2  = MapInGame[b].GetComponent<Map>();
            map2.doorMaininMap.ResetMap();
            map2.SettingLaimRoomm.SetupLightRoom();
            if(map2.doorMaininMap.IndexRoomDoorNumber>=100){
                WinGame();
            }
            SetupGamePlayMap(map2);
        }
        if(h==MapInGame.Count-1){
            MixNext();
            return;
        } 
    }
    public void CheckActiveEnemySCP(){
        //CheckActiveSCP = true;
        for(int i=0 ; i < SCPEnemys.childCount; i++){
            Transform enemy = SCPEnemys.GetChild(i);
            if(enemy.gameObject.activeInHierarchy){
                SCPEnemy scpenemy = enemy.GetComponent<SCPEnemy>();
                scpenemy.ActiveRun();
            }
        }
    }
    public void CheckDisActiveEnemySCP(){
        for(int i=0 ; i < SCPEnemys.childCount; i++){
            Transform enemy = SCPEnemys.GetChild(i);
            if(enemy.gameObject.activeInHierarchy){
                SCPEnemy scpenemy = enemy.GetComponent<SCPEnemy>();
                scpenemy.ActiveSitdown();
            }
        }
    }

    public void ResetMode5(){
        for(int i = 0; i < Maps100door.transform.childCount; i++){
            GameObject g = Maps100door.transform.GetChild(i).gameObject;
                //if(g.activeInHierarchy){
                    Map map = g.GetComponent<Map>();
                    map.SettingLaimRoomm.ResetLightRoom();
                    map.LastBrick.SetActive(false);
                    if(map.GamePlay==5){
                        map.ResetMapChase();
                        TimelineStart.Instance.scpMain.CheckActive = false;
                    }
                    if(map.GamePlay==1){
                        if(map.TriggerDecorRoom!=null){
                            map.TriggerDecorRoom.ResetActive();
                        }
                    }
                    g.SetActive(false);
                //}
            }
            positionPre = new Vector3(0,0,0);
            MapInGame.Clear();
            RoomActived.Clear();
            RoomDisActived.Clear();
    }

    public void ActionAnimationLight(){
        for(int i=0 ; i< Lights.childCount; i++){
            GameObject g = Lights.GetChild(i).gameObject;
            if(g.activeInHierarchy){
                LightPoint lightPoint = g.GetComponent<LightPoint>();
                lightPoint.animator.SetFloat("Light",Random.Range(1,3));
            } 
        }
    }
    public void ActionAnimationLightRemix(){
        for(int i=0 ; i< Lights.childCount; i++){
            GameObject g = Lights.GetChild(i).gameObject;
            if(g.activeInHierarchy){
                LightPoint lightPoint = g.GetComponent<LightPoint>();
                lightPoint.animator.SetFloat("Light",4);
            } 
        }
    }

    public void SpawnEye(Vector3 positionSpawn){
        for(int i=0; i< Eyes.childCount; i++){
            GameObject g = Eyes.GetChild(i).gameObject;
            if(!g.activeInHierarchy){
                g.transform.position = positionSpawn;
                float hh = Random.Range(0.1f,0.4f);
                g.transform.localScale = new Vector3(hh, hh, hh);
                g.transform.LookAt(GameControll.Instance.Player.transform);
                g.SetActive(true);
                return;
            }
        }
        if(Eyes.childCount>=30){return;}
        GameObject e =  Instantiate(EyePreb, positionSpawn, Quaternion.identity, Eyes);
        e.transform.LookAt(GameControll.Instance.Player.transform);
    }

    public void SetupGamePlayMap(Map map2){
        switch(map2.GamePlay){
            case 1:
                map2.SetUpMapGameMode1and2(); 
                NotificationUI.Instance.SendNotofication("FIND THE KEY TO OPEN DOOR");
                break;
            case 3:
                map2.SetUpMapGameMode1and2(); 
                ActionAnimationLightRemix();
                NotificationUI.Instance.SendNotofication("FIND THE KEY TO OPEN DOOR");
                break;
            case 2:
                GameControll.Instance.KeysUI.SetActive(false);
                map2.ActiveEnemy.ResetTriggerEnemy();
                if(!map2.CheckBake){
                    map2.BakeMap();
                    map2.CheckBake = true;
                }
                break;
            case 5:
                GameControll.Instance.KeysUI.SetActive(false);
                if(!map2.CheckBake){
                    map2.BakeMap();
                    map2.CheckBake = true;
                }
                break;
        }
    }
}
