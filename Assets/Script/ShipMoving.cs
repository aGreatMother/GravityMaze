using UnityEngine;
using System.Collections;

public class ShipMoving : MonoBehaviour {

	// Use this for initialization
	void Start () {
		maxUp=Random.Range ( maxUpPer-0.3f, maxUpPer+0.3f);
		maxDown = Random.Range (maxDownPer-0.3f,maxDownPer+0.3f);
		StartCoroutine (DelayForChangeY ());
	}
	public float maxUpPer=-0.5f;
	public float maxDownPer=-1f;

	// Update is called once per frame
	void Update () {

	}

	public float delay=0.4f;
	IEnumerator DelayForChangeY()
	{
		while (delay>=-1f) {
			delay-=Time.deltaTime;
			if(delay<=0f)
			{
				StartCoroutine(RandomY() );
				StopCoroutine (DelayForChangeY ());
				break;
			}
			yield return 0;

		}
	}



	public float changeSpeed=0.2f;
	//float originYscale;
	float maxUp;
	bool grow=false;
	float maxDown;

	IEnumerator RandomY()
	{
		if(!grow)
		while (transform.position.y > maxDown) {
			Vector3 currentPos = this.transform.position;
				currentPos -= Time.deltaTime * changeSpeed * Vector3.up;
			this.transform.position = currentPos;
			//			transform.localScale -= Time.deltaTime * changeYscaleSpeed * Vector3.up;
			//Ypos = sprite.sprite.border.y / 2;
			//transform.position -= transform.right * Ypos;
			yield return 0;

		}
		if (transform.position.y<=maxDown  )
			grow = true;
		if (grow)
			while (this.transform.position.y < maxUp) {
				Vector3 currentPos = this.transform.position;
				currentPos += Time.deltaTime * changeSpeed * Vector3.up;
				this.transform.position = currentPos;
				yield return 1;
			}

			

		if (grow && transform.position.y>=maxUp) {
			grow=false;
			Vector3 currentPos=this.transform.position;
			currentPos=maxUp*Vector3.up+this.transform.position.z*Vector3.forward;
			this.transform.position=currentPos;
			maxUp=Random.Range ( maxUpPer-0.3f, maxUpPer+0.3f);
			maxDown = Random.Range (maxDownPer-0.3f,maxDownPer+0.3f);
			StartCoroutine(RandomY());
		}

	}
}
