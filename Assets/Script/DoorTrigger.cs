using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    private Door Door;
    [SerializeField]
    private DoorMain doorMain;
    private void OnTriggerEnter(Collider other)
    {
        if(doorMain.FlagKeyOpen == 1 && GameControll.Instance.KeyInt <= 0){
            return;
        }else if(doorMain.FlagKeyOpen == 1){
            GameControll.Instance.KeyInt--;
            UIManager.Instance.TextCoin.text = GameControll.Instance.KeyInt+"";
            doorMain.FlagKeyOpen=0;
        }

        if (other.CompareTag("Player") || other.CompareTag("Enemy") || other.CompareTag("Bot"))
        {
            if (!Door.IsOpen)
            {
                Door.Open(other.transform.position);
                if(other.CompareTag("Player")){
                    AudioManager.Instance.SFXSource.PlayOneShot(AudioManager.Instance.OpenDoorTrack);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy") || other.CompareTag("Bot"))
        {
            if (Door.IsOpen)
            {
                Door.Close();
                if(other.CompareTag("Player")){
                    AudioManager.Instance.SFXSource.PlayOneShot(AudioManager.Instance.CloseDoorTrack);
                }
            }
        }
    }
}