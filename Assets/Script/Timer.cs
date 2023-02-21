using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    Coroutine coroutine;
    [Header("Component")]
    public TextMeshProUGUI timerText;
    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;
    [Header ("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;
    private bool check = false;

    // Update is called once per frame

    private void SetTimerText(float h){
        timerText.text = h.ToString("0.00");
    }
    public void SetupTime(){
        gameObject.SetActive(true);
        coroutine =  StartCoroutine(StartTime(currentTime));
    }
    public void DestroyTimer(){
        if(check){
            StopCoroutine(coroutine);
            check = false;
        }
        ResetText();
    }
    IEnumerator StartTime(float currentTime)
    {   check = true;
        while(currentTime>0){
            yield return null;
            currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;
            if(hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime >= timerLimit))){
                currentTime = timerLimit;
                SetTimerText(currentTime);
                timerText.color = Color.red;
                enabled = false;
            }
            SetTimerText(currentTime);
        }
        check = false;
        GameControll.Instance.WinGame();
        ResetText();
    }
    private void ResetText(){
        timerText.color = Color.white;
    }
    public void HideTime(){
        gameObject.SetActive(false);
    }
 
}
