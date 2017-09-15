using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum MovingDiraction{
	up,
	down,
	left,
	right,
	df

}
public class GameControlManager : MonoBehaviour {
	[Header("moving")]
	public Animator movingPanelanim;
	[Header("grivatyControl")]
	public Animator GcontrolPanel;

	public static Vector3 gStage = Vector3.down;
	private Trandy hero;
	private Rigidbody2D[] rigids;
	private float delayTime = 0.2f;
	private float delayCounter=0f;
	private bool afterDelay = false;
	// Use this for initialization
	void Awake () {
		gStage = Vector3.down;
		rigids = FindObjectsOfType<Rigidbody2D> ();
		hero = GameObject.FindObjectOfType<Trandy> ();

	}
	void Start(){
		preIsFloating = hero.GetFloating ();
	}

	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape))
			SceneManager.LoadScene (0);

		GiveDelay ();
		ReactToInput ();
		CheckGravityUI ();
		GiveGravity ();
	}


	void GiveGravity(){
		
		for (int i = 0;i < rigids.Length; i++) {
			if(!rigids[i].gameObject.name.StartsWith("oct"))
			rigids [i].AddForce (gStage * 10f);
		}
	}
	void ReactToInput(){//for G change input
		Vector3 preGstage = gStage;
		if (!afterDelay||hero.GetFloating()) {
			
			return;//cant change the G when Trandy is on the sky
		}
		if (Input.GetKey (KeyCode.UpArrow))
			gStage = Vector3.up;
		if (Input.GetKey (KeyCode.DownArrow))
			gStage = Vector3.down;
			if (Input.GetKey (KeyCode.RightArrow))
			gStage = Vector3.right;
			if (Input.GetKey (KeyCode.LeftArrow))
			gStage = Vector3.left;

		if(gStage!=preGstage&&!Singleton<SoundContainer>.Instance.changingG.isPlaying)
			Singleton<SoundContainer>.Instance.changingG.Play ();

		if (gStage != preGstage) {//to make sure that change to floating 
			for (int i = 0; i < rigids.Length; i++) {
				if (rigids [i].gameObject.GetComponent<CharacterHitFloorControl> ()&&!rigids[i].gameObject.name.StartsWith("oct")) {
					rigids [i].gameObject.GetComponent<CharacterHitFloorControl> ().LeaveFloor ();
					if (rigids [i].gameObject.GetComponent<CharacterRotateControl> ()) {
						rigids [i].gameObject.GetComponent<CharacterRotateControl> ().SwitchDirection (gStage);
					}
				}

			}
		}
	
	}


	public void CheckMovingUI(){
		if (hero.GetFloating()) {
			movingPanelanim.Play ("Disable");
			return;
		}

		if (gStage == Vector3.up || gStage == Vector3.down) {
			switch (Trandy.movingDrc) {
			case MovingDiraction.left:
				movingPanelanim.Play ("LeftKeyDown");
				return;
				break;
			case MovingDiraction.right:
				movingPanelanim.Play ("RightKeyDown");
				return;
				break;

			}
			movingPanelanim.Play ("Horizon");

		}
		if (gStage == Vector3.right || gStage == Vector3.left) {

			switch (Trandy.movingDrc) {

			case MovingDiraction.up:
				movingPanelanim.Play ("UpKeyDown");
				return;
				break;
			case MovingDiraction.down:
				movingPanelanim.Play ("DownKeyDown");
				return;
				break;

			}
			movingPanelanim.Play ("Vertical");

		}


	
	}


	private void CheckGravityUI(){
		if (!afterDelay||hero.GetFloating()) {
			GcontrolPanel.Play ("Disable");
			return;
		}


		if(gStage== Vector3.up)
			GcontrolPanel.Play ("UpKeyDown");
		
		if(gStage== Vector3.down)
			GcontrolPanel.Play ("DownKeyDown");

		if(gStage== Vector3.right)
			GcontrolPanel.Play ("RightKeyDown");


		if(gStage== Vector3.left)
			GcontrolPanel.Play ("LeftKeyDown");



		}

	bool preIsFloating=true;		
	void GiveDelay(){
		if (preIsFloating != hero.GetFloating()&&!hero.GetFloating()) {
			afterDelay = false;
		}
		if (!afterDelay) {
			delayCounter += Time.deltaTime;
			if (delayCounter > delayTime) {
				afterDelay = true;
				delayCounter = 0f;
			}
		}

		preIsFloating = hero.GetFloating ();
	}


}
