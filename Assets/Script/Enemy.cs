using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;
public class Enemy : MonoBehaviour
{
    public NavMeshAgent Mob;
    public GameObject Player;
    public float MobDistanceRun = 4.0f;
    public float RotationAmount = 2f;
    public int TicksPerSecond = 60;
    public bool Play = false;
    public GameObject EnemyBody;
    public SpriteRenderer imageEnemy;
    public VideoPlayer videoPlayer;
    // [SerializeField] private NavMeshAgent _navMeshAgentEnemy;
    public float speed = 10.0f;
    public float rotSpeed = 5.0f;

    //See player
    public float lookRadius = 10.0f;
    public AudioSource audioSource;
    public int currentWp = 0;
    public bool checkVisionPlayer = false;
    public GameObject TargetChase;
    void Start()
    {
        Player = GameControll.Instance.Player;
    }
    private void Update() {
        //if(GameControll.Instance.CheckStart || Play){
            // if(TargetChase == null){
            //     ChooseTarget();
            // }
            // if (TargetChase.transform.position == null) return;
            Mob.SetDestination(GameControll.Instance.Player.transform.position); //brings enemy to the target(player) position
        //}
    }
    public void ChooseTarget()
    {
        float closestTargetDistance = float.MaxValue;
        NavMeshPath Path = new NavMeshPath();;
        for (int i = 0; i < GameControll.Instance.TargetforEnemy.Count; i++)
        {
            if(GameControll.Instance.TargetforEnemy[i].activeInHierarchy) { 
                if (NavMesh.CalculatePath(transform.position, GameControll.Instance.TargetforEnemy[i].transform.position, Mob.areaMask, Path))
                {
                    float distance = Vector3.Distance(transform.position, Path.corners[0]);

                    for (int j = 1; j < Path.corners.Length; j++)
                    {
                        distance += Vector3.Distance(Path.corners[j - 1], Path.corners[j]);
                    }

                    if (distance < closestTargetDistance)
                    {
                        closestTargetDistance = distance;
                        TargetChase = GameControll.Instance.TargetforEnemy[i];
                
                    }
                }
            }
        }
                       
    }
    public void Setup(){
        //InvokeRepeating("CheckTargetChase",0,3);
        Mob.speed = 20;
        audioSource.clip = GameControll.Instance.audioClip;
        audioSource.Play();
        if(GameControll.Instance.PathImageSprite!=null){
            imageEnemy.sprite = GameControll.Instance.PathImageSprite;
			float newY = imageEnemy.sprite.bounds.size.y;
			float newX = imageEnemy.sprite.bounds.size.x;
            
			imageEnemy.transform.localScale = new Vector2(3/newX , 3/newY);
            //imageEnemy.transform.localScale = new Vector2( temp ,1);
            imageEnemy.gameObject.SetActive(true);
			videoPlayer.gameObject.SetActive(false);
            return;
        }
        if(GameControll.Instance.PathVideo!=null){
            if(GameControll.Instance.PathVideo!=""){
                videoPlayer.url = GameControll.Instance.PathVideo;
            }
        }else{
            if(GameControll.Instance.linkVideo != null){
                videoPlayer.clip = GameControll.Instance.linkVideo;  
            }else{
                videoPlayer.clip = GameControll.Instance.defaultVideo;
            }
        }
        imageEnemy.gameObject.SetActive(false);
		videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
    }
    public void SetupPosition(Vector3 positionVector){
        Mob.Warp(positionVector);
    }
}
