using UnityEngine;

// Example script showing how you can easily call into the AdMobPlugin.
public class AdMobPluginDemoScript : MonoBehaviour {

    void Start()
    {
		#if UNITY_ANDROID
			print("----------->>>>>>>>>>>>>>>> Started");
        	AdMobPlugin.CreateBannerView("ca-app-pub-1613529252194340/1232139911",
                                     AdMobPlugin.AdSize.SmartBanner,
                                     true);
        	print("----------->>>>>>>>>>>>>>>> Created Banner View");
        	AdMobPlugin.RequestBannerAd(true);
        	print("----------->>>>>>>>>>>>>>>> Requested Banner Ad");
		#else
			print ("AdMobPlugin for Android won't show because you aren't over and Android Device");
		#endif
    }

    void OnEnable()
    {
		print("----------->>>>>>>>>>>>>>>> Registering for AdMob Events");
        AdMobPlugin.ReceivedAd += HandleReceivedAd;
        AdMobPlugin.FailedToReceiveAd += HandleFailedToReceiveAd;
        AdMobPlugin.ShowingOverlay += HandleShowingOverlay;
        AdMobPlugin.DismissedOverlay += HandleDismissedOverlay;
        AdMobPlugin.LeavingApplication += HandleLeavingApplication;
    }

    void OnDisable()
    {
        print("----------->>>>>>>>>>>>>>>> Unregistering for AdMob Events");
		AdMobPlugin.ReceivedAd -= HandleReceivedAd;
        AdMobPlugin.FailedToReceiveAd -= HandleFailedToReceiveAd;
        AdMobPlugin.ShowingOverlay -= HandleShowingOverlay;
        AdMobPlugin.DismissedOverlay -= HandleDismissedOverlay;
        AdMobPlugin.LeavingApplication -= HandleLeavingApplication;
    }

    public void HandleReceivedAd()
    {
        print("----------->>>>>>>>>>>>>>>> HandleReceivedAd event received");
    }

    public void HandleFailedToReceiveAd(string message)
    {
        print("----------->>>>>>>>>>>>>>>> HandleFailedToReceiveAd event received with message:");
        print("----------->>>>>>>>>>>>>>>>" + message);
    }

    public void HandleShowingOverlay()
    {
        print("----------->>>>>>>>>>>>>>>> HandleShowingOverlay event received");
    }

    public void HandleDismissedOverlay()
    {
        print("----------->>>>>>>>>>>>>>>> HandleDismissedOverlay event received");
    }

    public void HandleLeavingApplication()
    {
        print("----------->>>>>>>>>>>>>>>> HandleLeavingApplication event received");
    }
}