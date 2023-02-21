using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartGameUI : MonoBehaviour
{
    [SerializeField] private Button StartBtn;
    [SerializeField] private Button QuitBtn;
    void Start()
    {
        StartBtn.onClick.AddListener(()=>{
            UIManager.Instance.UICanvasSelectModes.SetActive(true);
            gameObject.SetActive(false);
        });
    }
}
