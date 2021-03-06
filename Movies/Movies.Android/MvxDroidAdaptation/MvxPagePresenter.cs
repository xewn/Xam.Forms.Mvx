using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;

namespace CoolBeans.Droid.MvxDroidAdaptation
{
    public class MvxPagePresenter
        : MvxAndroidViewPresenter
          , IMvxPageNavigationHost
    {
        public override void Show(MvxViewModelRequest request)
        {
            if (TryShowPage(request))
                return;

            Mvx.Error("Skipping request for {0}", request.ViewModelType.Name);
        }

        private bool TryShowPage(MvxViewModelRequest request)
        {
            if (NavigationProvider == null)
                return false;

            var page = MvxPresenterHelpers.CreatePage(request);
            if (page == null)
                return false;

            var viewModel = MvxPresenterHelpers.LoadViewModel(request);
            page.BindingContext = viewModel;

            NavigationProvider.Push(page);

            return true;
        }

        public override void Close(IMvxViewModel viewModel)
        {
            if (NavigationProvider == null)
                return;

            NavigationProvider.Pop();
        }

        public IMvxPageNavigationProvider NavigationProvider { get; set; }
    }
}