using UnityEngine;
using UnityEngine.UI;
using YleService;

public class SearchMenu : MonoBehaviour, TableViewDataSource {

	public TableView MainTableView;
	public ScrollRect MainScrollRect;
	public InputField SearchInputField;
	public Button SearchButton;
	public YleProgramSearchService SearchService;

	[SpaceAttribute(10)]
	public GameObject CellPrefab;

    void Start ()
	{
		MainTableView.DataSource = this;
		SearchService.ProgramListWasUpdated += ProgramListWasUpdated;
		SearchService.ProgramListFailedLoading += ProgramListFailedLoading;

		SearchButton.onClick.AddListener(SearchButtonWasSelected);
	}

    void Update ()
	{
		if (MainScrollRect.normalizedPosition.y <= 0.0 && !SearchService.IsLoading)
			SearchService.LoadProgramBatch();
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

	private void SearchButtonWasSelected()
	{
		if (!string.IsNullOrEmpty(SearchInputField.text))
			SearchService.InitializeProgramSearch(SearchInputField.text, 10);
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
