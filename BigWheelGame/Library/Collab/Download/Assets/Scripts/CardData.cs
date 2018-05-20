using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CardData
//public class CardData
{
    public static int totalCardSlots = 0;
	public static CardType[] cardType;
    

	public static List<CardInfo> GetData()
    {
		List<CardInfo> allCardsData = new List<CardInfo>();

        //loading data from Jason files
        TextAsset loadJsonText = Resources.Load<TextAsset>("jsonFiles" + "/" + "CardData");
		//TextAsset loadJsonText = Resources.Load<TextAsset>("jsonFiles" + "/" + "CardData2");
        string jsonString = loadJsonText.text;
		string[] splitJson = jsonString.Split('\n');

		cardType = new CardType[splitJson.Length];

        //gather data from CardType catagaries
		for (int i = 0; i < splitJson.Length; i++)
		{
			cardType[i] = JsonUtility.FromJson<CardType>(splitJson[i]);
            
            //gather sequence data and split it into integers
			string[] tempCardIDString = cardType[i].sequence.Split(',');
			int[] tempCardID = new int[tempCardIDString.Length];

            //gathering colour data
			Color tempColour = getColorRGBCode(cardType[i].cardColour);

	        //assign data to every single identical useful cards
			for (int j = 0; j < tempCardID.Length; j++)
			{
				tempCardID[j] = int.Parse(tempCardIDString[j]);

				allCardsData.Add(new CardInfo(tempCardID[j], cardType[i].cardName, tempColour, cardType[i].rewardGoldCoins));
				totalCardSlots++;//counting total slot number
			}
         }
        //sorting data by card id number
		allCardsData.Sort(delegate (CardInfo infoA, CardInfo infoB)
        {
            return infoA.cardID.CompareTo(infoB.cardID);
        });		
		
		return allCardsData;
    }
	 
	public static Color getColorRGBCode(string colourCodeInString)
	{
		//gathering colour data
		string[] tempColStrs = colourCodeInString.Split(',');
		float colourR = float.Parse(tempColStrs[0]);
		float colourG = float.Parse(tempColStrs[1]);
		float colourB = float.Parse(tempColStrs[2]);
		Color colourRGB =  new Color(colourR, colourG, colourB);

		return colourRGB;
	}


}

public struct CardType
{
	public string cardName;
	public string cardColour;
	public string sequence;
	public int rewardGoldCoins;
}
public struct CardInfo
{
	public int cardID;
    public string cardName;
    public Color colourRGB;
	public int rewardGoldCoins;

	public CardInfo(int id, string name, Color colour, int reward)
	{
		cardID = id;
		cardName = name;
		colourRGB = colour;
		rewardGoldCoins = reward;
	}
}

