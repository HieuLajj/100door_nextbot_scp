using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NotificationUI : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] private Image _imageBackground;
    [SerializeField] private GameObject _notifiGameObjects;
    [SerializeField] private TMP_Text _notifiText;
    Coroutine coroutineNotification;
    private bool checkCoroutine;
    private static NotificationUI instance;
    public static NotificationUI Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<NotificationUI>();
            }
            return instance;
        }
        private set{
            instance = value;
        }
    }

    IEnumerator StartChangeColor(int currentColor)
    {   
        checkCoroutine = true;
        while(currentColor < 127){
            yield return null;
            currentColor += 1;
            //_imageBackground.color = new Color32( 0, 0, 0, (byte)currentColor);
            _notifiText.color = new Color32( 255, 0, 0, (byte)(currentColor*2));
        }
        _notifiText.color = new Color32( 255, 0, 0, 0);
        checkCoroutine = false;
        _notifiGameObjects.SetActive(false);
    }
    public void SendNotofication(string textNoti){
       _notifiGameObjects.SetActive(true);
        coroutineNotification = StartCoroutine(StartChangeColor(0));
        _notifiText.text = textNoti;
    }
    public void HideNotification(){
        if(checkCoroutine == true){
            StopCoroutine(coroutineNotification);
            checkCoroutine = false;
        }
       _notifiGameObjects.SetActive(false);
    }
}
