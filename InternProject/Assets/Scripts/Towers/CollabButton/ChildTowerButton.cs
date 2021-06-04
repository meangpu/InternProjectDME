using UnityEngine;
using UnityEngine.UI;

public class ChildTowerButton : MonoBehaviour
{
	// [HideInInspector] public Image img;   ********
	[HideInInspector] public Transform trans;

	//SettingsMenu reference
	ParentTowerButton settingsMenu;

	//item button
	Button button;

	//index of the item in the hierarchy
	int index;

	Transform parentTrans;

	[SerializeField] GameObject towerPfb;

	GameObject previewTower;

	void Awake ()
	{
		// img = GetComponent<Image> ();        ******
		trans = transform;
		parentTrans = transform.parent.parent;

		settingsMenu = trans.parent.GetComponent <ParentTowerButton> ();

		//-1 to ignore the main button
		index = trans.GetSiblingIndex () - 1;

		//add click listener

		// button = GetComponent<Button> ();
		// button.onClick.AddListener (OnItemClick);
	}

	void OnItemClick ()
	{
		settingsMenu.OnItemClick (index);
	}

	void OnDestroy ()
	{
		//remove click listener to avoid memory leaks

		// button.onClick.RemoveListener (OnItemClick);
	}

	public void previewBuy()
	{
		previewTower = Instantiate(towerPfb, parentTrans.position, Quaternion.identity);
	}

	public void destroyPreview()
	{
		if (previewTower != null)
		{
			Destroy(previewTower);
		}
	}
}
