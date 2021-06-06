using UnityEngine;
using UnityEngine.UI;

public class ChildTowerButton : MonoBehaviour
{
	// [HideInInspector] public Image img;   ********
	[HideInInspector] public Transform trans;

	//SettingsMenu reference
	ParentTowerButton settingsMenu;

	//item button
	[SerializeField] Button button;

	//index of the item in the hierarchy
	int index;

	Transform parentTrans;

	[SerializeField] GameObject towerPfb;
	public ObjTower towerObject;

	GameObject previewTower;

	void Awake ()
	{
		// img = GetComponent<Image> ();        ******
		trans = transform;
		parentTrans = transform.parent.parent.parent.GetChild(0);

		settingsMenu = trans.parent.GetComponent <ParentTowerButton> ();

		//-1 to ignore the main button
		index = trans.GetSiblingIndex () - 1;

		//add click listener
		button.onClick.AddListener (OnItemClick);
	}

	void OnItemClick ()
	{
		settingsMenu.OnItemClick (index);
	}

	void OnDestroy ()
	{
		// remove click listener to avoid memory leaks
		button.onClick.RemoveListener (OnItemClick);
	}

	public void previewBuy()
	{
		previewTower = Instantiate(towerPfb, parentTrans.position, Quaternion.identity);
		previewTower.GetComponent<TowerStats>().SetTowerType(towerObject);
		previewTower.transform.parent = parentTrans;
	}

	public void destroyPreview()
	{
		if (previewTower != null)
		{
			Destroy(previewTower);
		}
	}
}
