﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class Avatar2 : MonoBehaviourPun, IPunObservable
{

    //------------------------------------------------------------------------------------------------------------------------------//
    void Start()
    {

        // このオブジェクトはPhotonInstanciate()から生成される
        // Start()は他クライアント所有のオブジェクトが自動で生成された場合にも呼ばれるので注意

        // 自クライアント所有のオブジェクトの場合のみ実行
        if (photonView.IsMine)
        {

            //アバターの名前を変更する
            ChangeMyName("Player-No:" + PhotonNetwork.LocalPlayer.ActorNumber);

            //定期的にアバターの色を変更する
            StartCoroutine("ChangeMyColor");
        }
    }



    //------------------------------------------------------------------------------------------------------------------------------//
    // ストリームによる状態の同期
    //------------------------------------------------------------------------------------------------------------------------------//
    void ChangeMyName(string name)
    {

        // 自分の名前を変更する
        this.gameObject.name = name;
        transform.Find("NameUI").gameObject.GetComponent<TextMesh>().text = name;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {

            // 自クライアント所有のオブジェクトの状態変更を送信
            string myName = this.gameObject.name;
            stream.SendNext(myName);

        }
        else
        {

            // 他クライアント所有のオブジェクトの状態変更を受信
            string otherName = (string)stream.ReceiveNext();

            // 名前の変更を反映する
            this.gameObject.name = otherName;
            transform.Find("NameUI").gameObject.GetComponent<TextMesh>().text = otherName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 自クライアントが生成したオブジェクトの場合のみ実行
        if (photonView.IsMine == false)
        {
            return;
        }
        // キーボード入力による移動処理
        var v = Input.GetAxis("Vertical");
        Vector3 velocity = new Vector3(0, 0, v);
        velocity = transform.TransformDirection(velocity);
        velocity *= 5f;
        transform.localPosition += velocity * Time.fixedDeltaTime;

        // キーボード入力による回転処理
        var h = Input.GetAxis("Horizontal");
        transform.Rotate(0, h * 3f, 0);
    }

    //-----------------------------------------------------------------------------------------------------
    //PRCによるイベントの同期
    //-----------------------------------------------------------------------------------------------------
    IEnumerator ChangeMyColor()
    {

        while (true)
        {

            //定期的に自分の色を変更する

            //自クライアントを含めた全クライアントに送信 / バッファリングはしない
            Vector3 color = new Vector3(Random.value, Random.value, Random.value);
            PhotonView.Get(this).RPC("OnChangeMyColor", RpcTarget.All, color);

            yield return new WaitForSeconds(2f);
        }
    }

    [PunRPC]
    void OnChangeMyColor(Vector3 color)
    {

        // 色の変更を反映する
        this.GetComponent<Renderer>().material.color = new Color(color.x, color.y, color.z);
    }



}
