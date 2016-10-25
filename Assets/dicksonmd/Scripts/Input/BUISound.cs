using UnityEngine;
using System.Collections;

public class BUISound : MonoBehaviour {

    public AudioClip missileAlertSound;
    public int missileAlertSoundIndex = 1;

    private AudioSource[] audioSources;

	// Use this for initialization
	void Start () {
        this.audioSources = this.GetComponents<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    AudioSource getEmptySource()
    {
        int i = 0;
        for(i=0; i < this.audioSources.Length; i++)
        {
            if(!this.audioSources[i].isPlaying)
            {
                return this.audioSources[i];
            }
        }
        return this.audioSources[i];
    }

    public void playMissileAlert()
    {
        var source = this.audioSources[this.missileAlertSoundIndex];
        if(! source.isPlaying) source.PlayOneShot(this.missileAlertSound);
    }

    public void stopMissileAlert()
    {
        var source = this.audioSources[this.missileAlertSoundIndex];
        source.Stop();
        source.loop = false;
    }

}
