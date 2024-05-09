using System;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalrWpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HubConnection connection;  // Объект, через который мы можем взаимодействовать с хабом.
        public MainWindow()
        {
            InitializeComponent();

            // Создаем подключение к хабу.
            // Для создания объекта HubConnection применяется специальный
            // класс-строитель HubConnectionBuilder
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7248/chat")
                .Build();
            // Через метод WithUrl передается адрес, по которому доступен хаб.
            // Метод Build() создает объект подключения HubConnection.

            // Регистрируем функцию AddMessage для получения данных от хаба.
            connection.On<string, string>("AddMessage", (user, message) =>
            {
                Dispatcher.Invoke(() =>
                {
                    var newMessage = $"{user}: {message}";
                    chatbox.Items.Add(newMessage);
                });
            });
        }

        // Обработчик загрузки окна
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // После получения объекта HubConnection для подключения к хабу
                // необходимо вызвать метод StartAsync()
                await connection.StartAsync();
                chatbox.Items.Add("Вы вошли в чат");
                sendBtn.IsEnabled = true;
            }
            catch (Exception ex)
            {
                chatbox.Items.Add(ex.Message);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Для отправки данных хабу у объекта HubConnection применяется метод InvokeAsync()
                await connection.InvokeAsync("Send", userTextBox.Text, messageTextBox.Text);
            }
            catch (Exception ex)
            {
                chatbox.Items.Add(ex.Message);
            }
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            await connection.InvokeAsync("Send", "", $"Пользователь {userTextBox.Text} выходит из чата");
            await connection.StopAsync();   // отключение от хаба
        }
    }
}
