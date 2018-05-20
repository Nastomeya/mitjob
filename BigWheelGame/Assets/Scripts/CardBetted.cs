using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBetted : MonoBehaviour
{
	public Text cardBatttedValue;
	public int _currentCardValue = 0;
    
	public static CardBetted cardBettedClass;

	void Awake()
	{
		cardBettedClass = this;
	}

	public void betTheCard()
	{
		if (BetButtonManager.betButManagerClass._currentCredit > 0)
		{
			BetButtonManager.betButManagerClass.DeductCredit();
			_currentCardValue++;
			cardBatttedValue.text = _currentCardValue.ToString();


			//adding all bet value and check if the user can exit
			BetButtonManager.betButManagerClass.allBettedValues++;

		}

		UI_Controller_InGame.uiControlClass.NoExitRule(BetButtonManager.betButManagerClass.allBettedValues);

	}

	public void sellTheCard()
    {
		if (_currentCardValue > 0)
		{
			BetButtonManager.betButManagerClass.AddCredit();
            _currentCardValue--;
			cardBatttedValue.text = _currentCardValue.ToString();

			//remove value from total addup
			BetButtonManager.betButManagerClass.allBettedValues--;

		}

		UI_Controller_InGame.uiControlClass.NoExitRule(BetButtonManager.betButManagerClass.allBettedValues);

    }
}
