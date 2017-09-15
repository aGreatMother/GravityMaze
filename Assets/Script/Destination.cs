using UnityEngine;
using System.Collections;
using DG.Tweening;
public class Destination : MonoBehaviour {
	public Animator light;
	private Transform finalPoint;
	private Transform trandy;
	private AudioSource sound;
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name=="trandy") {
			sound.Play ();

			trandy.GetComponent<Collider2D> ().enabled = false;
			trandy.GetComponent<CharacterRotateControl> ().SwitchDirection (this.transform.up);
			GameObject.FindObjectOfType<GameManager> ().GameSuccess ();
			if(Vector3.Project((finalPoint.position-trandy.position),finalPoint.up).y<9f)
				trandy.DOJump (finalPoint.position, -this.transform.up.y*5f, 0, 1f, false);
				else
				trandy.DOMove (finalPoint.position,1f, false);
				
			trandy.DOScale (0.6f, 1f);
			StartCoroutine (AfterAWhileThenTurnOffTheLight ());
		
		}
	}
	// Use this for initialization
	void Start () {
		finalPoint = transform.FindChild ("point");
		trandy = Singleton<Trandy>.Instance.transform;
		sound = this.gameObject.AddComponent<AudioSource> ();
		sound.playOnAwake = false;
		sound.clip= Resources.Load ("Audio/WATER", typeof(AudioClip)) as AudioClip;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator AfterAWhileThenTurnOffTheLight(){
		yield return new WaitForSeconds (0.5f);
		light.Play ("fade");
		trandy.GetComponent<Renderer> ().enabled = false;
	}
}
