using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class road : MonoBehaviour {

	public Transform tRoad_Style;
	public Transform controller;
	public Transform obstacle_Style;
    public Transform flag_Style;

	public UILabel lStart;

	public float delta_y=0.0f;
	public float timeLeft;
	public float mTimeSpeed;
	public float start_delay;
	public float roadspeed;
	public float screen_width;
	public float move_x;
	public float deltaspeed;
	public float tyle_step;
	public float l_step1;
	public float l_step2;
	public float l_step3;
    public int list_num;


	[Header("MousePosition")]
	public Vector3 mMousePos;

	[Header("Screen Width, Height")]
	public int mWidth;
	public int mHeight;

	public Vector2 mFirstPos;
	public Vector2 mSecondPos;

	public GameObject black_cube;
	public GameObject white_cube;
	public GameObject obstacle;
    public GameObject flag;

	void Start () {

		mWidth = Screen.width;
		mHeight = Screen.height;

		start_delay = 4.0f;
		mFirstPos = new Vector2 (0.0f, 0.48f);
		tyle_step = 20.0f;

		roadspeed = 0.12f;
		mTimeSpeed = 0.03f;
		timeLeft = mTimeSpeed;
		deltaspeed = 40.0f;

        list_num = 27;

		l_step1 = 0; l_step2 = 0;l_step3 = 0;

		set_tyle();	

	}


	public void set_tyle(){
		
		   for (int i = 0; i < 30; i++) {
			
			int[] re=random_value ();
			if( i<=2 ){
				re [0] = 0;re [1] = 0;re [2] = 0;

			}

			l_step1 = re[0]; l_step2 = re[1];l_step3 = re[2];

			GameObject cube1;

			if (re [0] == 0) 
				 cube1=GameObject.Instantiate(black_cube);
			else
				cube1=GameObject.Instantiate(white_cube);
			
			cube1.transform.parent = tRoad_Style;
			cube1.transform.position = new Vector3(-2.2f, 0.0f, (i-1)*2.2f);			    
			cube1.transform.localScale = new Vector3 (2.2f,0.05f,2.2f);
	

			GameObject cube2;

			if (re [1] == 0) 
				cube2=GameObject.Instantiate(black_cube);
			else
				cube2=GameObject.Instantiate(white_cube);
			cube2.transform.parent = tRoad_Style;
			cube2.transform.position = new Vector3(0.0f, 0.0f, (i-1)*2.2f);			    
			cube2.transform.localScale = new Vector3 (2.2f,0.05f,2.2f);

			GameObject cube3;

			if (re [2] == 0)
				cube3=GameObject.Instantiate(black_cube);
			else
				cube3=GameObject.Instantiate(white_cube);
			
			cube3.transform.parent = tRoad_Style;
			cube3.transform.position = new Vector3(2.2f, 0.0f, (i-1)*2.2f);			    
			cube3.transform.localScale = new Vector3 (2.2f,0.05f,2.2f);

			cube1.SetActive (true);
			cube2.SetActive (true);
			cube3.SetActive (true);

            if (i == 24) {
                GameObject redflag;

                for (int j = 0; j < 3; j++)
                {
                    if (re[j] == 1)
                    {
                        redflag = GameObject.Instantiate(flag);
                        redflag.transform.parent = flag_Style;
                        redflag.transform.position = new Vector3(-3.0f + j * 2.2f, 1.8f, tRoad_Style.GetChild(69).position.z + 1.1f);
                        redflag.transform.localScale = new Vector3(0.8f, 0.6f, 0.8f);

                        redflag.SetActive(true);

                        break;
                    }
                }

                
            }

		}

	}
	public void controller_ymove(){
			
		controller.transform.position = new Vector3 (controller.position.x,controller.position.y+delta_y*1.5f,controller.position.z);

	}

	public void road_move(){
			for (int i = 0; i < 90; i++) {
				tRoad_Style.GetChild (i).position = new Vector3 (tRoad_Style.GetChild (i).position.x, tRoad_Style.GetChild (i).position.y, tRoad_Style.GetChild (i).position.z - roadspeed);			 
			}
    }
	public void obstacle_move(){
		
		for (int i = 0; i < obstacle_Style.childCount; i++) {
			 obstacle_Style.GetChild (i).position = new Vector3 (obstacle_Style.GetChild (i).position.x, obstacle_Style.GetChild (i).position.y, obstacle_Style.GetChild (i).position.z - roadspeed);			 
		}

	}
    public void flag_move()
    {

        for (int i = 0; i < flag_Style.childCount; i++)
        {
            flag_Style.GetChild(i).position = new Vector3(flag_Style.GetChild(i).position.x, flag_Style.GetChild(i).position.y, flag_Style.GetChild(i).position.z - roadspeed);
        }

    }

	public void add_list(){
		int[] re=random_value ();
       

       

            l_step1 = re[0]; l_step2 = re[1]; l_step3 = re[2];
        GameObject cube1;
       
		if (re [0] == 0) 
			cube1=GameObject.Instantiate(black_cube);
		else
			cube1=GameObject.Instantiate(white_cube);
		
		cube1.transform.parent = tRoad_Style;
		cube1.transform.position = new Vector3(-2.2f, 0.0f, tRoad_Style.GetChild (84).position.z+2.2f);			    
		cube1.transform.localScale = new Vector3 (2.2f,0.05f,2.2f);

		GameObject cube2;
		if (re [1] == 0) 
			cube2=GameObject.Instantiate(black_cube);
		else
			cube2=GameObject.Instantiate(white_cube);
		
		cube2.transform.parent = tRoad_Style;
		cube2.transform.position = new Vector3(0.0f, 0.0f, tRoad_Style.GetChild (84).position.z+2.2f);			    
		cube2.transform.localScale = new Vector3 (2.2f,0.05f,2.2f);

		GameObject cube3;
		if (re [2] == 0) 
			cube3=GameObject.Instantiate(black_cube);
		else
			cube3=GameObject.Instantiate(white_cube);
		
		cube3.transform.parent = tRoad_Style;
		cube3.transform.position = new Vector3(2.2f, 0.0f, tRoad_Style.GetChild (84).position.z+2.2f);			    
		cube3.transform.localScale = new Vector3 (2.2f,0.05f,2.2f);

        cube1.SetActive (true);
		cube2.SetActive (true);
		cube3.SetActive (true);

        list_num++;

        for (int i = 0; i < 3; i++)
        {
            if (re[i] == 1)
            {
                add_flag(i);
                break;
            }
        }
        
	/*	if( tRoad_Style.GetChild (82).name=="black_cube(Clone)" && tRoad_Style.GetChild (83).name=="white_cube(Clone)" && tRoad_Style.GetChild (84).name=="black_cube(Clone)" && tRoad_Style.GetChild (85).name=="white_cube(Clone)" && tRoad_Style.GetChild (86).name=="black_cube(Clone)" &&  tRoad_Style.GetChild (88).name=="black_cube(Clone)"){
			add_obstacle ();
		}*/
		controller.transform.position = new Vector3 (controller.position.x,0.6f,controller.position.z);

		int l_sum = 0;
		if( tRoad_Style.GetChild (87).name=="black_cube(Clone)"){
			l_sum = l_sum + 1;
		}
		if( tRoad_Style.GetChild (88).name=="black_cube(Clone)"){
			l_sum = l_sum + 1;
		}
		if( tRoad_Style.GetChild (89).name=="black_cube(Clone)"){
			l_sum = l_sum + 1;
		}
		if(l_sum==2){
			add_obstacle ();
		}

	}
    public void add_flag(int param) {        

        if (list_num % 20 == 0)
        {
            GameObject redflag;
            redflag = GameObject.Instantiate(flag);
            redflag.transform.parent = flag_Style;
            redflag.transform.position = new Vector3(-3.0f+param*2.2f, 1.8f, tRoad_Style.GetChild(84).position.z + 1.1f);
            redflag.transform.localScale = new Vector3(0.8f, 0.6f, 0.8f);
            redflag.transform.GetChild(0).GetComponent<TextMesh>().text = list_num.ToString();

            redflag.SetActive(true);
            
        }

    }
	public void add_obstacle(){
		if (Random.Range (0, 4)<1.0f) {
			GameObject ob;
			ob = GameObject.Instantiate (obstacle);
			ob.transform.parent = obstacle_Style;
			ob.transform.position = new Vector3 (2.2f, 0.5f, tRoad_Style.GetChild (84).position.z + 2.2f);			    
			ob.transform.localScale = new Vector3 (0.7f, 0.7f, 0.7f);

			ob.SetActive (true);
		}
	}

	public void remove_list(){
		
		Transform child = tRoad_Style.GetChild (0);

		child.parent = null;
		Destroy (child.gameObject);

		child = tRoad_Style.GetChild (0);
		child.parent = null;
		Destroy (child.gameObject);

		child = tRoad_Style.GetChild (0);
		child.parent = null;
		Destroy (child.gameObject);

		controller.position = new Vector3 (controller.position.x,0.48f,controller.position.z);
        speedcontrol ();

	}

	public void remove_obstacle(){
		Transform child = obstacle_Style.GetChild (0);
		child.parent = null;
		Destroy (child.gameObject);
	}

    public void remove_flag()
    {
        Transform child = flag_Style.GetChild(0);
        child.parent = null;
        Destroy(child.gameObject);
    }


	public void speedcontrol(){
		if (gl.cur_score % tyle_step == 0.0f && gl.cur_score>0) {
			roadspeed += 0.01f;
			deltaspeed += 1.0f;
		}
    }

	void Update () {
		if (gl.gameover)
			return;
		timeLeft -= Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            mMousePos = Input.mousePosition;
            if (mMousePos.x <= mWidth / 7)
            {
                move_x = (float)(7.56 / 7 - 3.78);
            }
            if (mMousePos.x > mWidth / 7 && mMousePos.x < mWidth * 6 / 7)
            {
                move_x = (float)(7.56 * mMousePos.x / mWidth - 3.78);
            }
            if (mMousePos.x >= mWidth * 6 / 7)
            {
                move_x = (float)(7.56 * 6 / 7 - 3.78);
            }
            //deltaspeed control lerp 
            mSecondPos = Vector2.Lerp(mFirstPos, new Vector2(move_x, 0), Time.deltaTime * deltaspeed);

            controller.transform.position = new Vector3(mSecondPos.x, controller.position.y, controller.position.z);
            mFirstPos = mSecondPos;
        }

        if (start_delay >= 0) {

            start_delay -= Time.deltaTime;

            /*mSecondPos = Vector2.Lerp(mFirstPos, new Vector2(move_x, 0), Time.deltaTime * deltaspeed);
            controller.transform.position = new Vector3(mSecondPos.x, controller.position.y, controller.position.z);
            mFirstPos = mSecondPos;*/

		    if ( start_delay < 4.0f && start_delay > 3.0f) {
			    lStart.text = "3";
                return;
		    }
		    if (start_delay < 3.0f && start_delay > 2.0f) {
			    lStart.text = "2";
                return;
		    }
		    if (start_delay < 2.0f && start_delay > 1.0f) {
			    lStart.text = "1";            
                GameObject.FindWithTag("Player").GetComponent<Animator>().SetTrigger("Tri_Fly");
                return;
		    }
		    if (start_delay < 1.0f && start_delay > 0.0f) {
			    lStart.text = "Start";
			    lStart.fontSize = 50;
                return;
		    }		   
        }
        else {   
                // Start letter: none display
                lStart.gameObject.SetActive(false);
        
                //road, obstacle, controller move
		        if ( timeLeft  < 0.0f) {
			          road_move (); 
                      obstacle_move ();
                      flag_move();
                      controller_ymove ();
			          timeLeft  = mTimeSpeed;			 
		        }

              
		        if (tRoad_Style.GetChild (0).position.z <= -4.4f) {
			            remove_list ();
				        add_list ();
		        } 
		        if (obstacle_Style.childCount!=0 && obstacle_Style.GetChild (0).position.z <= -4.4f) {
			         remove_obstacle();
		        }
                if (flag_Style.childCount != 0 && flag_Style.GetChild(0).position.z <= -4.4f)
                {
                    remove_flag();
                } 
		        if (tRoad_Style.GetChild (3).position.z < -1.1f) {
			        delta_y =-0.1f;
		        }
		        else{
			        delta_y =0.1f;
		        }
        }

        //Controller touch event
       
	}

	public int[] random_value(){			
		int[] ran = new int[3];

		ran [0] = Random.Range (0, 2);
		ran [1] = Random.Range (0, 2);
		ran [2] = Random.Range (0, 2);

        if (ran[0] == l_step1 && ran[1] == l_step2 && ran[2] == l_step3)
        {
           
            ran[1] = 1 - ran[1];
            ran[2] = 1 - ran[2];
        }

        if (ran [0] == 1 && ran [1] == 1 && ran [2] == 1 )
		{
			
            ran[2] = 0;
		}

		if (ran [0] == 0 && ran [1] == 0 && ran [2] == 0 )
		{
			
            ran[2] = 1;
		}

		if (ran [0] == 1 && ran [1] == 1 && ran [2] == 0 && l_step1==0 && l_step2==1 && l_step3==1)
		{
			
            ran[1] = 0;

		}
		if (ran [0] == 0 && ran [1] == 1 && ran [2] == 1 && l_step1==1 && l_step2==1 && l_step3==0)
		{
			
            ran[1] = 0;

		}
		if (ran [0] == 0 && ran [1] == 1 && ran [2] == 1 && l_step1==0 && l_step2==1 && l_step3==0)
		{
			
            ran[1] = 0;

		}

		if (ran [0] == 0 && ran [1] == 1 && ran [2] == 0 && l_step1==1 && l_step2==1 && l_step3==0)
		{
			
            ran[1] = 0;
            ran[2] = 1;
		}

		if (ran [0] == 1 && ran [1] == 1 && ran [2] == 0 && l_step1==0 && l_step2==1 && l_step3==0)
		{
			
            ran[1] = 0;
            ran[2] = 1;

		}

		if (ran [0] ==0  && ran [1] == 1 && ran [2] == 0 && l_step1==0 && l_step2==1 && l_step3==1)
		{
			
            ran[1] = 0;
            ran[2] = 1;
		}
	
		return ran;				
	}
}
