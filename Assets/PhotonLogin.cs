using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonLogin : MonoBehaviourPunCallbacks
{
    //ゲームバージョン指定（設定しないと警告が出る
    string GameVersion = "Ver1.0";

    static RoomOptions RoomOPS = new RoomOptions()
    {
        MaxPlayers = 2,
        IsOpen = true,
        IsVisible = true
    };

    // Start is called before the first frame update
    void Start()
    {
        //PhotonCloudに接続
        Debug.Log("PhotonLogin");
        //ゲームバージョン設定
        PhotonNetwork.GameVersion = GameVersion;
        //PhotonServerSettingsファイルで構成されたPhotonに接続
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        //ルームへの参加 or 新規作成
        PhotonNetwork.JoinOrCreateRoom("photonroom", RoomOPS, null);
    }

    public override void OnJoinedRoom()
    {
        Room myroom = PhotonNetwork.CurrentRoom;
        Photon.Realtime.Player player = PhotonNetwork.LocalPlayer;
        Debug.Log("ルーム名:" + myroom.Name);
        Debug.Log("PlayerNo" + player.ActorNumber);
        Debug.Log("プレイヤーID" + player.UserId);

        if(player.ActorNumber == 1)
        {
            player.NickName = "私は1です";
        }

        Debug.Log("プレイヤー名" + player.NickName);
        Debug.Log("ルームマスター" + player.IsMasterClient); //ルームマスターならTrue。最初に部屋を作成した場合は、基本的にルームマスターなはず。


    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("入室失敗");

        //ルーム作成
        PhotonNetwork.CreateRoom(null, RoomOPS); //JoinOnCreateroomと同じ引数が使用可能。nullはルーム名を作成したくない場合roomNameを勝手に割り当てる。

    }
    // Update is called once per frame


    //ルーム作成失敗したときに動作
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("作成失敗");
    }
}
