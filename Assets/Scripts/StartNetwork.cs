using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class StartNetwork : MonoBehaviour {

	void Start () {
		Debug.Log ("Starting");
		bool headless = false;
		if (System.Environment.GetCommandLineArgs () != null) {
			foreach (string arg in System.Environment.GetCommandLineArgs()) {
				if (arg == "-headless") {
					headless = true;
				}
			}
		}
		NetworkManager nm = GetComponent<NetworkManager>();
		if (headless) {
			Debug.Log ("Starting server");
			nm.networkAddress = "0.0.0.0";
			nm.StartServer ();
		} else {
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
				Debug.Log ("Starting client");
				nm.networkAddress = "10.0.1.2";
				nm.logLevel = LogFilter.FilterLevel.Developer;
				nm.StartClient ();
			} else {
				Debug.Log ("Starting HUD");
				GetComponent<NetworkManagerHUD>().enabled = true;
			}
		}
	}
}
