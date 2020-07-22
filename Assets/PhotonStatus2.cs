using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class PhotonStatus2 : MonoBehaviour
{

	//------------------------------------------------------------------------------------------------------------------------------//
	void OnGUI()
	{

		// Photonとの接続状況を表示する
		string status = "Photon: " + PhotonNetwork.NetworkClientState.ToString() + "\n";

		// ルームに入室したら部屋の状況を表示する
		if (PhotonNetwork.InRoom)
		{
			status += "-------------------------------------------------------\n";
			status += "Room Name: " + PhotonNetwork.CurrentRoom.Name + "\n";
			status += "Player Num: " + PhotonNetwork.CurrentRoom.PlayerCount + "\n";
			status += "-------------------------------------------------------\n";
			status += "Player No: " + PhotonNetwork.LocalPlayer.ActorNumber + "\n";
			status += "IsMasterClient: " + PhotonNetwork.IsMasterClient;
		}

		GUI.TextField(new Rect(10, 10, 220, 120), status);
	}
}
