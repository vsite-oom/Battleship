namespace GUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadData(rowPicker);
            LoadData(columnPicker);
        }

        private void LoadData(Picker picker)
        {
            picker.Items.Add("6");
            picker.Items.Add("7");
            picker.Items.Add("8");
            picker.Items.Add("9");
            picker.Items.Add("10");
            picker.Items.Add("11");
            picker.Items.Add("12");
            picker.Items.Add("13");
            picker.SelectedIndex = 0;
        }

        private async void startButton_Clicked(object sender, EventArgs e)
        {
            int rows = int.Parse(rowPicker.SelectedItem.ToString());
            int columns = int.Parse(columnPicker.SelectedItem.ToString());
            await Navigation.PushAsync(new GamePage(rows, columns));
        }
    }

}