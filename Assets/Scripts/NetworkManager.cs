using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	public int maxPlayers;

	private const string typeName = "20150123_GGJ_CGL_stauderutz";			// unique(!) game description
	private const string gameName = "roomName";								// room name

	private HostData[] hostList;											// list of open game servers

	public GameObject playerPrefab;											// the player object
	public Transform spawnServer;											// the spawning point
	public Transform spawnClient;

	public Transform previewCamera;
	public Canvas canvas;

	// Use this for initialization
	void Start () {
		// create the server on the client machine (localhost)
		//MasterServer.ipAddress = "127.0.0.1";
		DeactivateCanvas();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void StartServer(int maxPlayers){
		// initialize a game server on port 25000 with a maximum of 2 players and register it with the unity master server
		Network.InitializeServer(maxPlayers, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

		void OnServerInitialized(){
			Debug.Log("Server initialized!");
			SpawnPlayer(spawnServer.position);
			DestroyCamera();
			ActivateCanvas();
		}

	private void RefreshHostList(){
		MasterServer.RequestHostList(typeName);
	}

		void OnMasterServerEvent(MasterServerEvent msEvent){
			if (msEvent == MasterServerEvent.HostListReceived){
				hostList = MasterServer.PollHostList();
			}
		}

	private void JoinServer(HostData hostData){
		Network.Connect(hostData);
	}

		void OnConnectedToServer(){
			Debug.Log("Server joined!");
			SpawnPlayer(spawnClient.position);
			DestroyCamera();
			ActivateCanvas();
		}

	private void SpawnPlayer(Vector3 position){
		Network.Instantiate(playerPrefab, position, Quaternion.identity, 0);
	}

		void OnPlayerDisconnected(NetworkPlayer player){
			Debug.Log("Cleaning up after the player. That dirty little shit!");
			Network.RemoveRPCs(player);
			Network.DestroyPlayerObjects(player);
		}

	private void DestroyCamera(){
		Destroy(previewCamera.gameObject, 0);
	}

	private void ActivateCanvas(){
		canvas.enabled = true;
	}

	private void DeactivateCanvas(){
		canvas.enabled = false;
	}

	// TODO: implement NEW UI!
	void OnGUI(){
		if (!Network.isClient && !Network.isServer){
			if (GUI.Button(new Rect(100,100,250,100), "Start Server")){
				StartServer (maxPlayers);
			}

			if (GUI.Button(new Rect(100,250,250,100), "Refresh Hostlist")){
				RefreshHostList ();
			}

			if (hostList != null){
				for (int i = 0; i < hostList.Length; i++){
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName)){
						JoinServer(hostList[i]);
					}
				}
			}

		}
	}
}
