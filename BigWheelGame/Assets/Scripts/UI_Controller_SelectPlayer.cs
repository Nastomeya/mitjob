using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller_SelectPlayer : MonoBehaviour 
{
	public GameObject exitBtnPreb;
	public GameObject loadingScenePreb;

	private GameObject exitBtnObj;
	private Button exitBut;

	public Button alphaBtn;

	private LoadingSenceControl loadingSenceControlScript;

	void Start()
	{
		exitBtnObj = Instantiate(exitBtnPreb) as GameObject;
		exitBtnObj.transform.SetParent(gameObject.transform, false);

		exitBut = exitBtnObj.GetComponent<Button>();
		loadingSenceControlScript = loadingScenePreb.GetComponent<LoadingSenceControl> ();

		alphaBtn.onClick.AddListener(()=>LoadingSenceControl.loadSceneControlClass.LoadScene(2));
		//exitBut.onClick.AddListener(()=>StartCoroutine(loadScreenScript.loadingScreen(1)));
	}


	void OnDestroy()
	{
		alphaBtn.onClick.RemoveListener(()=>LoadingSenceControl.loadSceneControlClass.LoadScene(2));
					
		//exitBut.onClick.RemoveListener(()=>StartCoroutine(loadScreenScript.loadingScreen(1)));
	}

}
