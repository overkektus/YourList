using YourList.ViewModels.Utilities;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace YourList.ViewModels
{
    public class NotifyViewModel : ViewModelBase
    {
        #region notifier configuration
        private Notifier _notifier;

        public NotifyViewModel()
        {
            _notifier = CreateNotifier(Corner.TopRight, PositionProviderType.Window, NotificationLifetimeType.Basic);
            Application.Current.MainWindow.Closing += MainWindowOnClosing;
        }

        public Notifier CreateNotifier(Corner corner, PositionProviderType relation, NotificationLifetimeType lifetime)
        {
            _notifier?.Dispose();
            _notifier = null;

            return new Notifier(cfg =>
            {
                cfg.PositionProvider = CreatePositionProvider(corner, relation);
                cfg.LifetimeSupervisor = CreateLifetimeSupervisor(lifetime);
                cfg.Dispatcher = Dispatcher.CurrentDispatcher;
            });
        }

        public void ChangePosition(Corner corner, PositionProviderType relation, NotificationLifetimeType lifetime)
        {
            _notifier = CreateNotifier(corner, relation, lifetime);
        }

        private void MainWindowOnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            _notifier.Dispose();
        }

        private static INotificationsLifetimeSupervisor CreateLifetimeSupervisor(NotificationLifetimeType lifetime)
        {
            if (lifetime == NotificationLifetimeType.Basic)
                return new CountBasedLifetimeSupervisor(MaximumNotificationCount.FromCount(5));

            return new TimeAndCountBasedLifetimeSupervisor(TimeSpan.FromSeconds(3), MaximumNotificationCount.UnlimitedNotifications());
        }

        private static IPositionProvider CreatePositionProvider(Corner corner, PositionProviderType relation)
        {
            switch (relation)
            {
                case PositionProviderType.Window:
                    {
                        return new WindowPositionProvider(Application.Current.MainWindow, corner, 25, 10);
                    }
                case PositionProviderType.Screen:
                    {
                        return new PrimaryScreenPositionProvider(corner, 5, 5);
                    }
                case PositionProviderType.Control:
                    {
                        var mainWindow = Application.Current.MainWindow as MainWindow;
                        var trackingElement = mainWindow?.TrackingElement;
                        return new ControlPositionProvider(mainWindow, trackingElement, corner, 5, 5);
                    }
            }

            throw new InvalidEnumArgumentException();
        }
        #endregion

        #region notifier messages
        internal void ShowWarning(string message)
        {
            _notifier.ShowWarning(message);
        }

        internal void ShowSuccess(string message)
        {
            _notifier.ShowSuccess(message);
        }

        public void ShowInformation(string message)
        {
            _notifier.ShowInformation(message);
        }

        public void ShowError(string message)
        {
            _notifier.ShowError(message);
        }

        public void ShowCustomizedMessage(string message)
        {
            var options = new ToastNotifications.Messages.Core.MessageOptions
            {
                FontSize = 25,
                ShowCloseButton = false,
                NotificationClickAction = n =>
                {
                    n.Close();
                    _notifier.ShowSuccess("clicked!");
                }
            };

            _notifier.ShowError(message, options);
        }
        #endregion

        #region example settings
        private Corner _corner;
        public Corner Corner
        {
            get { return _corner; }
            set
            {
                _corner = value;
                OnPropertyChanged("Corner");
                ChangePosition(_corner, _positionProviderType, _lifetime);
            }
        }

        private PositionProviderType _positionProviderType;
        public PositionProviderType PositionProviderType
        {
            get { return _positionProviderType; }
            set
            {
                _positionProviderType = value;
                OnPropertyChanged("PositionProviderType");
                ChangePosition(_corner, _positionProviderType, _lifetime);
            }
        }

        private NotificationLifetimeType _lifetime;

        public NotificationLifetimeType Lifetime
        {
            get
            {
                return _lifetime;
            }
            set
            {
                _lifetime = value;
                OnPropertyChanged("Lifetime");
                ChangePosition(_corner, _positionProviderType, _lifetime);
            }
        }

#endregion
    }
}
