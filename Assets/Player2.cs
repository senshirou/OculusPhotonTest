using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player2 : MonoBehaviourPunCallbacks
{

	// Joinイベントをリッスン
	//------------------------------------------------------------------------------------------------------------------------------//
	public override void OnJoinedRoom()
	{

		// ルームログイン後に呼ぶ
		CreateAvatar();
	}

	//------------------------------------------------------------------------------------------------------------------------------//
	void CreateAvatar()
	{

		Debug.Log("Player: 自分のアバターを生成します.");

		// Photon経由で自分のアバターを動的に生成
		// 自分のアバターが他クライアント上にも自動で生成される = 他クライアントが生成したアバターは自クライアント上に自動で生成される
		GameObject avatar = PhotonNetwork.Instantiate("Avatar", new Vector3(0, 1, 0), Quaternion.identity, 0);
		avatar.transform.parent = transform;

		// カメラを自身のアバターの子にする
		GameObject camera = transform.Find("Camera").gameObject;
		camera.transform.parent = avatar.transform;
	}
}

