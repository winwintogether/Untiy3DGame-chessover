using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class setting : MonoBehaviour {

	// Use this for initialization
	public GameObject setting_panel;

	void Awake(){
		if (PlayerPrefs.GetInt ("soundeffect", 0) == 0) {
			soundBtn.normalSprite2D = sounds [0];
			

		} else {
			soundBtn.normalSprite2D = sounds [1];
			
		}
		if (PlayerPrefs.GetInt ("music", 0) == 0) {
			musicBtn.normalSprite2D = musics [0];
			

		} else {
			musicBtn.normalSprite2D = musics [1];
			
		}
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void _close(){
		setting_panel.SetActive (false);
	}

	public void OnMusicBtnClick()
	{
		if (ThirdManager.instance.gameObject.GetComponents<AudioSource> ()[0].mute) {
			ThirdManager.instance.gameObject.GetComponents<AudioSource> ()[0].mute = false;

			musicBtn.normalSprite2D = musics [0];
			PlayerPrefs.SetInt ("music", 0);

		} else {
			
			PlayerPrefs.SetInt ("music", 1);
			ThirdManager.instance.gameObject.GetComponents<AudioSource> ()[0].mute = true;
			musicBtn.normalSprite2D = musics [1];
		}
	}

	public void OnSoundBtnClick()
	{
		if (ThirdManager.instance.gameObject.GetComponents<AudioSource> ()[1].mute) {
			ThirdManager.instance.gameObject.GetComponents<AudioSource> ()[1].mute = false;
            ThirdManager.instance.gameObject.GetComponents<AudioSource>()[2].mute = false;
			PlayerPrefs.SetInt ("soundeffect", 0);
			soundBtn.normalSprite2D = sounds [0];		
		} else {
			PlayerPrefs.SetInt ("soundeffect", 1);
			ThirdManager.instance.gameObject.GetComponents<AudioSource> ()[1].mute = true;
            ThirdManager.instance.gameObject.GetComponents<AudioSource>()[2].mute = true;
			soundBtn.normalSprite2D = sounds [1];
		}
	}

	public Sprite[] musics;
	public Sprite[] sounds;

	public UIButton musicBtn;
	public UIButton soundBtn;

}
