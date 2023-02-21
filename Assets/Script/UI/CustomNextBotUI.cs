using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomNextBotUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button BackBtn;
    [SerializeField] private Button CreateBtn;
    [SerializeField] private GameObject imagePreview;
    public Image ImageNextBotCustom;
    void Start()
    {
        BackBtn.onClick.AddListener(()=>{
            imagePreview.SetActive(false);
            UIManager.Instance.UICanvasSelectHero.SetActive(true);
            gameObject.SetActive(false);
        });
        CreateBtn.onClick.AddListener(()=>{
            PickFileAndroid.Instance.PickImage();
            ImageNextBotCustom.sprite = GameControll.Instance.PathImageSprite;
            if(GameControll.Instance.PathImageSprite!=null){
                imagePreview.SetActive(true);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
