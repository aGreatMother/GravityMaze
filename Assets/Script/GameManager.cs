using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public GameObject gameOver;
	public GameObject UIback;
	public GameObject successUI;
	private AudioSource backgroundSound;
	private GameObject finalShow;

	// Use this for initialization
	void Start () {
		backgroundSound = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckGameOver ();
	}
	void CheckGameOver(){
		if (Trandy.dead && !gameOver.activeSelf && !UIback.activeSelf) {
			UIback.SetActive (true);
			if (finalShow == null) {
				gameOver.SetActive (true);
				finalShow = gameOver;
			}
			backgroundSound.enabled = false;
			Singleton<SoundContainer>.Instance.gameObject.SetActive (false);
		}
	}

	public	void  GameSuccess(){
		UIback.SetActive (true);
		if (finalShow == null) {
			successUI.SetActive (true);
			finalShow = successUI;
		}

		backgroundSound.clip = Resources.Load ("Audio/END2", typeof(AudioClip)) as AudioClip;
		backgroundSound.Play ();

		Singleton<SoundContainer>.Instance.gameObject.SetActive (false);



	}
}
