using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ModeSCPUI : MonoBehaviour
{
    public Button boomBtn;
    public Button enemyBtn;
    public Button invisibleBtn;
    [SerializeField] private GameObject BoomPrefab;
    public Boom boomWait;
    [SerializeField] private Transform cameraMain;
    [SerializeField] private GameObject PreEnemy;
    [SerializeField] private GameObject PreBoom;
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;
    [SerializeField] private Image imageBoomBtn;
    [SerializeField] private Image imageInvisible;
    public WaitUI waitUI;
    // Start is called before the first frame update
    private static ModeSCPUI instance;
    public static ModeSCPUI Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<ModeSCPUI>();
            }
            return instance;
        }
        private set{
            instance = value;
        }
    }
    void Start()
    {
        boomBtn.onClick.AddListener(()=>{
            if(GameControll.Instance.flagButtonBoom==0){
                if(GameControll.Instance.IntBoom < 1){
                    SpawnBoomboom();
                    GameControll.Instance.CheckActiveBotMode4 = false;
                }
            }else if (GameControll.Instance.flagButtonBoom == 1){
                boomBtn.interactable = false;
                enemyBtn.interactable = false;
                GameControll.Instance.CheckActiveEnemySCP();
                boomWait.ActiveBoom();
            }
        });
        enemyBtn.onClick.AddListener(()=>{
            if(GameControll.Instance.IntSCP < 3){
                SpawnEnemy();
            }else{
                NotificationUI.Instance.SendNotofication("Max 3 enemy");
            }
        });
        invisibleBtn.onClick.AddListener(()=>{
            GameControll.Instance.CheckInvisible = !GameControll.Instance.CheckInvisible;
            if(GameControll.Instance.CheckInvisible){
                imageInvisible.color =  new Color32( 0, 0, 0, 255 );
            }else{
                imageInvisible.color =  new Color32( 255, 255, 255, 255 );
            }
            if(GameControll.Instance.CheckActiveSCP){
                GameControll.Instance.ChangChaseEnemyMod4();
            }
        });
    }

    public void SpawnEnemy(){
        Ray ray = new Ray(cameraMain.position, cameraMain.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 5f) && hit.collider.tag == "Floor"){
            SpawnSCP(hit.point);
        }
    }
    public void SpawnBoomboom(){
        Ray ray = new Ray(cameraMain.position, cameraMain.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 5f) && hit.collider.tag == "Floor"){
            SetSprite(2);
            GameControll.Instance.flagButtonBoom++;
            GameControll.Instance.IntBoom++;
            //GameObject g =   Instantiate( BoomPrefab, hit.point, Quaternion.identity, GameControll.Instance.Booms);
            GameObject g = BoomsSpawn(hit.point);
            boomWait = g.GetComponent<Boom>();
            if(!boomWait.ModelBoom.activeInHierarchy){
                boomWait.ModelBoom.SetActive(true);
            }
        }
    }
    //spawn enemy scp
    public GameObject SpawnSCP(Vector3 spawnPosition){
        if(!boomBtn.interactable){
            boomBtn.interactable = true;
        }

        GameControll.Instance.IntSCP++;
        for(int i = 0 ; i < GameControll.Instance.SCPEnemys.transform.childCount; i++){
            GameObject g = GameControll.Instance.SCPEnemys.transform.GetChild(i).gameObject;
            if(!g.activeInHierarchy){
                g.SetActive(true);
                SCPEnemy scpenemy = g.GetComponent<SCPEnemy>();
                scpenemy.SetupPosition(spawnPosition);
                scpenemy.TargetChase = null;
                return g;
            }
        }
        GameObject h =  Instantiate(PreEnemy, spawnPosition, Quaternion.identity, GameControll.Instance.SCPEnemys);
        return h;
    }
    public void SetSprite(int h){
        if(h==1){
            imageBoomBtn.sprite = sprite1;
            boomBtn.interactable = false;
            enemyBtn.interactable = true;
            
        }else{
            imageBoomBtn.sprite = sprite2;
        }

    }
    public void ResetUI(){
        SetSprite(1);
        imageInvisible.color =  new Color32( 255, 255, 255, 255 );
    }
    public void ResetUIWhenDieMod4(){
        imageBoomBtn.sprite = sprite1;
        boomBtn.interactable = true;
        enemyBtn.interactable = true;
    }

    public GameObject BoomsSpawn(Vector3 positionBoom){
        for(int i=0; i< GameControll.Instance.Booms.childCount; i++){
            GameObject g =  GameControll.Instance.Booms.GetChild(i).gameObject;
            if(!g.activeInHierarchy){
                g.transform.position = positionBoom;
                g.SetActive(true);
                return g;
            }
        }
        return  Instantiate( BoomPrefab, positionBoom, Quaternion.identity, GameControll.Instance.Booms);
    }
}
