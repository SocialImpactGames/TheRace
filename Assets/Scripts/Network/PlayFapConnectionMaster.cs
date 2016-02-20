using UnityEngine;
using System.Collections;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFapConnectionMaster : MonoBehaviour {
	public string PlayFabId;


	void Login()
	{
		LoginWithCustomIDRequest request = new LoginWithCustomIDRequest()
		{
			CreateAccount = true,
			CustomId = SystemInfo.deviceUniqueIdentifier
		};

		PlayFabClientAPI.LoginWithCustomID(request, (result) => {
			PlayFabId = result.PlayFabId;
			Debug.Log("Got PlayFabID: " + PlayFabId);

			if(result.NewlyCreated)
			{
				Debug.Log("(new account)");
			}
			else
			{
				Debug.Log("(existing account)");
			}
				
			GetPhotonAuthenticationTokenRequest PunRequest = new GetPhotonAuthenticationTokenRequest();
			PunRequest.PhotonApplicationId = PrivateSettings.GetPhotonAppID();

			// get an authentication ticket to pass on to Photon
			PlayFabClientAPI.GetPhotonAuthenticationToken(PunRequest, (ph_r) =>{
					print("Photon Connected "+ph_r.PhotonCustomAuthenticationToken);

					PhotonNetwork.ConnectUsingSettings("0.0.1");
				}, (error) => {
					Debug.Log("Error logging in player with custom ID:");
					Debug.Log(error.ErrorMessage);
				}
			);

		},
			(error) => {
				Debug.Log("Error logging in player with custom ID:");
				Debug.Log(error.ErrorMessage);
			});
	}


	// Use this for initialization
	void Start () {
		PlayFabSettings.TitleId = PrivateSettings.GetPlayFapTitleID ();
//		Login ();
		PhotonNetwork.ConnectUsingSettings("0.0.1");

	}
}
