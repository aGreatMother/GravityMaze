using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SenceButton : MonoBehaviour {

	public Scene level;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OpenLevel(int x){
		SceneManager.LoadScene (x);
	}
}
