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

namespace CSharp_test3
{
    public partial class Form1 : Form
    {
        private Label NameLabel;
        private Label NoLabel;
        private Label DateLabel;
        private GroupBox drinkGroupBox;
        private GroupBox addGroupBox;
        private GroupBox iceGroupBox;
        private Label priceLabel;
        private TextBox NameTextBox;
        private TextBox NoTextBox;
        private TextBox priceTextBox;
        private TextBox drinkPrice;
        private TextBox addPrice;
        private DateTimePicker dateTimePicker;
        private Button saveButton;
        private Button exportButton;
        private RadioButton blackTea;
        private RadioButton greenTea;
        private RadioButton noIce;
        private RadioButton normalIce;
        private RadioButton add2;
        private RadioButton add1;
        private Label textlabel;
        private int drinkprice = 0;
        private int addprice = 0;
        private int totalPrice = 0;
        private List<Order> orderList = new List<Order>();

        private void drink_check(object sender, EventArgs e)
        {
            if (blackTea.Checked)
            {
                greenTea.Checked = false;
                drinkPrice.Text = 25.ToString();
                drinkprice = 25;
            }
            else if (greenTea.Checked)
            {
                blackTea.Checked = false;
                drinkPrice.Text = 20.ToString();
                drinkprice = 20;
            }
            price_check(sender, e);
        }
        private void add_check(object sender, EventArgs e)
        {
            if (add1.Checked)
            {
                add2.Checked = false;
                addPrice.Text = 10.ToString();
                addprice = 10;
            }
            else if (add2.Checked)
            {
                add1.Checked = false;
                addPrice.Text = 15.ToString();
                addprice = 15;
            }
            else
            {
                addPrice.Text = 0.ToString();
                addprice = 0;
            }
            price_check(sender ,e);
        }
        private void price_check(object sender, EventArgs e)
        {
            totalPrice = drinkprice + addprice;
            priceTextBox.Text = totalPrice.ToString();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string name = NameTextBox.Text;
            string number = NoTextBox.Text;
            DateTime date = dateTimePicker.Value;
            string drink = "";
            string addOn = "";
            string ice = "";
            if (blackTea.Checked)
                drink ="紅茶";
            else if (greenTea.Checked)
                drink = "綠茶";

            if (add1.Checked)
                addOn = "珍珠";
            else if (add2.Checked)
                addOn = "椰果";

            if (noIce.Checked)
                ice = "去冰";
            else if (normalIce.Checked)
                ice = "正常冰";

            Order order = new Order(name, number, date, drink, addOn, ice, totalPrice);
            orderList.Add(order);
            
            textlabel.Text = "已儲存";
        }

        private void exportButton_Click(object sender ,EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
            saveFileDialog.Title = "匯出檔案";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                using(StreamWriter writer = new StreamWriter(filePath,false,Encoding.UTF8))
                {
                    writer.WriteLine("姓名,工號,日期,飲品,加購,冰塊,價格");
                    foreach(Order order in orderList)
                    {
                        string line = $"{order.Name},{order.Number},{order.Date},{order.Drink},{order.Add},{order.Ice},{order.Price}";

                        writer.WriteLine(line);
                    }
                }
                textlabel.Text = "已匯出";
            }
        }

        public Form1()
        {
            this.Text = "CSharp_test3";
            this.Size = new Size(450, 400);

            NameLabel = new Label();
            NameLabel.Text = "姓名 :";
            NameLabel.AutoSize = true;
            NameLabel.Location = new Point(30, 30);
            this.Controls.Add(NameLabel);

            NameTextBox = new TextBox();
            NameTextBox.Size = new Size(60, 30);
            NameTextBox.Location = new Point(70, 27);
            this.Controls.Add(NameTextBox);

            NoLabel = new Label();
            NoLabel.Text = "工號 :";
            NoLabel.AutoSize = true;
            NoLabel.Location = new Point(140, 30);
            this.Controls.Add(NoLabel);

            NoTextBox = new TextBox();
            NoTextBox.Size = new Size(60, 30);
            NoTextBox.Location = new Point(180, 27);
            this.Controls.Add(NoTextBox);

            DateLabel = new Label();
            DateLabel.Text = "日期 :";
            DateLabel.AutoSize = true;
            DateLabel.Location = new Point(250, 30);
            this.Controls.Add(DateLabel);

            dateTimePicker= new DateTimePicker();
            dateTimePicker.Size = new Size(120, 30);
            dateTimePicker.Location = new Point(290, 27);
            this.Controls.Add(dateTimePicker);

            drinkGroupBox = new GroupBox();
            drinkGroupBox.Text = "飲品";
            drinkGroupBox.Size = new Size(130,200);
            drinkGroupBox.Location = new Point(15, 60);
            this.Controls.Add(drinkGroupBox);

            blackTea = new RadioButton();
            blackTea.Text = "紅茶 -- 25";
            blackTea.AutoSize = true;
            blackTea.Location = new Point(30,30);
            blackTea.CheckedChanged += new System.EventHandler(this.drink_check);
            drinkGroupBox.Controls.Add(blackTea);

            greenTea = new RadioButton();
            greenTea.Text = "綠茶 -- 20";
            greenTea.AutoSize = true;
            greenTea.Location = new Point(30, 60);
            greenTea.CheckedChanged += new System.EventHandler(this.drink_check);
            drinkGroupBox.Controls.Add(greenTea);

            drinkPrice = new TextBox();
            drinkPrice.Text = drinkprice.ToString();
            drinkPrice.Size = new Size(65, 30);
            drinkPrice.Location = new Point(30, 100);
            drinkGroupBox.Controls.Add(drinkPrice);


            addGroupBox = new GroupBox();
            addGroupBox.Text = "加購";
            addGroupBox.Size = new Size(130, 200);
            addGroupBox.Location = new Point(155, 60);
            this.Controls.Add(addGroupBox);

            add1 = new RadioButton();
            add1.Text = "珍珠 -- 10";
            add1.AutoSize = true;
            add1.Location = new Point(30, 30);
            add1.CheckedChanged += new System.EventHandler(this.add_check);
            addGroupBox.Controls.Add(add1);

            add2 = new RadioButton();
            add2.Text = "椰果 -- 15";
            add2.AutoSize = true;
            add2.Location = new Point(30, 60);
            add2.CheckedChanged += new System.EventHandler(this.add_check);
            addGroupBox.Controls.Add(add2);

            addPrice = new TextBox();
            addPrice.Text = addprice.ToString();
            addPrice.Size = new Size(65, 30);
            addPrice.Location = new Point(30, 100);
            addGroupBox.Controls.Add(addPrice);

            iceGroupBox = new GroupBox();
            iceGroupBox.Text = "冰塊";
            iceGroupBox.Size = new Size(130, 200);
            iceGroupBox.Location = new Point(295, 60);
            this.Controls.Add(iceGroupBox);

            normalIce = new RadioButton();
            normalIce.Text = "正常冰";
            normalIce.AutoSize = true;
            normalIce.Location = new Point(30, 30);
            iceGroupBox.Controls.Add(normalIce);

            noIce = new RadioButton();
            noIce.Text = "去冰";
            noIce.AutoSize = true;
            noIce.Location = new Point(30, 60);
            iceGroupBox.Controls.Add(noIce);


            priceTextBox = new TextBox();
            priceTextBox.Text = totalPrice.ToString();
            priceTextBox.Size = new Size(65, 30);
            priceTextBox.Location = new Point(55, 275);
            priceTextBox.ReadOnly = true;
            this.Controls.Add(priceTextBox);

            priceLabel = new Label();
            priceLabel.Text = "價格 :                        元";
            priceLabel.AutoSize = true;
            priceLabel.Location = new Point(20, 280);
            this.Controls.Add(priceLabel);

            saveButton = new Button();
            saveButton.Text = "儲存";
            saveButton.Size = new Size(70, 30);
            saveButton.Location = new Point(180, 273);
            saveButton.Click += SaveButton_Click;
            this.Controls.Add(saveButton);

            exportButton = new Button();
            exportButton.Text = "匯出CSV檔";
            exportButton.Size = new Size(100, 30);
            exportButton.Location = new Point(310, 273);
            exportButton.Click += new System.EventHandler(this.exportButton_Click);
            this.Controls.Add(exportButton);

            textlabel = new Label();
            textlabel.Dock = DockStyle.Bottom;
            textlabel.BorderStyle = BorderStyle.Fixed3D;
            textlabel.Size = new Size(450, 30);
            this.Controls.Add(textlabel);
        }

        private class Order
        {
            public string Name { get; set; }
            public string Number { get; set; }
            public DateTime Date { get; set; }
            public string Drink { get; set; }
            public string Add { get; set; }
            public string Ice { get; set; }
            public int Price { get; set; }
            public Order(string name, 
                string number, 
                DateTime date, 
                string drink, 
                string addOn, 
                string ice, 
                int totalPrice)
            {
                Name = name;
                Number = number;
                Date = date;
                Drink = drink;
                Add = addOn;
                Ice = ice;
                Price = totalPrice;
            }

            
        }

        
    }
}
