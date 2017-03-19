using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowScript : MonoBehaviour {

    [Header("Player Controller's position")]
    public Transform tController;

    private Vector3 tempPos;

	// Use this for initialization

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        tempPos.Set(tController.position.x, transform.position.y, tController.position.z);
        transform.position = tempPos;
	}
}
