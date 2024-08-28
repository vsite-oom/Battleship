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
            for (int i = 0; i < 11; i++) {
                for (int j = 0; j < 11; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    else if (i == 0)
                    {
                        Controls.Add(CreateLetterLabel(j, 150 + j * 50, 100));  // Fleet grid
                        Controls.Add(CreateLetterLabel(j, 800 + j * 50, 100));  // Shots grid
                    }
                    else if (j == 0)
                    {
                        Controls.Add(CreateNumberLabel(i, 150, 100 + i * 50));  // Fleet grid
                        Controls.Add(CreateNumberLabel(i, 800, 100 + i * 50));  // Shots grid
                    }
                    else
                    {
                        int FleetGridLeftMargin = 150;
                        int ShotsGridLeftMargin = 800;
                        int topMargin = 100;
                        Controls.Add(CreateButton(i, j, FleetGridLeftMargin, topMargin));
                        Controls.Add(CreateButton(i, j, ShotsGridLeftMargin, topMargin));
                    }
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

        private Label CreateNumberLabel(int i, int leftMargin, int topMargin)
        {
            Label label = new Label();
            label.AutoSize = false;
            label.Location = new Point(leftMargin, topMargin);
            label.Name = "label" + i;
            label.Size = new Size(50, 50);
            label.TabIndex = i;
            label.Text = i.ToString();
            label.Padding = new Padding(0, 0, 10, 0);
            label.Font = new Font("Regular", 12);
            label.TextAlign = ContentAlignment.MiddleRight;
            return label;
        }

        private Label CreateLetterLabel(int j, int leftMargin, int topMargin)
        {
            Label label = new Label();
            label.AutoSize = false;
            label.Location = new Point(leftMargin, topMargin);
            label.Name = "label" + j;
            label.Size = new Size(50, 50);
            label.TabIndex = j;
            label.Text = ((char)(j + 64)).ToString();
            label.Padding = new Padding(0, 0, 0, 10);
            label.Font = new Font("Regular", 12);
            label.TextAlign = ContentAlignment.BottomCenter;

            return label;
        }
    }
}
