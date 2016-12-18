using System;
using UnityEngine;
using YleService;

public class SearchMenu : MonoBehaviour, TableViewDataSource {

	public TableView MainTableView;
	public YleProgramSearchService SearchService;

	[SpaceAttribute(10)]
	public GameObject CellPrefab;

    void Start ()
	{
		MainTableView.DataSource = this;
		SearchService.ProgramListWasUpdated += ProgramListWasUpdated;
		SearchService.ProgramListFailedLoading += ProgramListFailedLoading;
	}

    void Update ()
	{

	}

	// Events
	private void ProgramListWasUpdated()
	{
		 MainTableView.ReloadData();
	}

	private void ProgramListFailedLoading(string error)
	{
		 Debug.LogError(error);
	}

	// TableViewDataSource implementation
	public int NumberOfElementsInTableView(TableView tableView)
    {
        return SearchService.Programs.Count;
    }

    public GameObject TableViewCellForIndex(int index, TableView tableView)
    {
		GameObject cell = tableView.DequeueCellAtIndex(index);
		if (!cell)
			cell = Instantiate(CellPrefab);
		
		ProgramPresenter presenter = cell.GetComponent<ProgramPresenter>();
		presenter.PresentProgram(SearchService.Programs[index]);
		return cell;
    }
}
