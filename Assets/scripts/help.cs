using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class help : MonoBehaviour {

	// Use this for initialization
	public GameObject help_panel;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void _close(){
		help_panel.SetActive (false);
	}
}
