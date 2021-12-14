using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	//Músicas
	//public AudioClip[] clips;
	//public AudioSource musicaBG;
	//SonsFX
	public AudioClip[] clipsFX;
	public AudioSource sonsFX;
	//SonsFXLoop
	public AudioClip[] clipsFXL;
	public AudioSource sonsFXL;


	public static AudioManager instance;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad (this.gameObject);
		}
		else
		{
			Destroy (gameObject);
		}
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		/*
        if (OndeEstou.instance.fase <= 1)
        {
            if (!musicaBG.isPlaying)
            {
				musicaBG.clip = clips[0];
				musicaBG.Play();
			}

		}
		else if(OndeEstou.instance.fase >= 2)
		{
            if (musicaBG.isPlaying)
            {
				musicaBG.Stop();
            }
            if (!musicaBG.isPlaying)
            {
				musicaBG.clip = clips[1];
				musicaBG.Play();
			}

		}

		/*
		if(!musicaBG.isPlaying)
		{
			musicaBG.clip = GetRandom ();
			musicaBG.Play ();
		}
		*/
	}
	/*
	AudioClip GetRandom()
	{
		return clips [Random.Range (0, clips.Length)];
	}
	*/
	public void SonsFXToca(int index)
	{
		sonsFX.clip = clipsFX [index];
		sonsFX.Play ();
	}
	public void SonsFXTocaL(int index)
	{
		sonsFXL.clip = clipsFXL[index];
		sonsFXL.Play();
	}
}
