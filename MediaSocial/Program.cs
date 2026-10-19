using System;
using System.Windows.Forms;
using MediaSocial.Classes;

namespace MediaSocial
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Глобальные обработчики необработанных исключений (задача M3.1)
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        /// <summary>
        /// Обработчик необработанных исключений UI-потока: лог + предложение продолжить работу.
        /// </summary>
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            AppLog.Write(e.Exception, "Необработанное исключение в UI-потоке");

            DialogResult result = MessageBox.Show(
                "Произошла непредвиденная ошибка:\n\n" + e.Exception.Message +
                "\n\nПодробности записаны в журнал:\n" + AppLog.LogFilePath +
                "\n\nПродолжить работу приложения?",
                "MediaSocial — необработанное исключение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Error);

            if (result != DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// Обработчик необработанных исключений вне UI-потока: лог + сообщение о закрытии.
        /// </summary>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            AppLog.Write(ex, "Необработанное исключение вне UI-потока (приложение будет закрыто)");

            MessageBox.Show(
                "Произошла непреодолимая ошибка:\n\n" + (ex != null ? ex.Message : e.ExceptionObject) +
                "\n\nПодробности записаны в журнал:\n" + AppLog.LogFilePath +
                "\n\nПриложение будет закрыто.",
                "MediaSocial — критическая ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
