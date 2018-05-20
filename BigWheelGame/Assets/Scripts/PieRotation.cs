using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieRotation : MonoBehaviour
{
	private float speed = 100f;
	private float timeCounting = 5000f;
	int spinningDirection = 1;

	private bool hasResultFound = false;
	private bool isCardRemoved = false;

	public int currentNumResult;
	public GameObject dock;
	public Transform pieTransform;


	public float dockAngle, centerliasingPeiRotation;

	public GameObject targetRotation;

	private GameObject resultCardShowObj;


	void Start()
	{
		
		targetRotation = new GameObject ();
		resultCardShowObj = UI_Controller_InGame.uiControlClass.buildResultUI (Color.white);

	}
	void Update()
	{
		if (Timer.timerClass.modeStatus[0].IsActiviating)//initial
		{
			if (!isCardRemoved) 
			{
				Destroy (resultCardShowObj);
				isCardRemoved = true;
			}
		}
			
		else if (Timer.timerClass.modeStatus[2].IsActiviating)//starting the game
		{
			spinningMode ();
			if (!hasResultFound) 
			{
				//Debug.Log (GetRandomSeedNum.randomSeedNumClass.GetNumResult()%CardData.totalCardSlots + ":"+GetRandomSeedNum.randomSeedNumClass.GetNumResult());
				hasResultFound = true;
				currentNumResult = GetRandomSeedNum.randomSeedNumClass.GetNumResult () % CardData.totalCardSlots;//GetRandomSeedNum.randomSeedNumClass.GetNumResult();
				pieTransform = PieGenerator.pieGenClass.pieceOfPie [currentNumResult].transform;


			}
		}
		else if (Timer.timerClass.modeStatus [4].IsActiviating) //spinning slowing down
		{
			getFinalRotation();
			slowDownMode ();
		}
		else if (Timer.timerClass.modeStatus [3].IsActiviating) //result
		{
			if (hasResultFound)
			{	
				List<CardInfo> getCardData = CardData.GetData();
				
				resultCardShowObj = UI_Controller_InGame.uiControlClass.buildResultUI(getCardData [currentNumResult - 1].colourRGB);// PieGenerator.pieGenClass.getCardData[currentNumResult-1].colourRGB);

				hasResultFound = false;
				isCardRemoved = false;
			}

		}
	}
	public void getFinalRotation()
	{
		dockAngle = Quaternion.Angle (pieTransform.rotation, dock.transform.transform.rotation);


		float tempAngle = 0f;

		tempAngle = pieTransform.rotation.eulerAngles.z;

		centerliasingPeiRotation = 360f /CardData.totalCardSlots/2f;

		//on the left or right
		int sign;
		if (tempAngle > 180)
			sign = -1;
		else
			sign = 1;

		float centerliaingPeiPosition = 360 /CardData.totalCardSlots;
		targetRotation.transform.rotation *= Quaternion.Euler (0, 0, 180 - (sign * dockAngle));// * Quaternion.Euler (0, 0, centerliasingPeiRotation);

	}
	void spinningMode()
	{
		transform.Rotate(Vector3.back, speed* Time.time);
	}


	void slowDownMode()
	{
		gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, targetRotation.transform.rotation, 0.5f);
	}

}
