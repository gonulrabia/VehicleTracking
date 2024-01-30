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
        private Vehicle _vehicle;//Global deðiþken
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Vehicle vehicleRequest = new Vehicle();

                //güncelleme
                if (_vehicle?.Id > 0)
                {
                    if (string.IsNullOrWhiteSpace(textBoxPlateNumber.Text) ||
                       string.IsNullOrWhiteSpace(textBoxRawMaterial.Text) ||
                       string.IsNullOrWhiteSpace(textBoxAmount.Text))
                    {
                        MessageBox.Show("Lütfen tüm alanlarý doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Hata durumunda metodu burada bitir
                    }
                    vehicleRequest = _vehicle;
                    vehicleRequest.PlateNumber = textBoxPlateNumber.Text;
                    vehicleRequest.RawMaterial = textBoxRawMaterial.Text;
                    vehicleRequest.Amount = int.Parse(textBoxAmount.Text);

                    using (var apiService = new ApiService(_apiServiceUrl))
                    {

                        string responseData = apiService.SendPutRequest($"/api/Vehicles/{vehicleRequest.Id}", vehicleRequest);

                        MessageBox.Show("Veri güncellendi.");
                        //MessageBox.Show(responseData, "API Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    tabControl1.SelectedIndex = 1;

                }
                //yeni kayýt ekleme
                else
                {  // TextBox'larýn boþ olup olmadýðýný kontrol et
                    if (string.IsNullOrWhiteSpace(textBoxPlateNumber.Text) ||
                        string.IsNullOrWhiteSpace(textBoxRawMaterial.Text) ||
                        string.IsNullOrWhiteSpace(textBoxAmount.Text))
                    {
                        MessageBox.Show("Lütfen tüm alanlarý doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Hata durumunda metodu burada bitir
                    }


                    vehicleRequest.PlateNumber = textBoxPlateNumber.Text;
                    vehicleRequest.RawMaterial = textBoxRawMaterial.Text;
                    vehicleRequest.Amount = int.Parse(textBoxAmount.Text);

                    // ApiService kullanarak POST isteði gönderme
                    using (var apiService = new ApiService(_apiServiceUrl))
                    {
                        string responseData = apiService.SendPostRequest("/api/Vehicles", vehicleRequest);
                        MessageBox.Show("Veri kayýt edildi.");
                        //MessageBox.Show(responseData, "API Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    tabControl1.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lütfen miktar alanýna sayý giriniz.");
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



            // Düzenle butonu
            DataGridViewButtonColumn updateButtonColumn = new DataGridViewButtonColumn();
            updateButtonColumn.HeaderText = "Düzenle";
            updateButtonColumn.Text = "Düzenle";
            updateButtonColumn.Name = "Düzenle"; 
            updateButtonColumn.ReadOnly=true;
            updateButtonColumn.UseColumnTextForButtonValue = true;
           dataGridView1.Columns.Add(updateButtonColumn);
        }
        private void LoadDataForSecondTab()
        {
            // ApiService'yi using bloðu içinde kullanarak
            using (var apiService = new ApiService(_apiServiceUrl))
            {
                try
                {
                    // Web API'den veri çekme
                    string responseData = apiService.GetDataFromApi("/api/Vehicles");

                    // Json string'i model listesine dönüþtür
                    List<Vehicle> vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(responseData);

                    // DataGridView'e eklenecek butonlarý temizle
                    dataGridView1.Columns.Clear();
                    // responseData içerisinde Web API'den gelen veriyi iþleyin
                    dataGridView1.DataSource = ConvertToDataTable(vehicles);
                    AddUpdateAndDeleteButtons();

                    //MessageBox.Show(responseData, "Web API Response", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } // Burada using bloðu bittiðinde, ApiService nesnesi Dispose edilecek

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

            // ApiService kullanarak PUT isteði gönderme
            using (var apiService = new ApiService(_apiServiceUrl))
            {
                string responseData = apiService.SendPutRequest($"api/Vehicles/{vehicle.Id}", vehicle);
               
            }
        }
        private bool showEditButton = false; // Onay butonuna týklandýðýnda gösterilecek düzenleme butonu kontrolü
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             DataGridViewRow selectedRow;
            if (e.ColumnIndex == dataGridView1.Columns["Düzenle"].Index && e.RowIndex>=0)s
            {
                selectedRow = dataGridView1.Rows[e.RowIndex];
                var currentValue = (Vehicle.EnStatus)dataGridView1.Rows[e.RowIndex].Cells["Durum"].Value;
                if (currentValue == Vehicle.EnStatus.TartimBekliyor)
                {

                    textBoxPlateNumber.Text = (string)selectedRow.Cells["Plaka"].Value;
                    textBoxRawMaterial.Text = (string)selectedRow.Cells["Hammadde"].Value;
                    textBoxAmount.Text = selectedRow.Cells["Miktar"].Value.ToString();
                    dataGridView1.Rows[e.RowIndex].Cells["Durum"].Value = Vehicle.EnStatus.ÝkinciOnayBekliyor;
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
                    // ÝkinciOnayBekliyor durumu dýþýnda bir durumda ise uyarý ver
                    MessageBox.Show("Sadece Tartým Bekliyor durumundaki kayýtlarý düzenleyebilirsiniz.");
                }

            }
            
            else if (e.ColumnIndex == dataGridView1.Columns["Onayla"].Index && e.RowIndex>=0)
            {
                selectedRow = dataGridView1.Rows[e.RowIndex];
                var currentValue = (Vehicle.EnStatus)dataGridView1.Rows[e.RowIndex].Cells["Durum"].Value;

                if (currentValue == Vehicle.EnStatus.TartimBekliyor)
                {
                    MessageBox.Show("Düzenleden Tartým Yapýlmalý!");
                }

                else if (currentValue == Vehicle.EnStatus.ÝlkOnayBekliyor)
                {
                    dataGridView1.Rows[e.RowIndex].Cells["Durum"].Value = Vehicle.EnStatus.TartimBekliyor;
                }
                else if (currentValue == Vehicle.EnStatus.ÝkinciOnayBekliyor)
                {
                    dataGridView1.Rows[e.RowIndex].Cells["Durum"].Value = Vehicle.EnStatus.Onaylandi;
                    selectedRow = dataGridView1.Rows[e.RowIndex];
                    UpdateItem(selectedRow,true); // "Onaylandi" durumunda UpdateItem fonksiyonunu çaðýr ve 'true' deðeri ile iþaretle           
                    tabControl1.SelectedIndex = 0;
                    tabControl1.SelectedIndex = 1;
                }
                

               
                if (currentValue != Vehicle.EnStatus.TartimBekliyor)
                {
                    MessageBox.Show("Onay gerçekleþti");
                }
              

            }
          
        }
    }
}
