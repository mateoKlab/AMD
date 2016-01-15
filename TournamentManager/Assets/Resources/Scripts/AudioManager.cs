using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager: MonoBehaviour {
	
	private static AudioManager _instance = null;
	public static AudioManager instance {
		get {
			if(_instance == null) {
				GameObject go = new GameObject("z_audiomanager");

				_instance = go.AddComponent<AudioManager>();
			}
			return _instance;
		}
	}
	private const float DEFAULT_BGM_VOL = 0.125f;
	private float bgm_volume = DEFAULT_BGM_VOL;
	private const float DEFAULT_SFX_VOL = 0.65f;
	private float sfx_volume = DEFAULT_SFX_VOL;

	private List<AudioSource> sfx_audiosources = new List<AudioSource>();
	private int sfx_base_count = 10;

	private string bgm_playing = string.Empty;

	private struct BgmClipToPlay {
		public BgmClipToPlay (AudioClip clip, float volume, bool loop) {
			this.clip = clip;
			this.volume = volume;
			this.loop = loop;
		}
		public AudioClip clip;
		public float volume;
		public bool loop;
		public void Copy (AudioSource audioSource) {
			audioSource.clip = clip;
			audioSource.volume = volume;
			audioSource.loop = loop;
		}
	}

	private Dictionary<string,AudioClip> clip_bank = new Dictionary<string, AudioClip>();
	private Dictionary<AudioSource,Transform> asrouce_transform = new Dictionary<AudioSource, Transform>();
	private Dictionary<AudioClip,AudioSource> asrouce_stillplaying = new Dictionary<AudioClip, AudioSource>();

	private bool bgmOn = true;
	private bool sfxOn = true;
	//private GameObject playerReference = null;

	private AudioSource _bgmAudioSource = null;
	public AudioSource bgmAudioSource {
		get {
			if(_bgmAudioSource == null) {
				if((_bgmAudioSource = GetComponent<AudioSource>()) == null) {
					_bgmAudioSource = gameObject.AddComponent<AudioSource>();
				}
			}
			return _bgmAudioSource;
		}
	}

	void Awake () {
		_instance = this;
		GameObject.DontDestroyOnLoad(gameObject);

		AudioListener[] auListeners = GameObject.FindObjectsOfType<AudioListener>();
		for(int i = 0; i < auListeners.Length; i++) {
			GameObject.Destroy(auListeners[i]);
		}
		
		gameObject.AddComponent<AudioListener>();
		
		
		for (int x = 0 ; x < sfx_base_count; x++) {
			GameObject g = new GameObject("sfx_instance_"+(x+1));
			AudioSource asrc = g.AddComponent<AudioSource>();
			GameObject.DontDestroyOnLoad(g);
			g.transform.parent = gameObject.transform;
			g.transform.localPosition = Vector3.zero;
			sfx_audiosources.Add(asrc);
			asrouce_transform.Add(asrc,g.transform);
		}

	}
	void OnDestroy () {
		_instance = null;
	}

	public void AddBank(AudioClip clip, string s) {
		if (!clip_bank.ContainsKey(s))
			clip_bank.Add(s,clip);
	}
	
	public Transform get_transform_from_asrouce(AudioSource asrc) {
		return asrouce_transform[asrc];
	}

	public void play_sfx_delayed(AudioClip clip, float f) {
		object[] obj = new object[1];
		obj[0] = clip;
		//static_coroutine.getInstance.DoReflection (this, "playsfx_delayed", obj, f);
	}

	// wee need this reflection cannot find the right playsfx it has multiple function
	public void playsfx_delayed(AudioClip clip)
	{
		play_sfx (clip);
	}

	public AudioSource get_source_inbank() {

		AudioSource ret = null;

		foreach (AudioSource g in sfx_audiosources)
		{
			if (!g.isPlaying || !g.gameObject.activeSelf)
				ret = g;
		}

		if (ret == null) {
			foreach (AudioSource g in sfx_audiosources)
			{
				if (!g.loop)
				{
					ret = g;
					break;
				}
			}
		}
		return ret;
			
	}
	public AudioSource play_sfx(string path, bool loop = false) {
		return play_sfx ((AudioClip)Resources.Load(path), loop);
	}
	public AudioSource play_sfx(AudioClip aclp, bool loop = false) {
		if (aclp == null)
			return null;

		AudioSource asrc = get_source_inbank();
		if (asrc.clip == aclp && asrc.isPlaying)
			return null;
		Transform tt = get_transform_from_asrouce(asrc);

			tt.parent = transform;
			tt.localPosition = Vector3.zero;
		if (!asrouce_stillplaying.ContainsKey (aclp))
			asrouce_stillplaying.Add (aclp, asrc);
		asrouce_stillplaying [aclp] = asrc;
		asrc.loop = loop;
		asrc.clip = aclp;
		asrc.volume = sfx_volume;
		asrc.Play();

		return asrc;
	}

	public void stop_allsfx(){
		if (asrouce_stillplaying.Count < 1)
			return;

		foreach (AudioSource ac in asrouce_stillplaying.Values)
			ac.Stop ();
	}

	public void stopSFX (AudioClip clip)
	{
		asrouce_stillplaying [clip].Stop ();
	}

	public void stop_bgm(bool fade) {
		if (bgmAudioSource) {
			if(fade)
				FadeoutBgm();
			else
				bgmAudioSource.Stop ();
		}
	}
	public void stop_bgm(){stop_bgm(true);}

	public AudioSource play_sfx(string path, Vector3 p) { return play_sfx (path, p, 1, true, 1, false, false); }
	public AudioSource play_sfx(string path, Vector3 p, float rvol) { return play_sfx (path, p, rvol, true, 1 , false, false); }
	public AudioSource play_sfx(string path, Vector3 p, float rvol, bool connected_listner) {  return play_sfx (path, p, rvol, connected_listner, 1, false, false); }
	public AudioSource play_sfx(string path, Vector3 p, float rvol, bool connected_listner, float pitch) {return play_sfx (path, p, rvol, connected_listner, pitch, false, false);}
	public AudioSource play_sfx(string path, Vector3 p, float rvol, bool connected_listner, float pitch, bool loop) {return play_sfx (path, p, rvol, connected_listner, pitch, loop, false);}
	public AudioSource play_sfx(string path, Vector3 p, float rvol, bool connected_listner, float pitch, bool loop, bool reduct_bgm_noise) {
		if(!sfxOn)
			return null;
		
		AudioClip aclp = null;
		if (clip_bank.ContainsKey (path)) {
			aclp = clip_bank [path];
		} else {
			aclp = Resources.Load(path) as AudioClip;
			if (aclp != null)
				clip_bank.Add(path,aclp);
		}
		
		if (aclp == null) {
			Debug.LogError(" NOT EXISTING SFX ");
			Debug.LogError (path);
			return null;
		}
		
		AudioSource asrc = get_source_inbank();
		Transform tt = get_transform_from_asrouce(asrc);
		if (connected_listner) {
			tt.parent = gameObject.transform;
			tt.localPosition = Vector3.zero;
		}
		else {
			if (tt.parent != null)
				tt.parent = null;
			tt.position = p;
			
		}
		
		if (!asrouce_stillplaying.ContainsKey (aclp))
			asrouce_stillplaying.Add (aclp, asrc);
		asrouce_stillplaying [aclp] = asrc;
		
		asrc.pitch = pitch;
		asrc.clip = aclp;
		asrc.volume = sfx_volume * rvol;
		asrc.loop = loop;
		asrc.Play();

		if (reduct_bgm_noise) {
			StopCoroutine("play_sfx_coroutine");
			StartCoroutine("play_sfx_coroutine", asrc);
		}
		
		return asrc;
	}
	private IEnumerator play_sfx_coroutine (AudioSource asrc) {
		SetRelativeBGMVolume (0.25f);
		while (asrc.isPlaying)
			yield return new WaitForEndOfFrame();
		ResetRelativeBGMVolume ();
	}
	
	public AudioSource play_bgm(string path, bool loop, bool fade) {
		
		bgm_playing = path;

			
		AudioClip aclip = Resources.Load(path) as AudioClip;
		if (aclip == null || bgmAudioSource.clip == aclip){
			return null;
		}

		if (fade) {
			FadeInBgm(new BgmClipToPlay(aclip, bgm_volume, loop));
			return null;
		} else {
			bgmAudioSource.clip = aclip;
			bgmAudioSource.loop = loop;
			bgmAudioSource.volume = bgm_volume;
			bgmAudioSource.Play();
		}
		return bgmAudioSource;

	}
	public AudioSource play_bgm(string path){return play_bgm(path, true, true);}
	public AudioSource play_bgm(string path, bool loop){return play_bgm(path, loop, true);}

	public void ResetRelativeBGMVolume () {
		SetRelativeBGMVolume (1f);
	}
	public void SetRelativeBGMVolume (float val) {
		StopCoroutine ("SetRelativeBGMVolumeCoroutine");
		StartCoroutine ("SetRelativeBGMVolumeCoroutine", val);
	}
	public IEnumerator SetRelativeBGMVolumeCoroutine (float val) {
		val = Mathf.Clamp01(val);
		float volstart = bgmAudioSource.volume;
		float dur = 0.65f;
		float t = 0;
		while (t < dur) {
			t += Time.deltaTime;
			bgmAudioSource.volume = Mathf.Lerp(volstart, val * DEFAULT_BGM_VOL, (t / dur));
			yield return new WaitForEndOfFrame();
		}
	}

	public void SetRelativeBGMVolumeNoFade (float val) {
		bgmAudioSource.volume = val * DEFAULT_BGM_VOL;
	}

	public void FadeoutBgm() {
		StopCoroutine("FadeOutBgmCoroutine");
		StartCoroutine ("FadeOutBgmCoroutine");
	}

	public IEnumerator FadeOutBgmCoroutine() {

		if (!bgmAudioSource.isPlaying)
			yield break;
		float volstart = bgmAudioSource.volume;
		float dur = 0.65f;
		float t = 0;
		while (t < dur) {
			t += Time.deltaTime;
			bgmAudioSource.volume = (1 - (t / dur)) * volstart;
			yield return new WaitForEndOfFrame();
		}
	}

	private void FadeInBgm(BgmClipToPlay bgmClipToPlay) {
		StopCoroutine("FadeInBgmCoroutine");
		StartCoroutine ("FadeInBgmCoroutine", bgmClipToPlay);
	}

	private IEnumerator FadeInBgmCoroutine(BgmClipToPlay bgmClipToPlay) {
		yield return StartCoroutine("FadeOutBgmCoroutine");
		bgmClipToPlay.Copy(bgmAudioSource);
			
		if (!bgmAudioSource.isPlaying)
			bgmAudioSource.Play();
		float volstart = DEFAULT_BGM_VOL;
		float dur = 0.65f;
		float t = 0;
		bgmAudioSource.volume = 0;
		Debug.Log ("volstart " + volstart);
		while (t < dur) {
			t += Time.deltaTime;
			bgmAudioSource.volume = (t / dur) * volstart;
			yield return new WaitForEndOfFrame();
		}
	}

	public bool isBGMOn() {
		return bgmOn;
	}

	public bool isSFXOn() {
		return sfxOn;
	}

	public void setBGMOn(bool flag) {
		bgmAudioSource.enabled = bgmOn = flag;
	}

	public void setSFXOn(bool flag) {
		sfxOn = flag;
		for (int i = 0; i < sfx_audiosources.Count; i++) {
			sfx_audiosources[i].enabled = flag;
		}
	}

	public bool toggleBGM() {
		bgmOn = !bgmOn;
		bgmAudioSource.enabled = bgmOn;
		return bgmOn;
	}

	public bool toggleSFX() {
		sfxOn = !sfxOn;
		for (int i = 0; i < sfx_audiosources.Count; i++) {
			sfx_audiosources[i].enabled = sfxOn;
		}
		return sfxOn;
	}

}
