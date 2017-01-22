using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// This script automatically connects to Photon (using the settings file),
/// tries to join a random room and creates one if none was found (which is ok).
/// </summary>
public class Networking : Photon.MonoBehaviour
{
    /// <summary>Connect automatically? If false you can set this to true later on or call ConnectUsingSettings in your own scripts.</summary>
    public bool AutoConnect = true;
    int roomNum = 1;
    string roomName = "MidnightWar";
    RoomOptions roomOptions = new RoomOptions() { IsVisible = false, MaxPlayers = 2 };


    public void Start()
    {
        PhotonNetwork.autoJoinLobby = false;    // we join randomly. always. no need to join a lobby to get the list of rooms.
        PhotonNetwork.ConnectUsingSettings("v4.2");
    }

    public void Connect()
    {
        PhotonNetwork.JoinOrCreateRoom(roomName + roomNum, roomOptions, TypedLobby.Default);
    }

    public void OnJoinedRoom()
    {
        checkPlayers();
    }

    public void OnPhotonPlayerConnected()
    {
        checkPlayers();
    }

    public void OnJoinRoomFailed()
    {
        roomNum++;
        PhotonNetwork.JoinOrCreateRoom(roomName + roomNum, roomOptions, TypedLobby.Default);
    }

    public void OnPhotonPlayerDisconnected()
    {
        if (PhotonNetwork.room != null)
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    public void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }

    public void OnLeftLobby()
    {

        PhotonNetwork.Disconnect();
    }

    public void OnDisconnectedFromPhoton()
    {
        reload();
    }

    private void checkPlayers()
    {
        Room currRoom;
        if (Room.playerCount() == 2)
        {
            //Go to the new scene to play the game
        }
    }

    public void reload()
    {
        StartCoroutine("reloadScene");
    }

    IEnumerator reloadScene()
    {
        yield return new WaitForSeconds(3);
        Scenemanager.setActiveScene(Scenemanager.GetActiveScene().buildIndex());
    }
}
