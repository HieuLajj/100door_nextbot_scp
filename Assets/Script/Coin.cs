using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            GameControll.Instance.SavePlayerMain.coin+=2;
            //audioCoin.PlayOneShot(audioCoin.clip);
            AudioManager.Instance.SFXSource.PlayOneShot(AudioManager.Instance.MagicCoinTrack);
            GameControll.Instance.KeyInt+=1;
            UIManager.Instance.TextCoin.text = GameControll.Instance.KeyInt+"";
            gameObject.SetActive(false);
            other.GetComponent<RigidbodyFirstPersonController>().uIPlayerManager.ResetFindKey();
        }
    }
}
