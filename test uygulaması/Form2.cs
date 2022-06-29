using DataAcsess;
using DataAcsess.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace test_uygulaması
{
    public partial class Form2 : Form
    {
        TestDBContext context = new TestDBContext();

        public object Context { get; private set; }


        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ComboBoxListGetir();
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var personelDepartman = (comboBox1.SelectedItem as ComboboxItem).Value.ToString();




            var prsnl = context.departmans.Where(s => s.PersonelID == int.Parse(personelDepartman)).FirstOrDefault();
            if (prsnl == null)
            {
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {

                textBox1.Text = prsnl.Departman;
                textBox2.Text = prsnl.Gorevi;
            }
        }

        private void ComboBoxListGetir()
        {

            comboBox1.Items.Clear();
            var pers = context.Personels.ToList();

            foreach (var items in pers)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = items.Adi + " " + items.Soyadi;
                item.Value = items.Id;
                comboBox1.Items.Add(item);
            }
        }

        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)  // Kayıt Buton
        {
            var personelId = (comboBox1.SelectedItem as ComboboxItem).Value.ToString();

            var pers = new PersonelDepartman
            {
                Departman = textBox1.Text,
                Gorevi = textBox2.Text,
                PersonelID = int.Parse(personelId)
                //Id ile Personel ID eşleştir 
            };


            context.departmans.Add(pers);
            context.SaveChanges();

            MessageBox.Show("Başarılı Şekilde Kaydedildi.");
        }

        private void button2_Click(object sender, EventArgs e) // Güncelleme
        {

            var personelId = (comboBox1.SelectedItem as ComboboxItem).Value.ToString();


            var updt = context.departmans.Where(i => i.PersonelID == int.Parse(personelId)).FirstOrDefault();
            updt.Departman = textBox1.Text;
            updt.Gorevi = textBox2.Text;



            context.departmans.Update(updt);
            context.SaveChanges();

            Form1 aa = new Form1();
            aa.GuncellenenData();
           
            MessageBox.Show("Güncelleme Başarılı.");

        }

        private void button3_Click(object sender, EventArgs e)  // Sİlme
        {

            var personelId = (comboBox1.SelectedItem as ComboboxItem).Value.ToString();

            var remove = context.departmans.Where(i => i.PersonelID == int.Parse(personelId)).FirstOrDefault();

            context.departmans.Remove(remove);
            context.SaveChanges();

            MessageBox.Show("Silme Başarılı.");
        }
    }
}
