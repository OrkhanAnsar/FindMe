using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Markup;

namespace FindMePrism.Notifications
{
    [ContentProperty("ContentDataTemplate")]
    public class ShowChildWindowsAction : TriggerAction<UIElement>
    {
        /// <summary>
        /// Шаблон, для отображения контента.
        /// </summary>
        public DataTemplate ContentDataTemplate { get; set; }

        protected override void Invoke(object parameter)
        {
            var args = (InteractionRequestedEventArgs)parameter;

            // Получаем ссылку на окно, содержащее объект, в который помещён триггер.
            Window parentWindows = Window.GetWindow(AssociatedObject);

            // Создаём дочернее окно, устанавливаем его содержиомое и его шаблон.
            var childWindows =
                new Window
                {
                    Owner = parentWindows,
                    WindowStyle = WindowStyle.ToolWindow,
                    SizeToContent = SizeToContent.WidthAndHeight,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                    Title = args.Context.Title,
                    Content = args.Context,
                    ContentTemplate = ContentDataTemplate,
                };

            // Обрабатываем делегат обратного вызова при закрытии окна.
            childWindows.Closed +=
                (sender, eventArgs) => {
                    if (args.Callback != null)
                    {
                        args.Callback();
                    }
                };

            // Показываем диалог.
            childWindows.ShowDialog();
        }
    }
}
