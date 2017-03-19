using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Delay30 : MonoBehaviour {

	public int delay;
	public GameObject timefore;
	public GameObject timeback;
	public double startTime;

	bool getSession;
	public static GameObject instance;
	[HideInInspector]public bool created = false;
	// Use this for initialization
	void Start ()
	{
		//initialize
		created = true;
		getSession = false;
		timefore = gameObject.transform.Find ("time-back").Find ("time-fore").gameObject;
		timeback = gameObject.transform.Find ("time-back").Find ("back-circle").gameObject;

		//get current seconds
		long currentTick = DateTime.UtcNow.Ticks;
		TimeSpan startSpan = new TimeSpan (currentTick);
		double startSec = startSpan.TotalSeconds;

		startTime = Convert.ToDouble(PlayerPrefs.GetString ("StartTime", "0"));

		if (startTime == 0)
		{
			startTime = startSec;
			PlayerPrefs.SetString ("StartTime", startTime+"");
		}
		StartCoroutine (CountDelay ());
	}

	IEnumerator CountDelay()
	{
		while (true)
		{
			yield return new WaitForSeconds (1.0f);

			//get current seconds
			long currentTick = System.DateTime.UtcNow.Ticks;
			System.TimeSpan currentSpan = new System.TimeSpan (currentTick);
			double currentTime = currentSpan.TotalSeconds;

			delay = 720 - (int)(currentTime - startTime);

			UILabel timelab = gameObject.transform.Find ("time").gameObject.GetComponent<UILabel> ();
			if (delay > 0)
			{
				int min = (int)(delay / 60);
				int sec = delay % 60;
				timelab.text = min + " Min " + sec + " Sec";
				float rate = (float)(1.0f-delay / 720.0f);
				timefore.transform.localRotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, -90.0f - rate * 360.0f));

			}
			else if (delay <= 0)
			{
				float rate = (float)(delay / 720.0f);
				timefore.transform.localRotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, -90.0f - rate * 360.0f));

				PlayerPrefs.SetString ("StartTime", "0");
//				R.session = 0;
				PlayerPrefs.SetInt ("sessioncount", 0);

				timelab.GetComponent<UILabel>().color = new Color (0.6f, 1f, 0.6f, 1f);
				transform.Find("Label").gameObject.GetComponent<UILabel>().color = new Color (0.6f, 1f, 0.6f, 1f);
				timelab.text = "You have "+PlayerPrefs.GetInt ("maxsession", 10)+" sessions";
				getSession = true; 

				//SceneManager.LoadScene ("start");
				break;
			}
		}
	}

	void Update()
	{
		if (Input.GetKeyUp (KeyCode.Escape)) {
			OnBack ();
		}
	}
	
	// Update is called once per frame
	public void resume ()
	{
		StartCoroutine (CountDelay ());
	}

	public void OnBack()
	{
		if (getSession)
		{
			gameObject.SetActive (false);
		}
		else
		{
			gameObject.SetActive (false);

			MobileNativeRateUs adsPopUp = new MobileNativeRateUs("No More Sessions", "Watch ads to get more sessions.");
			adsPopUp.SetAppleId("");
			adsPopUp.SetAndroidAppUrl("");
			adsPopUp.yes = "$1.99 for Unlimited Session";
			adsPopUp.no = "No, Wait 12 minutes";
			adsPopUp.later = "Yes";
			adsPopUp.OnComplete += OnAdsPopUpClose;

			adsPopUp.Start();
		}
	}

	public void OnAdsPopUpClose(MNDialogResult result)
	{
		switch (result)
		{
		case MNDialogResult.RATED:
			ThirdManager.instance.BuyProduct ();
			break;
		case MNDialogResult.REMIND:
			ThirdManager.instance.ShowAd();
		//	ThirdManager.instance.gameObject.GetComponents<AudioSource> ()[0].Stop ();
		    
			gameObject.SetActive (false);
			break;
		case MNDialogResult.DECLINED:
			gameObject.SetActive (true);
			gameObject.GetComponent<Delay30>().resume ();
			break;
		}
	}
}
