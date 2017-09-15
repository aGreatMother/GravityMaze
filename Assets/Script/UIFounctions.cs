using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIFounctions : MonoBehaviour {

	// Use this for initialization
	public GameObject tutorial;
	void Start () {
		//NOTE delect
		//PlayerPrefs.SetInt ("tutorialShowed",0);

		if (tutorial != null && PlayerPrefs.GetInt ("tutorialShowed") == 0)
			tutorial.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NextLevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex+1);
	}

	public void Retry(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}
	public void TutorialShowed(){
		PlayerPrefs.SetInt ("tutorialShowed", 1);
	}

	public void Exit(){
		Application.Quit ();
	}

	public void BackToStart(){
		SceneManager.LoadScene (0);
	}
}
