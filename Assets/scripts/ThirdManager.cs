using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Advertisements;

public class ThirdManager : MonoBehaviour, IStoreListener {


	
	void Awake()
	{
		instance = this;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		DontDestroyOnLoad (this);


	
	}



	// Use this for initialization
	void Start () {

		//IAP Code
		if (m_StoreController == null)
		{
			InitializePurchasing();
		}

		//AD Code
		string gameID = null;
		#if UNITY_IOS // If build platform is set to iOS...
		gameID = iosID;
		Advertisement.Initialize(gameID);
		#elif UNITY_ANDROID // Else if build platform is set to Android...
		gameID = androidID;
		Advertisement.Initialize(gameID);
		#endif
	}

	//////IAP
	private static IStoreController m_StoreController;          // The Unity Purchasing system.
	private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

	public void InitializePurchasing() 
	{
		if (IsInitialized())
		{
			return;
		}

		// Create a builder, first passing in a suite of Unity provided stores.
		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
		builder.AddProduct("com.hs.chessover.advertisement", ProductType.NonConsumable);

		UnityPurchasing.Initialize(this, builder);
	}

	private bool IsInitialized()
	{
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}

	public void BuyProduct()
	{
		if (IsInitialized())
		{
			// ... look up the Product reference with the general product identifier and the Purchasing 
			// system's products collection.
			Product product = m_StoreController.products.WithID("com.hs.chessover.advertisement");

			// If the look up found a product for this device's store and that product is ready to be sold ... 
			if (product != null && product.availableToPurchase)
			{
				Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
				// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
				// asynchronously.
				m_StoreController.InitiatePurchase(product);
			}
			else
			{
				Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
			}
		}
		// Otherwise ...
		else
		{
			Debug.Log("BuyProductID FAIL. Not initialized.");
		}
	}

	// Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
	// Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
	public void RestorePurchases()
	{
		// If Purchasing has not yet been set up ...
		if (!IsInitialized())
		{
			// ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
			Debug.Log("RestorePurchases FAIL. Not initialized.");
			return;
		}

		// If we are running on an Apple device ... 
		if (Application.platform == RuntimePlatform.IPhonePlayer || 
			Application.platform == RuntimePlatform.OSXPlayer)
		{
			// ... begin restoring purchases
			Debug.Log("RestorePurchases started ...");

			// Fetch the Apple store-specific subsystem.
			var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
			// Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
			// the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
			apple.RestoreTransactions((result) => {
				// The first phase of restoration. If no more responses are received on ProcessPurchase then 
				// no purchases are available to be restored.
				Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		}
		else
		{
			// We are not running on an Apple device. No work is necessary to restore purchases.
			Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}

	//  
	// --- IStoreListener
	//

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		// Purchasing has succeeded initializing. Collect our Purchasing references.
		Debug.Log("OnInitialized: PASS");

		// Overall Purchasing system, configured with products for this application.
		m_StoreController = controller;
		// Store specific subsystem, for accessing device-specific store features.
		m_StoreExtensionProvider = extensions;
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
	{
		// A consumable product has been purchased by this user.
		if (string.Equals(args.purchasedProduct.definition.id, "com.hs.chessover.advertisement", System.StringComparison.Ordinal))
		{
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
			// The consumable item has been successfully purchased, add 100 coins to the player's in-game score.
			//purchase success 

			PlayerPrefs.SetInt ("purchase", 1);

		}
		else 
		{
			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}

		// Return a flag indicating whether this product has completely been received, or if the application needs 
		// to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
		// saving purchased products to the cloud, and when that save is delayed. 
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
	{
		// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
		// this reason with the user to guide their troubleshooting actions.
		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}
	//-------------------------------------------------------------------

	// -------------------------------------------- ADS

	public void ShowAd(string zone = "rewardedVideo"){ 
		#if UNITY_EDITOR
		StartCoroutine(WaitForAd ());
		#endif

		StartCoroutine (ShowADS(zone));
	}

	IEnumerator ShowADS(string zone)
	{
		ShowOptions options = new ShowOptions();
		options.resultCallback = AdCallbackhanler;
		while (!Advertisement.IsReady(zone))
		{
			yield return null;
		}
		Advertisement.Show (zone,options);
	}

	IEnumerator WaitForAd()
	{
		float currentTimeScale = Time.timeScale;
		Time.timeScale = 0f;
		yield return null;

		while (Advertisement.isShowing)
			yield return null;

		Time.timeScale = currentTimeScale;
	}

	void AdCallbackhanler(ShowResult result){
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log ("Ad Finished. Rewarding player...");
			PlayerPrefs.SetInt ("sessioncount", 0);

			//initialize session

			break;
		case ShowResult.Skipped:			
			break;
		case ShowResult.Failed:			
			break;
		}
		//gameObject.GetComponents<AudioSource> ()[0].Play ();
	}

	static public ThirdManager instance;
	[SerializeField] string androidID = "1188716";
	[SerializeField] string iosID = "1188715";
}
