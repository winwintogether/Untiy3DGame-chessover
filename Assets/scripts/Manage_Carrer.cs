using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Manage_Carrer : MonoBehaviour {

	public UILabel lScore;
    public UILabel lLife;

	public int mScore = 0;
	public bool check=false;
	
    [Header("Dragon Object")]
    public GameObject player;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		lScore.text = "Score: " + mScore;
        lLife.text = (11 - PlayerPrefs.GetInt("sessioncount", 0)).ToString() + "/ 10";

	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "black_cube(Clone)" && check==false) {

			mScore++;

			gl.cur_score = mScore;

            if (ThirdManager.instance != null)
			    ThirdManager.instance.gameObject.GetComponents<AudioSource> () [1].Play ();
		}


		if (other.gameObject.name == "white_cube(Clone)" || other.gameObject.name == "obstacle(Clone)") {

            // Play Death Animation
            player.GetComponent<Animator>().SetTrigger("Tri_Death");

            //Die sound effect
            if (ThirdManager.instance != null)
            ThirdManager.instance.gameObject.GetComponents<AudioSource>()[2].Play();

			//Die count increase
                 int sessioncount = PlayerPrefs.GetInt("sessioncount", 0);
                 PlayerPrefs.SetInt("sessioncount", ++sessioncount);
            //current score
            gl.cur_score = mScore;
            //garbage collection
            System.GC.Collect();
			GameObject[] obstacles = GameObject.FindGameObjectsWithTag ("obstacle");
			foreach(GameObject ob in obstacles){
				ob.GetComponent<obstacle> ().stop_obstacle();
			}

            if (PlayerPrefs.GetInt("maxscore", 0) < gl.cur_score)
            {
				  PlayerPrefs.SetInt("maxscore",gl.cur_score);			
			}

			check = true;

			other.gameObject.GetComponent<MeshRenderer> ().material.color = Color.red;

			gl.gameover = true;
			StartCoroutine (WaitAndContinue ());
            
		}

	}

	IEnumerator WaitAndContinue()
	{
		yield return new WaitForSeconds (2.0f);
		SceneManager.LoadScene ("Finish");
	}
}
