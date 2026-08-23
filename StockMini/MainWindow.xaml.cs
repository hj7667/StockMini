using System.Linq;
using System.Windows;
using StockMini.Models;

namespace StockMini
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadItems();
        }

        private void LoadItems()
        {
            using var db = new AppDbContext();
            var items = db.Items.ToList();
            ItemGrid.ItemsSource = items;
        }

        // "+ 품목추가" 버튼 눌렀을 때 실행되는 코드
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddItemWindow();   // 입력창 새로 만들기
            addWindow.ShowDialog();                 // 팝업으로 띄우고, 닫힐 때까지 여기서 대기

            LoadItems();   // 입력창 닫히면 (저장 완료됐으니) 목록 다시 불러와서 새로고침
        }
    }
}