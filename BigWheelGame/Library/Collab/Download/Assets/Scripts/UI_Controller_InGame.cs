using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller_InGame : MonoBehaviour 
{
	public GameObject exitBtnPreb;
	public GameObject loadingScenePreb;
	public GameObject resultPanelPreb;

	public static UI_Controller_InGame uiControlClass;
	private GameObject exitBtnObj;
	private Button exitBut;

	private LoadingSenceControl loadingSenceControlScript;
	void Awake()
    {
		uiControlClass = this;
		buildResultUI ();
    }

	void Start()
	{
		exitBtnObj = Instantiate(exitBtnPreb) as GameObject;
		exitBtnObj.transform.SetParent(gameObject.transform, false);

		exitBut = exitBtnObj.GetComponent<Button>();
		loadingSenceControlScript = loadingScenePreb.GetComponent<LoadingSenceControl> ();

		//exitBut.onClick.AddListener(()=>StartCoroutine(loadingSenceControlScript.LoadingScene(2)));

		//exitBut.onClick.AddListener(()=>LoadingSenceControl.loadSceneControlClass.LoadScene(2));
		//exitBut.onClick.AddListener(()=>LoadingSenceControl.loadSceneControlClass.UnloadScene(3));
	}


	public void buildResultUI()
	{
		//resultPanelPreb 

		Text[] allTextUIs = resultPanelPreb.GetComponentsInChildren<Text>();

		Debug.Log (allTextUIs.Length);
	}

	public void NoExitRule(int bettedValue)
	{	
		if (bettedValue > 0) 
		{
			exitBut.interactable = false;
			//exitBut.onClick.RemoveListener(()=>StartCoroutine(loadingSenceControlScript.LoadingScene(2)));
			//exitBut.onClick.AddListener(()=>LoadingSenceControl.loadSceneControlClass.LoadScene(2));

		}
		else
			exitBut.interactable = true;
    }
	public void ExitBtnReset()
	{
		exitBut.interactable = true;
	}
	void OnDestroy()
	{
		//exitBut.onClick.RemoveListener(()=>StartCoroutine(loadingSenceControlScript.LoadingScene(2)));
		//exitBut.onClick.RemoveListener(()=>LoadingSenceControl.loadSceneControlClass.LoadScene(2));



		PieGenerator.pieGenClass.getCardData = null;

	}

}
