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
    private int _currentActiveCells;

    // MonoBehaviour
    void Awake()
    {
        _layoutGroup = GetComponent<VerticalLayoutGroup>();
    }

    // Public methods
    public void SetupMissingCells()
    {
        int numberOfElements = DataSource.NumberOfElementsInTableView(this);
        SetupCells(_currentActiveCells, numberOfElements - _currentActiveCells);
        _currentActiveCells = numberOfElements;
    }

    public void ReloadData()
    {
        int numberOfElements = DataSource.NumberOfElementsInTableView(this);
        DeactivateAllCells();
        SetupCells(0, numberOfElements);
        _currentActiveCells = numberOfElements;
    }

    public void ForceReloadData()
    {
        int numberOfElements = DataSource.NumberOfElementsInTableView(this);
        DestroyAllCells();
        SetupCells(0, numberOfElements);
        _currentActiveCells = numberOfElements;
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

    // Private methods
    private void DeactivateAllCells()
    {
        for (int i = 0; i < transform.childCount; ++i)
            transform.GetChild(i).gameObject.SetActive(false);
        _currentActiveCells = 0;
    }

    private void DestroyAllCells()
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
            Destroy(transform.GetChild(i).gameObject);
        _currentActiveCells = 0;
    }

    private void SetupCells(int startIndex, int size)
    {
        int numberOfElements = startIndex + size;
        for (int i = startIndex; i < numberOfElements; ++i)
            SetupCellAtIndex(i);
    }

    private void SetupCellAtIndex(int index)
    {
        GameObject cell = DataSource.TableViewCellForIndex(index, this);
        cell.transform.SetParent(_layoutGroup.transform, false);
    }

}
