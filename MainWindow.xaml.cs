using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lineHeight;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;           
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            FlowDocument doc = new FlowDocument();
           
            for (int i = 1; i <= 30; i++)
            {
                Paragraph para = new Paragraph()
                {
                    LineHeight = 14
                };
                para.Inlines.Add($"L{i} :  Đây là một đoạn văn bản được thêm bằng code.");
                doc.Blocks.Add(para);
            }

            rtb.Document = doc;



            var firstIndex = rtb.CaretPosition.GetCharacterRect(LogicalDirection.Forward).Top;
            var secondIndex = rtb.CaretPosition.GetLineStartPosition(1)?.GetCharacterRect(LogicalDirection.Forward).Top;

            if (secondIndex.HasValue)
            {
                lineHeight = secondIndex.Value - firstIndex;
            }
            double das = GetLineNumberOfWord(rtb, "L30");


            Dispatcher.BeginInvoke(new Action(() =>
            {
                double offset = das; //(das - 1) * lineHeight; // Cuộn đến dòng 20 (dòng đầu tiên là 0)
                rtb.ScrollToVerticalOffset(offset);
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        public static double GetLineNumberOfWord(RichTextBox richTextBox, string searchText)
        {
            if (richTextBox.Document == null) return -1;

            // Tìm vị trí của từ cần tìm
            TextRange documentRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            int index = documentRange.Text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase);
            if (index == -1) return -1; // Không tìm thấy từ

            // Lấy TextPointer tại vị trí của từ
            TextPointer wordPointer = GetTextPointerAtOffset(richTextBox.Document.ContentStart, index);
            if (wordPointer == null) return -1;

            // Lấy tọa độ của từ cần tìm
            double wordY = wordPointer.GetCharacterRect(LogicalDirection.Forward).Top;

            // Duyệt toàn bộ văn bản để tìm các vị trí dòng
            List<double> lineHeights = new List<double>();
            TextPointer pointer = richTextBox.Document.ContentStart;

            while (pointer != null)
            {
                double currentY = pointer.GetCharacterRect(LogicalDirection.Forward).Top;

                // Nếu tọa độ Y này khác hẳn so với tọa độ cuối cùng, thì đây là dòng mới
                if (lineHeights.Count == 0 || Math.Abs(currentY - lineHeights[^1]) > 1)
                {
                    lineHeights.Add(currentY);
                }

                // Tiến tới ký tự tiếp theo
                pointer = pointer.GetNextInsertionPosition(LogicalDirection.Forward);
            }

            // So sánh tọa độ từ cần tìm với danh sách dòng để lấy số dòng
            for (int i = 0; i < lineHeights.Count; i++)
            {
                if (wordY == lineHeights[i])
                {
                    //if (i > 6)
                    //    return lineHeights[i - 6];
                    return wordY; // Dòng bắt đầu từ 1
                }
            }

            return -1;
        }

        private static TextPointer GetTextPointerAtOffset(TextPointer start, int offset)
        {
            TextPointer navigator = start;
            int count = 0;

            while (navigator != null && count < offset)
            {
                TextPointerContext context = navigator.GetPointerContext(LogicalDirection.Forward);
                if (context == TextPointerContext.Text)
                {
                    int textLength = navigator.GetTextRunLength(LogicalDirection.Forward);
                    if (count + textLength >= offset)
                    {
                        return navigator.GetPositionAtOffset(offset - count);
                    }
                    count += textLength;
                }
                navigator = navigator.GetNextContextPosition(LogicalDirection.Forward);
            }
            return start;
        }
    }
    
}