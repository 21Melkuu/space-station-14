using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Content.Shared.SS220.ApproveMaths;

namespace Content.Server.SS220.ApproveMaths
{
    public sealed class ApproveMathsSystem : EntitySystem
    {
        public override void Initialize()
        {
            base.Initialize();

            SubscribeLocalEvent<ApproveMathsComponent, UiButtonPressedMessage>(OnUiButtonPressed);
        }

        private void OnUiButtonPressed(Entity<ApproveMathsComponent> ent, ref UiButtonPressedMessage args)
        {
            var user = args.Actor;
            if (!Exists(user))
                return;

            switch (args.Button)
            {
                case UiButton.Check:
                    var sosi = 0;
                    break;
            }
        }
    }
}
