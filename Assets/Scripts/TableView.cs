using UnityEngine;
using UnityEngine.UI;

public interface TableViewDataSource
{
	int NumberOfElementsInTableView(TableView tableView);
	GameObject TableViewCellForIndex(int index, TableView tableView);
}

[RequireComponent (typeof (VerticalLayoutGroup))]
public class TableView : MonoBehaviour
{

	public TableViewDataSource DataSource;
	private VerticalLayoutGroup _layoutGroup;

	void Awake()
	{
		_layoutGroup = GetComponent<VerticalLayoutGroup>();
	}

	public void ReloadData()
	{
		DeactivateAllCells();
		SetupCells();
	}

	public void ForceReloadData()
	{
		DestroyAllCells();
		SetupCells();
	}

	public GameObject DequeueCellAtIndex(int index)
	{
		GameObject cell = CellAtIndex(index);
		if (cell)
			cell.SetActive(true);
		return cell;
	}

	public GameObject CellAtIndex(int index)
	{
		if (index >= 0 && index < transform.childCount)
			return transform.GetChild(index).gameObject;
		return null;
	}

	private void DeactivateAllCells()
	{
		for (int i = 0; i < transform.childCount; ++i)
			transform.GetChild(i).gameObject.SetActive(false);
	}

	private void DestroyAllCells()
	{
		for (int i = transform.childCount - 1; i >= 0; --i)
			Destroy(transform.GetChild(i).gameObject);
	}

	private void SetupCells()
	{
		int numberOfElements = DataSource.NumberOfElementsInTableView(this);
		for (int i = 0; i < numberOfElements; ++i)
			SetupCellAtIndex(i);
	}

	private void SetupCellAtIndex(int index)
	{
		GameObject cell = DataSource.TableViewCellForIndex(index, this);
		cell.transform.SetParent(_layoutGroup.transform, false);
	}

}
