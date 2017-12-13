using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DataConverter.Contours;
using DataConverter.Grids2D;

namespace DataConverter
{
    public partial class FrmConverter : Form, ILogger
    {
        private DataType _currentType;
        private Dictionary<DataType, DataTypeStuff> _typeToFormats;

        public FrmConverter()
        {
            InitializeComponent();
        }

        private void FrmConvertor_Load(object sender, EventArgs e)
        {
            ReadConfig();

            CreateLogContextMenu();
        }

        private void ReadConfig()
        {
            Config cfg = Config.Instance;
            _currentType = cfg.DataType;

            ConfigDictionary cfgDict = ConfigDictionary.Instance;
            _typeToFormats = cfgDict.TypeToFormats;
            switch (_currentType)
            {
                case DataType.Contour:
                    radioCont.Checked = false;
                    radioCont.Checked = true;
                    break;
                case DataType.Grid2D:
                    radioGrid2D.Checked = false;
                    radioGrid2D.Checked = true;
                    break;
                case DataType.Point:
                    radioPoint.Checked = false;
                    radioPoint.Checked = true;
                    break;
            }

            if (cfg.IsSubfolder)
            {
                radioSubFolder.Checked = false;
                radioSubFolder.Checked = true;
            }
            else
            {
                radioSubFolder.Checked = false;
                radioSubFolder.Checked = true;
            }
            txtSelectedFolder.Text = cfg.OutSelectedFolder;
            txtSubFolder.Text = cfg.OutSubfolder;
        }

        private void radioDataType_CheckedChanged(object sender, EventArgs e)
        {
            if (!((RadioButton) sender).Checked) return;

            SaveCurrentFormats();

            if (sender.Equals(radioCont))
            {
                _currentType = DataType.Contour;
                SetCombos<ContourFormat>(cbxFormatIn, cbxFormatOut);
            }
            else if (sender.Equals(radioGrid2D))
            {
                _currentType = DataType.Grid2D;
                SetCombos<Grid2DFormat>(cbxFormatIn, cbxFormatOut);
            }
            else if (sender.Equals(radioPoint))
            {
                _currentType = DataType.Point;
                cbxFormatIn.DataSource = cbxFormatOut.DataSource = null;
            }
        }

        private void SetCombos<T>(ComboBox cbxIn, ComboBox cbxOut)
        {
            cbxIn.DataSource = Enum.GetValues(typeof(T));
            cbxOut.DataSource = Enum.GetValues(typeof(T));
            cbxIn.SelectedItem = Enum.Parse(typeof(T), _typeToFormats[_currentType].FormatIn);
            cbxOut.SelectedItem = Enum.Parse(typeof(T), _typeToFormats[_currentType].FormatOut);
        }

        private void SaveCurrentFormats()
        {
            if (cbxFormatIn.SelectedItem != null)
                _typeToFormats[_currentType].FormatIn = cbxFormatIn.SelectedItem.ToString();
            if (cbxFormatOut.SelectedItem != null)
                _typeToFormats[_currentType].FormatOut = cbxFormatOut.SelectedItem.ToString();
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            Config cfg = Config.Instance;
            DataType dataType = DataType.Contour;
            if (radioCont.Checked) dataType = DataType.Contour;
            else if (radioGrid2D.Checked) dataType = DataType.Grid2D;
            else if (radioPoint.Checked) dataType = DataType.Point;
            cfg.DataType = dataType;

            cfg.IsSubfolder = radioSubFolder.Checked;
            cfg.OutSelectedFolder = txtSelectedFolder.Text;
            cfg.OutSubfolder = txtSubFolder.Text;
            cfg.Save();

            ConfigDictionary cfgDict = ConfigDictionary.Instance;
            SaveCurrentFormats();
            cfgDict.TypeToFormats = _typeToFormats;
            cfgDict.Save();
        }

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Выберите файлы для обработки...";
            ofd.Filter = "Все файлы (*.*)|*.*";
            ofd.Multiselect = true;
            if (Directory.Exists(Config.Instance.LastPath))
                ofd.InitialDirectory = Config.Instance.LastPath;

            if ((ofd.ShowDialog() == DialogResult.OK) && (ofd.FileNames != null))
            {
                Config.Instance.LastPath = Path.GetDirectoryName(ofd.FileNames[0]);
                lstFiles.DataSource = ofd.FileNames;
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (lstFiles.Items.Count == 0)
            {
                FormUtils.ShowWarning("Не выбраны файлы для обработки!");
                return;
            }
            if (cbxFormatIn.SelectedIndex == cbxFormatOut.SelectedIndex)
            {
                FormUtils.ShowWarning("Совпадают входной и выходной форматы файлов!");
                return;
            }

            IList<string> filesIn = lstFiles.Items.Cast<string>().ToList();
            IList<string> filesOut;
            try
            {
                filesOut = GetFileNamesOut(filesIn);
            }
            catch (ApplicationException exc)
            {
                FormUtils.ShowWarning(exc.Message);
                return;
            }

            AddMessage("Начало обработки файлов...\r\n");

            switch (_currentType)
            {
                case DataType.Contour:
                    ContourFormat contFormatIn = (ContourFormat) cbxFormatIn.SelectedValue;
                    ContourFormat contFormatOut = (ContourFormat) cbxFormatOut.SelectedValue;
                    ContourProcessor contProcessor = new ContourProcessor(this);
                    contProcessor.Process(filesIn, filesOut, contFormatIn, contFormatOut, true);
                    break;

                case DataType.Grid2D:
                    Grid2DFormat gridFormatIn = (Grid2DFormat) cbxFormatIn.SelectedValue;
                    Grid2DFormat gridFormatOut = (Grid2DFormat) cbxFormatOut.SelectedValue;
                    Grid2DProcessor gridProcessor = new Grid2DProcessor(this);
                    gridProcessor.Process(filesIn, filesOut, gridFormatIn, gridFormatOut, true);
                    break;

                case DataType.Point:
                    break;
            }

            AddMessage("Обработка файлов завершена.");
        }

        private IList<string> GetFileNamesOut(IEnumerable<string> fileNamesIn)
        {
            bool isSubFolder;
            string outFolder;
            if (radioSelectedFolder.Checked)
            {
                outFolder = txtSelectedFolder.Text;
                if (!Directory.Exists(outFolder))
                    throw new ApplicationException("Не существует заданная выходная папка!");
                isSubFolder = false;
            }
            else
            {
                outFolder = txtSubFolder.Text;
                if (outFolder.Length == 0)
                    throw new ApplicationException("Не задана подпапка для вывода данных!");
                isSubFolder = true;
            }

            var fileNamesOut = new List<string>();
            foreach (var fileNameIn in fileNamesIn)
            {
                string fileNameOut;
                if (isSubFolder)
                {
                    string folder = Path.Combine(Path.GetDirectoryName(fileNameIn), outFolder);
                    fileNameOut = Path.Combine(folder, Path.GetFileName(fileNameIn));
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);
                }
                else
                {
                    fileNameOut = Path.Combine(outFolder, Path.GetFileName(fileNameIn));
                }
                fileNamesOut.Add(fileNameOut);
            }

            return fileNamesOut;
        }

        #region Для логгирования

        private void CreateLogContextMenu()
        {
            var popUpMenu = new ContextMenu();
            popUpMenu.MenuItems.Add("Очистить", (sender, o) => this.Clear());
            txtLog.ContextMenu = popUpMenu;
        }

        public void AddMessage(string message)
        {
            txtLog.AppendText(string.Format("{0}  {1}", DateTime.Now, message) + "\r\n");
        }

        public void AddEmptyLine()
        {
            txtLog.AppendText("\r\n");
        }

        public void Clear()
        {
            txtLog.Clear();
        }

        #endregion

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtSelectedFolder.Text = fbd.SelectedPath;
            }
        }

        private void radioFolders_CheckedChanged(object sender, EventArgs e)
        {
            bool isSubFolder = radioSubFolder.Checked;
            txtSelectedFolder.Enabled = !isSubFolder;
            btnSelectFolder.Enabled = !isSubFolder;
            txtSubFolder.Enabled = isSubFolder;
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Config.Instance.AboutProgram, "О программе...",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}