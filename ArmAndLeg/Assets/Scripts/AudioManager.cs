using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }
	// Use this for initialization
	void Start () {
        FindObjectOfType<ZombiePlayerController>().inventory.armCountEvent.AddListener(PlayAudio);
        FindObjectOfType<ZombiePlayerController>().inventory.legCountEvent.AddListener(PlayAudio);
    }
    private AudioSource m_audioSource;
    void PlayAudio(int num)
    {

        if (num > 2)
        {
            if(m_audioSource.isPlaying)
                m_audioSource.Stop();            
            m_audioSource.Play();
        }
    }
	
	
}
