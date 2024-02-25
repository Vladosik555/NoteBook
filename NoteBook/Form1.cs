using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace NoteBook
{
    public partial class Form1 : Form
    {
        public string nameFile;
        public string _openFile;
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Кнопка для создании нового окна, если блокнот содержит какие-либо записи, то программа предлагает сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "") 
            {
                DialogResult result = MessageBox.Show("Сохранить изменение в файле?", "Сохранение файла", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes) 
                {
                    if (nameFile == null)
                    {
                        сохранитьКакToolStripMenuItem_Click(sender, e);
                    }
                    else
                    {
                        richTextBox1.SaveFile(nameFile);
                    }
                }
            }
            richTextBox1.Text = "";
        }
        /// <summary>
        /// Тёмная тема
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void тёмнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.White;
            richTextBox1.BackColor = Color.DimGray;
            menuStrip1.BackColor = Color.DarkGray;
        }
        /// <summary>
        /// Светлая тема
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void светлаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.Black;
            richTextBox1.BackColor = Color.White;
            menuStrip1.BackColor = Color.SeaShell;
        }
        /// <summary>
        /// Кнопка для выбора тем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void темыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK) 
            {
                richTextBox1.Font = fontDialog.Font;
            }
        }
        /// <summary>
        /// При нажатии данной кнопки в блокнот выводятся дата и время
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void времяИДатаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += DateTime.Now.ToString();
        }
        /// <summary>
        /// Создание нового окна без сохрания 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void новоеОкноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }
        /// <summary>
        /// Кнопка для открытия файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "all (*.*) |*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(openFileDialog.FileName);
                _openFile = openFileDialog.FileName;
            }
        }
        /// <summary>
        /// Кнопка для сохранения файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void сохранитьToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            if (nameFile == null)
            {
                сохранитьКакToolStripMenuItem_Click(sender, e);
            }
            else
            {
                richTextBox1.SaveFile(nameFile);
            }
        }
        /// <summary>
        /// Кнопка для сохранения файла, как документ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "all (*.*) |*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, richTextBox1.Text);
                _openFile = saveFileDialog.FileName;
            }
        }
        /// <summary>
        /// Кнопка для печати файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument document = new PrintDocument();
            document.PrintPage += _PrintPage;
            PrintDialog dialog = new PrintDialog();
            dialog.Document = document;
            if (dialog.ShowDialog() == DialogResult.OK) 
            {
                dialog.Document.Print();
            }
        }
        /// <summary>
        /// Метод, для печати файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="print"></param>
        public void _PrintPage(object sender, PrintPageEventArgs print) 
        {
            print.Graphics.DrawString(richTextBox1.Text, richTextBox1.Font, Brushes.Black, 0, 0);
        }
        /// <summary>
        /// Кнопка, которая отвечает за цвет фона
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void цветФонаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.BackColor = colorDialog.Color;
            }
        }
        /// <summary>
        /// Кнопка, которая отвечает за цвет шрифта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void цветШрифтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = color.Color;
            }
        }
        /// <summary>
        /// Кнопка для копирования текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(richTextBox1.SelectionLength != 0)
            {
                Clipboard.SetText(richTextBox1.SelectedText);
            }
        }
        /// <summary>
        /// Кнопка для вырезки текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength != 0) 
            {
                Clipboard.SetText(richTextBox1.SelectedText);
                richTextBox1.Text = richTextBox1.Text.Remove(richTextBox1.SelectionStart, richTextBox1.SelectionLength);
            }
        }
        /// <summary>
        /// Кнопка для вставки текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text.Substring(0, richTextBox1.SelectionStart) + Clipboard.GetText() + richTextBox1.Text.Substring(richTextBox1.SelectionStart, richTextBox1.Text.Length - richTextBox1.SelectionStart);
        }
        /// <summary>
        /// Информация о программе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Блокнот" +
                "\nВерсия 1.0 (сборка ОС 19045.3930)" +
                "\n Корпорация <сборка приложений>. Все права" +
                "\nОперационная система WWindows 10 Pro и пользовательский интерфейс" + 
                "\nв ней защищены правами на товарные знаки и иные" + 
                "\nобъекты интеллектуальной собственности");
        }
        /// <summary>
        /// Кнопка для выхода из программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
