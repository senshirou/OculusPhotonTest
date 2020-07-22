﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PhotonLogin2 : MonoBehaviourPunCallbacks
{

	static string GAME_VERSION = "Ver.1";

	static RoomOptions ROOM_OPTIONS = new RoomOptions()
	{
		MaxPlayers = 20,
		IsOpen = true,
		IsVisible = true
	};

	//------------------------------------------------------------------------------------------------------------------------------//
	void Start()
	{

		// アプリの起動と同時にPhotonCloudに接続
		Debug.Log("PhotonLogin: マスターサーバーに接続します");
		PhotonNetwork.GameVersion = GAME_VERSION;
		PhotonNetwork.ConnectUsingSettings();
	}

	public override void OnConnectedToMaster()
	{

		Debug.Log("PhotonLogin: マスターサーバーに接続しました");
		Debug.Log("PhotonLogin: ルームに入室します");

		// ルームへの参加 or 新規作成
		PhotonNetwork.JoinOrCreateRoom("VR-Room", ROOM_OPTIONS, null);
	}

	//------------------------------------------------------------------------------------------------------------------------------//
	public override void OnJoinedRoom()
	{

		Room room = PhotonNetwork.CurrentRoom;
		Photon.Realtime.Player player = PhotonNetwork.LocalPlayer;
		Debug.Log("PhotonLogin: ルーム入室に成功 - " + room.Name);
		Debug.Log("PhotonLogin: プレイヤー情報: " + " プレイヤーNo: " + player.ActorNumber + " ユーザーID: " + player.UserId + " ルームマスター: " + player.IsMasterClient);
		Debug.Log("PhotonLogin: ルーム情報: " + room);
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		Debug.Log("PhotonManager: ルーム入室に失敗. ルームを新規作成します");
		PhotonNetwork.CreateRoom(null, ROOM_OPTIONS);
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		Debug.Log("PhotonLogin: ルーム作成に失敗");
	}
}

