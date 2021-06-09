using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class upgradeButton
{
    public GameObject button;
    public Transform wantedLocation;
}



public class ParentUpgradeButton : MonoBehaviour
{
	[Space]
	[Header ("Main button rotation")]
	[SerializeField] float rotationDuration;
	[SerializeField] Ease rotationEase;

	[Header("allButton")]
	[SerializeField] upgradeButton[] allButtonList;

	[Space]
	[Header ("Animation")]
	[SerializeField] float expandDuration;
	[SerializeField] float collapseDuration;
	[SerializeField] Ease expandEase;
	[SerializeField] Ease collapseEase;
	[SerializeField] float disableChildTimer;

	
	[Space]
	[Header ("Material")]
	[SerializeField] Image changeMat;
	[SerializeField] Image upgradeMat;
	[SerializeField] Material notUpgradeMat;
	[SerializeField] Material canUpgradeMat;
	[SerializeField] Material notGlowMat;


	ChildTowerButton[] menuItems;


	//is menu opened or not
	bool isExpanded = false;

	Vector2 mainButtonPosition;
	int itemsCount;

	[SerializeField] GameObject mainButton;

	[Header ("main tower")]
	[SerializeField] Transform parentOfTower;
	[SerializeField] Button upgradeButton;
	[SerializeField] GameObject mainUpgradeButton;
	TowerStats mainTower;
	[SerializeField] TowerPreview towerPreviewScript;
	bool canUpgrade;
	bool towerMaxLevel;
	

	[Header ("GoBack")]
	[SerializeField] ParentTowerButton buyTower;


	[Header ("Effect")]
	[SerializeField] ParticleSystem upgradeEffect;
	[SerializeField] ParticleSystem sellEffect;

	[SerializeField] GameObject parentPreview;
	[SerializeField] GameObject mainTowerPreview;
	[SerializeField] GameObject newRangePreview;
 
	private void Start() 
	{
		GameManager.Instance.OnBuyModeTrigger += UpdateMaterial;
		setupChild();
	}

	public void setPreviewToMainTower()
	{
		mainTower = parentOfTower.GetChild(0).GetComponent<TowerStats>();
		ObjTower _towerObj = mainTower.GetTowerType();
		towerPreviewScript.SetTowerType(_towerObj);
	}

	void setupChild()
	{
		//SetAsLastSibling () to make sure that the main button will be always at the top layer
		mainButton.transform.SetAsLastSibling();
		mainButtonPosition = mainButton.transform.position;

		//set all menu items position to mainButtonPosition
		ResetPositions ();
	}

	void UpdateMaterial()
	{
		if (GameManager.Instance.isBuying)
		{
			mainButton.GetComponent<Button>().interactable = false;
			changeMat.material = notGlowMat;
			if (isExpanded)
			{
				ToggleMenu();
			}
		}
		else
		{
			mainButton.GetComponent<Button>().interactable = true;
			parentPreview.SetActive(true);
		}
	}

	void ResetPositions ()
	{
		for (int i = 0; i < allButtonList.Length; i++) {
			allButtonList[i].button.transform.position = mainButtonPosition;
		}
	}

	public void ToggleMenu()
	{
		isExpanded = !isExpanded;
		mainTower = parentOfTower.GetChild(0).GetComponent<TowerStats>();
		chekIfCanUpgrade();
		
		if (isExpanded) 
		{
			//menu opened
			EnableObject();
			for (int i = 0; i < allButtonList.Length; i++) {
				allButtonList[i].button.transform.DOMove(allButtonList[i].wantedLocation.position, expandDuration).SetEase (expandEase);
			}
			RotateMainButton(180, 0);
		} 
		else 
		{
			//menu closed
			for (int i = 0; i < allButtonList.Length; i++) {
				allButtonList[i].button.transform.DOMove(mainButtonPosition, collapseDuration).SetEase (collapseEase);
			}
			RotateMainButton(0, 180);
			StartCoroutine(DisableObject());
		}
	}

	public void closeToggle()
	{
		parentPreview.SetActive(false);
		isExpanded = false;
		for (int i = 0; i < allButtonList.Length; i++) 
		{
			allButtonList[i].button.transform.DOMove(mainButtonPosition, collapseDuration).SetEase (collapseEase);
		}
		RotateMainButton(0, 180);
		StartCoroutine(DisableObject());
	}

	IEnumerator DisableObject()
	{
		yield return new WaitForSeconds(disableChildTimer);
		int childCount = transform.childCount;
		for (int i = 0; i < childCount-1; i++) {
			transform.GetChild(i).gameObject.SetActive(false);
		}
	}

	public void DisableObjectInstant()
	{
		int childCount = transform.childCount;
		for (int i = 0; i < childCount-1; i++) {
			transform.GetChild(i).gameObject.SetActive(false);
		}
	}

	void EnableObject()
	{
		int childEnableCount = transform.childCount;
		for (int i = 0; i < childEnableCount; i++) 
		{
			transform.GetChild(i).gameObject.SetActive(true);
		}
		if (towerMaxLevel)
		{
			mainUpgradeButton.SetActive(false);
		}
	}

	void RotateMainButton(float angle, float startAngel)
	{
		mainButton.transform
		.DORotate (Vector3.forward * angle, rotationDuration)
		.From (Vector3.zero + new Vector3(0, 0, startAngel))
		.SetEase (rotationEase);
	}

	public void upgradeTower()
	{
		DisableUpgradePreview();
		parentPreview.gameObject.SetActive(true);
		upgradeEffect.Play();
		mainTower = parentOfTower.GetChild(0).GetComponent<TowerStats>();
		chekIfCanUpgrade();
		if (canUpgrade)
		{
			PlayerStats.Instance.SpendGold(mainTower.GetPrice());
			mainTower.LevelUp();
			towerPreviewScript.LevelUp();
			towerPreviewScript.disableTowerImage();
		}
	}

	public void sellTower()
	{
		DisableUpgradePreview();
		int _getCoin;
		sellEffect.Play();
		mainTower = parentOfTower.GetChild(0).GetComponent<TowerStats>();
		_getCoin = mainTower.GetSellPrice();

		PlayerStats.Instance.AddGold(_getCoin);
		Destroy(parentOfTower.GetChild(0).gameObject);

		buyTower.haveSellTower();
		towerPreviewScript.showTowerImage();
		towerPreviewScript.resetLevel();
		canUpgrade = true;
		towerMaxLevel = false;

		parentPreview.transform.GetChild(0).gameObject.SetActive(true);
	}

	public void chekIfCanUpgrade()
	{
		if (mainTower.GetTowerLevel() <= 1)
		{

			if (PlayerStats.Instance.GetGoldSystem().GetGold() >= mainTower.GetPrice())
			{
				canUpgrade = true;
			}
			else
			{
				canUpgrade = false;
			}
			updateVisualCanUpgrade();
		}
		else
		{
			towerMaxLevel = true;
			canUpgrade = false;
			updateVisualCanUpgrade();
			
		}
	}

	public void updateVisualCanUpgrade()
	{
		if (canUpgrade)
		{
			upgradeMat.material = canUpgradeMat;
			upgradeButton.interactable = true;
		}
		else
		{
			upgradeMat.material = notUpgradeMat;
			upgradeButton.interactable = false;
		}
	}

	public void DisableUpgradePreview()
	{
		// parentPreview.SetActive(false);
		mainTowerPreview.SetActive(false);
		newRangePreview.SetActive(false);
	}



}
