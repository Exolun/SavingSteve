using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour {

    public GameObject Music;
    public GameObject DeathSound;
    public GameObject DartSound;

    private Dictionary<string, AudioSource> sounds = new Dictionary<string, AudioSource>();
    
	void Start () {
        sounds.Add("Death", DeathSound.GetComponent<AudioSource>());
        sounds.Add("Dart", DartSound.GetComponent<AudioSource>());
    }
    
    public void PlaySound(string name)
    {
        if (sounds.ContainsKey(name))
        {
            sounds[name].Play();
        }
    }
}
