using DataAcsess;
using DataAcsess.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace test_uygulaması
{


    public partial class Form1 : Form
    {
        TestDBContext context = new TestDBContext();

        public object Context { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            ComboBoxListGetir();
            GuncellenenData();
            MaasToplamı();

        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var personelID = (comboBox1.SelectedItem as ComboboxItem).Value.ToString();

            var personel = context.Personels.Where(s => s.Id == int.Parse(personelID)).FirstOrDefault();

            textBox1.Text = personel.Adi;
            textBox2.Text = personel.Soyadi;
            textBox3.Text = personel.Maas.ToString();
            textBox5.Text = personel.Id.ToString();

        }

        private void button1_Click(object sender, EventArgs e) // Kayıt et
        {

            var pers = new Personel
            {
                Adi = textBox1.Text,
                Soyadi = textBox2.Text,
                Maas = int.Parse(textBox3.Text)
            };

            context.Personels.Add(pers);
            context.SaveChanges();

            GuncellenenData();
            ComboBoxListGetir();
            Temizle();
            MaasToplamı();

            MessageBox.Show("Başarılı Şekilde Kaydedildi.");
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {





        }

        private void button3_Click(object sender, EventArgs e) // Sil
        {
            int ID = int.Parse(textBox5.Text);
            var remove = context.Personels.Where(i => i.Id == ID).FirstOrDefault();
            context.Personels.Remove(remove);

            var departmanlist = context.departmans.Where(i => i.PersonelID == ID).FirstOrDefault();
            if (departmanlist != null)
            {
                context.departmans.Remove(departmanlist);
            }

            context.SaveChanges();

            MessageBox.Show("Silme Başarılı");

            GuncellenenData();
            ComboBoxListGetir();
            Temizle();
            MaasToplamı();
        }

        private void button4_Click(object sender, EventArgs e) // Guncelle
        {

            int ID = int.Parse(textBox5.Text);
            var updt = context.Personels.Where(i => i.Id == ID).FirstOrDefault();
            updt.Adi = textBox1.Text;
            updt.Soyadi = textBox2.Text;
            updt.Maas = int.Parse(textBox3.Text);


            context.Personels.Update(updt);
            context.SaveChanges();

            MessageBox.Show("Güncelleme Başarılı");

            GuncellenenData();
            ComboBoxListGetir();
            Temizle();
            MaasToplamı();
        }

        private void button5_Click(object sender, EventArgs e)
        {

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

        public void GuncellenenData()
        {
            var personelList =  (from personel in context.Personels
                                  join personeldept in context.departmans on personel.Id equals personeldept.PersonelID
                                  select new PersonelDepartmanView
                                  {
                                      Id=personel.Id,
                                      AdiSoyadi = personel.Adi + " " + personel.Soyadi,
                                      Departman = personeldept.Departman,
                                      Gorev = personeldept.Gorevi

                                  }).ToList();
            dataGridView1.DataSource = personelList;
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


        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox5.Clear();
            oluşturmaTarihiBox.Clear();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Temizle();
        }

        private void MaasToplamı()
        {
            int toplamMaas = context.Personels.Sum(i => i.Maas);

            textBox4.Text = toplamMaas.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void oluşturmaTarihiBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void departmanEkleme_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.InitializeComponent();

            GuncellenenData();
        }
    }
}
