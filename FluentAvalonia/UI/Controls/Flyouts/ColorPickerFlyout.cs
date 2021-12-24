using Avalonia.Controls;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls.Primitives;
using System;
using System.ComponentModel;

namespace FluentAvalonia.UI.Controls
{
    public sealed class ColorPickerFlyout : PickerFlyoutBase
    {
        public ColorPicker ColorPicker => _picker ??= new ColorPicker();

        public event TypedEventHandler<ColorPickerFlyout, object> Confirmed;
        public event TypedEventHandler<ColorPickerFlyout, object> Dismissed;

        protected override Control CreatePresenter()
        {
            if (_picker == null)
                _picker = new ColorPicker();

            var pfp = new PickerFlyoutPresenter()
            {
                Content = _picker
            };
            pfp.Confirmed += OnFlyoutConfirmed;
            pfp.Dismissed += OnFlyoutDismissed;

            return pfp;
        }

		protected override void OnConfirmed()
		{
			Hide();
			Confirmed?.Invoke(this, EventArgs.Empty);
		}

		protected override void OnOpening(CancelEventArgs args)
		{
			base.OnOpening(args);
			
		}

		protected override void OnOpened()
		{
			base.OnOpened();
			// TEMPORARY FIX...REVERT TO ONOPENING AFTER NRE ISSUE FIXED
			(Popup.Child as PickerFlyoutPresenter).ShowHideButtons(ShouldShowConfirmationButtons());
		}

        protected override bool ShouldShowConfirmationButtons() => _showButtons;

		private void OnFlyoutDismissed(PickerFlyoutPresenter sender, object args)
		{
			Hide();
			Dismissed?.Invoke(this, EventArgs.Empty);
		}

        private void OnFlyoutConfirmed(PickerFlyoutPresenter sender, object args)
        {
            OnConfirmed();
        }

        internal void ShowHideButtons(bool show)
        {
            _showButtons = show;
        }

        private bool _showButtons = true;
        private ColorPicker _picker;
    }
}
