namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateGrids();
        }

        private void CreateGrids()
        {
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++)
                {
                    int FleetGridLeftMargin = 100;
                    int ShotsGridLeftMargin = 700;
                    int topMargin = 100;
                    Controls.Add(CreateButton(i, j, FleetGridLeftMargin, topMargin));
                    Controls.Add(CreateButton(i, j, ShotsGridLeftMargin, topMargin));
                }
            }
        }

        private CustomButton CreateButton(int i, int j, int leftMargin, int topMargin)
        {
            CustomButton button = new CustomButton(i, j);
            button.Location = new Point(leftMargin + i * 50, topMargin + j * 50);
            button.Name = "button" + i + j;
            button.Size = new Size(50, 50);
            button.TabIndex = i * 10 + j;
            button.Text = "";
            button.UseVisualStyleBackColor = true;
            return button;
        }
    }
}
