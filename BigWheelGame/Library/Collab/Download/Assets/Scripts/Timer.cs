using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

	//Text timeLabel;
	Shadow timeTextShadow;
	public GameObject timerTextObj;
	private Text timerText;

	private float initialWaitingTime;
	private float stopBettingTime;
	private float spinningTime;
	private float totalTime;

	private float mainTimeCountdown;//initial time setting
	private float showTimeCounting;

	public static Timer timerClass;
	public TimelineStatus[] modeStatus = new TimelineStatus[5];

	void Awake()
	{
		timerClass = this;
	}

	void Start()
	{
		setUPTimeValues();

		initialFontStyle();//set up the timer of font style initially
		initalTimelineMode();
	}
	void Update()
	{
		//when there is enough time to count down
		if (mainTimeCountdown > 0)
		{
			mainTimeCountdown -= Time.deltaTime;
			showTimeCounting = mainTimeCountdown - (totalTime - stopBettingTime);

			if ((totalTime - mainTimeCountdown <= initialWaitingTime) && (mainTimeCountdown > totalTime - initialWaitingTime))//waiting
			{
				waitingFontStyle(getTimeMinutesFormat(showTimeCounting));

				if(mainTimeCountdown>totalTime-1f)//reset everything
				{
					timelineState(0);
                    LockButtons(false);
					BetButtonManager.betButManagerClass.ResetBtns();
				}
				    
			}

			else if ((totalTime - mainTimeCountdown <= stopBettingTime) && (mainTimeCountdown > totalTime - stopBettingTime))//stop betting
			{
				stopBettingFontStyle(getTimeMinutesFormat(showTimeCounting));
				timelineState(1);
				LockButtons(true);
			}
			else if ((totalTime - mainTimeCountdown <= spinningTime) && (mainTimeCountdown > totalTime - spinningTime))//start
			{
				//Debug.Log ((totalTime - mainTimeCountdown)+":"+(spinningTime-6f) );
				startSpinningFontStyle("Start");
				if (totalTime - mainTimeCountdown > spinningTime -2f)
					timelineState (4);
				else
					timelineState (2);

            }

			else if ((totalTime - mainTimeCountdown <= totalTime) && (mainTimeCountdown > 0))//result
			{
				resultFontStyle("Result");
				timelineState(3);
				UI_Controller_InGame.uiControlClass.ExitBtnReset ();

			}
				

		}
		else //end of old game, new game start and reset.
		{
			mainTimeCountdown = totalTime;

		}

	}
	private void LockButtons(bool isLock)
	{
		int btnsLength = BetButtonManager.betButManagerClass.addButtons.Length;

		for (int i = 0; i < btnsLength; i++)
		{
			if(isLock)
			{
				BetButtonManager.betButManagerClass.addButtons[i].interactable = false;
				BetButtonManager.betButManagerClass.remButtons[i].interactable = false;
			}
			else
			{
				BetButtonManager.betButManagerClass.addButtons[i].interactable = true;
				BetButtonManager.betButManagerClass.remButtons[i].interactable = true;
			}
		}
	}
    private void setUPTimeValues()
	{
		//setting up time values
		initialWaitingTime = 2f;// 35f;
		stopBettingTime = initialWaitingTime + 2f;//5f;
		spinningTime = stopBettingTime + 5f;//10f;
		totalTime = spinningTime + 5f;//10f;
        mainTimeCountdown = totalTime;

	}

	//Timer label style setting
	private void initialFontStyle()
	{
		
		timerText = timerTextObj.GetComponent(typeof(Text)) as Text;
		timerText.fontSize = 60;
		timerText.fontStyle = FontStyle.Bold;
		timerText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
		timerText.alignment = TextAnchor.MiddleCenter;

		timerText.color = Color.white;
	}
	private void waitingFontStyle(string timeText)
	{
		timerText.text = timeText;
		timerText.color = Color.white;
		timerText.fontSize = 60;
	}
	private void stopBettingFontStyle(string timeText)
	{
		timerText.text = timeText;
		timerText.color = new Color(0.8f, 0.3f, 0.3f);
		timerText.fontSize = 60;
	}
	private void startSpinningFontStyle(string timeText)
	{
		timerText.text = timeText;
		timerText.color = new Color(1f, 0.84f, 0f);
		timerText.fontSize = 40;
	}
	private void resultFontStyle(string timeText)
	{
		timerText.text = timeText;
		timerText.color = new Color(0.2f, 1f, 0.4f);
		timerText.fontSize = 33;
	}

	//Timer label display format
	private string getTimeMinutesFormat(float timer)
	{
		int minutes = Mathf.FloorToInt(timer / 60);
		float seconds = timer - minutes * 60f;
		string timerFormat;

		if (timer > 10)
			timerFormat = string.Format("{0:00}", seconds);//string.Format("{0:00}:{1:00.00}", minutes, seconds);//

		else
			timerFormat = string.Format("{0:0}", seconds);
		return timerFormat;
	}



	private void initalTimelineMode()
	{
		modeStatus[0].Mode = "bettingTime";
		modeStatus[1].Mode = "stopBetting";
		modeStatus[2].Mode = "spinningWheel";
		modeStatus[4].Mode = "slowDownSpinning";
		modeStatus[3].Mode = "result";

		for (int i = 0; i < modeStatus.Length; i++)
		{
			modeStatus[i].IsActiviating = false;
		}
	}
	private void timelineState (int stateNum)
	{
		for (int i = 0; i < modeStatus.Length; i++)
		{
			if (i == stateNum)
			{
				modeStatus[i].IsActiviating = true;
				//Debug.Log(modeStatus[i].Mode + " isture");
			}
			else
				modeStatus[i].IsActiviating = false;
		}

	}

}
public struct TimelineStatus
{
	public string Mode;
	public bool IsActiviating;
}