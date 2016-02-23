using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFapConnectionMaster : Photon.PunBehaviour {
	public string PlayFabId;

	void Awake () {
		PlayFabSettings.TitleId = PrivateSettings.GetPlayFapTitleID ();
	}

	public void Login(InputField nickname)
	{
		LoginWithCustomIDRequest request = new LoginWithCustomIDRequest()
		{
			CreateAccount = true,
			CustomId = SystemInfo.deviceUniqueIdentifier + nickname.text
		};

		PlayFabClientAPI.LoginWithCustomID(request, (result) => {
			PlayFabId = result.PlayFabId;
			PlayerPrefs.SetString("nickname", nickname.text);
			PlayerPrefs.SetString("playfabID", PlayFabId);
				

			ConnectToPhoton();
		},
			(error) => {
				Debug.Log("Error logging in player with custom ID:");
				Debug.Log(error.ErrorMessage);
			});
	}

	void ConnectToPhoton(){
		GetPhotonAuthenticationTokenRequest PunRequest = new GetPhotonAuthenticationTokenRequest();
		PunRequest.PhotonApplicationId = PrivateSettings.GetPhotonAppID();

		PlayFabClientAPI.GetPhotonAuthenticationToken(PunRequest, (ph_r) =>{

			PhotonNetwork.AuthValues = new AuthenticationValues();
			PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.Custom;
			//				PhotonNetwork.AuthValues.UserId = "3"; // alternatively set by server
			PhotonNetwork.AuthValues.AddAuthParameter("username", PlayFabId);
			PhotonNetwork.AuthValues.AddAuthParameter("token", ph_r.PhotonCustomAuthenticationToken);
			PhotonNetwork.ConnectUsingSettings("1.0");


		}, (error) => {
			Debug.Log("Error logging in player with custom ID:");
			Debug.Log(error.ErrorMessage);
		});
	}

	public override void OnJoinedRoom()
	{
		PhotonNetwork.LoadLevel ("Game");
	}
}
