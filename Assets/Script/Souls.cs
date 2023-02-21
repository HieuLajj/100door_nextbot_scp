using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Souls : MonoBehaviour
{
    public TMP_Text TextCoin;
    private static Souls instance;
    public static Souls Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<Souls>();
            }
            return instance;
        }
        private set{
            instance = value;
        }
    }
    
}
