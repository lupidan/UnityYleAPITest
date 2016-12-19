using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YleService;

public class SearchMenu : MonoBehaviour, TableViewDataSource
{
    public TableView MainTableView;
    public ScrollRect MainScrollRect;
    public InputField SearchInputField;
    public Button SearchButton;
    public YleProgramSearchService SearchService;

    [SpaceAttribute(10)]
    public GameObject CellPrefab;

    // MonoBehaviour
    void Start ()
    {
        MainTableView.DataSource = this;
        SearchService.LoadProgramBatchFinished += LoadProgramBatchFinished;
        SearchButton.onClick.AddListener(SearchButtonWasSelected);
    }

    void Update ()
    {
        if (MainScrollRect.normalizedPosition.y <= 0.0 && !SearchService.IsLoading)
            SearchService.LoadProgramBatch();
    }

    // Events
    private void LoadProgramBatchFinished(List<YleProgram> programs, string error)
    {
        if (programs != null && error == null)
            MainTableView.SetupMissingCells();
        else
            Debug.LogError(error);
    }

    private void SearchButtonWasSelected()
    {
        if (!string.IsNullOrEmpty(SearchInputField.text))
        {
            SearchService.InitializeProgramSearch(SearchInputField.text, 10);
            SearchService.LoadProgramBatch();
            MainTableView.ReloadData();
        }	
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
