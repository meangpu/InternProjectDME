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


	ChildTowerButton[] menuItems;


	//is menu opened or not
	bool isExpanded = false;

	Vector2 mainButtonPosition;
	int itemsCount;

	[SerializeField] GameObject mainButton;
 
	private void Start() 
	{
		setupChild();
	}


	void setupChild()
	{
		//SetAsLastSibling () to make sure that the main button will be always at the top layer
		mainButton.transform.SetAsLastSibling();
		mainButtonPosition = mainButton.transform.position;

		//set all menu items position to mainButtonPosition
		ResetPositions ();
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
		
		if (isExpanded) {
			//menu opened
			for (int i = 0; i < allButtonList.Length; i++) {
				allButtonList[i].button.transform.DOMove(allButtonList[i].wantedLocation.position, expandDuration).SetEase (expandEase);
			}
			RotateMainButton(180, 0);


		} else {
			//menu closed
			for (int i = 0; i < allButtonList.Length; i++) {
				allButtonList[i].button.transform.DOMove(mainButtonPosition, collapseDuration).SetEase (collapseEase);
			}
			RotateMainButton(0, 180);
		}
	}



	void RotateMainButton(float angle, float startAngel)
	{
		mainButton.transform
		.DORotate (Vector3.forward * angle, rotationDuration)
		.From (Vector3.zero + new Vector3(0, 0, startAngel))
		.SetEase (rotationEase);
	}


}
