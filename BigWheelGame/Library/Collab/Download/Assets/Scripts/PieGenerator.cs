using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieGenerator : MonoBehaviour 
{
	public Image wedgePrefabs;
	public List<CardInfo>  getCardData; //public List<CardApiInfo>  getCardData;//
	private int totalCardSlots;
	public CardType[] cardType;//CardApiType[] cardType;//

	public static PieGenerator pieGenClass;

	public static bool isGameStart;

	public GameObject[] pieceOfPie;
	void Awake()
	{
		pieGenClass = this; 
	}

	void Start () 
	{
		GeneratePie();
	}

    public void GeneratePie()
	{
		getCardData = CardData.GetData();//CardDataApi.cardDataApiClass.FetchData();//
		totalCardSlots = CardData.totalCardSlots;//totalCardSlots = CardDataApi.cardDataApiClass.totalCardSlots;//		

		//cardType = CardDataApi.cardDataApiClass.card;//CardData.cardType;//CardData.cardType;//
		pieceOfPie = new GameObject[totalCardSlots];

		float zRotation = 0f;

		for (int i = 0; i < totalCardSlots; i++)
		{
			//Debug.Log(getCardData[i].CardID+":"+getCardData[i].CardName);
			pieceOfPie[i] = new GameObject();
			RectTransform rect = pieceOfPie[i].AddComponent<RectTransform> ();
			pieceOfPie[i].name = i +":"+getCardData[i].cardName.Replace(" Card","") ;;//pieceOfPie[i].name = i +":"+getCardData[i].CardName.Replace(" Card","") ;
				

			Text label = pieceOfPie[i].AddComponent<Text> ();
			//label.transform.SetParent(pei.transform, false);
			label.fontSize = 10;
			label.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
			label.text = i.ToString();
			label.alignment = TextAnchor.LowerCenter;
			rect.sizeDelta = new Vector2 (350f, 350f);
			//label.transform.rotation = Quaternion (label.transform.rotation.x, label.transform.rotation.y, label.transform.rotation.z);


			Image newWedge = Instantiate(wedgePrefabs) as Image;
			newWedge.name = getCardData[i].cardName.Replace(" Card"," img") ;//newWedge.name = getCardData[i].CardName.Replace(" Card"," img") ;

			newWedge.transform.SetParent(gameObject.transform, false);
			//newWedge.transform.SetParent(transform, false);
			newWedge.color = getCardData[i].colourRGB;;//newWedge.color = getCardData[i].ColourRGB;
			newWedge.fillAmount = 1 / (float)totalCardSlots;//i / (float)totalCardNum;
			newWedge.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, zRotation));

			zRotation -= newWedge.fillAmount * 360f;



			pieceOfPie[i].transform.SetParent (newWedge.transform, false);

			float angle = Quaternion.Angle(transform.rotation, newWedge.transform.rotation);

			//Debug.Log (angle+" :  "+i+" : "+newWedge.name);
		}
	}
	void OnDestroy()
	{
		//getCardData.Clear();
	}
}
