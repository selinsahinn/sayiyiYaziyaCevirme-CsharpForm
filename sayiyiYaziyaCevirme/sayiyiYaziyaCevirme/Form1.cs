using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sayiyiYaziyaCevirme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TextBox txtSayi = new TextBox();    //txtSayi metodu olusturuldu
        Button btn = new Button();          //btn adli buton olusturuldu
        Label lbl = new Label();            //lbd metodu olusturuldu
        Label lblYazi = new Label();        //lblYazi metodu olusturuldu    
        Label lblYazi2 = new Label();       //lblYazi2 metodu olusturuldu
        private void txtSayi_KeyPress(object sender, KeyPressEventArgs e)       //txtSayi ogesinde tusa basildiginda
        {
            if (char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)08)      //yazi(harf) girilmemesi ve backspace durumunda
            {
                e.Handled = true;               //islenen deger dogru olur
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Aquamarine;          //formun arka plan rengi turkuaz olur

            btn.Text = "ÇEVİR";         //butonun üzerinde cevir yazar
            btn.Width = 100;        //buton genisligi 100dur
            btn.Height = 50;        //buton yuksekligi 50dir
            btn.Location = new Point(165, 200);     //butonun konumu verilir
            this.Controls.Add(btn);     //buton kontrolleri eklenir
            btn.Click += new EventHandler(btn_Click);       //butona tiklandiginda olay islenmesi baslatilir
            btn.BackColor = Color.Red;      //butonun arka plan rengi kirmizidir
            btn.ForeColor = Color.White;    //buton ustundeki yazi rengi beyazdir

            lbl.AutoSize = true;            //labela otomatik boyut verilir
            lbl.Text = "Sayı Giriniz";      //labelda yazilacak yazi belirlenir
            lbl.Location = new Point(115, 83);  //label konumu verilir
            this.Controls.Add(lbl);         //label kontrolu eklenir

            txtSayi.Name = "txtSayi";       //txtsayiya isim verildi
            txtSayi.Width = 150;            //txtsayi genisligi belirlendi
            txtSayi.Location = new Point(200, 80);            //txtsayi yuksekligi belirlendi
            this.Controls.Add(txtSayi);        //txtsayi kontrolu eklenir

            lblYazi.AutoSize = true;               //labela otomatik boyut verilir     
            lblYazi.Location = new Point(200, 150);  //label konumu verilir
            this.Controls.Add(lblYazi);         //label kontrolu eklenir

            lblYazi2.AutoSize = true;              //labela otomatik boyut verilir     
            lblYazi2.Text = "Okunuşu: ";     //labelda yazilacak yazi belirlenir
            lblYazi2.Location = new Point(120, 150);  //label konumu verilir
            this.Controls.Add(lblYazi2);        //label kontrolu eklenir
        }

        private string yaziyaCevir(decimal tutar)       //tutara bagli yaziyacevir metodu olusturuldu
        {

            string sTutar = tutar.ToString("F2").Replace('.', ',');     // Replace('.',',') ondalik ayracinin . olma durumu icin            
            string lira = sTutar.Substring(0, sTutar.IndexOf(','));     //tutarin tam kismi alindi
            string kurus = sTutar.Substring(sTutar.IndexOf(',') + 1, 2);    //kurusa girilen degerler
            string yazi = "";       //yazi degeri bos alindi

            string[] birler = { "", "Bir ", "İki ", "Üç ", "Dört ", "Beş ", "Altı ", "Yedi ", "Sekiz ", "Dokuz " };     //birler basamagi dizisi olsuturuldu
            string[] onlar = { "", "On ", "Yirmi ", "Otuz ", "Kırk ", "Elli ", "Altmış ", "Yetmiş ", "Seksen ", "Doksan " };        //onlar basamagi dizisi olsuturuldu
            string[] binler = { "Bin ", "" };     //binler basamagi dizisi olsuturuldu

            int grupSayisi = 2; //sayidaki 3'lü grup sayisi 2ye esitlendi(binlere gore)

            lira = lira.PadLeft(grupSayisi * 3, '0'); //sayinin soluna '0' eklenerek sayi "grup sayisi x 3" basakmakli yapildi          

            string grupDegeri;      //grupdegeri olusturuldu

            for (int i = 0; i < grupSayisi * 3; i += 3)     //sayi uclu gruplar halinde alindi
            {
                grupDegeri = "";    //grup degeri 0'a esitlendi

                if (lira.Substring(i, 1) != "0")    //yuzler basamagi 0sa
                    grupDegeri += birler[Convert.ToInt32(lira.Substring(i, 1))] + "Yüz "; //yüz yazdirildi                

                if (grupDegeri == "Bir Yüz ") //bir yuz yazisi olusmamasi icin
                    grupDegeri = "Yüz ";        //yuz duzeltmesi yapildi

                grupDegeri += onlar[Convert.ToInt32(lira.Substring(i + 1, 1))]; //onlar basamagi ele alindi

                grupDegeri += birler[Convert.ToInt32(lira.Substring(i + 2, 1))]; //birler basamagi ele alindi

                if (grupDegeri != "")       //binler basamagi ele alindi
                    grupDegeri += binler[i / 3];    //binler basamagindaki deger bulundu
                if (grupDegeri == "Bir Bin ")   //bir bin yazisi olusmasi durumunda
                    grupDegeri = "Bin ";        //bin yazildi

                yazi += grupDegeri; //yaziya grup degeri yazdirildi
            }
            if (yazi != "")     //yazi degeri bos degil ise
                yazi += "TL ";      //tl yazdirildi

            int yaziUzunlugu = yazi.Length;     //yazi uzunlugu bulundu

            if (kurus.Substring(0, 1) != "0")   //kurusun onlar basamagi 0 degilse
                yazi += onlar[Convert.ToInt32(kurus.Substring(0, 1))];  //yaziya kurusun onlar basamagi yazdirildi

            if (kurus.Substring(1, 1) != "0")   //kurusun birler basamagi 0 degilse
                yazi += birler[Convert.ToInt32(kurus.Substring(1, 1))]; //yaziya kurusun birler basamagi yazdirildi

            if (yazi.Length > yaziUzunlugu) //girilen yazi uzunlugu hesaplanandan buyuk ise
                yazi += " Kr.";     //yaziya kurus yazdirildi
            else            //diger kosul saglanmiyorsa
                yazi += "Sıfır Kr.";    //yaziya sifir kurus yazdirildi  

            return yazi;        //yazi degeri donduruldu
        }

        void lblYazi_TextChanged(object sender, EventArgs e)        //yazi labelina girilen deger kontrol ettirildi
        {
            throw new NotFiniteNumberException();       //sinifin bir ornegi baslatildi
        }

        void btn_Click(object sender, EventArgs e)      //butona tiklandiginda
        {
            if (txtSayi.TextLength <= 8)        //textbox uzunlugu 8e kadarsa
            {
                lblYazi.Text = yaziyaCevir(Convert.ToDecimal(txtSayi.Text));    //yaziyacevir metodu ile labela deger yazdirildi
                this.BackColor = Color.GreenYellow;     //arkaplan rengi yesil yapildi
            }
            else if (txtSayi.TextLength > 8)       //textboxa girilen deger uzunlugu 8den buyukse
            {
                this.BackColor = Color.PaleVioletRed;       //arkaplan rengi kirmizi yapildi
                MessageBox.Show("Girilen sayi degeri 6 basamağa kadar olmalıdır.");     //uyari mesaji verildi
            }
            else { }        //diger durumlarda hata olusmamasi icin else acildi
        }

    }
}
