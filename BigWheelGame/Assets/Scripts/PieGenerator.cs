using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieGenerator : MonoBehaviour 
{
	public Image wedgePrefabs;
	public List<CardInfo>  getCardData;
	private int totalCardSlots;
	public CardType[] cardType;

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
		getCardData = CardData.GetData();
		totalCardSlots = CardData.totalCardSlots;

		cardType = CardData.cardType;
		pieceOfPie = new GameObject[totalCardSlots];

		float zRotation = 0f;

		for (int i = 0; i < totalCardSlots; i++)
		{
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

			float centerliasingPeiRotation = 360f /CardData.totalCardSlots/2f;
			label.transform.rotation *= Quaternion.Euler (0, 0, centerliasingPeiRotation);// Quaternion. (label.transform.rotation.x, label.transform.rotation.y, label.transform.rotation.z);


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
