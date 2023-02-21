using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoBehaviour
{
    public Button RestartGameBtn;
    public Button BackgameBtn;
    public Button QuitBtn;
    public GameObject UICanvasSelectModes;
    public GameObject UICanvasReStartGame;
    public GameObject UICanvasSelectNextBot;
    public GameObject UICanvasSelectHero;
    public GameObject UICanvasInGame;
    public GameObject UICanvasStartStartGame;
    public GameObject UICanvasCustomNextBot;
    public TMP_Text text_InHero;
    [SerializeField] private Button BackInSelectHero;
    [SerializeField] private Button NextBtn;
    public GameObject Jumpscare;
    public TextEffect TextEffectDied;

    //player
    public TextMeshProUGUI TextCoin;

    private static UIManager instance;
    public static UIManager Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
        private set{
            instance = value;
        }
    }
    void Start()
    {

        RestartGameBtn.onClick.AddListener(()=>{
            UICanvasReStartGame.SetActive(false);
            UICanvasInGame.SetActive(true);
            AudioManager.Instance.SFXSource.PlayOneShot(AudioManager.Instance.StartHorrorTrack);
            GameControll.Instance.RestartGame();
            AudioManager.Instance.SFXSource.PlayOneShot(AudioManager.Instance.ButtonTrack);
        });

        QuitBtn.onClick.AddListener(()=>{
            GameControll.Instance.CheckStart = false;
            GameControll.Instance.CheckStartPlayer = false;
            UICanvasSelectModes.SetActive(true);
            UICanvasInGame.SetActive(false);
            GameControll.Instance.DisableChildEnemyPlayerKeys();
            SwitchCamera.Instance.cameraPositionChange(1);
            AudioManager.Instance.SFXSource.PlayOneShot(AudioManager.Instance.ButtonTrack);
        });

        BackgameBtn.onClick.AddListener(()=>{
            UICanvasSelectModes.SetActive(true);
            UICanvasReStartGame.SetActive(false);
            AudioManager.Instance.SFXSource.PlayOneShot(AudioManager.Instance.ButtonTrack);
        });

        BackInSelectHero.onClick.AddListener(()=>{
            UICanvasSelectModes.SetActive(true);
            UICanvasSelectHero.SetActive(false);
            AudioManager.Instance.SFXSource.PlayOneShot(AudioManager.Instance.ButtonTrack);
        });
    }
}
