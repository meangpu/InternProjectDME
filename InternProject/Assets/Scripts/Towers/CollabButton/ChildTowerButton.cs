using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChildTowerButton : MonoBehaviour
{
	[HideInInspector] public Transform trans;
	[SerializeField] Button buyButton;

	//SettingsMenu reference
	ParentTowerButton parentTowerButton;
	Transform previewTranform;
	Transform towerBuyTransform;

	[SerializeField] GameObject towerPfb;
	public ObjTower towerObject;

	GameObject previewTower;
	bool canBuy = true;

	[Header("VisualRepresent")]
	[SerializeField] Color32 cannotBuyColor;
	[SerializeField] Image towerImage;
	[SerializeField] TMP_Text pricetext;


	void Awake ()
	{
		trans = transform;
		GameManager.Instance.OnCheckWhatCanBuy += chekIfCanBuy;
		// obj preview tower
		previewTranform = transform.parent.parent.parent.GetChild(0);
		towerBuyTransform = transform.parent.parent.parent.GetChild(1);
		parentTowerButton = transform.parent.GetComponent<ParentTowerButton>();
	}

	private void Start() 
	{
		GameManager.Instance.checkWhatCanBuy();
	}


	public void previewBuy()
	{
		if (canBuy)
		{
			previewTranform.gameObject.SetActive(true);
			previewTranform.GetChild(0).GetComponent<TowerPreview>().SetTowerType(towerObject);
		}
	}

	public void chekIfCanBuy()
	{
		if (PlayerStats.Instance.GetGoldSystem().GetGold() >= towerObject.GetUpgradeCost()[0])
		{
			canBuy = true;
		}
		else
		{
			canBuy = false;
		}

		updateVisualCanBuy();

	}

	public void buyTower()
	{
		// check if player already buy tower
		if (towerBuyTransform.childCount == 1)
		{
			towerBuyTransform.gameObject.SetActive(true);
			PlayerStats.Instance.SpendGold(towerObject.GetUpgradeCost()[0]);
			GameObject buildTower = Instantiate(towerPfb, towerBuyTransform.position, Quaternion.identity);
			buildTower.GetComponent<TowerStats>().SetTowerType(towerObject);
			buildTower.transform.parent = towerBuyTransform;
			GameManager.Instance.checkWhatCanBuy();
			parentTowerButton.haveBuildTower();
		}
		
		disablePreview();
	}

	public void updateVisualCanBuy()
	{
		if(canBuy)
		{
			pricetext.color =  new Color(1, 1, 1, 1);
			towerImage.color = new Color(1, 1, 1, 1);
			buyButton.interactable = true;
		}
		else
		{
			towerImage.color = new Color(1, 1, 1, 0.2f);
			pricetext.color = cannotBuyColor;
			buyButton.interactable = false;
		}
	}

	public void disablePreview()
	{
		previewTranform.gameObject.SetActive(false);
	}
}
