using System;

using Xamarin.Forms;
using ThoughtsAndPrayersThree.ViewModels.Base;

namespace ThoughtsAndPrayersThree
{
	public abstract class BaseContentPage<T> : ContentPage where T : BaseViewModel, new()
	{

		T _viewModel;

		public BaseContentPage()
		{
			BindingContext = MyViewModel;
		}

		public T MyViewModel
		{
			get
			{
				return _viewModel ?? (_viewModel = new T());
			}
		}
	}
}



