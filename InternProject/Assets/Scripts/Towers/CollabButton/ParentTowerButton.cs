using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ParentTowerButton : MonoBehaviour
{
	[Header ("space between menu items")]
	[SerializeField] Vector2 spacing;

	[Space]
	[Header ("Main button rotation")]
	[SerializeField] float rotationDuration;
	[SerializeField] Ease rotationEase;

	[Space]
	[Header ("Animation")]
	[SerializeField] float expandDuration;
	[SerializeField] float collapseDuration;
	[SerializeField] Ease expandEase;
	[SerializeField] Ease collapseEase;

	[Space]
	[Header ("TowerData")]
	[SerializeField] ObjTower[] towerToChoose;
	[SerializeField] GameObject childBuyTower;
	[SerializeField] float disableChildTimer = 0.3f;

	// mainChildButton mainButton;
	Button mainButton;
	ChildTowerButton[] menuItems;
	[SerializeField] Image changeMat;
    [SerializeField] Material notGlowMat;
    [SerializeField] Material glowMat;
	[SerializeField] Material haveTower;
	[SerializeField] GameObject upgradeParent;
	[SerializeField] ParticleSystem buildEffect;

	//is menu opened or not
	bool isExpanded = false;
	public bool alreadyHaveTower;

	Vector2 mainButtonPosition;
	int itemsCount;

	[SerializeField] Transform previewTower;


	void Start ()
	{
		GameManager.Instance.OnBuyModeTrigger += UpdateMaterial;
		GameManager.Instance.OnBuyModeTrigger += deletePreview;
		deletePreview();
		spawnChild();
		setupChild();
	}

	void spawnChild()
	{
        foreach (var tower in towerToChoose)
        {
            GameObject newTowerButton = Instantiate(childBuyTower, gameObject.transform);
			newTowerButton.GetComponent<ChildTowerButton>().towerObject = tower;
			newTowerButton.SetActive(false);
			var imageSetter = newTowerButton.transform.GetChild(0);
            imageSetter.GetComponent<TowerChildDisplay>().displayImg(tower); 
        }
	}

	void deletePreview()
	{
		previewTower.gameObject.SetActive(false);
	}

	IEnumerator deletePreviewCD(float _waitTime)
	{
		yield return new WaitForSeconds(_waitTime);
		deletePreview();
	}


	public void haveBuildTower()
	{
		buildEffect.Play();
		alreadyHaveTower = true;
		mainButton.gameObject.SetActive(false);
		UpdateMaterial();
		upgradeParent.SetActive(true);
		upgradeParent.GetComponent<ParentUpgradeButton>().DisableObjectInstant();
		previewTower.gameObject.SetActive(true);
		previewTower.GetChild(0).gameObject.SetActive(false);
	}

	public void haveSellTower()
	{
		alreadyHaveTower = false;
		mainButton.gameObject.SetActive(true);
		upgradeParent.SetActive(false);
		UpdateMaterial();
		changeMat.material = glowMat;		
		mainButton.interactable = true;
	}
	

	void setupChild()
	{
		//add all the items to the menuItems array
		itemsCount = transform.childCount - 1;
		menuItems = new ChildTowerButton[itemsCount];
		for (int i = 0; i < itemsCount; i++) {
			// +1 to ignore the main button
			menuItems[i] = transform.GetChild (i + 1).GetComponent<ChildTowerButton>();
		}

		mainButton = transform.GetChild(0).GetComponent<Button>();

		//SetAsLastSibling () to make sure that the main button will be always at the top layer
		mainButton.transform.SetAsLastSibling();
		mainButtonPosition = mainButton.transform.position;

		//set all menu items position to mainButtonPosition
		ResetPositions ();
	}

	void EnableObject()
	{
		int childEnableCount = transform.childCount;
		for (int i = 0; i < childEnableCount; i++) {
			transform.GetChild (i).gameObject.SetActive(true);
		}
	}


	IEnumerator DisableObject()
	{
		yield return new WaitForSeconds(disableChildTimer);
		int childCount = transform.childCount;
		for (int i = 0; i < childCount-1; i++) {
			transform.GetChild(i).gameObject.SetActive(false);
		}
	}


	void ResetPositions ()
	{
		for (int i = 0; i < itemsCount; i++) {
			menuItems[i].trans.position = mainButtonPosition;
		}
	}

	public void ToggleMenu ()
	{
		isExpanded = !isExpanded;
		GameManager.Instance.CheckWhatCanBuy();

		if (isExpanded) {
			//menu opened
			EnableObject();
			for (int i = 0; i < itemsCount; i++) {
				menuItems [i].trans.DOMove (mainButtonPosition + spacing * (i + 1), expandDuration).SetEase (expandEase);
			}
			RotateMainButton(180, 0);
		} 
		else 
		{
			//menu closed
			for (int i = 0; i < itemsCount; i++) 
			{
				menuItems [i].trans.DOMove (mainButtonPosition, collapseDuration).SetEase (collapseEase);
			}
			RotateMainButton(0, 180);
			StartCoroutine(DisableObject());
		}
		deletePreview();
		// StartCoroutine(deletePreviewCD(0.5f));
		// StartCoroutine(deletePreviewCD(0.8f));
		// StartCoroutine(deletePreviewCD(1.2f));
		// StartCoroutine(deletePreviewCD(1.5f));

	}


	public void closeToggle()
	{
		isExpanded = false;
		for (int i = 0; i < itemsCount; i++) {
			menuItems [i].trans.DOMove (mainButtonPosition, collapseDuration).SetEase (collapseEase);
		}
		deletePreview();
		RotateMainButton(0, 180);
		StartCoroutine(DisableObject());
	}

	void RotateMainButton(float angle, float startAngel)
	{
		mainButton.transform
		.DORotate (Vector3.forward * angle, rotationDuration)
		.From (Vector3.zero + new Vector3(0, 0, startAngel))
		.SetEase (rotationEase);
	}

	public void UpdateMaterial()
	{
		if (!alreadyHaveTower)
		{
			if (GameManager.Instance.isBuying)
			{
				mainButton.interactable = false;
				changeMat.material = notGlowMat;
				if (isExpanded)
				{
					ToggleMenu();
				}
			}
			else
			{
				changeMat.material = glowMat;		
				mainButton.interactable = true;
			}
		}
		else
		{
			mainButton.interactable = false;
			changeMat.material = haveTower;
			if (isExpanded)
			{
				ToggleMenu();
			}
		}
	}

}
