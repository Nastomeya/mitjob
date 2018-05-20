using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSenceControl : MonoBehaviour 
{
	public GameObject loadingScreenObj;
	public Slider progressSlider;

	AsyncOperation async;

	public static LoadingSenceControl loadSceneControlClass;

	bool isGameStart;


	void Awake()
	{
		if (!isGameStart) 
		{
			loadSceneControlClass = this;
			//SceneManager.LoadSceneAsync (3, LoadSceneMode.Additive);
			StartCoroutine (LoadingScene (1));
			isGameStart = true;
		}
	}

	public void UnloadScene(int scenNum)
	{
		StartCoroutine (UnloadingScene (scenNum));
	}

	IEnumerator UnloadingScene(int scenNum)
	{
		yield return null;
		SceneManager.UnloadScene (scenNum);
	}
	public void LoadScene(int scenNum)
	{
		//if (!isGameStart) 
		{
			StartCoroutine (LoadingScene (scenNum));
			//	isGameStart = true;
		}
	}
	public void LoadScene(int loadScenNum, int unloadScenNum)
	{
		//if (!isGameStart) 
		{
			StartCoroutine (LoadingScene (loadScenNum));
			StartCoroutine (UnloadingScene (unloadScenNum));
			//	isGameStart = true;
		}
	}
	IEnumerator LoadingScene(int scenNum)
	{
		loadingScreenObj.SetActive (true);
		//async = SceneManager.LoadSceneAsync (scenNum);
		async = SceneManager.LoadSceneAsync (scenNum, LoadSceneMode.Additive);

		async.allowSceneActivation = false;

		while (async.isDone == false) 
		{
			progressSlider.value = async.progress;

			if (async.progress == 0.9f) 
			{
				progressSlider.value =1f;
				async.allowSceneActivation = true;
			}

			yield return null;
		}
		Debug.Log (scenNum);

	}
	// Use this for initialization
	void Start () 
	{
		//StartCoroutine (LoadingScene (1));
	}

}
