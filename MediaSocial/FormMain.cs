// Copyright © 2025 Maxim Otrokhov. All rights reserved.

using MediaSocial.Classes;
using PhotoEdit;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace MediaSocial
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            pictureBox.AllowDrop = true;
        }

        // Создаем таймер обработки картинки
        private System.Windows.Forms.Timer delayTimer = new System.Windows.Forms.Timer();

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Lets call the close for all the plugins before we truly exit!
            Global.Plugins.ClosePlugins();
        }

        // Загрузка плагинов в память
        private void LoadPlugins()
        {
            //Call the find plugins routine, to search in our Plugins Folder
            Global.Plugins.FindPlugins(Application.StartupPath + @"\Plugins");

            //Add each plugin to the treeview
            foreach (Types.AvailablePlugin pluginOn in Global.Plugins.AvailablePlugins)
            {
                toolStripCmbPlugin.Items.Add(pluginOn.Instance.Name);
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Проверяем обновление версии для обновления и переноса настроек
            if (Properties.Settings.Default.CallUpgrade)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.CallUpgrade = false;
                Properties.Settings.Default.Save();
            }
            // Загружаем иконку
            IntPtr Hicon = Properties.Resources.icon.GetHicon();
            this.Icon = Icon.FromHandle(Hicon);
            // Загружаем настройки
            LoadSetting();

            // Загружаем плагины
            LoadPlugins();
            // Подписываемся на изменение главной картинки
            Global.ImageOutChanged += Global_ImageOutChanged;
            // Подписываем на событие изменения размера изображения
            Global.EditorChanged += Global_EditorChanged;
            // Подписываемся на событие изменения состояния плагина
            Global.StateChanged += Global_StateChanged;
            // Выбираем первый плагин если он есть
            if (toolStripCmbPlugin.Items.Count > 0)
            {
                toolStripCmbPlugin.SelectedIndex = 0;
            }
            // Проверяем кнопку сохранения
            buttonImgSaveQuick.Enabled = textBoxSaveNameFile.Text == "" ? false : true;
            // Устанавливаем таймер срабатыванися обработки картинки
            delayTimer.Interval = 500;
            delayTimer.Tick += new EventHandler(delayTimer_Tick);
        }



        // Загрузка настроек
        private void LoadSetting()
        {
            labelSavePath.Text = Properties.Settings.Default.pathSave;
            cmbBoxSaveFormat.SelectedItem = Properties.Settings.Default.typeSave;
            cmbBoxSaveQuality.SelectedIndex = Properties.Settings.Default.qualitySave;
            radioButtonSouser.Checked = Properties.Settings.Default.inputSave == 0;
            radioButtonRender.Checked = Properties.Settings.Default.inputSave == 1;
            radioButtonDone.Checked = Properties.Settings.Default.inputSave == 2;
        }

        // Сохранение настроек
        private void SaveSetting()
        {
            Properties.Settings.Default.pathSave = labelSavePath.Text;
            Properties.Settings.Default.typeSave = cmbBoxSaveFormat.SelectedItem.ToString();
            Properties.Settings.Default.qualitySave = cmbBoxSaveQuality.SelectedIndex;
            Properties.Settings.Default.inputSave = radioButtonDone.Checked ? 2 : (radioButtonRender.Checked ? 1 : 0);
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        // Настройки. Каталог сохранения
        private void buttonSavePath_Click(object sender, EventArgs e)
        {
            // Выбираем папку сохранения
            FolderBrowserDialog ofd = new FolderBrowserDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            ofd.Description = "Укажите папку сохранения результата";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string folder = ofd.SelectedPath;
                try
                {
                    labelSavePath.Text = folder;
                    Properties.Settings.Default.pathSave = folder;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Reload();
                }
                catch
                {
                    toolStripStatusLabel1.Text = "Каталог не доступен!";
                }
            }
        }

        // Настройки. Выбор Формата
        private void cmbBoxSaveFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.typeSave = cmbBoxSaveFormat.SelectedItem.ToString();
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }
        // Настройки. Выбор качества
        private void cmbBoxSaveQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.qualitySave = cmbBoxSaveQuality.SelectedIndex;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        // Настройки. Выбор что сохраняем
        private void radioButtonSouser_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.inputSave = radioButtonDone.Checked ? 2 : (radioButtonRender.Checked ? 1 : 0);
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        // Действия при обновлении итогового изображения
        private void Global_ImageOutChanged(object sender, EventArgs e)
        {
            // Дополнительная проверка на случай вызова из другого потока
            if (cmbBoxImgType.InvokeRequired)
            {
                cmbBoxImgType.BeginInvoke(new Action(() =>
                {
                    cmbBoxImgType.SelectedIndex = 2;
                }));
            }
            else
            {
                cmbBoxImgType.SelectedIndex = 2;
            }

            showPicturebox();
        }



        // Активация плагина
        private void cmbPlugins_SelectedValueChanged(object sender, EventArgs e)
        {
            //Make sure there's a selected Plugin
            if (toolStripCmbPlugin.Text != "")
            {
                //Get the selected Plugin
                Types.AvailablePlugin selectedPlugin = Global.Plugins.AvailablePlugins.Find(toolStripCmbPlugin.Text);

                if (selectedPlugin != null)
                {
                    //Clear the current panel of any other plugin controls... 
                    //Note: this only affects visuals.. doesn't close the instance of the plugin
                    this.pnlPlugin.Controls.Clear();

                    //Set the dockstyle of the plugin to fill, to fill up the space provided
                    selectedPlugin.Instance.MainInterface.Dock = DockStyle.Fill;

                    //Finally, add the usercontrol to the panel... Tadah!
                    this.pnlPlugin.Controls.Add(selectedPlugin.Instance.MainInterface);

                    // Заполняем Комбобокс со списком картинок
                    comboBoxImg.Items.Clear();
                    Global.imagesSouser.Clear();
                    Global.imagesRender.Clear();
                    Global.imagesSettings.Clear();
                    Global.ImageOut = Properties.Resources.PhotoNotExist;

                    panelEditor.Enabled = selectedPlugin.Instance.Editor;

                    // Заполняем Комбобокс с картинками
                    for (int i = 1; i <= selectedPlugin.Instance.SizesList.Count; i++)
                    {
                        ComboboxItem item = new ComboboxItem();
                        item.Text = selectedPlugin.Instance.SizesList[i - 1].Name.ToString();
                        item.Value = i;

                        comboBoxImg.Items.Add(item);
                        Global.imagesSouser.Add(new Images { Id = i, Pictures = Properties.Resources.PhotoNotExist, Exist = false });
                        Global.imagesRender.Add(new Images { Id = i, Pictures = Properties.Resources.PhotoNotExist, Exist = false });
                    }

                    if (selectedPlugin.Instance.SizesList.Count == 0)
                    {
                        // Если нет изображений сохраняем только готовое изображение
                        radioButtonDone.Checked = true;
                        Properties.Settings.Default.inputSave = 2;
                        radioButtonRender.Enabled = false;
                        radioButtonSouser.Enabled = false;
                        panelImg.Enabled = false;
                        panelEditor.Enabled = false;
                    }
                    else
                    {
                        radioButtonRender.Enabled = true;
                        radioButtonSouser.Enabled = true;
                        panelImg.Enabled = true;
                    }

                    if (selectedPlugin.Instance.SizesList.Count > 0)
                    {
                        comboBoxImg.SelectedIndex = 0;
                        cmbBoxImgType.SelectedIndex = 0;
                    }

                    comboBoxImg.Enabled = selectedPlugin.Instance.SizesList.Count > 1;
                    cmbBoxImgType.Enabled = selectedPlugin.Instance.SizesList.Count > 0;
                    OpenToolStripMenuItem.Enabled = selectedPlugin.Instance.SizesList.Count > 0;
                    toolStripButtonOpen.Enabled = selectedPlugin.Instance.SizesList.Count > 0;
                }
            }
        }
        // Открытие по кнопке
        private void buttonImgOpen_Click(object sender, EventArgs e)
        {
            if (comboBoxImg.Items.Count > 0)
            {
                openImage();
            }
            else
            {
                MessageBox.Show("Не выбран модуль!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Открытие перетаскиванием
        private void pictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && ((e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)) e.Effect = DragDropEffects.Move;
        }

        private void pictureBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && e.Effect == DragDropEffects.Move)
            {
                toolStripStatusLabel1.Text = "Открытие изображения...";
                string[] objects = (string[])e.Data.GetData(DataFormats.FileDrop); // В objects хранятся пути к папкам и файлам
                Image image = openImage(@objects[0]); // Берем первый файл
                if (image != null) insertImage(image);
            }
        }

        private void openImage()
        {
            Image image = null;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Images files |*.jpeg;*.jpg;*.png;*.bmp;*.gif;*.tiff;*.webp|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    toolStripStatusLabel1.Text = "Открытие изображения";
                    Application.DoEvents();
                    //Get the path of specified file
                    image = openImage(openFileDialog.FileName);
                    Application.DoEvents();
                }
            }

            if (image != null) insertImage(image);
        }

        // Буфер обмена вставка (включая ctrl + v)
        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Если в буфере изображение
            if (Clipboard.ContainsImage())
            {
                Image image = Clipboard.GetImage();
                insertImage(image);
            }
            // Если текст
            else if (Clipboard.ContainsText())
            {
                // Получаем текст из буфера обмена
                string text = Clipboard.GetText();

                Control container = ActiveControl;
                // Ищем текст бокс верхний по слоям
                TextBox activeTextBox = FindActiveTextBox(ActiveControl);
                if (activeTextBox != null)
                {
                    // Вставляем текст
                    activeTextBox.Paste(text);
                }

            }
        }

        // Поиск активного контрола (textbox)
        private TextBox FindActiveTextBox(Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl is TextBox textBox && textBox.Focused)
                {
                    return textBox;
                }
                else if (childControl.HasChildren)
                {
                    TextBox textBoxInChildren = FindActiveTextBox(childControl);
                    if (textBoxInChildren != null)
                    {
                        return textBoxInChildren;
                    }
                }
            }
            return null;
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image image = null;
            try
            {
                if (cmbBoxImgType.SelectedIndex == 0 && Global.imagesSouser[comboBoxImg.SelectedIndex].Exist) image = Global.imagesSouser[comboBoxImg.SelectedIndex].Pictures;
                if (cmbBoxImgType.SelectedIndex == 1 && Global.imagesRender[comboBoxImg.SelectedIndex].Exist) image = Global.imagesRender[comboBoxImg.SelectedIndex].Pictures;
                if (cmbBoxImgType.SelectedIndex == 2) image = Global.ImageOut;
            }
            catch { }
            if (image != null) Clipboard.SetImage(image);
        }



        private Image openImage(string pathImage)
        {
            Image image = null;
            try
            {
                image = Image.FromStream(new MemoryStream(File.ReadAllBytes(pathImage)));

                // Поворачиваем изображение
                var rotationImage = new RotationImage();
                try
                {
                    image = rotationImage.RotateImage(image);
                    toolStripStatusLabel1.Text = "";
                }
                catch (Exception ex)
                {
                    toolStripStatusLabel1.Text = "Ошибка открытия изображения. " + ex.Message;
                }
                Application.DoEvents();
            }
            catch
            {

                toolStripStatusLabel1.Text = "Ошибка файла!";

            }

            // Конвертируем изображение библиотекой FFMPEG если со стандарным способом что-то не так
            if (image == null)
            {
                toolStripStatusLabel1.Text = "Открытие изображения...";
                try
                {
                    ConverFormat converFormat = new ConverFormat();
                    Image imageFfmpeg = converFormat.readImage(pathImage);
                    if (imageFfmpeg != null)
                    {
                        image = imageFfmpeg;
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = "Изображение повреждено или формат не поддерживается.";
                    }
                }
                catch
                {
                    toolStripStatusLabel1.Text = "Ошибка открытия изображения.";
                }
            }
            return image;
        }


        // Добавление изображения в глобальные списки изображения + в pictureBox'ы
        private void insertImage(Image image)
        {
            if (image != null && comboBoxImg.Items.Count > 0)
            {
                Global.imagesSouser[comboBoxImg.SelectedIndex].Pictures = image;
                Global.imagesSouser[comboBoxImg.SelectedIndex].Exist = true;

                Global.imagesRender[Global.ImageIndexNow].Pictures = renderEditor(Global.imagesSouser[Global.ImageIndexNow].Pictures, Global.ImageIndexNow);
                Global.imagesRender[Global.ImageIndexNow].Exist = true;

                // Обнуляем параметры редактора
                ResetEditorSetting();

                // Переводим отображение picturebox на исходное изображение (если нужно)
                if (cmbBoxImgType.SelectedIndex != 0)
                {
                    cmbBoxImgType.SelectedIndex = 0;
                } else {
                    // Обновляем picturebox
                    showPicturebox(); 
                }
                
            } else
            {
                System.Media.SystemSounds.Exclamation.Play();
            }
        }

        private void clearImage()
        {
            if (comboBoxImg.Items.Count > 0)
            {
                Global.imagesSouser[comboBoxImg.SelectedIndex].Pictures = Properties.Resources.PhotoNotExist;
                Global.imagesSouser[comboBoxImg.SelectedIndex].Exist = false;

                Global.imagesRender[comboBoxImg.SelectedIndex].Pictures = Properties.Resources.PhotoNotExist;
                Global.imagesRender[comboBoxImg.SelectedIndex].Exist = false;

                Global.ImageOut = Properties.Resources.PhotoNotExist;

                pictureBox.Image = Properties.Resources.PhotoNotExist;

                Global.imagesSettings.Clear();
            }
        }

        private void Quit()
        {
            this.Close();
        }

        private void buttonImgClear_Click(object sender, EventArgs e)
        {
            clearImage();
        }

        private void ImgSouserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmbBoxImgType.SelectedIndex = 0;
        }

        private void ImgRenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmbBoxImgType.SelectedIndex = 1;
        }

        private void ImgDoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmbBoxImgType.SelectedIndex = 2;
        }

        private void tabControlImg_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (tabControlImg.SelectedIndex == 1) { tabControlPlugins.SelectedTab = tabPluginEditor; }
        }

        private void toolStripButtonEditor_Click(object sender, EventArgs e)
        {
            tabControlPlugins.SelectedTab = tabPluginEditor;
            cmbBoxImgType.SelectedValue = 1;
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            tabControlPlugins.SelectedTab = tabPluginSave;
        }

        private void textBoxSaveNameFile_TextChanged(object sender, EventArgs e)
        {
            buttonImgSaveQuick.Enabled = textBoxSaveNameFile.Text == "" ? false : true;
        }

        private void textBoxSaveNameFile_Leave(object sender, EventArgs e)
        {
            string fileName = textBoxSaveNameFile.Text;
            string invalidCharsPattern = $"[{Regex.Escape(new string(Path.GetInvalidFileNameChars()))}]"; // escape invalid chars
            string safeFileName = Regex.Replace(fileName, invalidCharsPattern, "");
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(safeFileName);
            string validFileName = $"{fileNameWithoutExtension}{Path.GetExtension(safeFileName)}";
            textBoxSaveNameFile.Text = validFileName;
        }

        private void comboBoxImg_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.ImageIndexNow = comboBoxImg.SelectedIndex;
            showPicturebox();
            LoadEditorSetting(Global.ImageIndexNow);
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quit();
        }

        private void btnEditorOk_Click(object sender, EventArgs e)
        {
            EditorOk();
        }
        // Обработка изображения по изменению формата из плагина
        private void Global_EditorChanged(object sender, EventArgs e)
        {
            if (Global.renderEditor)
            {
                EditorOk();
                Global.renderEditor = false;
            }
        }

        // Обработка состояния плагина
        private void Global_StateChanged(object sender, EventArgs e)
        {
            if (Global.IsExecuting)
            {
                panelImg.Enabled = false;
                toolStripStatusLabel1.Text = $"Выполняется операция... {Global.Progress}%";
            }
            else
            {
                panelImg.Enabled = radioButtonRender.Enabled;
                toolStripStatusLabel1.Text =  "Готово";
            }
        }

        // Обработка картинки по таймеру
        private void delayTimer_Tick(object sender, EventArgs e)
        {
            // Останавливаем таймер
            delayTimer.Stop();

            // Выполняем необходимые действия
            EditorOk();
        }

        private void EditorOk()
        {
            try
            {
                if (Global.imagesRender[Global.ImageIndexNow].Exist)
                {
                    if (!checkBoxAuto.Checked) btnEditorOk.Enabled = false;
                    Global.imagesRender[Global.ImageIndexNow].Pictures = renderEditor(Global.imagesSouser[Global.ImageIndexNow].Pictures, Global.ImageIndexNow);
                    pictureBox.Image = Global.imagesRender[Global.ImageIndexNow].Pictures;
                    if (!checkBoxAuto.Checked) btnEditorOk.Enabled = true;
                }
            } catch
            {
                pictureBox.Image = Properties.Resources.PhotoNotExist;
            }
        }

        private Image renderEditor(Image image, int Id)
        {
            if (image == null) { return Properties.Resources.PhotoNotExist; }
            toolStripStatusLabel1.Text = "Идет обработка...";
            Application.DoEvents();

            SettingImage item = new SettingImage();
            item.Id = Id;
            item.Brightness = (trackBarBrightness.Value + 100) / 100f;
            item.Contrast = (trackBarContrast.Value + 100) / 100f;
            item.Saturation = (trackBarSaturation.Value / 100f) * 2 + 1;
            item.Tone = trackBarTone.Value;
            item.Temperature = trackBarTemperature.Value / 100f;
            item.Rotate = Convert.ToInt16(numericUpDownRotate.Value < 0 ? (360 + numericUpDownRotate.Value) : numericUpDownRotate.Value);
            item.Zoom = trackBarZoom.Value;
            item.Horizontal = trackBarHorisontal.Value;
            item.Vertical = trackBarVertical.Value;
            item.canvasHeight = Global.Plugins.AvailablePlugins.Find(toolStripCmbPlugin.Text).Instance.SizesList[Id].SizeX;
            item.canvasWidth = Global.Plugins.AvailablePlugins.Find(toolStripCmbPlugin.Text).Instance.SizesList[Id].SizeY;
            // Поиск максимальнного значения Step для указанного id
            var maxStep = 0;
            var filteredList = Global.imagesSettings.Where(img => img.Id == Id);
            if (filteredList.Any())
            {
                maxStep = filteredList.Max(img => img.Step);
            }

            item.Step = maxStep + 1;

            // Добавляем в историю параметры изображения
            Global.imagesSettings.Add(item);

            // Вращение с обрезкой черных областей
            if (item.Rotate != 0)
            {
                toolStripStatusLabel1.Text = "Поворачиваю изображение...";
                Application.DoEvents();
                RotateImage rotateImage = new RotateImage
                {
                    image = image,
                    angle = item.Rotate
                };
                image = rotateImage.Rotate();
            }

            // Масштабирование и позиционирование
            toolStripStatusLabel1.Text = "Меняю размеры изображения...";
            Application.DoEvents();
            PhotoSize photoSize = new PhotoSize
            {
                
                source = image,
                width = item.canvasWidth,
                height = item.canvasHeight,
                zoom = item.Zoom,
                horizontal = item.Horizontal,
                vertical = item.Vertical
            };

            image = photoSize.ScaleImage();

            // Изменяем цвета, яркость и т.п.
            toolStripStatusLabel1.Text = "Меняю цвета изображения...";
            Application.DoEvents();
            ModifyImage mi = new ModifyImage();
            mi.brightness = item.Brightness;
            mi.contrast = item.Contrast;
            mi.hue = item.Tone;
            mi.saturation = item.Saturation;
            mi.temperature = item.Temperature;


            toolStripStatusLabel1.Text = "Меняю яркость изображения...";
            Application.DoEvents();
            mi.image = image;
            image = mi.BrightnessImage();

            toolStripStatusLabel1.Text = "Меняю констрастность изображения...";
            Application.DoEvents();
            mi.image = image;
            image = mi.ContrastImage();

            toolStripStatusLabel1.Text = "Меняю температуру изображения...";
            Application.DoEvents();
            mi.image = image;
            image = mi.ChangeTemperature();

            toolStripStatusLabel1.Text = "Меняю насыщенность изображения...";
            Application.DoEvents();
            mi.image = image;
            image = mi.ChangeSaturation();

            toolStripStatusLabel1.Text = "Меняю оттенок изображения...";
            Application.DoEvents();
            mi.image = image;
            image = mi.ChangeHue();

            // Закончили изменять цвета
            GC.Collect();

            toolStripStatusLabel1.Text = "";
            Application.DoEvents();
            return image;
        }

        // Сброс параметров редактора

        private void btnEditorReset_Click(object sender, EventArgs e)
        {
            ResetEditorSetting();

            if (checkBoxAuto.Checked) EditorOk();
        }

        private void ResetEditorSetting()
        {
            trackBarVertical.Value = 0;
            trackBarHorisontal.Value = 0;
            trackBarZoom.Value = 0;

            trackBarBrightness.Value = 0;
            trackBarContrast.Value = 0;
            trackBarSaturation.Value = 0;
            trackBarTemperature.Value = 0;
            trackBarTone.Value = 0;

            numericUpDownRotate.Value = 0;
        }

        // Загрузка параметров редактора

        private void LoadEditorSetting(int id)
        {
            // Фильтруем последний шаг для картинки с нужным id
            var filteredImages = Global.imagesSettings.Where(img => img.Id == id)
                                    .OrderByDescending(img => img.Step);

            if (filteredImages.Any())
            {
                trackBarVertical.Value = Convert.ToInt16(filteredImages.FirstOrDefault(img => img.Step == filteredImages.First().Step)?.Vertical);
                trackBarHorisontal.Value = Convert.ToInt16(filteredImages.FirstOrDefault(img => img.Step == filteredImages.First().Step)?.Horizontal);
                trackBarZoom.Value = Convert.ToInt16(filteredImages.FirstOrDefault(img => img.Step == filteredImages.First().Step)?.Zoom);

                trackBarBrightness.Value = Convert.ToInt16(filteredImages.FirstOrDefault(img => img.Step == filteredImages.First().Step)?.Brightness * 100 - 100);
                trackBarContrast.Value = Convert.ToInt16(filteredImages.FirstOrDefault(img => img.Step == filteredImages.First().Step)?.Contrast * 100 - 100);
                trackBarSaturation.Value = Convert.ToInt16(filteredImages.FirstOrDefault(img => img.Step == filteredImages.First().Step)?.Saturation * 10);
                trackBarTemperature.Value = Convert.ToInt16(filteredImages.FirstOrDefault(img => img.Step == filteredImages.First().Step)?.Temperature * 100);
                trackBarTone.Value = Convert.ToInt16(filteredImages.FirstOrDefault(img => img.Step == filteredImages.First().Step)?.Tone);

                numericUpDownRotate.Value = Convert.ToInt16(filteredImages.FirstOrDefault(img => img.Step == filteredImages.First().Step)?.Rotate);
            }
            else
            {
                ResetEditorSetting();
            }

        }

        // Вращение изображение
        private void buttonRotate90_Click(object sender, EventArgs e)
        {
            numericUpDownRotate.Value = calcRotateBox(numericUpDownRotate.Value, 90);
        }
        private void buttonRotate180_Click(object sender, EventArgs e)
        {
            numericUpDownRotate.Value = calcRotateBox(numericUpDownRotate.Value, 180);
        }
        private void buttonRotate270_Click(object sender, EventArgs e)
        {
            numericUpDownRotate.Value = calcRotateBox(numericUpDownRotate.Value, -90);
        }
        /// <summary>
        /// Расчет вращения размера вращения
        /// </summary>
        /// <param name="value">прежнее значение</param>
        /// <param name="valueNew">значение на какое увеличивается</param>
        /// <returns></returns>
        private decimal calcRotateBox(decimal value, decimal valueNew)
        {
            value += valueNew;
            if (value >= 360) value -= 360;
            if (value < 0) value += 360;
            return value;
        }

        private void btnAutoBr_Click(object sender, EventArgs e)
        {
            ModifyImage mi = new ModifyImage();
            var br = mi.CalcBrightness(Global.imagesSouser[Global.ImageIndexNow].Pictures);
            if (br > 2) br = 2;
            trackBarBrightness.Value = Convert.ToInt16(br * 100 - 100);
            EditorOk();
        }
        // Окончание вращения изображения
        private void buttonImgSaveClear_Click(object sender, EventArgs e)
        {
            textBoxSaveNameFile.Text = "";
        }

        //Сохранение файла
        private void saveFile(string filename, bool quick=true)
        {
            SaveImageFile saveImageFile = new SaveImageFile();
            saveImageFile.image = null;
            if (Properties.Settings.Default.inputSave == 2) saveImageFile.image = Global.ImageOut;
            if (Properties.Settings.Default.inputSave == 1) saveImageFile.image = Global.imagesRender[Global.ImageIndexNow].Pictures;
            if (Properties.Settings.Default.inputSave == 0) saveImageFile.image = Global.imagesSouser[Global.ImageIndexNow].Pictures;
            saveImageFile.saveFormat = Properties.Settings.Default.typeSave;
            saveImageFile.quality = (int)(Properties.Settings.Default.qualitySave / 4.0 * 100);

            string path = filename;
            bool writeFile = true;

            if (quick) { 
                path = Path.Combine(Properties.Settings.Default.pathSave, filename + "." + Properties.Settings.Default.typeSave);

                if (File.Exists(path) && MessageBox.Show("Файл с указанным именем уже существует. Заменить?", "Замена файла!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    writeFile = false;
                }
            }

            if (writeFile)
            {
                try
                {
                    saveImageFile.savePath = path;
                    saveImageFile.SaveImage();
                }
                catch
                {
                    toolStripStatusLabel1.Text = "Ошибка сохранения файла";
                }
            }
        }

        private void buttonImgSaveQuick_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Сохранение файла";
            Thread thread = new Thread(() => saveFile(textBoxSaveNameFile.Text, true));
            thread.Start();
            thread.Join();
            toolStripStatusLabel1.Text = "Готово";
        }

        private void buttonImgSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Image file (*." + Properties.Settings.Default.typeSave + ")|*." + Properties.Settings.Default.typeSave; //фильтр для расширения файла
                saveFileDialog.InitialDirectory = Properties.Settings.Default.pathSave; //начальная директория
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = "Выберите файл для сохранения";
                saveFileDialog.FileName = textBoxSaveNameFile.Text;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    toolStripStatusLabel1.Text = "Сохранение файла";
                    Thread thread = new Thread(() => saveFile(saveFileDialog.FileName, false));
                    thread.Start();
                    thread.Join();
                    toolStripStatusLabel1.Text = "Готово";
                }
            }
        }
        // Изменение положения изображения мышью
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (tabControlPlugins.SelectedTab == tabPluginEditor && e.Button == MouseButtons.Left)
            {
                /*
                int x = (e.Location.X - (pictureBox.Width / 2)) / 2;
                int y = (e.Location.Y - (pictureBox.Height / 2)) / 2;
                x = x > trackBarHorisontal.Maximum ? trackBarHorisontal.Maximum : x;
                x = x < trackBarHorisontal.Minimum ? trackBarHorisontal.Minimum : x;
                y = y > trackBarVertical.Maximum ? trackBarVertical.Maximum : y;
                y = y < trackBarVertical.Minimum ? trackBarVertical.Minimum : y;

                trackBarHorisontal.Value = x;
                trackBarVertical.Value = y;
                */
                int maxMove = 50; // Максимальное смещение в %
                int x = (e.X - pictureBox.Width / 2) * 100 / (pictureBox.Width / 2);
                int y = (e.Y - pictureBox.Height / 2) * 100 / (pictureBox.Height / 2);

                trackBarHorisontal.Value =
                    Math.Max(-maxMove, Math.Min(maxMove, x));

                trackBarVertical.Value =
                    Math.Max(-maxMove, Math.Min(maxMove, y));
            }
        }
        // Изменение размера изображения мышью
        private void pictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (tabControlPlugins.SelectedTab == tabPluginEditor)
            {
                int delta = e.Delta > 0 ? 5 : -5;
                int newValue = trackBarZoom.Value + delta;

                newValue = Math.Max(trackBarZoom.Minimum,
                          Math.Min(trackBarZoom.Maximum, newValue));

                trackBarZoom.Value = newValue;
                /*
                int delta = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
                if (delta > 0 && trackBarZoom.Value < 100)
                {
                    trackBarZoom.Value++;
                }
                else if (delta < 0 && trackBarZoom.Value > -100)
                {
                    trackBarZoom.Value--;
                }
                */
            }
        }

        // Фокус на PictureBox при попадании на него курсора мыши (когда открыта страница редактора фото)
        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            if (tabControlPlugins.SelectedTab == tabPluginEditor) pictureBox.Focus();
        }

        // Убираем фокус с PictureBox при убирании с него курсора мыши (когда открыта страница редактора фото)

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            if (tabControlPlugins.SelectedTab == tabPluginEditor) this.Focus();
        }

        // Меняем картинку отображаемую в программе
        private void cmbBoxImgType_SelectedIndexChanged(object sender, EventArgs e)
        {
            showPicturebox();
        }

        private void showPicturebox()
        {
            if (pictureBox.InvokeRequired)
            {
                pictureBox.Invoke(new MethodInvoker(delegate {
                    showPicturebox();
                }));
                return;
            }

            if (cmbBoxImgType.InvokeRequired)
            {
                cmbBoxImgType.Invoke(new MethodInvoker(delegate {
                    showPicturebox();
                }));
                return;
            }

            pictureBox.Image = Properties.Resources.PhotoNotExist;
            try
            {
                switch (cmbBoxImgType.SelectedIndex)
                {
                    case 0:
                        if (Global.imagesSouser[comboBoxImg.SelectedIndex].Exist)
                        {
                            pictureBox.Image = Global.imagesSouser[comboBoxImg.SelectedIndex].Pictures;
                        }
                        break;
                    case 1:
                        if (Global.imagesRender[comboBoxImg.SelectedIndex].Exist)
                        {
                            pictureBox.Image = Global.imagesRender[comboBoxImg.SelectedIndex].Pictures;
                        }
                        break;
                    case 2:
                        pictureBox.Image = Global.ImageOut;
                        break;
                    default:
                        pictureBox.Image = Properties.Resources.PhotoNotExist;
                        break;
                }
            }
            catch
            {

            }
        }

        private void PictAutoRender()
        {
            if (checkBoxAuto.Checked)
            {
                delayTimer.Stop(); // Останавливаем таймер, если он уже запущен
                delayTimer.Start(); // Запускаем таймер
            }
        }

        private void trackBarVertical_ValueChanged(object sender, EventArgs e)
        {
            PictAutoRender();
        }

        private void checkBoxAuto_CheckedChanged(object sender, EventArgs e)
        {
            btnEditorOk.Enabled = !checkBoxAuto.Checked;
            EditorOk();
        }

        private void tabControlPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlPlugins.SelectedIndex == 1) {
                cmbBoxImgType.SelectedIndex = 1;
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox1 = new AboutBox1();
            aboutBox1.ShowDialog();
        }

        private void PhotoEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlPlugins.SelectedTab = tabPluginEditor;
            cmbBoxImgType.SelectedValue = 1;
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlPlugins.SelectedTab = tabPluginSave;
        }

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            clearImage();
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearImage();
        }

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearImage();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBoxImg.Items.Count > 0)
            {
                openImage();
            }
            else
            {
                MessageBox.Show("Не выбран модуль!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            if (comboBoxImg.Items.Count > 0)
            {
                openImage();
            }
        }
    }
}
