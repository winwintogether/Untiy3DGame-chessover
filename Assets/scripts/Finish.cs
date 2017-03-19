using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
public class Finish : MonoBehaviour {

	// Use this for initialization
	public UILabel MaxScore;
	public UILabel CurrentScore;
	void Start () {
		//MaxScore.GetComponent()
		MaxScore.text="Max Score: "+PlayerPrefs.GetInt("maxscore",0);
		CurrentScore.text = "Current Score: " + gl.cur_score;

        System.GC.Collect();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void _clickMainMenu(){

        ThirdManager.instance.gameObject.GetComponents<AudioSource>()[0].Stop();
		SceneManager.LoadScene ("MainMenu");

	}

	public void _clickClose(){
		Application.Quit();

	}
	public void _return(){
		gl.gameover = false;

        int sessioncount = PlayerPrefs.GetInt("sessioncount",0);


        if (sessioncount >= 10 && PlayerPrefs.GetInt("purchase", 0)==0) {

            if (ThirdManager.instance != null)
            ThirdManager.instance.gameObject.GetComponents<AudioSource>()[0].Stop();
            SceneManager.LoadScene("MainMenu");
        }
        else { 
		SceneManager.LoadScene ("play");
        }
	}

	public void _share(){
		#if UNITY_IOS // If build platform is set to iOS...
		StartCoroutine (ShareAndroid());
		#elif UNITY_ANDROID // Else if build platform is set to Android...
		StartCoroutine (ShareAndroid());
		#endif
	}

	public void _rate(){
		#if UNITY_IOS // If build platform is set to iOS...
		Application.OpenURL ("https://itunes.apple.com/us/app/");
		#elif UNITY_ANDROID // Else if build platform is set to Android...
		Application.OpenURL ("https://play.google.com/store/apps/details?id=com.silver.ninjaspin");
		#endif
	}

	private IEnumerator ShareAndroid()
	{
		yield return new WaitForEndOfFrame();
		// Create a texture the size of the screen, RGB24 format
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D( width, height, TextureFormat.RGB24, false );
		// Read screen contents into the texture
		tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		tex.Apply();
		byte[] screenshot = tex.EncodeToJPG();


		string destination = Path.Combine(Application.persistentDataPath, System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".jpg");
		Debug.Log(destination);
		File.WriteAllBytes(destination, screenshot);

		string details = "This is crazy game. That's great!";
		string gameLink = "https://play.google.com/store/apps/details?id=com.silver.ninjaspin";
		string subject = "Ninja Spin Game";

		#if UNITY_ANDROID
		if(!Application.isEditor)
		{
			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), details +"\n"+ gameLink);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);

			AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", destination);// Set Image Path Here
			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObject);
			bool fileExist = fileObject.Call<bool>("exists");
			Debug.Log("File exist : " + fileExist);
			if (fileExist)
				intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			currentActivity.Call("startActivity", intentObject);
		}
		#elif UNITY_IOS
		string applink = "https://itunes.apple.com/us/app/";
		string path = Application.persistentDataPath + "/MyImage.png";
		File.WriteAllBytes (path, screenshot);
		string path_ = "MyImage.png";
		GeneralSharingiOSBridge.ShareTextWithImage(path, "This is crazy game. That's great!"+applink);
		#endif


		Destroy(tex);
	}

}
