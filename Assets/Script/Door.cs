using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public RandomPosition randomPosition;
    public bool IsOpen = false;
    [SerializeField]
    private bool IsRotatingDoor = true;
    [SerializeField]
    private float Speed = 4f;
    [Header("Rotation Configs")]
    [SerializeField]
    private float RotationAmount = 90f;
    [SerializeField]
    private float ForwardDirection = 0;
    [Header("Sliding Configs")]
    [SerializeField]
    private Vector3 SlideDirection = Vector3.back;
    [SerializeField]
    private float SlideAmount = 1.9f;

    private Vector3 StartRotation;
    private Vector3 StartPosition;
    private Vector3 Forward;
    public DoorMain doorMain;
    private Coroutine AnimationCoroutine;

    
    public Transform Khungdoor;
    public Transform Trongdoor;

    private void Awake()
    {
        StartRotation = transform.rotation.eulerAngles;
        // Since "Forward" actually is pointing into the door frame, choose a direction to think about as "forward" 
        Forward = transform.right;
        StartPosition = transform.position;
    }

    public void Open(Vector3 UserPosition)
    {
        if (!IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if (IsRotatingDoor)
            {
                float dot = Vector3.Dot(Forward, (UserPosition - transform.position).normalized);
                
                if(GameControll.Instance.Mod == 5){
                    if (doorMain.FakeDoor==1){
                        Debug.Log("Ban da chon sai cua");
                        GameControll.Instance.switchCamera.cameraPositionChange(1);
                        LookAtTargetCamera.Instance.CheckActiveFollow = false;
                        GameControll.Instance.ThrowPlayer = Vector3.zero;
                        LookAtTargetCamera.Instance.TimelineDestroy();
                        GameControll.Instance.Player.GetComponent<RigidbodyFirstPersonController>().CheckDestroy = true;
                    }else{
                        if(dot>0){
                            GameControll.Instance.GameActiveMapGo(doorMain.IndexDoor);
                        }
                    }
                    // else{
                    //     GameControll.Instance.GameActiveMapBack(doorMain.IndexDoor);
                    // }
                }
                AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
            }
            else
            {
                AnimationCoroutine = StartCoroutine(DoSlidingOpen()); 
            }
        }
    }

    private IEnumerator DoRotationOpen(float ForwardAmount)
    {
        if(randomPosition?.enemyPos!=null){
            randomPosition.enemyPos.Play = true;
            randomPosition.enemyPos.audioSource.mute = false;
        }


        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;
        if (ForwardAmount < ForwardDirection)
        {
            endRotation = Quaternion.Euler(new Vector3(-90, StartRotation.y + RotationAmount, 0));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(-90, StartRotation.y - RotationAmount, 0));
        }

        IsOpen = true;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }

    private IEnumerator DoSlidingOpen()
    {
        Vector3 endPosition = StartPosition + SlideAmount * SlideDirection;
        Vector3 startPosition = transform.position;

        float time = 0;
        IsOpen = true;
        while (time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }

    public void Close()
    {
        if (IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if (IsRotatingDoor)
            {
                AnimationCoroutine = StartCoroutine(DoRotationClose());
            }
            else
            {
                AnimationCoroutine = StartCoroutine(DoSlidingClose());
            }
        }
    }

    public void ResetDooor(){
        Khungdoor.localRotation = Quaternion.Euler(new Vector3(-90,0,0));
        Trongdoor.localRotation = Quaternion.Euler(new Vector3(-90,0,0));
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        IsOpen = false;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }

    private IEnumerator DoSlidingClose()
    {
        Vector3 endPosition = StartPosition;
        Vector3 startPosition = transform.position;
        float time = 0;

        IsOpen = false;

        while (time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }
}