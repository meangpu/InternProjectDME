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

	// [Space]
	// [Header ("Fading")]
	// [SerializeField] float expandFadeDuration;
	// [SerializeField] float collapseFadeDuration;

	[Space]
	[Header ("TowerData")]
	[SerializeField] ObjTower[] towerToChoose;
	[SerializeField] GameObject childBuyTower;
	[SerializeField] float disableChildTImer;

	// mainChildButton mainButton;
	Button mainButton;
	ChildTowerButton[] menuItems;
	[SerializeField] Image changeMat;
    [SerializeField] Material notGlowMat;
    [SerializeField] Material glowMat;

	//is menu opened or not
	bool isExpanded = false;

	Vector2 mainButtonPosition;
	int itemsCount;


	void Start ()
	{
		GameManager.Instance.onBuyModeTrigger += UpdateMaterial;
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
		// mainButton.OnPointerClick.AddListener(ToggleMenu);



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
		yield return new WaitForSeconds(disableChildTImer);
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

		if (isExpanded) {
			//menu opened
			EnableObject();
			for (int i = 0; i < itemsCount; i++) {
				menuItems [i].trans.DOMove (mainButtonPosition + spacing * (i + 1), expandDuration).SetEase (expandEase);
				//Fade to alpha=1 starting from alpha=0 immediately
				// menuItems [i].img.DOFade (1f, expandFadeDuration).From (0f);   ******
			}
			RotateMainButton(180, 0);


		} else {
			//menu closed
			for (int i = 0; i < itemsCount; i++) {
				menuItems [i].trans.DOMove (mainButtonPosition, collapseDuration).SetEase (collapseEase);
				//Fade to alpha=0
				// menuItems [i].img.DOFade (0f, collapseFadeDuration);   *****
			}
			RotateMainButton(0, 180);
			StartCoroutine(DisableObject());
			
		}

		
	}

	void RotateMainButton(float angle, float startAngel)
	{
		mainButton.transform
		.DORotate (Vector3.forward * angle, rotationDuration)
		.From (Vector3.zero + new Vector3(0, 0, startAngel))
		.SetEase (rotationEase);
	}

	public void OnItemClick (int index)
	{
		//here you can add you logic 
		switch (index) {
			case 0: 
				//first button
				Debug.Log ("Music");
				break;
			case 1: 
				//second button
				Debug.Log ("Sounds");
				break;
			case 2: 
				//third button
				Debug.Log ("Vibration");
				break;
		}
	}

	public void UpdateMaterial()
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

	void OnDestroy ()
	{
		//remove click listener to avoid memory leaks


		// mainButton.onClick.RemoveListener (ToggleMenu);
	}
}
