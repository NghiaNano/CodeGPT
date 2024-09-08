using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace Clinic
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
        //private static string logFilePath = "error_log.txt";
        //private static readonly string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Log", $"log_{DateTime.Now.ToString("yyyy/MM/dd")}.txt");
        string binPath = AppDomain.CurrentDomain.BaseDirectory;
        
        protected override void OnStartup(StartupEventArgs e)
        {
            // Đăng ký sự kiện bắt lỗi toàn bộ ứng dụng
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            base.OnStartup(e);
        }

        // Bắt các ngoại lệ không xử lý trong UI thread
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            LogErrorToFile(e.Exception, "UI thread exception");
            MessageBox.Show($"An unhandled exception occurred: {e.Exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            // Ngăn ứng dụng không bị đóng
            e.Handled = true;
        }
        // Bắt các ngoại lệ trong non-UI thread
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = e.ExceptionObject as Exception;
            LogErrorToFile(exception, "Non-UI thread exception");
            MessageBox.Show($"An unhandled domain exception occurred: {exception?.Message}", "Critical Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // Bắt lỗi không đồng bộ (tasks)
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            LogErrorToFile(e.Exception, "Task exception");
            MessageBox.Show($"An unhandled task exception occurred: {e.Exception.Message}", "Task Error", MessageBoxButton.OK, MessageBoxImage.Error);

            // Ngăn task bị đánh dấu là lỗi nghiêm trọng
            e.SetObserved();
        }
        // Ghi log lỗi ra file
        private void LogErrorToFile(Exception exception, string exceptionSource)
        {
            if (exception == null)
                return;

            try
            {
                string logFolderPath = Path.Combine(binPath, "Log");
                if (!Directory.Exists(logFolderPath))
                {
                    Directory.CreateDirectory(logFolderPath);
                }
                string logFileName = $"log_{DateTime.Now:yyyyMMdd}.txt";
                string logFilePath = Path.Combine(logFolderPath, logFileName);

                string logMessage = $"[{DateTime.Now:yyyy/MM/dd_HH:mm:ss}] {exceptionSource}: {exception.Message}\n{exception.StackTrace}\n";
                File.AppendAllText(logFilePath, logMessage);
            }
            catch (Exception ex)
            {
                // Nếu việc ghi log gặp lỗi, hiển thị thông báo nhưng không gây crash ứng dụng
                MessageBox.Show($"Failed to write to log file: {ex.Message}", "Log Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}