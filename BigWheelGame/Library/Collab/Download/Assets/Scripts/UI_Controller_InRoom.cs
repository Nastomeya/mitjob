using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller_InRoom : MonoBehaviour 
{
	public GameObject exitBtnPreb;
	public GameObject loadingScenePreb;
	//public Button roomBBtn;

	public GameObject RoomUIBtnPrefb;
	public GameObject UsernameUIPrefb;
	public GameObject CreditValueUIPrefb;

	private GameObject exitBtnObj;
	private Button exitBut;

	private LoadingSenceControl loadingSenceControlScript;
	GameRoomsData thisRoomData;



	void Start()
	{
		exitBtnObj = Instantiate(exitBtnPreb) as GameObject;
		exitBtnObj.transform.SetParent(gameObject.transform, false);

		exitBut = exitBtnObj.GetComponent<Button>();
		loadingSenceControlScript = loadingScenePreb.GetComponent<LoadingSenceControl> ();


		thisRoomData = new GameRoomsData();
		GameRoomInfo.randomSeedNumClass.GetRoomInfo ();
		StartCoroutine (waitingToGetRoomData());



		//exitBut.onClick.AddListener(()=>StartCoroutine(loadingSenceControlScript.LoadingScene(1)));
		//roomBBtn.onClick.AddListener(()=>StartCoroutine(loadingSenceControlScript.LoadingScene(3)));
		//exitBut.onClick.AddListener(()=>LoadingSenceControl.loadSceneControlClass.LoadScene(1));
		//roomBBtn.onClick.AddListener(()=>LoadingSenceControl.loadSceneControlClass.LoadScene(3));

	}

	IEnumerator waitingToGetRoomData()
	{
		while (!GameRoomInfo.randomSeedNumClass.hasRoomDataFetched) 
			yield return thisRoomData;

		thisRoomData = GameRoomInfo.randomSeedNumClass.gameRoomData;

		buildUI ();
		Debug.Log("the player is: "+thisRoomData.user.name+", he has: "+thisRoomData.user.coins+"\n");

	}

	public void buildUI()
	{
		//all buttons
		float uiButDistanceInbetween = 140f;
		for (int i = 0; i < thisRoomData.rooms.Length; i++) 
		{
			GameObject roomUI;
			roomUI = Instantiate(RoomUIBtnPrefb) as GameObject;
			roomUI.transform.SetParent(gameObject.transform, false);

			Text roomNameLabel, currUserLabel;
			GameObject roomNameLabelObj, currUserLabelObj;

			Text[] allTextUIs = roomUI.GetComponentsInChildren<Text>();
			Image roomUIbg = roomUI.GetComponent<Image> ();
			Button roomUIbtn = roomUI.GetComponent<Button> ();

			roomNameLabel = allTextUIs[0].gameObject.GetComponent<Text>();
			currUserLabel = allTextUIs[1].gameObject.GetComponent<Text>();

			roomNameLabel.text = thisRoomData.rooms[i].name;

			roomUI.name = thisRoomData.rooms[i].name+"UI";
			roomUI.transform.position = new Vector3 (roomUI.transform.position.x, roomUI.transform.position.y-i*uiButDistanceInbetween,roomUI.transform.position.z);

			if (thisRoomData.rooms [i].current_user <= thisRoomData.rooms [i].max_user) 
			{
				currUserLabel.text = thisRoomData.rooms [i].current_user + "/" + thisRoomData.rooms [i].max_user;
				currUserLabel.color = Color.green;
				roomUIbg.color = new Color (0.286f, 0.7137f, 1f);
				roomUIbtn.interactable = true;
			} 
			else 
			{
				currUserLabel.text = "Full";
				currUserLabel.color = Color.red;
				roomUIbg.color = new Color (0.5568f, 0.5568f, 0.5568f);
				roomUIbtn.interactable = false;
			}

		}

		//user name and coin credit
		GameObject userNameObj, creditObj;
		userNameObj = Instantiate(UsernameUIPrefb) as GameObject;
		creditObj = Instantiate(CreditValueUIPrefb) as GameObject;

		userNameObj.transform.SetParent(gameObject.transform, false);
		creditObj.transform.SetParent(gameObject.transform, false);

		Text userNameLabel = userNameObj.GetComponent<Text> ();
		Text creditLable = creditObj.GetComponent<Text> ();

		userNameLabel.text = thisRoomData.user.name;
		creditLable.text = thisRoomData.user.coins.ToString();


	}
	void OnDestroy()
	{
		//exitBut.onClick.RemoveListener(()=>StartCoroutine(loadingSenceControlScript.LoadingScene(1)));
		//roomBBtn.onClick.RemoveListener(()=>StartCoroutine(loadingSenceControlScript.LoadingScene(3)));
		//exitBut.onClick.RemoveListener(()=>LoadingSenceControl.loadSceneControlClass.LoadScene(1));
		//roomBBtn.onClick.RemoveListener(()=>LoadingSenceControl.loadSceneControlClass.LoadScene(3));
	}

}
