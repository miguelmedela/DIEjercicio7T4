using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DIEjercicio7T4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private bool flagSave = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Size = this.Size;

            try
            {
                using (FileStream fs = new FileStream("BinaryFile.bin", FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        Console.WriteLine(sr.ReadLine());
                        Console.WriteLine();

                        fs.Position = 0;

                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            textBox1.WordWrap = br.ReadBoolean();
                            caseChecked = br.ReadInt32();
                            fontColor = br.ReadInt32();
                            backColor = br.ReadInt32();
                            path = br.ReadString();
                            //Console.WriteLine(); //lista


                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            


        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            textBox1.Size = this.Size;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                using (FileStream fs = new FileStream("BinaryFile.bin", FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        bw.Write(textBox1.WordWrap);
                        bw.Write(caseChecked);
                        bw.Write(fontColor);
                        bw.Write(backColor);
                        bw.Write(path);
                        //bw.Write(lista); Aqui faltaria la lista
                    }
                }

            }
            catch (Exception)
            {

            }
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!flagSave)
            {
                DialogResult dr = MessageBox.Show("¿You want to save changes?", "Notepad 1.0",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    Save();
                }
                else
                {
                    if (dr == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            flagSave = false;
            int contPalabras = 0;
            int contFrases = 0;

            for (int i = 0; i < textBox1.Text.Length; i++)
            {
                if (textBox1.Text[i] == ' ' || textBox1.Text[i] == '\n' || textBox1.Text[i] == '\t')
                {
                    contPalabras++;
                }

                if (textBox1.Text[i] == '\n' || textBox1.Text[i] == '.')
                {
                    contFrases++;
                }

            }

            toolTip1.SetToolTip(textBox1, String.Format("Frases:{0}\nPalabras:{1}\nCaracteres:{2}",
               contFrases, contPalabras, textBox1.Text.Length));

            if (normalCaseToolStripMenuItem.Checked)
            {
                text = textBox1.Text;
            }

        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
            textBox1.Text = "";
        }

        SaveFileDialog saveFileDialog = new SaveFileDialog();
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }
        string path;
        private void Save()
        {
            saveFileDialog.Title = "Save as";
            saveFileDialog.InitialDirectory = "C:\\";
            saveFileDialog.Filter = "Texto (*.txt)| *.txt|Todos los archivos|*.*";
            saveFileDialog.ValidateNames = true;

            saveFileDialog.ShowDialog();

            try
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                {
                    sw.Write(textBox1.Text);
                    sw.Close();
                    path=saveFileDialog.FileName;
                    flagSave = true;
                }
            }
            catch (Exception)
            {
                flagSave = false;
            }
        }


        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

 
        OpenFileDialog openFileDialog = new OpenFileDialog();
        private void OpenFile()
        {
            openFileDialog.Title = "Open File";
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.Filter = "Todos los archivos| *.*|Texto| *.txt|Ini| *.ini|Java| *.java|Cs| *.cs|" +
                "Python| *.py|Html| *html|css| *.css|xml| *.xml";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                    {
                        textBox1.Text = sr.ReadToEnd();
                        sr.Close();
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Esto no he conseguido encontrar la manera de hacerlo
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Cut();
        }


        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }


        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }


        private void selectionInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
            f.label1.Text = "Initial Point: " + textBox1.SelectionStart + "\nLenght: " + textBox1.SelectionLength;
            f.textBox1.Text = textBox1.Text;
            f.textBox1.Select(textBox1.SelectionStart, textBox1.SelectionLength);

        }

        private void lineWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!lineWrapToolStripMenuItem.Checked)
            {
                textBox1.WordWrap = false;
            }
            else
            {
                textBox1.WordWrap = true;
            }
        }

        string text;
        int caseChecked;
        private void upperCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Checked)
            {
                normalCaseToolStripMenuItem.Checked = false;
                upperCaseToolStripMenuItem.Checked = false;
                lowerCaseToolStripMenuItem.Checked = false;
                ((ToolStripMenuItem)sender).Checked = true;
            }
            if (((ToolStripMenuItem)sender).Equals(upperCaseToolStripMenuItem))
            {
                textBox1.Text = textBox1.Text.ToUpper();
                caseChecked =1;

            }
            else
            {
                if (((ToolStripMenuItem)sender).Equals(lowerCaseToolStripMenuItem))
                {
                    textBox1.Text = textBox1.Text.ToLower();
                    caseChecked = 2;
                }
                else
                {
                    textBox1.Text = text;
                    caseChecked = 0;
                }
            }
            
        }
        int fontColor;
        private void fontColorSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;
            fontDialog1.Font = textBox1.Font;
            fontDialog1.Color = textBox1.ForeColor;

            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                textBox1.Font = fontDialog1.Font;
                textBox1.ForeColor = fontDialog1.Color;
                fontColor = fontDialog1.Color.ToArgb();
            }

        }
        int backColor;
        public ColorDialog colorDialog = new ColorDialog();
        private void backcolorSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog.AllowFullOpen = false;
            colorDialog.ShowHelp = true;
            colorDialog.Color = textBox1.ForeColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.BackColor = colorDialog.Color;
                backColor = colorDialog.Color.ToArgb();
            }

        }

        private void aboutOfToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Action<string,string,MessageBoxButtons,MessageBoxIcon> showMessage = (x,y,z,w) => MessageBox.Show(x,y,z,w);

            showMessage("Lanzada con lambda","Acerca de",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
    }
}
