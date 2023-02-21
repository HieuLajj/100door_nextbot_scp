using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip StartHorrorTrack;
    public AudioClip WarningTrack;
    public AudioClip ButtonTrack;
    public AudioClip OpenDoorTrack;
    public AudioClip CloseDoorTrack;
    public AudioClip SwitchFlastlight;
    public AudioClip JumpcareTrack;
    public AudioClip DoorOSTTrack;
    public AudioClip ThunderTrack;
    public AudioClip MagicCoinTrack;
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get{
            instance = FindObjectOfType<AudioManager>();
            if(instance == null){
                instance = new GameObject("Audio Manager", typeof(AudioManager)).GetComponent<AudioManager>();
            }
            return instance;
        }
        private set{
            instance = value;
        }
    }
    public AudioSource SFXSource;
    public AudioSource LongSource;
    void Start()
    {
        SFXSource = this.gameObject.AddComponent<AudioSource>();
        SFXSource.loop = false;
        SFXSource.playOnAwake = false;

        LongSource = this.gameObject.AddComponent<AudioSource>();
        LongSource.loop = false;
        LongSource.playOnAwake = false;
    }
}
