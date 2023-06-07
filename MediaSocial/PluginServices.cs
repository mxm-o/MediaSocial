// Copyright © 2023 Maxim Otrokhov. All rights reserved.

using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using PluginInterface;

namespace MediaSocial
{

    // Кое где машинный перевод

    /// <summary>
    /// Общее описание PluginServices.
    /// </summary>
    /// Обратите внимание, как он наследует интерфейс IPluginHost!
    public class PluginServices : IPluginHost   //<--- Notice how it inherits IPluginHost interface!
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public PluginServices()
        {
        }

        /// <summary>
        /// Коллекция всех плагинов, найденных и загруженных методом FindPlugins()
        /// </summary>
        public Types.AvailablePlugins AvailablePlugins { get; set; } = new Types.AvailablePlugins();

        /// <summary>
        /// Ищет плагины в каталоге запуска приложения
        /// </summary>
        public void FindPlugins()
        {
            FindPlugins(AppDomain.CurrentDomain.BaseDirectory);
        }
        /// <summary>
        /// Поиск в Path плагинов
        /// </summary>
        /// <param name="Path">Каталог поиска плагинов</param>
        public void FindPlugins(string Path)
        {
            //Сначала очищаем коллекцию
            AvailablePlugins.Clear();
            // Проверяем существует ли указанный каталог
            if (Directory.Exists(Path))
            {
                //Перебираем все файлы в дирректории плагинов
                foreach (string fileOn in Directory.GetFiles(Path, "*.dll", SearchOption.AllDirectories))
                {
                    FileInfo file = new FileInfo(fileOn);

                    //Предварительно смотрим что это .dll
                    if (file.Extension.Equals(".dll"))
                    {
                        //Добавляем плагин в 'plugin'
                        this.AddPlugin(fileOn);
                    }
                }
            }
        }

        /// <summary>
        /// Выгрузка и закрытие всех плагинов
        /// </summary>
        public void ClosePlugins()
        {
            foreach (Types.AvailablePlugin pluginOn in AvailablePlugins)
            {
                //Закрываем все экземпляры плагинов
                //Вызываем Dispose, если нужно
                pluginOn.Instance.Dispose();

                //После того, как плагин все очистил, избавляемся от него
                pluginOn.Instance = null;
            }

            //В конце очищаем коллекцию плагинов
            AvailablePlugins.Clear();
        }

        private void AddPlugin(string FileName)
        {
            //Создаем новую сборку из файла плагина
            Assembly pluginAssembly = Assembly.LoadFrom(FileName);

            //Далее мы пройдемся по всем типам, найденным в сборке.
            foreach (Type pluginType in pluginAssembly.GetTypes())
            {
                if (pluginType.IsPublic) //Смотрим только public types
                {
                    if (!pluginType.IsAbstract)  //Смотрим только неабстрактные types
                    {
                        //Получаем тип интерфейса, какой нам нужен
                        Type typeInterface = pluginType.GetInterface("PluginInterface.IPlugin", true);

                        //Создаем планин если все в порядке по параметрам у интерфейса
                        if (typeInterface != null)
                        {
                            //Реализуем IPlugin interface
                            Types.AvailablePlugin newPlugin = new Types.AvailablePlugin();

                            //Установите имя файла, где мы его нашли
                            newPlugin.AssemblyPath = FileName;

                            // Создаем новый экземпляр и сохраняем экземпляр в коллекции для последующего использования
                             // Мы могли бы изменить это позже, чтобы не загружать экземпляр. У нас есть 2 варианта
                             //1- Создаем один экземпляр и используем его всякий раз, когда он нам нужен... он всегда там
                             //2- Не создавайте экземпляр, а вместо этого создавайте экземпляр всякий раз, когда мы его используем, а затем закройте его
                             // Сейчас мы просто создадим экземпляр всех плагинов
                            newPlugin.Instance = (IPlugin)Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString()));

                            //Set the Plugin's host to this class which inherited IPluginHost
                            newPlugin.Instance.Host = this;

                            //Вызов подпрограммы инициализации плагина
                            newPlugin.Instance.Initialize();

                            //Добавьте новый плагин в нашу коллекцию здесь
                            this.AvailablePlugins.Add(newPlugin);

                            //прибираемся
                            newPlugin = null;
                        }
                        typeInterface = null; //еще раз прибираемся		
                    }
                }
            }
            pluginAssembly = null; //вообщем вы поняли с уборкой
        }

        /// <summary>
        /// Отображение диалогового окна обратной связи от плагина
        /// </summary>
        /// <param name="Feedback">Строка сообщения от плагина</param>
        /// <param name="Plugin">Имя плагина, вызывающий обратную связь</param>
        public void Feedback(string Feedback, IPlugin Plugin)
        {
            //Создание новой формы фидбека и её заполнение информацией
            //Этот метод можно вызвать из плагина с свойствами Host Property

            System.Windows.Forms.Form newForm = null;
            FormFeedback newFeedbackForm = new FormFeedback();

            //Здесь мы устанавливаем свойства frmFeedback, которые я сделал пользовательскими.
            newFeedbackForm.PluginAuthor = "Автор: " + Plugin.Author;
            newFeedbackForm.PluginDesc = Plugin.Description;
            newFeedbackForm.PluginName = Plugin.Name;
            newFeedbackForm.PluginVersion = Plugin.Version;
            newFeedbackForm.Feedback = Feedback;

            //Мы также создали объект Form для хранения экземпляра frmFeedback
            //Если бы мы сначала объявили if not как объект frmFeedback,
            //У нас не было бы доступа к нужным нам свойствам на нем
            newForm = newFeedbackForm;
            newForm.ShowDialog();

            newFeedbackForm = null;
            newForm = null;
        }

        public Image[] SendImages()
        {
            Image[] img = new Image[Global.imagesRender.Count];
            for (var i = 0; i < Global.imagesRender.Count; i++)
            {
                if (Global.imagesRender[i].Exist)
                {
                    img[i] = Global.imagesRender[i].Pictures;
                }
                else
                {
                    img[i] = null;
                }
            }
            return img;
        }

        public void ReciveImage(Image img)
        {
            Global.ImageOut = img;
        }

        public int IndexImage()
        {
            return Global.ImageIndexNow;
        }
    }

    namespace Types
    {
        /// <summary>
        /// Коллекция для AvailablePlugin Type
        /// </summary>
        public class AvailablePlugins : System.Collections.CollectionBase
        {
            //Простой класс Homebrew для хранения некоторой информации о наших доступных плагинах.

            /// <summary>
            /// Добавить плагин в коллекцию доступных плагинов
            /// </summary>
            /// <param name="pluginToAdd">Плагин для добавления</param>
            public void Add(Types.AvailablePlugin pluginToAdd)
            {
                this.List.Add(pluginToAdd);
            }

            /// <summary>
            /// Удалить плагин из коллекции доступных плагинов
            /// </summary>
            /// <param name="pluginToRemove">Плагин для удаления</param>
            public void Remove(Types.AvailablePlugin pluginToRemove)
            {
                this.List.Remove(pluginToRemove);
            }

            /// <summary>
            /// Находит плагин в доступных плагинах
            /// </summary>
            /// <param name="pluginNameOrPath">Имя или путь к файлу плагина, который нужно найти</param>
            /// <returns>Доступный плагин или ноль, если плагин не найден</returns>
            public Types.AvailablePlugin Find(string pluginNameOrPath)
            {
                Types.AvailablePlugin toReturn = null;

                //Перебираем все плагины
                foreach (Types.AvailablePlugin pluginOn in this.List)
                {
                    //Поиск файла с совпадающим названием или именем файла
                    if (pluginOn.Instance.Name.Equals(pluginNameOrPath) || pluginOn.AssemblyPath.Equals(pluginNameOrPath))
                    {
                        toReturn = pluginOn;
                        break;
                    }
                }
                return toReturn;
            }
        }

        /// <summary>
        /// Класс данных для доступного плагина. Содержит и экземпляр загруженного плагина, а также путь сборки плагина
        /// </summary>
        public class AvailablePlugin
        {
            //Это актульный объект AvailablePlugin 
            //Содержит экземпляр плагина для доступа
            //Также содержит путь сборки... на самом деле не нужно
            private IPlugin myInstance = null;
            private string myAssemblyPath = "";

            public IPlugin Instance
            {
                get { return myInstance; }
                set { myInstance = value; }
            }
            public string AssemblyPath
            {
                get { return myAssemblyPath; }
                set { myAssemblyPath = value; }
            }
        }
    }
}
