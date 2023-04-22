using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pract14_3_5
{
    public partial class Form1 : Form
    {
        private Queue<int> _queue = new Queue<int>();
        private Queue<string> queue = new Queue<string>();
        private Person _person;
        private int n;

        public Form1()
        {
            InitializeComponent();
        }

        string CheckError()
        {
            string mess = "";
            if (textBox1.Text == "")
            {
                mess += "n пустое\n";
            }

            foreach (var el in textBox1.Text)
            {
                if (!char.IsDigit(el))
                {
                    mess += "Поле должно содержать только цифры\n";
                    break;
                }
            }

            return mess;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            textBox1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            listBox1.Visible = true;
            button5.Visible = false;
            button6.Visible = false;
            listBox2.Visible = false;
            listBox3.Visible = false;
            button8.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string text = CheckError();
                if (text == "")
                {
                    n = Convert.ToInt32(textBox1.Text);

                    for (int i = 1; i <= n; i++)
                    {
                        _queue.Enqueue(i);
                    }

                    foreach (var el in _queue)
                    {
                        listBox1.Items.Add(el);
                    }
                }
                else MessageBox.Show(text, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string text = CheckError();
                if (text == "")
                {
                    n = Convert.ToInt32(textBox1.Text);

                    for (int i = 1; i <= n; i++)
                    {
                        _queue.Dequeue();
                    }

                    listBox1.Items.Clear();
                }
                else MessageBox.Show(text, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!File.Exists("Person.txt")) { MessageBox.Show( "Файл пустой или не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            else
            {
                label1.Visible = false;
                textBox1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                listBox1.Visible = false;
                button5.Visible = true;
                button6.Visible = true;
                listBox2.Visible = true;
                button8.Visible = false;
                listBox3.Visible = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                listBox2.Items.Clear();
                using (StreamReader sr = new StreamReader("Person.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split(' ');
                        string family = parts[0];
                        string name = parts[1];
                        string otchestvo = parts[2];
                        int age = int.Parse(parts[3]);
                        int weight = int.Parse(parts[4]);
                        Person person = new Person(family, name, otchestvo, age, weight);
                        queue.Enqueue(person.GetInfo());
                        listBox2.Items.Add(person.GetInfo());
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                listBox2.Items.Clear();
                string[] lines = File.ReadAllLines("Person.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split(' ');
                    string family = parts[0];
                    string name = parts[1];
                    string otchestvo = parts[2];
                    int age = Int32.Parse(parts[3]);
                    int weight = Int32.Parse(parts[4]);
                    if (age < 40)
                    {
                        Person person = new Person(family, name, otchestvo, age, weight);
                        queue.Enqueue(person.GetInfo());
                        listBox2.Items.Add(person.GetInfo());
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            textBox1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            listBox1.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            listBox2.Visible = false;
            button8.Visible = true;
            listBox3.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Queue<string> people = new Queue<string>();
            listBox3.Items.Clear();

            FileInfo fileInfo = new FileInfo("Person.txt");
            if (!fileInfo.Exists || fileInfo.Length == 0)
            {
                MessageBox.Show("Ошибка", "Файл пустой или не найден", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (StreamReader sr = new StreamReader("Person.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    people.Enqueue(line);
                }
            }

            people = new Queue<string>(people.OrderBy(p => int.Parse(p.Split()[3])));
            Queue sortedPeople = new Queue();

            foreach (string person in people) 
            {
                string[] parts = person.Split();
                sortedPeople.Enqueue($"{parts[0]} {parts[1]} {parts[2]} {parts[3]} {parts[4]}");
            }

            foreach (string person in sortedPeople)
            {
                listBox3.Items.Add(person);
            }
        }
    }
}