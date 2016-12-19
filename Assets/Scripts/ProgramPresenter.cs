using UnityEngine;
using UnityEngine.UI;
using YleService;

public class ProgramPresenter : MonoBehaviour
{
    public Text ProgramTitleLabel;
    public Text ProgramDescriptionLabel;

    public void PresentProgram(YleProgram program)
    {
        ProgramTitleLabel.text = program.Title;
        ProgramDescriptionLabel.text = program.Description;
    }
}
