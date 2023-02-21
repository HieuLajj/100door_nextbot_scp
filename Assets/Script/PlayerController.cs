using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
public class PlayerController : MonoBehaviour
{
    //public FixedJoystick MoveJoystick;
    public FloatingJoystick floatingJoystick;
    public FixedButton JumpButton;
    public FixedTouchField TouchField;
    public RigidbodyFirstPersonController fps;
    [SerializeField] private Rigidbody rigidbodyPlayer;
    [SerializeField] private PlayerStatus playerStatus;
    private static PlayerController instance;
    public static PlayerController Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<PlayerController>();
            }
            return instance;
        }
        private set{
            instance = value;
        }
    }
    public GameObject SpawnEyesInWall;

    // Update is called once per frame
    void Update()
    {  
        fps.RunAxis = new Vector2(floatingJoystick.Horizontal,floatingJoystick.Vertical);
        fps.JumpAxis = JumpButton.Pressed;
        fps.mouseLook.LookAxis = TouchField.TouchDist;
    }

    public void ResetPlayerController(){
        if(GameControll.Instance.Mod == 4){
            transform.position = new Vector3(0, 0.5f, 0);
        }else{
            transform.position = GameControll.Instance.GetIndex().transform.position;
        }
        playerStatus.ResetStatus();
        fps.ResetPlayer();
        if(SpawnEyesInWall.activeInHierarchy){
            SpawnEyesInWall.SetActive(false);
        }
        if(!fps.cam.gameObject.activeInHierarchy){
            fps.cam.gameObject.SetActive(true);
        }
        if(!playerStatus.UIPlayer.gameObject.activeInHierarchy){
            playerStatus.UIPlayer.gameObject.SetActive(true);
        }
    }
}
