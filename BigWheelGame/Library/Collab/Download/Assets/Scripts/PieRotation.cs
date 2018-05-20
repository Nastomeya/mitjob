using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieRotation : MonoBehaviour
{
	private float speed = 100f;
	private float timeCounting = 5000f;
	int spinningDirection = 1;

	private bool hasResultFound = false;

	public int currentNumResult;
	public GameObject dock;
	public Transform pieTransform;//, targetTrans;


	public float dockAngle, centerliasingPeiRotation;

	public GameObject targetRotation;



	void Start()
	{

		targetRotation = new GameObject ();
		//targetRotation.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0-1800f));
	}
	void Update()
	{

		if (Timer.timerClass.modeStatus[2].IsActiviating)//starting the game
		{
			spinningMode ();
			if (!hasResultFound) 
			{
				hasResultFound = true;
				currentNumResult = GetRandomSeedNum.randomSeedNumClass.GetNumResult();
				pieTransform = PieGenerator.pieGenClass.pieceOfPie [currentNumResult].transform;
				//targetTrans = targetRotation.transform;

			}
		}
		else if (Timer.timerClass.modeStatus [4].IsActiviating) //spinning slowing down
		{
			//Debug.Log (transform.rotation);
			getFinalRotation();
			slowDownMode ();

			//Debug.Log (currentNumResult+" : "+PieGenerator.pieGenClass.pieceOfPie [currentNumResult].name);

		}
		else if (Timer.timerClass.modeStatus [3].IsActiviating) //result
		{
			if (hasResultFound)
				hasResultFound = false;

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
		targetRotation.transform.rotation *=  Quaternion.Euler (0, 0, 180-(sign*dockAngle)) * Quaternion.Euler (0, 0, centerliasingPeiRotation);


		//using towrard function to rotating
		//Vector3 direction = (pieTransform.position - dock.transform.position).normalized;
		//targetRotation.transform.rotation = Quaternion.LookRotation(direction);
	}
	void spinningMode()
	{

		transform.Rotate(Vector3.back, speed* Time.time);
		//transform.Rotate(Vector3.zero, speedslow*Time.deltaTime*spinningDirection);

	}
	float speedslow = 0f;//speed;
	private bool runOnce = false;

	//float rotationSpeed = 30f;
	//float timeScale = 0f;
	//float rate = (1f / rotationSpeed);
	void slowDownMode()
	{
		//speedslow = Mathf.Lerp(10f, 0f, 0.08f * Time.time);//Time.time);//0.0005f
		//transform.Rotate(Vector3.zero, speedslow*Time.deltaTime*spinningDirection);



		//Debug.Log ("before rotate angle :"+angle);
		//Debug.Log ("rotation: "+piePosition.rotation);

		//Debug.Log(speedslow);
		speedslow = Mathf.Lerp(speedslow, 0.5f, 0.001f * Time.time);//Time.time);//0.0005f


		//Debug.Log (targetRotation.transform.rotation+":"+gameObject.transform.rotation);
		//transform.rotation  = piePosition.rotation;
		//timeScale += Time.deltaTime;

		gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, targetRotation.transform.rotation, 0.5f);//speedslow

		//StartCoroutine (QuatRotate (0.8f));

		//transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation.transform.rotation, Time.deltaTime * speed);

		//Debug.Log ("rotation: "+piePosition.rotation);
		//Debug.Log ("current angle :"+angle);

	}

 public IEnumerator QuatRotate(float _rotateSpeed)
 {
    float rate = 1.0F / _rotateSpeed;
    float timeScale = 0.0F;
    Quaternion q = transform.rotation;
         
	while(timeScale < rate)
    {
      timeScale += Time.deltaTime;
		transform.rotation = Quaternion.Slerp(q, targetRotation.transform.rotation, timeScale/rate);
      //print(timeScale);
      yield return new WaitForEndOfFrame();
    }
 }
}
