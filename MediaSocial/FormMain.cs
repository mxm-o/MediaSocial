using MediaSocial.Classes;
using PhotoEdit;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MediaSocial
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

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
                //TreeNode newNode = new TreeNode(pluginOn.Instance.Name);
                //this.cmbPlugins.Items.Add(newNode);
                //this.cmbPlugins.Items.Add(pluginOn.Instance.Name);
                toolStripCmbPlugin.Items.Add(pluginOn.Instance.Name);
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Загружаем настройки

            LoadSetting();

            // Загружаем плагины
            LoadPlugins();
            // Подписываемся на изменение главной картинки
            Global.ImageOutChanged += Global_ImageOutChanged;
            // Выбираем первый плагин если он есть
            if (toolStripCmbPlugin.Items.Count > 0)
            {
                toolStripCmbPlugin.SelectedIndex = 0;
            }
            // Проверяем кнопку сохранения
            buttonImgSaveQuick.Enabled = textBoxSaveNameFile.Text == "" ? false : true;
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

        private void Global_ImageOutChanged(object sender, EventArgs e)
        {
            pictureBoxDone.Image = Global.ImageOut;
            tabControlImg.SelectedTab = tabPageDone;
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
                    //Again, if the plugin is found, do some work...


                    //This part adds the plugin's info to the 'Plugin Information:' Frame
                    //this.lblPluginName.Text = selectedPlugin.Instance.Name;
                    //this.lblPluginVersion.Text = "(" + selectedPlugin.Instance.Version + ")";
                    //this.lblPluginAuthor.Text = "By: " + selectedPlugin.Instance.Author;
                    //this.lblPluginDesc.Text = selectedPlugin.Instance.Description;

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

                    if (selectedPlugin.Instance.SizesList.Count == 0) { 
                        // Если нет изображений сохраняем только готовое изображение
                        radioButtonDone.Checked = true;
                        Properties.Settings.Default.inputSave = 2;
                        radioButtonRender.Enabled= false;
                        radioButtonSouser.Enabled= false;
                        panel1.Enabled = false;
                        tabControlImg.SelectedTab = tabPageDone;
                        panelEditor.Enabled = false;
                    } else
                    {
                        radioButtonRender.Enabled = true;
                        radioButtonSouser.Enabled = true;
                        panel1.Enabled = true;
                    }

                    if (selectedPlugin.Instance.SizesList.Count > 0)  comboBoxImg.SelectedIndex = 0;
                }

            }
        }

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

        private void openImage()
        {
            System.Drawing.Image fileImage = null;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.jpg)|*.jpg|(*.png)|*.png|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 3;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    toolStripStatusLabel1.Text = "Открытие изображения";
                    Application.DoEvents();
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    // Поворачиваем изображение

                    var rotationImage = new RotationImage();

                    fileImage = rotationImage.RotateImage(System.Drawing.Image.FromStream(fileStream));
                    toolStripStatusLabel1.Text = "";
                    Application.DoEvents();
                }
            }

            if (filePath != string.Empty)
            {

                Global.imagesSouser[comboBoxImg.SelectedIndex].Pictures = fileImage;
                Global.imagesSouser[comboBoxImg.SelectedIndex].Exist = true;
                pictureBoxSouser.Image = Global.imagesSouser[comboBoxImg.SelectedIndex].Pictures;

                Global.imagesRender[Global.ImageIndexNow].Pictures = renderEditor(Global.imagesSouser[Global.ImageIndexNow].Pictures, Global.ImageIndexNow);
                Global.imagesRender[Global.ImageIndexNow].Exist = true;
                pictureBoxRender.Image = Global.imagesRender[Global.ImageIndexNow].Pictures;
            }
        }

        private void clearImage()
        {
            if (comboBoxImg.Items.Count > 0)
            {
                Global.imagesSouser[comboBoxImg.SelectedIndex].Pictures = global::MediaSocial.Properties.Resources.PhotoNotExist;
                pictureBoxSouser.Image = global::MediaSocial.Properties.Resources.PhotoNotExist;

                Global.imagesRender[comboBoxImg.SelectedIndex].Pictures = global::MediaSocial.Properties.Resources.PhotoNotExist;
                pictureBoxRender.Image = global::MediaSocial.Properties.Resources.PhotoNotExist;

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
            tabControlImg.SelectedTab = tabPageSouser;
        }

        private void ImgRenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlImg.SelectedTab = tabPageRender;
        }

        private void ImgDoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlImg.SelectedTab = tabPageDone;
        }

        private void tabControlImg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlImg.SelectedIndex == 1) { tabControlPlugins.SelectedTab = tabPluginEditor; }
        }

        private void toolStripButtonEditor_Click(object sender, EventArgs e)
        {
            tabControlPlugins.SelectedTab = tabPluginEditor;
            tabControlImg.SelectedTab = tabPageRender;
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            tabControlPlugins.SelectedTab = tabPluginSave;
            tabControlImg.SelectedTab = tabPageDone;
        }

        private void textBoxSaveNameFile_TextChanged(object sender, EventArgs e)
        {
            string fileName = textBoxSaveNameFile.Text;
            string invalidCharsPattern = $"[{Regex.Escape(new string(Path.GetInvalidFileNameChars()))}]"; // escape invalid chars
            string safeFileName = Regex.Replace(fileName, invalidCharsPattern, "");
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(safeFileName);
            string validFileName = $"{fileNameWithoutExtension}{Path.GetExtension(safeFileName)}";
            textBoxSaveNameFile.Text = validFileName;
            textBoxSaveNameFile.SelectionStart = textBoxSaveNameFile.Text.Length;
            buttonImgSaveQuick.Enabled = textBoxSaveNameFile.Text == "" ? false : true;
        }

        private void comboBoxImg_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.ImageIndexNow = comboBoxImg.SelectedIndex;
            pictureBoxSouser.Image = Global.imagesSouser[Global.ImageIndexNow].Pictures;
            pictureBoxRender.Image = Global.imagesRender[Global.ImageIndexNow].Pictures;
            LoadEditorSetting(Global.ImageIndexNow);
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quit();
        }

        private void btnEditorOk_Click(object sender, EventArgs e)
        {
            if (Global.imagesRender[Global.ImageIndexNow].Exist)
            {
                btnEditorOk.Enabled = false;
                Global.imagesRender[Global.ImageIndexNow].Pictures = renderEditor(Global.imagesSouser[Global.ImageIndexNow].Pictures, Global.ImageIndexNow);
                pictureBoxRender.Image = Global.imagesRender[Global.ImageIndexNow].Pictures;
                btnEditorOk.Enabled = true;
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
            item.Saturation = trackBarSaturation.Value / 10f;
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

            // Вращаем изображение
            if (item.Rotate != 0)
            {
                toolStripStatusLabel1.Text = "Поворачиваю изображение...";
                Application.DoEvents();
                // Вращаем изображение
                RotateImage rotateImage = new RotateImage();
                rotateImage.image = image;
                rotateImage.angle = item.Rotate;
                image = rotateImage.Rotate();
            }

            // Меняем размеры
            toolStripStatusLabel1.Text = "Меняю размеры изображения...";
            Application.DoEvents();
            PhotoSize photoSize = new PhotoSize();
            photoSize.height = item.canvasHeight;
            photoSize.width = item.canvasWidth;
            photoSize.zoom = item.Zoom;
            photoSize.horizontal = item.Horizontal;
            photoSize.vertical = item.Vertical;
            photoSize.source = image;
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

        private void btnEditorReset_Click(object sender, EventArgs e)
        {
            ResetEditorSetting();
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

        private decimal calcRotateBox(decimal value, decimal x)
        {
            value = value + x;
            if (value >= 360) value = value - 360;
            if (value < 0) value = value + 360;
            return value;
        }

        private void btnAutoBr_Click(object sender, EventArgs e)
        {
            ModifyImage mi = new ModifyImage();
            var br = mi.CalcBrightness(Global.imagesSouser[Global.ImageIndexNow].Pictures);
            if (br > 2) br = 2;
            trackBarBrightness.Value = Convert.ToInt16(br * 100 - 100);
        }
        // Окончание вращения изображения


        private void buttonImgSaveClear_Click(object sender, EventArgs e)
        {
            textBoxSaveNameFile.Text = "";
        }

        private void buttonImgSaveQuick_Click(object sender, EventArgs e)
        {
            SaveImageFile saveImageFile = new SaveImageFile();
            saveImageFile.image = null;
            if (Properties.Settings.Default.inputSave == 2) saveImageFile.image = Global.ImageOut;
            if (Properties.Settings.Default.inputSave == 1) saveImageFile.image = Global.imagesRender[Global.ImageIndexNow].Pictures;
            if (Properties.Settings.Default.inputSave == 0) saveImageFile.image = Global.imagesSouser[Global.ImageIndexNow].Pictures;
            saveImageFile.saveFormat = Properties.Settings.Default.typeSave;
            saveImageFile.quality = (int) (Properties.Settings.Default.qualitySave / 4.0 * 100);

            string path = Path.Combine(Properties.Settings.Default.pathSave, textBoxSaveNameFile.Text + "." + Properties.Settings.Default.typeSave);
            bool writeFile = true;
            if (File.Exists(path) && MessageBox.Show("Файл с указанным именем уже существует. Заменить?", "Замена файла!", MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                writeFile = false;
            }

            if (writeFile) {
                try
                {
                    saveImageFile.savePath = path;
                    saveImageFile.SaveImage();
                }  
                catch {
                    toolStripStatusLabel1.Text = "Ошибка сохранения файла";
                }

            }
        }

        private void buttonImgSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {

                saveFileDialog.Filter = "Text files (*." + Properties.Settings.Default.typeSave + ")|*." + Properties.Settings.Default.typeSave; //фильтр для расширения файла
                saveFileDialog.InitialDirectory = Properties.Settings.Default.pathSave; //начальная директория
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = "Выберите файл для сохранения";
                saveFileDialog.FileName = textBoxSaveNameFile.Text;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName;
                    SaveImageFile saveImageFile = new SaveImageFile();
                    saveImageFile.image = null;
                    if (Properties.Settings.Default.inputSave == 2) saveImageFile.image = Global.ImageOut;
                    if (Properties.Settings.Default.inputSave == 1) saveImageFile.image = Global.imagesRender[Global.ImageIndexNow].Pictures;
                    if (Properties.Settings.Default.inputSave == 0) saveImageFile.image = Global.imagesSouser[Global.ImageIndexNow].Pictures;
                    saveImageFile.saveFormat = Properties.Settings.Default.typeSave;
                    saveImageFile.quality = (int)(Properties.Settings.Default.qualitySave / 4.0 * 100);
                    
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
        }
    }
}
