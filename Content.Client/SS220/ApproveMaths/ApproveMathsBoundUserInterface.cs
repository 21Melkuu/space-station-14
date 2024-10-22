using Content.Shared.SS220.ApproveMaths;
using JetBrains.Annotations;
using Robust.Client.UserInterface;

namespace Content.Client.SS220.ApproveMaths
{
    [UsedImplicitly]
    public sealed class ApproveMathsBoundUserInterface : BoundUserInterface
    {
        private ApproveMathsWindow? _window;
        public ApproveMathsBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
        {
        }

        protected override void Open()
        {
            base.Open();

            _window = this.CreateWindow<ApproveMathsWindow>();
            _window.OnApproveButton += ButtonPressed;
        }
        public void ButtonPressed(UiButton button)
        {
            SendMessage(new UiButtonPressedMessage(button));
        }

    }
}
