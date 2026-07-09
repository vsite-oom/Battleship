using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BattleshipGUI;

public partial class BoardControl : UserControl
{
    private const int Size = 10;
    private readonly Border[,] _cells = new Border[Size, Size];
    private readonly string _columns = "ABCDEFGHIJ";

    public event Action<int, int>? CellClicked;
    public event Action<int, int>? CellHovered;
    public event Action? CellLeft;

    private readonly int _cellSize;

    public BoardControl(int cellSize = 32)
    {
        InitializeComponent();
        _cellSize = cellSize;
        BuildGrid();
    }

    private void BuildGrid()
    {
        var bg = (SolidColorBrush)FindResource("CrtBgBrush");
        var dim = (SolidColorBrush)FindResource("CrtDimBrush");

        BoardGrid.Children.Add(new Border { Width = _cellSize, Height = _cellSize });

        for (int c = 0; c < Size; c++)
        {
            var label = new TextBlock
            {
                Text = _columns[c].ToString(),
                Foreground = dim,
                FontFamily = new FontFamily("Consolas"),
                FontSize = 11,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            BoardGrid.Children.Add(new Border
            {
                Width = _cellSize, Height = _cellSize, Child = label
            });
        }

        for (int r = 0; r < Size; r++)
        {
            var rowLabel = new TextBlock
            {
                Text = (r + 1).ToString(),
                Foreground = dim,
                FontFamily = new FontFamily("Consolas"),
                FontSize = 11,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            BoardGrid.Children.Add(new Border
            {
                Width = _cellSize, Height = _cellSize, Child = rowLabel
            });

            for (int c = 0; c < Size; c++)
            {
                var cell = new Border
                {
                    Width = _cellSize,
                    Height = _cellSize,
                    BorderBrush = new SolidColorBrush(Color.FromArgb(60, 92, 255, 138)),
                    BorderThickness = new Thickness(1),
                    Background = bg,
                    Cursor = Cursors.Cross
                };

                int row = r, col = c;
                cell.MouseLeftButtonDown += (_, _) => CellClicked?.Invoke(row, col);
                cell.MouseEnter += (_, _) => CellHovered?.Invoke(row, col);
                cell.MouseLeave += (_, _) => CellLeft?.Invoke();

                _cells[r, c] = cell;
                BoardGrid.Children.Add(cell);
            }
        }
    }

    public void SetCellColor(int row, int col, Brush background, Brush? border = null)
    {
        if (row < 0 || row >= Size || col < 0 || col >= Size) return;
        _cells[row, col].Background = background;
        if (border != null)
            _cells[row, col].BorderBrush = border;
    }

    public void SetCellContent(int row, int col, string text, Brush foreground)
    {
        if (row < 0 || row >= Size || col < 0 || col >= Size) return;
        _cells[row, col].Child = new TextBlock
        {
            Text = text,
            Foreground = foreground,
            FontFamily = new FontFamily("Consolas"),
            FontSize = _cellSize > 24 ? 16 : 10,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
    }

    public void ClearCell(int row, int col)
    {
        if (row < 0 || row >= Size || col < 0 || col >= Size) return;
        var bg = (SolidColorBrush)FindResource("CrtBgBrush");
        _cells[row, col].Background = bg;
        _cells[row, col].BorderBrush = new SolidColorBrush(Color.FromArgb(60, 92, 255, 138));
        _cells[row, col].Child = null;
    }

    public void ClearAll()
    {
        for (int r = 0; r < Size; r++)
            for (int c = 0; c < Size; c++)
                ClearCell(r, c);
    }
}
