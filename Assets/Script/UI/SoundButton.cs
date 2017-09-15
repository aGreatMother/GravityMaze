using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SoundButton : MonoBehaviour {

	private Image image;
	private Text text;

	// Use this for initialization
	void Start () {
		image = this.GetComponent < Image> ();
		text = this.GetComponentInChildren<Text> ();



		if (PlayerPrefs.GetInt ("sound") == 0) {
			text.text = "SOUND:ON";

		} else {
			text.text = "SOUND:OFF";

		}

	}
	public void ChangeSoundOption(){
		if (PlayerPrefs.GetInt ("sound") == 1) {
			PlayerPrefs.SetInt ("sound", 0);
		}
		else {
			PlayerPrefs.SetInt ("sound", 1);
		}
		if (PlayerPrefs.GetInt ("sound") == 0) {
			text.text = "SOUND:ON";
			AudioListener.volume = 1f;
		} else {
			AudioListener.volume = 0f;
			text.text = "SOUND:OFF";
		}
	}


	// Update is called once per frame
	void Update () {
	
	}
}
