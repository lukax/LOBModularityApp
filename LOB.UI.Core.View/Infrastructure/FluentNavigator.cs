﻿#region Usings

using System;
using System.Windows;
using System.Windows.Controls;
using LOB.UI.Core.View.Controls.Util;
using LOB.UI.Interface;
using LOB.UI.Interface.ViewModel.Base;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.View.Infrastructure
{
    public class FluentNavigator : IFluentNavigator
    {
        private readonly IUnityContainer _container;
        private IBaseView _resolvedView;
        private IBaseViewModel _resolvedViewModel;

        [InjectionConstructor]
        public FluentNavigator(IUnityContainer container)
        {
            _container = container;
        }

        public event OnOpenViewEventHandler OnOpenView;

        public IBaseView GetView()
        {
            if (_resolvedView == null)
                throw new ArgumentException("First resolve the view", "ResolveView");
            if (_resolvedView.ViewModel == null)
                throw new ArgumentException("First resolve the view model", "ResolveViewModel");
            _resolvedView.InitializeServices();
            return _resolvedView;
        }

        public IBaseViewModel GetViewModel()
        {
            if (_resolvedViewModel == null)
                throw new ArgumentException("First resolveView the ViewModel", "ResolveViewModel");
            return _resolvedViewModel;
        }

        public IFluentNavigator ResolveViewModel(string param)
        {
            SetViewModel(_container.Resolve(OperationTypes.ViewModels[param]) as IBaseViewModel);
            return this;
        }

        public IFluentNavigator ResolveViewModel<TViewModel>() where TViewModel : IBaseViewModel
        {
            SetViewModel(_container.Resolve<TViewModel>());
            return this;
        }

        public IFluentNavigator ResolveView(string param)
        {
            SetView(_container.Resolve(OperationTypes.Views[param]) as IBaseView);
            return this;
        }

        public IFluentNavigator ResolveView<TView>() where TView : IBaseView
        {
            SetView(_resolvedView = _container.Resolve<TView>());
            return this;
        }

        public IFluentNavigator SetViewModel(IBaseViewModel viewModel)
        {
            _resolvedViewModel = viewModel;
            _resolvedView.ViewModel = viewModel;
            return this;
        }

        public IFluentNavigator SetView(IBaseView view)
        {
            _resolvedView = view;
            return this;
        }

        public void Show(bool asDialog = false)
        {
            var asUc = GetView() as UserControl;
            if (asUc != null)
            {
                var window = _container.Resolve<FrameWindow>();
                window.Content = asUc;
                window.DataContext = _resolvedView.ViewModel;
                window.Height = asUc.Height + 50;
                window.Width = asUc.Width + 50;
                if (!string.IsNullOrEmpty(_resolvedView.Header))
                    window.Title = (_resolvedView).Header;


                if (OnOpenView != null)
                    OnOpenView.Invoke(this, new OnOpenViewEventArgs((IBaseView) asUc));

                if (asDialog) window.ShowDialog();
                else window.Show();
            }
            else
            {
                var asW = _resolvedView as Window;
                if (asW != null)
                {
                    if (OnOpenView != null)
                        OnOpenView.Invoke(this, new OnOpenViewEventArgs((IBaseView) asW));

                    if (asDialog) asW.ShowDialog();
                    else asW.Show();
                }
            }
        }

        public bool PromptUser(string message)
        {
            return MessageBox.Show(message, "Prompt", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }
    }
}