// Copyright © 2026 Maxim Otrokhov. All rights reserved.

using System;
using System.IO;

namespace MediaSocial.Classes
{
    /// <summary>
    /// Минимальный логгер хоста: append-файл в %LOCALAPPDATA%\MediaSocial.
    /// Полноценный модуль логирования не планируется (см. docs/PROJECT_CHANGES_PLAN.md, раздел 2.6),
    /// здесь выполняется только минимальная запись ошибок уровня хоста (задачи M3.1, M1.3).
    /// </summary>
    public static class AppLog
    {
        private static readonly object SyncRoot = new object();

        /// <summary>
        /// Полный путь к файлу лога: %LOCALAPPDATA%\MediaSocial\MediaSocial.log
        /// </summary>
        public static string LogFilePath
        {
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "MediaSocial",
                    "MediaSocial.log");
            }
        }

        /// <summary>
        /// Записывает сообщение в лог. Ошибки самого логгера игнорируются — логирование не должно ронять приложение.
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        public static void Write(string message)
        {
            WriteInternal(null, message);
        }

        /// <summary>
        /// Записывает исключение с контекстом в лог. Ошибки самого логгера игнорируются.
        /// </summary>
        /// <param name="ex">Исключение</param>
        /// <param name="context">Контекст — что происходило в момент ошибки</param>
        public static void Write(Exception ex, string context)
        {
            WriteInternal(ex, context);
        }

        private static void WriteInternal(Exception ex, string message)
        {
            try
            {
                string logFile = LogFilePath;
                string directory = Path.GetDirectoryName(logFile);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string entry = string.Format(
                    "[{0:yyyy-MM-dd HH:mm:ss.fff}] {1}{2}{3}{4}",
                    DateTime.Now,
                    message ?? "",
                    Environment.NewLine,
                    ex != null ? ex.ToString() : "",
                    Environment.NewLine);

                lock (SyncRoot)
                {
                    File.AppendAllText(logFile, entry, System.Text.Encoding.UTF8);
                }
            }
            catch
            {
                // Логирование не должно приводить к падению приложения
            }
        }
    }
}
