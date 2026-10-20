// Copyright © 2026 Maxim Otrokhov. All rights reserved.

using MediaSocial.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MediaSocial
{
    /// <summary>
    /// Holds A static instance of global program shtuff
    /// </summary>
    internal class Global
    {
        //Constructor
        public Global() { }

        public static PluginServices Plugins = new PluginServices();

        // Приватные коллекции изображений (M2.1): публичный доступ только через методы ниже (M2.2)
        private static readonly List<Images> _sourceImages = new List<Images>();
        private static readonly List<Images> _renderImages = new List<Images>();
        private static readonly List<SettingImage> _imageSettings = new List<SettingImage>();

        /// <summary>Количество изображений-источников.</summary>
        public static int SourceImagesCount { get { return _sourceImages.Count; } }

        /// <summary>Количество отрендеренных изображений.</summary>
        public static int RenderImagesCount { get { return _renderImages.Count; } }

        /// <summary>Количество сохранённых настроек изображений.</summary>
        public static int ImageSettingsCount { get { return _imageSettings.Count; } }

        /// <summary>Добавляет изображение-источник.</summary>
        public static void AddSourceImage(Images image)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
            _sourceImages.Add(image);
        }

        /// <summary>Добавляет отрендеренное изображение.</summary>
        public static void AddRenderImage(Images image)
        {
            if (image == null) throw new ArgumentNullException(nameof(image));
            _renderImages.Add(image);
        }

        /// <summary>Добавляет настройку изображения (историю параметров).</summary>
        public static void AddSetting(SettingImage setting)
        {
            if (setting == null) throw new ArgumentNullException(nameof(setting));
            _imageSettings.Add(setting);
        }

        /// <summary>Возвращает изображение-источник по индексу; null — если индекс вне диапазона.</summary>
        public static Images GetSourceImage(int index)
        {
            if (index < 0 || index >= _sourceImages.Count) return null;
            return _sourceImages[index];
        }

        /// <summary>Возвращает отрендеренное изображение по индексу; null — если индекс вне диапазона.</summary>
        public static Images GetRenderImage(int index)
        {
            if (index < 0 || index >= _renderImages.Count) return null;
            return _renderImages[index];
        }

        /// <summary>Удаляет изображение-источник по индексу. Возвращает true, если удаление выполнено.</summary>
        public static bool RemoveSourceImage(int index)
        {
            if (index < 0 || index >= _sourceImages.Count) return false;
            _sourceImages.RemoveAt(index);
            return true;
        }

        /// <summary>Удаляет отрендеренное изображение по индексу. Возвращает true, если удаление выполнено.</summary>
        public static bool RemoveRenderImage(int index)
        {
            if (index < 0 || index >= _renderImages.Count) return false;
            _renderImages.RemoveAt(index);
            return true;
        }

        /// <summary>Возвращает настройки изображений с указанным Id (пустая коллекция — если совпадений нет).</summary>
        public static IEnumerable<SettingImage> GetSettings(int id)
        {
            return _imageSettings.Where(setting => setting.Id == id);
        }

        /// <summary>Очищает все коллекции изображений и настроек.</summary>
        public static void ClearImages()
        {
            _sourceImages.Clear();
            _renderImages.Clear();
            _imageSettings.Clear();
        }

        /// <summary>Очищает только историю настроек изображений.</summary>
        public static void ClearSettings()
        {
            _imageSettings.Clear();
        }

        private static Image imageOut;
        public static Image ImageOut
        {
            get { return imageOut; }
            set
            {
                imageOut = value;
                OnImageOutChanged();
            }
        }

        public static event EventHandler ImageOutChanged;

        private static void OnImageOutChanged()
        {
            if (ImageOutChanged != null)
                ImageOutChanged(null, EventArgs.Empty);
        }

        private static int _imageIndexNow;
        public static int ImageIndexNow
        {
            get { return _imageIndexNow; }
            set { _imageIndexNow = value; }
        }

        private static bool renderE;

        public static bool renderEditor
        {
            get { return renderE; }
            set
            {
                renderE = value;
                OnEditorChanged();
            }
        }

        public static event EventHandler EditorChanged;

        private static void OnEditorChanged()
        {
            if (EditorChanged != null)
                EditorChanged(null, EventArgs.Empty);
        }

        public static event EventHandler StateChanged;

        private static bool _isExecuting;
        public static bool IsExecuting
        {
            get { return _isExecuting; }
            set
            {
                _isExecuting = value;
                OnStateChanged();
            }
        }

        private static int _progress;
        public static int Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnStateChanged();
            }
        }

        private static string _message;
        public static string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnStateChanged();
            }
        }

        protected static  void OnStateChanged()
        {
            if (StateChanged != null)
                StateChanged(null, EventArgs.Empty);
        }
    }
}
