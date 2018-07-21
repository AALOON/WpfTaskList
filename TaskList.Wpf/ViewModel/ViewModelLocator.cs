using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using TaskList.Wpf.Model;

namespace TaskList.Wpf.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            DispatcherHelper.Initialize();
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataService, DesignDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, EmptyDataService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
        }


        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        
        public static void Cleanup()
        {
        }
    }
}