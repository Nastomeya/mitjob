using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour 
{
	public void ChangeScene(int changeTheScene)
	{
		SceneManager.LoadScene(changeTheScene);
	}
}
