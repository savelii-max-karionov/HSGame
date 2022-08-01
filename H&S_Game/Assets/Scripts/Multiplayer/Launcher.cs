using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace HS
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region Private Serializable Fields
        /// <summary>
        /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
        /// </summary>
        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        [SerializeField]
        private const byte maxPlayersPerRoom = 4;

        #endregion


        #region Private Fields

        private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

        private Photon.Realtime.Player[] cachedPlayers;

        /// <summary>
        /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
        /// </summary>
        string gameVersion = "1";
        /// <summary>
        /// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon,
        /// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
        /// Typically this is used for the OnConnectedToMaster() callback.
        /// </summary>
        bool isConnecting;

        [Tooltip("The Ui Panel to let the user enter name, connect and play")]
        [SerializeField]
        private GameObject loginPanel;

        [SerializeField]
        private Text UserIDText;

        [Tooltip("The UI Label to inform the user that the connection is in progress")]
        [SerializeField]
        private GameObject progressLabel;

        [Tooltip("The UI Label to inform the user which lobby he is in")]
        [SerializeField]
        private Text lobbyNameText;

        [Tooltip("The UI text to inform the user all the rooms in the lobby")]
        [SerializeField]
        private Text roomlistText;

        [Tooltip("The UI texts to show all the players in the room")]
        [SerializeField]
        private List<Text> PlayerTexts;

        [Tooltip("The UI panel to hold all UIs when the user is in the lobby")]
        [SerializeField]
        private GameObject lobbyPanel;

        [Tooltip("The Input for joining the room ")]
        [SerializeField]
        private Text roomNameInput;

        [SerializeField]
        private GameObject roomNameButon;

        private string userID;

        private const string lobbyName = "test";

        [SerializeField]
        private GameObject roomPanel;

        TypedLobby lobby = new TypedLobby(lobbyName, LobbyType.Default);


        #endregion

        #region Public Fields


        private void Start()
        {
            //progressLabel.SetActive(false);
            //controlPanel.SetActive(true);
        }

        #endregion


        #region MonoBehaviour CallBacks


        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {
            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.AutomaticallySyncScene = true;


        }



        #endregion


        #region Public Methods

        public void startGame()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel(1);
            }
        }

        public void becomeMonster()
        {
            GameStatus.IsMonster = true;
        }
        public void joinLobby()
        {
            PhotonNetwork.JoinLobby(lobby);
        }

        /// <summary>
        /// Start the connection process.
        /// - If already connected, we attempt joining a random room
        /// - if not yet connected, Connect this application instance to Photon Cloud Network
        /// </summary>
        public void Connect()
        {
            //progressLabel.SetActive(true);
            //controlPanel.SetActive(false);
            // we check if we are connected or not, we join if we are , else we initiate the connection to the server.

            this.loginPanel.gameObject.SetActive(false);
            this.progressLabel.SetActive(true);

            PhotonNetwork.AuthValues = new AuthenticationValues();

            this.userID = UserIDText.text;
            PhotonNetwork.AuthValues.UserId = this.userID;


            //this.ConnectingLabel.SetActive(true);

            PhotonNetwork.ConnectUsingSettings();

            //if (PhotonNetwork.IsConnected)
            //{
            //    // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
            //    PhotonNetwork.JoinRandomRoom();
            //}
            //else
            //{
            //    // #Critical, we must first and foremost connect to Photon Online Server.
            //    // keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
            //    isConnecting = PhotonNetwork.ConnectUsingSettings();
            //    PhotonNetwork.GameVersion = gameVersion;
            //}


        }

        public void joinRoom()
        {
            string roomName = roomNameInput.text;
            PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom, PublishUserId = true }, lobby);
        }

        public void leaveRoom()
        {
            roomPanel.SetActive(false);
            lobbyPanel.SetActive(true);
            PhotonNetwork.LeaveRoom();

            foreach (var i in PlayerTexts)
            {
                i.text = "";
            }

        }

        #endregion

        #region MonoBehaviourPunCallbacks Callbacks

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            UpdateCachedRoomList(roomList);
        }

        public override void OnConnected()
        {
            Debug.Log("PunCockpit:OnConnected()");


            this.progressLabel.SetActive(false);


        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster() was called by PUN");

            progressLabel.SetActive(false);
            lobbyPanel.SetActive(true);


            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            //PhotonNetwork.JoinRandomRoom();



        }



        public override void OnDisconnected(DisconnectCause cause)
        {
            //progressLabel.SetActive(false);
            //controlPanel.SetActive(true);
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        }

        public override void OnJoinedLobby()
        {
            progressLabel.SetActive(false);
            lobbyPanel.SetActive(false);
            roomPanel.SetActive(true);

            Debug.Log("On Joined Lobby");

            roomNameInput.gameObject.SetActive(true);



            Debug.Log("Joined lobby!");

        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            RefreshPlayerListAndUI();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            RefreshPlayerListAndUI();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

            // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {

            roomPanel.SetActive(true);

            Debug.Log("Now this client is in a room.");

            roomlistText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();

            RefreshPlayerListAndUI();

            // #Critical: We only load if we are the first player, else we rely on `PhotonNetwork.AutomaticallySyncScene` to sync our instance scene.
            //if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            //{
            //    Debug.Log("We load the 'Room for 1' ");
            //    // #Critical
            //    // Load the Room Level.
            //    PhotonNetwork.LoadLevel("Room for 1");
            //}
        }



        #endregion
        #region private methods
        private void UpdateCachedRoomList(List<RoomInfo> roomList)
        {
            for (int i = 0; i < roomList.Count; i++)
            {
                RoomInfo info = roomList[i];
                if (info.RemovedFromList)
                {
                    cachedRoomList.Remove(info.Name);
                }
                else
                {
                    cachedRoomList[info.Name] = info;
                }
            }

            foreach (var i in cachedRoomList)
            {
                roomlistText.text += i.Key;
            }
        }
        private void RefreshPlayerListAndUI()
        {
            cachedPlayers = PhotonNetwork.PlayerList;

            for (int i = 0; i < maxPlayersPerRoom; i++)
            {
                PlayerTexts[i].text = "";
            }

            for (int i = 0; i < cachedPlayers.Length; i++)
            {
                PlayerTexts[i].text = cachedPlayers[i].UserId;
            }
        }
        #endregion
    }


}