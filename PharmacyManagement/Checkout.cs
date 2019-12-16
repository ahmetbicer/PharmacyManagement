using iTextSharp.text;
using iTextSharp.text.pdf;
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

namespace PharmacyManagement
{
    public partial class Checkout : Form
    {
        string globalid;
        string id;
        string empName;

        bool isEmployee = false;

        DataTable dt1;

        Patient p = new Patient();
        Medicine m = new Medicine();
        Sell s = new Sell();
        Logs l = new Logs();
        public Checkout(DataTable dt, string patID, string globalID)
        {
            InitializeComponent();
            dt1 = dt;
            id = patID;
            globalid = globalID;
        }

        public Checkout(DataTable dt, string patID, string empUsername, string globalID)
        {
            InitializeComponent();
            dt1 = dt;
            id = patID;
            globalid = globalID;
            empName = empUsername;
            isEmployee = true;
        }

        float price = 0;
        private void Checkout_Load(object sender, EventArgs e)
        {

            DataTable dt2 = p.GetPatient(id, globalid);
            dataGridView2.DataSource = dt2;
            

            dataGridView1.DataSource = dt1;
            DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            col.HeaderText = "Details";
            col.Text = "Details";
            col.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(col);

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                price += float.Parse(row.Cells[3].Value.ToString()) * float.Parse(row.Cells[2].Value.ToString());
            }

            label5.Text = (price - ((price * 8) / 100)).ToString() + "$";

            label3.Text = ((price * 8) / 100).ToString() + "$";

            label4.Text = price.ToString() + "$";

            label8.Text = "Push details button to see...";
            label10.Text = "Push details button to see...";
            label12.Text = "Push details button to see...";


        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.ColumnIndex == 4 && e.RowIndex > -1)
            {
                DataTable dt = m.GetUsageDetails(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), globalid);
                
                label8.Text = dt.Rows[0].Field<string>(0);
                label10.Text = dt.Rows[0].Field<string>(2);
                if (dt.Rows[0].Field<string>(3) == "0")
                {
                    label12.Text = "Doesn't need doctor report to sell.";
                }
                else if (dt.Rows[0].Field<string>(3) == "1")
                {
                    label12.Text = "Need doctor report to sell.";
                }
                richTextBox1.Text = dt.Rows[0].Field<string>(1);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                s.SellMedicine(row.Cells[0].Value.ToString(), int.Parse(row.Cells[1].Value.ToString()), int.Parse(row.Cells[2].Value.ToString()), globalid);
                if (isEmployee)
                {
                    l.AddLogs(String.Format("{0}  -  '{1}' sell the medicine '{2}' to patient ID '{3}'", DateTime.Now.ToString(), empName, row.Cells[0].Value.ToString(), id), globalid);
                }
                else
                {
                    l.AddLogs(String.Format("{0}  -  '{1}' sell the medicine '{2}' to patient ID '{3}'", DateTime.Now.ToString(), globalid, row.Cells[0].Value.ToString(), id), globalid);
                }
            }

            PdfPCell pcell = new PdfPCell();
            pcell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            pcell.AddElement(new Phrase("Billing Information"));

            PdfPTable pdfTable = new PdfPTable(dataGridView2.ColumnCount);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfTable.DefaultCell.BorderWidth = 1;

            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdfTable.AddCell(cell);
            }

            
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    try
                    {
                        pdfTable.AddCell(cell.Value.ToString());
                    }
                    catch { }
                }
            }

            pdfTable.SpacingAfter = 20;

            PdfPTable pdfTable2 = new PdfPTable(dataGridView1.ColumnCount);
            pdfTable2.DefaultCell.Padding = 3;
            pdfTable2.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfTable2.DefaultCell.BorderWidth = 1;

            //Adding Header row
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {

                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                    pdfTable2.AddCell(cell);
            }

            

            //Adding DataRow
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    try
                    {
                       pdfTable2.AddCell(cell.Value.ToString()); 
                    }
                    catch { }
                }
            }

            PdfPCell pcell2 = new PdfPCell();
            pcell2.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            pcell2.AddElement(new Phrase(price.ToString()));

            //Exporting to PDF
            string folderPath = @"C:\Users\ahmtb\source\repos\PharmacyManagement\PharmacyManagement\Bills\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (FileStream stream = new FileStream(folderPath + "DataGridViewExport4.pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A4, 0f,0f,80f,0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(pcell);
                pdfDoc.Add(pdfTable);
                pdfDoc.Add(pdfTable2);
                pdfDoc.Add(pcell2);
                pdfDoc.Close();
                stream.Close();
            }



            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool mouseDown;
        private Point lastLocation;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
    }
}
