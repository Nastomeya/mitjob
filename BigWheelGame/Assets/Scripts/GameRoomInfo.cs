using System.Collections;
using UnityEngine;

using UnityEngine.Networking;

public class GameRoomInfo : MonoBehaviour 
{
	//api
	string requestURL = "http://128.199.91.255:8080/rooms?user_id=1";

	public static GameRoomInfo randomSeedNumClass;
	public GameRoomsData gameRoomData;
	public bool hasRoomDataFetched;


	void Awake()
	{ 
		randomSeedNumClass = this; 
	}
	void Start()
	{
		GetRoomInfo ();

	}
	public void GetRoomInfo()
	{
		StartCoroutine(AccessToRoom());
	}

	IEnumerator AccessToRoom()
	{
		GameRoomsData roomData;
		WWW damandingData = new WWW(requestURL);

		hasRoomDataFetched = false;
		yield return damandingData;


		if(!string.IsNullOrEmpty(damandingData.error)) 
		{
			Debug.Log( "Error connecting: " + damandingData.error );
		}
		else 
		{
			gameRoomData = JsonUtility.FromJson<GameRoomsData> (damandingData.text);

			hasRoomDataFetched = true;

		}
	}
}

[System.Serializable]
public struct GameRoomsData
{
	public RoomData[] rooms;
	public UserData user;
}
[System.Serializable]
public struct RoomData
{
	public string name;
	public int current_user;
	public int max_user;
}
[System.Serializable]
public struct UserData
{
	public string name;
	public int coins;
}