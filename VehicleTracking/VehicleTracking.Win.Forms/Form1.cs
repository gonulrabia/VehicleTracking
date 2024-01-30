using Newtonsoft.Json;
using System.Data;
using System.Windows.Forms;
using VehicleTracking.Core.Web.API.Models;
using VehicleTracking.Service.Implementation;

namespace VehicleTracking.Win.Forms
{


    public partial class Form1 : Form
    {

        private readonly string _apiServiceUrl = "https://localhost:7050";
        private Vehicle _vehicle;//Global de�i�ken
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Vehicle vehicleRequest = new Vehicle();

                //g�ncelleme
                if (_vehicle?.Id > 0)
                {
                    if (string.IsNullOrWhiteSpace(textBoxPlateNumber.Text) ||
                       string.IsNullOrWhiteSpace(textBoxRawMaterial.Text) ||
                       string.IsNullOrWhiteSpace(textBoxAmount.Text))
                    {
                        MessageBox.Show("L�tfen t�m alanlar� doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Hata durumunda metodu burada bitir
                    }
                    vehicleRequest = _vehicle;
                    vehicleRequest.PlateNumber = textBoxPlateNumber.Text;
                    vehicleRequest.RawMaterial = textBoxRawMaterial.Text;
                    vehicleRequest.Amount = int.Parse(textBoxAmount.Text);

                    using (var apiService = new ApiService(_apiServiceUrl))
                    {

                        string responseData = apiService.SendPutRequest($"/api/Vehicles/{vehicleRequest.Id}", vehicleRequest);

                        MessageBox.Show("Veri g�ncellendi.");
                        //MessageBox.Show(responseData, "API Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    tabControl1.SelectedIndex = 1;

                }
                //yeni kay�t ekleme
                else
                {  // TextBox'lar�n bo� olup olmad���n� kontrol et
                    if (string.IsNullOrWhiteSpace(textBoxPlateNumber.Text) ||
                        string.IsNullOrWhiteSpace(textBoxRawMaterial.Text) ||
                        string.IsNullOrWhiteSpace(textBoxAmount.Text))
                    {
                        MessageBox.Show("L�tfen t�m alanlar� doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Hata durumunda metodu burada bitir
                    }


                    vehicleRequest.PlateNumber = textBoxPlateNumber.Text;
                    vehicleRequest.RawMaterial = textBoxRawMaterial.Text;
                    vehicleRequest.Amount = int.Parse(textBoxAmount.Text);

                    // ApiService kullanarak POST iste�i g�nderme
                    using (var apiService = new ApiService(_apiServiceUrl))
                    {
                        string responseData = apiService.SendPostRequest("/api/Vehicles", vehicleRequest);
                        MessageBox.Show("Veri kay�t edildi.");
                        //MessageBox.Show(responseData, "API Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    tabControl1.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("L�tfen miktar alan�na say� giriniz.");
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0) // 1. tab (index 0)
            {
                if (_vehicle.Id <= 0)
                {
                    textBoxPlateNumber.Text = "";
                    textBoxRawMaterial.Text = "";
                    textBoxAmount.Text = "";
                }
            }
            if (tabControl1.SelectedIndex == 1) // 2. tab (index 1)
            {
                _vehicle = new Vehicle();
                LoadDataForSecondTab();
            }
        }

        private DataTable ConvertToDataTable(List<Vehicle> vehicles)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Plaka", typeof(string));
            dataTable.Columns.Add("Hammadde", typeof(string));
            dataTable.Columns.Add("Miktar", typeof(int));
           dataTable.Columns.Add("Durum", typeof(Vehicle.EnStatus));
            

            foreach (var vehicle in vehicles)
            {
                dataTable.Rows.Add(vehicle.Id, vehicle.PlateNumber, vehicle.RawMaterial, vehicle.Amount, vehicle.Approval);//, vehicle.SecondApproval, vehicle.WeighingProcess
            }

            return dataTable;
        }

        private void AddUpdateAndDeleteButtons()
        {
            // Onayla butonu
            DataGridViewButtonColumn approvalButtonColumn = new DataGridViewButtonColumn();           
            approvalButtonColumn.HeaderText = "Onayla";
            approvalButtonColumn.Text = "Onayla";
            approvalButtonColumn.Name = "Onayla";         
            approvalButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(approvalButtonColumn);



            // D�zenle butonu
            DataGridViewButtonColumn updateButtonColumn = new DataGridViewButtonColumn();
            updateButtonColumn.HeaderText = "D�zenle";
            updateButtonColumn.Text = "D�zenle";
            updateButtonColumn.Name = "D�zenle"; 
            updateButtonColumn.ReadOnly=true;
            updateButtonColumn.UseColumnTextForButtonValue = true;
           dataGridView1.Columns.Add(updateButtonColumn);
        }
        private void LoadDataForSecondTab()
        {
            // ApiService'yi using blo�u i�inde kullanarak
            using (var apiService = new ApiService(_apiServiceUrl))
            {
                try
                {
                    // Web API'den veri �ekme
                    string responseData = apiService.GetDataFromApi("/api/Vehicles");

                    // Json string'i model listesine d�n��t�r
                    List<Vehicle> vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(responseData);

                    // DataGridView'e eklenecek butonlar� temizle
                    dataGridView1.Columns.Clear();
                    // responseData i�erisinde Web API'den gelen veriyi i�leyin
                    dataGridView1.DataSource = ConvertToDataTable(vehicles);
                    AddUpdateAndDeleteButtons();

                    //MessageBox.Show(responseData, "Web API Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } // Burada using blo�u bitti�inde, ApiService nesnesi Dispose edilecek

        }


        private void UpdateItem(DataGridViewRow selectedRow, bool isDeleted = false)
        {

            Vehicle vehicle = new()
            {
                Id = (int)selectedRow.Cells["Id"].Value,
                PlateNumber = (string)selectedRow.Cells["Plaka"].Value,
                RawMaterial = (string)selectedRow.Cells["Hammadde"].Value,
                Amount = (int)selectedRow.Cells["Miktar"].Value,
                Approval = (int)selectedRow.Cells["Durum"].Value,
                IsDeleted = isDeleted
            };

            // ApiService kullanarak PUT iste�i g�nderme
            using (var apiService = new ApiService(_apiServiceUrl))
            {
                string responseData = apiService.SendPutRequest($"api/Vehicles/{vehicle.Id}", vehicle);
               
            }
        }
        private bool showEditButton = false; // Onay butonuna t�kland���nda g�sterilecek d�zenleme butonu kontrol�
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             DataGridViewRow selectedRow;
            if (e.ColumnIndex == dataGridView1.Columns["D�zenle"].Index && e.RowIndex>=0)s
            {
                selectedRow = dataGridView1.Rows[e.RowIndex];
                var currentValue = (Vehicle.EnStatus)dataGridView1.Rows[e.RowIndex].Cells["Durum"].Value;
                if (currentValue == Vehicle.EnStatus.TartimBekliyor)
                {

                    textBoxPlateNumber.Text = (string)selectedRow.Cells["Plaka"].Value;
                    textBoxRawMaterial.Text = (string)selectedRow.Cells["Hammadde"].Value;
                    textBoxAmount.Text = selectedRow.Cells["Miktar"].Value.ToString();
                    dataGridView1.Rows[e.RowIndex].Cells["Durum"].Value = Vehicle.EnStatus.�kinciOnayBekliyor;
                    _vehicle = new()
                    {
                        Id = (int)selectedRow.Cells["Id"].Value,
                        PlateNumber = (string)selectedRow.Cells["Plaka"].Value,
                        RawMaterial = (string)selectedRow.Cells["Hammadde"].Value,
                        Amount = (int)selectedRow.Cells["Miktar"].Value,
                        Approval = (int)selectedRow.Cells["Durum"].Value,
                       
                    };
                    tabControl1.SelectedIndex = 0;
                }
                else
                {
                    // �kinciOnayBekliyor durumu d���nda bir durumda ise uyar� ver
                    MessageBox.Show("Sadece Tart�m Bekliyor durumundaki kay�tlar� d�zenleyebilirsiniz.");
                }

            }
            
            else if (e.ColumnIndex == dataGridView1.Columns["Onayla"].Index && e.RowIndex>=0)
            {
                selectedRow = dataGridView1.Rows[e.RowIndex];
                var currentValue = (Vehicle.EnStatus)dataGridView1.Rows[e.RowIndex].Cells["Durum"].Value;

                if (currentValue == Vehicle.EnStatus.TartimBekliyor)
                {
                    MessageBox.Show("D�zenleden Tart�m Yap�lmal�!");
                }

                else if (currentValue == Vehicle.EnStatus.�lkOnayBekliyor)
                {
                    dataGridView1.Rows[e.RowIndex].Cells["Durum"].Value = Vehicle.EnStatus.TartimBekliyor;
                }
                else if (currentValue == Vehicle.EnStatus.�kinciOnayBekliyor)
                {
                    dataGridView1.Rows[e.RowIndex].Cells["Durum"].Value = Vehicle.EnStatus.Onaylandi;
                    selectedRow = dataGridView1.Rows[e.RowIndex];
                    UpdateItem(selectedRow,true); // "Onaylandi" durumunda UpdateItem fonksiyonunu �a��r ve 'true' de�eri ile i�aretle           
                    tabControl1.SelectedIndex = 0;
                    tabControl1.SelectedIndex = 1;
                }
                

               
                if (currentValue != Vehicle.EnStatus.TartimBekliyor)
                {
                    MessageBox.Show("Onay ger�ekle�ti");
                }
              

            }
          
        }
    }
}
