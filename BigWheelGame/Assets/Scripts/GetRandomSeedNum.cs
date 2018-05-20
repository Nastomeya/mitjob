using System.Collections;
using UnityEngine;

using UnityEngine.Networking;

public class GetRandomSeedNum : MonoBehaviour 
{
	//api
	string requestURL = "keepcalm246.hopto.org:8003/api/RandomSeed";
	public int resultNum = -1;
	bool isScoreGain;
	public static GetRandomSeedNum randomSeedNumClass;

	void Awake()
	{ 
		randomSeedNumClass = this; 
		GetNumResult ();
	}

	public int GetNumResult()
	{
		StartCoroutine(AccessScore());
		return resultNum;
	}

	IEnumerator AccessScore()
	{
		WWWForm form =  new WWWForm();
		WWW damandingData = new WWW(requestURL);
		yield return damandingData;
		
		UnityWebRequest webRequest = UnityWebRequest.Get(requestURL);

		if(!string.IsNullOrEmpty(damandingData.error)) 
		{
			Debug.Log( "Error connecting: " + damandingData.error );
		}
		else 
		{
			//Debug.Log( "Result: " + damandingData.text );
			resultNum = int.Parse(damandingData.text);
		}
	}
}
