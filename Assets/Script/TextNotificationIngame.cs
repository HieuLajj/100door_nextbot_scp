using System.Collections;
using UnityEngine;
using TMPro;

public class TextNotificationIngame : MonoBehaviour
{
    [SerializeField] public TMP_Text textnoti;
    Coroutine coroutineText;
    private static TextNotificationIngame instance;
    private bool checkActive = false;
    public static TextNotificationIngame Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<TextNotificationIngame>();
            }
            return instance;
        }
        private set{
            instance = value;
        }
    }   
    public void SetNotification(string h){
        string FlagString = $"[{System.DateTime.UtcNow.ToString("HH:mm:ss")}] {h} diee";
        textnoti.text  = FlagString;
        if(checkActive){
            StopCoroutine(coroutineText);
        }
        coroutineText = StartCoroutine(Faded());
    }
    IEnumerator Faded(){
        checkActive = true;
        for(int f = 255; f > 0; f -= 1){
            yield return null;
            textnoti.color = new Color32( 255, 0, 0, (byte)f );
        }
        checkActive = false;
    }
}
