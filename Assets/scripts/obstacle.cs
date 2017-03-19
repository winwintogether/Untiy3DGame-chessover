using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour {

	// Use this for initialization
	public float timeLeft;
	public Transform obs;
	public float delta_x;
	void Start () {
		timeLeft = 0.1f;
		delta_x = 0.2f;
	}
	public void move(){
		obs.transform.position=new Vector3 (obs.position.x+delta_x,obs.position.y,obs.position.z);
	}
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		if(timeLeft<0.0f){
			
			move ();

			timeLeft = 0.05f;
		}

		if(obs.position.x<-2.79f)
		{
			delta_x = 0.2f;
		}
		if(obs.position.x>2.79f)
		{
			delta_x = -0.2f;
		}

	}
	public void stop_obstacle(){
		delta_x = 0;
	}
}
