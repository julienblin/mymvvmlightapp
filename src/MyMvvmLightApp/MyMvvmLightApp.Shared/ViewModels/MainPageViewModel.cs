using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AsyncAwaitBestPractices.MVVM;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyMvvmLightApp.Services;

namespace MyMvvmLightApp.ViewModels
{
	public class MainPageViewModel : ViewModelBase
	{
		private IExampleService _exampleService;

		public MainPageViewModel(IExampleService exampleService)
		{
			_exampleService = exampleService;

			DoSomethingCommand = new RelayCommand(DoSomething);
			DoSomethingAsyncCommand = new AsyncCommand(DoSomethingAsync);
		}

		public ICommand DoSomethingCommand { get; }

		public ICommand DoSomethingAsyncCommand { get; }

		private void DoSomething()
		{
			_exampleService.DoSomething();
		}

		private async Task DoSomethingAsync()
		{
			await _exampleService.DoSomethingAsync();
		}
	}
}
