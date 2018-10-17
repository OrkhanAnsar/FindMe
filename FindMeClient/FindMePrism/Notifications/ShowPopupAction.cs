using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using System.Windows.Markup;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FindMePrism.Notifications
{
    [ContentProperty("ContentDataTemplate")]
    public class ShowPopupAction : TriggerAction<UIElement>
    {
        private Action _callback;
        private Popup _popup;
        private ContentControl _popupContent;
        private Panel _root;

        /// <summary>
        /// Шаблон, для отображения контента.
        /// </summary>
        public DataTemplate ContentDataTemplate { get; set; }

        protected override void OnAttached()
        {
            // Получаем корневое окно.
            Window window = Window.GetWindow(AssociatedObject);
            if (window == null)
            {
                throw new NullReferenceException("Windows is null.");
            }

            // Проверяем, является ли корневым элементом Grid, 
            // если нет - создаём новый.
            _root = window.Content as Panel;
            if (_root == null)
            {
                _root = new Grid();
                _root.Children.Add((UIElement)window.Content);
                window.Content = _root;
            }

            // Контент всплывающего окна.
            _popupContent =
                new ContentControl
                {
                    ContentTemplate = ContentDataTemplate,
                };

            // Создаём всплывающее окно, задаём его визуальные свойства и контент.
            _popup =
                new Popup
                {
                    StaysOpen = true,
                    PopupAnimation = PopupAnimation.Fade,
                    AllowsTransparency = true,
                    Placement = PlacementMode.Center,
                    Child = _popupContent,
                };

            _popup.Closed += PopupOnClosed;

            window.LocationChanged += (sender, a) => UpdatePopupLocation();

            _root.Children.Add(_popup);
        }

        private void UpdatePopupLocation()
        {
            // При смене положения главного окна, 
            // необходимо обновить положение всплывающего окна.
            // Делаем это с помощью такого нехитрого трюка.
            if (!_popup.IsOpen)
            {
                return;
            }
            const double delta = 0.1;
            _popup.HorizontalOffset += delta;
            _popup.HorizontalOffset -= delta;
        }

        private void PopupOnClosed(object sender, EventArgs eventArgs)
        {
            // Вызываем делегат обратного вызова и снимаем блокировку с главного окна.
            if (_callback != null)
            {
                _callback();
            }
            _root.IsEnabled = true;
        }

        protected override void Invoke(object parameter)
        {
            var args = (InteractionRequestedEventArgs)parameter;

            _callback = args.Callback;
            _popupContent.Content = args.Context;

            // Блокируем содержимое главного окна и показываем всплывающее окно.
            _root.IsEnabled = false;
            _popup.IsOpen = true;
        }
    }
}
