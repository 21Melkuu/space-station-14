using Robust.Client.UserInterface.Controllers;

namespace Content.Client.SS220.Questionnaire;


public sealed class ExperienceViewerUIController : UIController
{
    private QuestionnaireWindow? _window;

    public override void Initialize()
    {
        base.Initialize();

        _window = UIManager.CreateWindow<QuestionnaireWindow>();
        _window.OnSubmitAction += //тут поднять ивет короче идите нахуй
    }

}
