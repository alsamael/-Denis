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

namespace Ugolovnik
{
   


    public partial class Form1 : Form
    {

        DirectoryInfo Voprosi;

        int Categoriya; //категория, которая назначается игре
        int count_q = 0; // категория вопросов, которые берутся из файлов
        int n_file = 0; // номер файла

        List<RadioButton> Listrb=new List<RadioButton>(); // список радио-кнопок

        Button b2;


        FileInfo[] files;

        public Form1()
        {
            InitializeComponent();

            Voprosi = new DirectoryInfo(Application.StartupPath + @"\" + @"\Voprosi"); //пути к файлам
            files = Voprosi.GetFiles("*.*");
            


        }


        public void LoadTest(int n_file)
        {
            

           


            string name_file = files[n_file].FullName;
            name_file = name_file.Substring(name_file.Count() - 10); //имя файла с вопросами

            System.IO.StreamReader file = new System.IO.StreamReader(@name_file, System.Text.Encoding.GetEncoding(1251)); // открывает файл с вопросами

            string line;

            int schk_rb=0;

            // построчное считывание файла
            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("Категория")==true)
                {
                    // запоминает категорию вопросов
                    int n_s = line.IndexOf(":");
                    string categ = line.Substring( n_s+1, (line.Length - 10) );            
                    count_q = Convert.ToInt32(categ);

                }
                else
                {
                    //создает радио-кнопки с содержанием, взятым из файла
                    
                 RadioButton   rb = new RadioButton();
                    rb.Size = new Size(950, 40);
                    rb.Text = line.Substring(3, line.Length - 4);
                    rb.Location = new Point(70, 100 + schk_rb * 55);
                    rb.Name = "Rbutton"+schk_rb;

                    Listrb.Add(rb);
                   
                    this.Controls.Add(rb);
                    rb.Checked = true;
                    schk_rb++;

                    button1.Location = new Point(800, 100 + schk_rb * 55);

                }
                    
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {

            


            LoadTest(n_file);
            


        }

        private void b2_Click(object sender, EventArgs e)
        {
            // Обнуляет переменные, чтобы начать тест заново
            Categoriya=0;
            count_q = 0;
            n_file = 0;

            Voprosi = new DirectoryInfo(Application.StartupPath + @"\" + @"\Voprosi");
            files = Voprosi.GetFiles("*.*");

            Listrb = new List<RadioButton>();

            label1.Location = new Point(66, 50);
            label1.Text = "Выберите соответствующий игре вариант";

            LoadTest(n_file);
            button1.Visible = true;
            b2.Dispose();

        }



        private void button1_Click(object sender, EventArgs e)
        {
            // Очищает форму от созданных ранее радио-кнопок
            for (int j = 0; j < Listrb.Count(); j++)
            {
                Controls.Remove(Listrb[j]);
            }


            
            // проверят, какая радио-кнопка выбрана

            for (int i = 0; i < Listrb.Count(); i++)
            {


                if (i != Listrb.Count() - 1)
                {

                    if (Listrb[i].Checked == true)
                    {

                        Categoriya = count_q;

                       // если категорию можно определить сразу же - выводит результат

                        label1.Location = new Point(320, 220);
                        label1.Text = "Данной игре присваиваится рейтинг "+Categoriya.ToString()+"+.";
                        button1.Visible = false;

                        // создаёт кнопку для очищения параметров, чтобы можно начать тест занова
                        b2 = new Button()
                        { Location = new Point(420, 300), Size = new Size(200, 30), Text = "Начать новый тест", AutoSize = true , BackColor= Color.White};
                        Controls.Add(b2);
                        b2.Click += new EventHandler(b2_Click);

                        Listrb = new List<RadioButton>();
                        break;

                    }

                }
                else
                {
                    //если нет, выводит вопросы из следующего файла с другой возрастной категорией

                    Listrb = new List<RadioButton>();


                    n_file++;
                    // если ни один вопрос, характеризующий определенную категорию игры, не подходит, присваивает 0+
                    if (n_file >= files.Count())
                    {

                        label1.Location = new Point(320, 220);
                        label1.Text = "Данной игре присваиваится рейтинг 0+.";
                        button1.Visible = false;

                        // создаёт кнопку для очищения параметров, чтобы можно было начать тест занова
                        b2 = new Button()
                        { Location = new Point(420, 300), Size = new Size(200, 30), Text ="Начать новый тест", AutoSize=true, BackColor = Color.White };
                        Controls.Add(b2);
                        b2.Click += new EventHandler(b2_Click);
                        

                        break;
                    }

                    
                    LoadTest(n_file);




                }

            
                

            }

           




            



        }
    }
}
