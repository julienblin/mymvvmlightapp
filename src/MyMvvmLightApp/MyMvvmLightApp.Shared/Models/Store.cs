using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace MyMvvmLightApp.Models
{
	public class Store : ObservableObject
	{
		private ObservableCollection<Product> _products;
		public ObservableCollection<Product> Products
		{
			get => _products;
			set => Set(ref _products, value);
		}

		public Store()
		{
		}
	}
}
