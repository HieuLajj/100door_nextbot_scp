using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerCam : MonoBehaviour
{
    public Camera _cam;
    private float _camFOV;
    public float CheckTime;
    public float CheckAgle;
    private void Start() {
        _cam  =  transform.GetComponent<Camera>();
        _camFOV =  _cam.fieldOfView;
    }
    public void DoFov(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue,  100 * Time.deltaTime);  
        //  GetComponent<Camera>().DOFieldOfView(_camFOV, 2*Time.deltaTime); 
    }
     public void DoFovJump(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue,  10 * Time.deltaTime);  
    }

    public void DoTilt(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    }
}
