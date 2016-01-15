using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

	private List<AudioSource> sfxAudioSourceBank = new List<AudioSource>();
	private List<AudioSource> uiSFXAudioSourceBank = new List<AudioSource>();
	private int sfxBankSize = 10;
	public AudioMixer masterMixer;
	public AudioMixerGroup bgmGroup;
	public AudioMixerGroup sfxGroup;
	public AudioMixerGroup uiGroup;

	public AudioMixerSnapshot lowBGMSnapshot;
	public AudioMixerSnapshot defaultSnapshot;
	public AudioMixerSnapshot battleSnapshot;

	private Dictionary<AudioClip,AudioSource> activeAudioSources = new Dictionary<AudioClip, AudioSource>();
	public List<AudioClip> hitSFXList = new List<AudioClip>();
	public List<AudioClip> attackSFXList = new List<AudioClip>();
	public List<AudioClip> gruntSFXList = new List<AudioClip>();

	private static SoundManager _instance = null;
	public static SoundManager instance 
	{
		get 
		{
			if(_instance == null) 
			{
				GameObject go = Instantiate(Resources.Load("Prefabs/SoundManager", typeof(GameObject))) as GameObject;//new GameObject("Sound Manager");
				
				_instance = go.GetComponent<SoundManager>();
			}
			return _instance;
		}
	}

	public void Awake() 
	{
		_instance = this;
		GameObject.DontDestroyOnLoad(gameObject);
		
		AudioListener[] aListeners = GameObject.FindObjectsOfType<AudioListener>();
		for(int i = 0; i < aListeners.Length; i++) 
		{
			GameObject.Destroy(aListeners[i]);
		}
		
		gameObject.AddComponent<AudioListener>();
		
		for (int i = 0 ; i < sfxBankSize; i++) {
			GameObject g = new GameObject("SFXAudioSource" + (i + 1));
			AudioSource aSource = g.AddComponent<AudioSource>();
			aSource.outputAudioMixerGroup = sfxGroup;
			GameObject.DontDestroyOnLoad(g);
			g.transform.SetParent(gameObject.transform);
			sfxAudioSourceBank.Add(aSource);
		}

		for (int i = 0 ; i < sfxBankSize; i++) {
			GameObject g = new GameObject("UISFXAudioSource" + (i + 1));
			AudioSource aSource = g.AddComponent<AudioSource>();
			aSource.outputAudioMixerGroup = uiGroup;
			GameObject.DontDestroyOnLoad(g);
			g.transform.SetParent(gameObject.transform);
			uiSFXAudioSourceBank.Add(aSource);
		}
	
	}
	
	private AudioSource _bgmAudioSource = null;
	public AudioSource bgmAudioSource 
	{
		get 
		{
			if(_bgmAudioSource == null) 
			{
				GameObject g = new GameObject("BGMAudioSource");
				GameObject.DontDestroyOnLoad(g);
				g.transform.SetParent(gameObject.transform,false);
				_bgmAudioSource = g.AddComponent<AudioSource>();

				_bgmAudioSource.outputAudioMixerGroup = bgmGroup;
			}
			return _bgmAudioSource;
		}
	}

//	private AudioSource _uiAudioSource = null;
//	public AudioSource uiAudioSource 
//	{
//		get 
//		{
//			if(_uiAudioSource == null) 
//			{
//				GameObject g = new GameObject("UIAudioSource");
//				GameObject.DontDestroyOnLoad(g);
//				g.transform.SetParent(gameObject.transform,false);
//				_uiAudioSource = g.AddComponent<AudioSource>();
//				
//				_uiAudioSource.outputAudioMixerGroup = uiGroup;
//			}
//			return _uiAudioSource;
//		}
//	}

	public void PlayBGM(string path) 
	{
		AudioClip bgmClip = Resources.Load(path) as AudioClip;
		if (bgmClip == null || bgmAudioSource.clip == bgmClip){
			Debug.LogError("BGM is either not found or currently playing.");
			return;
		}
		lowBGMSnapshot.TransitionTo(0);

		bgmAudioSource.clip = bgmClip;
		bgmAudioSource.loop = true;

		bgmAudioSource.Play();
		if (Application.loadedLevelName != "BattleScene")
		{
			defaultSnapshot.TransitionTo(3);
		}
		else
		{
			battleSnapshot.TransitionTo(0);
		}

	}

//	public void PlayUISFX(string path) {
//		uiAudioSource.Stop();
//		AudioClip uiClip = Resources.Load(path) as AudioClip;
//		if (uiClip == null){
//			Debug.LogError("SFX not found");
//			return;
//		}
//		
//		uiAudioSource.clip = uiClip;
//		uiAudioSource.loop = false;
//		
//		uiAudioSource.Play();
//	}

	public AudioSource PlayHitSFX() {
		return PlaySFX(hitSFXList[(int)Random.Range(0,hitSFXList.Count)]);
	}

	public AudioSource PlayAttackSFX() {
		return PlaySFX(attackSFXList[(int)Random.Range(0,attackSFXList.Count)]);
	}

	public AudioSource PlayGruntSFX() {
		return PlaySFX(gruntSFXList[(int)Random.Range(0,gruntSFXList.Count)]);
	}

	public AudioSource PlaySFX(string path) {
		return PlaySFX ((AudioClip)Resources.Load(path));
	}
	public AudioSource PlaySFX(AudioClip aClip) {
		if (aClip == null) 
		{
			Debug.LogError("SFX not found");
			return null;
		}
		AudioSource aSource = GetAudioSourceFromBank();
		if (aSource.clip == aClip && aSource.isPlaying)
		{
			return null;
		}
		if (!activeAudioSources.ContainsKey (aClip))
			activeAudioSources.Add (aClip, aSource);
		activeAudioSources[aClip] = aSource;
		aSource.clip = aClip;
		aSource.Play();
		
		return aSource;
	}

	public AudioSource PlayUISFX(string path) {
		return PlaySFX ((AudioClip)Resources.Load(path));
	}
	public AudioSource PlayUISFX(AudioClip aClip) {
		if (aClip == null) 
		{
			Debug.LogError("SFX not found");
			return null;
		}
		AudioSource aSource = GetUIAudioSourceFromBank();
		if (aSource.clip == aClip && aSource.isPlaying)
		{
			return null;
		}
		if (!activeAudioSources.ContainsKey (aClip))
			activeAudioSources.Add (aClip, aSource);
		activeAudioSources[aClip] = aSource;
		aSource.clip = aClip;
		aSource.Play();
		
		return aSource;
	}

	public AudioSource GetAudioSourceFromBank() {
		
		AudioSource aSource = null;
		
		foreach (AudioSource a in sfxAudioSourceBank)
		{
			if (!a.isPlaying || !a.gameObject.activeSelf)
				aSource = a;
		}
		
		if (aSource == null) {
			foreach (AudioSource a in sfxAudioSourceBank)
			{
				if (!a.loop)
				{
					aSource = a;
					break;
				}
			}
		}
		return aSource;
	}

	public AudioSource GetUIAudioSourceFromBank() {
		
		AudioSource aSource = null;
		
		foreach (AudioSource a in uiSFXAudioSourceBank)
		{
			if (!a.isPlaying || !a.gameObject.activeSelf)
				aSource = a;
		}
		
		if (aSource == null) {
			foreach (AudioSource a in uiSFXAudioSourceBank)
			{
				if (!a.loop)
				{
					aSource = a;
					break;
				}
			}
		}
		return aSource;
	}

	public void FadeOutBGM(float fadeTime) {
		lowBGMSnapshot.TransitionTo(fadeTime);
	}

	public void StopBGM() {
		bgmAudioSource.Stop ();
	}
}
