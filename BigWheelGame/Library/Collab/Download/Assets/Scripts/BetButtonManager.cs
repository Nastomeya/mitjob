using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BetButtonManager : MonoBehaviour
{
    public int _currentCredit = 50;

    public Text coinValueOnUI;
	public GameObject ButObjPreb;
    
    public static BetButtonManager betButManagerClass;
    
	private ButtonData[] buttonDatas;
	public Button[] addButtons, remButtons;
	public int allBettedValues;
    
    void Awake()
    {
		betButManagerClass = this;
    }

    void Start()
    {
        gerneratingUIButtons();
    }

    public void gerneratingUIButtons()
    {
        float space = 72f;
        float initalXPosition = -210f;

		CardType[] cardType = PieGenerator.pieGenClass.cardType;
		//CardApiType[] cardType = PieGenerator.pieGenClass.cardType;
        buttonDatas = new ButtonData[cardType.Length];

		addButtons = new Button[cardType.Length];
		remButtons = new Button[cardType.Length];

		for (int i = 0; i < cardType.Length; i++)
		{
			//instantiating the prefeb button set, and assign it to main gameobject's child
			buttonDatas[i]._buttonObj = Instantiate(ButObjPreb) as GameObject;
			buttonDatas[i]._buttonObj.transform.SetParent(gameObject.transform, false);
           
			//get childern of prefeb button set UI components
			Transform[] buttonTransform = buttonDatas[i]._buttonObj.GetComponentsInChildren<Transform>();
			buttonDatas[i]._addCreditButObj = buttonTransform[1].gameObject;
            buttonDatas[i]._remCreditButObj = buttonTransform[4].gameObject;

            //rename buttons
			buttonDatas[i]._buttonObj.name = cardType[i].cardName.Replace(" ", "") + "ButSet";//cardType[i].CardName.Replace(" ", "") + "ButSet";
			buttonDatas[i]._addCreditButObj.name = cardType[i].cardName.Replace(" ", "") + "AddBut";//_addCreditButObj.name = cardType[i].CardName.Replace(" ", "") + "AddBut";
			buttonDatas[i]._remCreditButObj.name = cardType[i].cardName.Replace(" ", "") + "RemBut";//_remCreditButObj.name = cardType[i].CardName.Replace(" ", "") + "RemBut";
			buttonTransform[3].gameObject.name = cardType[i].cardName.Replace(" ", "") + "Label";//gameObject.name = cardType[i].CardName.Replace(" ", "") + "Label";
				
            //set up positions
			Vector3 originPosition = buttonDatas[i]._buttonObj.transform.position;
			buttonDatas[i]._buttonObj.transform.position = new Vector3(initalXPosition + (i * space) + originPosition.x, -130f + originPosition.y, 0f);
   			buttonDatas[i]._remCreditButObj.transform.position = new Vector3(buttonDatas[i]._buttonObj.transform.position.x, originPosition.y-180f, 0f);

            //set up the colours
			Image setAddButImage = buttonDatas[i]._addCreditButObj.GetComponent<Image>();
			setAddButImage.color = CardData.getColorRGBCode(cardType[i].cardColour);// CardData.getColorRGBCode(cardType[i].CardColour);
			Image setRemButImage = buttonDatas[i]._remCreditButObj.GetComponent<Image>();
			setRemButImage.color = CardData.getColorRGBCode(cardType[i].cardColour);// CardData.getColorRGBCode(cardType[i].CardColour);

            //labelling the name on the each
			Text cardNameText = buttonTransform[3].gameObject.GetComponent<Text>();
			cardNameText.text = cardType[i].cardName.Replace(" Card", "");//cardType[i].CardName.Replace(" Card", "");

			buttonDatas[i]._iD = i;
			buttonDatas[i]._coinValue = cardType[i].rewardGoldCoins;//cardType[i].RewardGoldCoins;

			addButtons[i] = buttonDatas[i]._addCreditButObj.GetComponent<Button>();
			remButtons[i] = buttonDatas[i]._remCreditButObj.GetComponent<Button>();

        }
    }
    public void DeductCredit()
    {
        if (_currentCredit > 0)
        {
            _currentCredit--;
        }
        coinValueOnUI.text = _currentCredit.ToString();
    }
	public void ResetBtns()
	{
		allBettedValues = 0;
		for (int i = 0; i < buttonDatas.Length; i++)
		{
			Text setNullText;
			Transform[] buttonTransform = buttonDatas[i]._buttonObj.GetComponentsInChildren<Transform>();
			setNullText = buttonTransform[6].gameObject.GetComponent<Text>();
			setNullText.text = "0";

			CardBetted cardBettedScript = buttonTransform[0].gameObject.GetComponent<CardBetted>();
			cardBettedScript._currentCardValue = 0;
		}
	}

	public void AddCredit()
    {
        _currentCredit++;
        coinValueOnUI.text = _currentCredit.ToString();
    }

    public void WinCredit(int value)
    {
        _currentCredit += value;
        coinValueOnUI.text = _currentCredit.ToString();
    }
}

public struct ButtonData
{
    public int _iD;
    public int _coinValue;
    public GameObject _buttonObj, _addCreditButObj, _remCreditButObj;

	public ButtonData(int id, int coin, GameObject butObj, GameObject addCredit, GameObject remCredit)
    {
        _iD = id;
        _coinValue = coin;
		_buttonObj = butObj;
        _addCreditButObj = addCredit;
        _remCreditButObj = remCredit;
    }
}