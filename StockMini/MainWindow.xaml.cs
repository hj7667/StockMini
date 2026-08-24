using System.Linq;
using System.Windows;
using StockMini.Models;
using Microsoft.VisualBasic;   // 간단한 입력창(InputBox) 쓰려고 추가

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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddItemWindow();
            addWindow.ShowDialog();
            LoadItems();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ItemGrid.SelectedItem as Item;
            if (selectedItem == null)
            {
                MessageBox.Show("삭제할 품목을 먼저 선택해주세요.");
                return;
            }

            var result = MessageBox.Show(
                $"'{selectedItem.Name}' 품목을 삭제하시겠습니까?",
                "삭제 확인",
                MessageBoxButton.YesNo
            );

            if (result == MessageBoxResult.Yes)
            {
                using var db = new AppDbContext();
                db.Items.Remove(selectedItem);
                db.SaveChanges();
                LoadItems();
            }
        }

        // "입고" 버튼: 수량을 늘림
        private void StockIn_Click(object sender, RoutedEventArgs e)
        {
            AdjustStock(isStockIn: true);
        }

        // "출고" 버튼: 수량을 줄임
        private void StockOut_Click(object sender, RoutedEventArgs e)
        {
            AdjustStock(isStockIn: false);
        }

        // 입고/출고 공통 처리 로직 (isStockIn이 true면 더하고, false면 뺌)
        private void AdjustStock(bool isStockIn)
        {
            var selectedItem = ItemGrid.SelectedItem as Item;
            if (selectedItem == null)
            {
                MessageBox.Show("품목을 먼저 선택해주세요.");
                return;
            }

            // 간단한 입력창으로 수량 물어보기
            string title = isStockIn ? "입고 수량 입력" : "출고 수량 입력";
            string input = Interaction.InputBox("수량을 입력하세요", title, "1");

            // 숫자로 변환 안 되면 (빈 값이거나 문자 입력 등) 그냥 취소
            if (!int.TryParse(input, out int amount) || amount <= 0)
            {
                return;
            }

            // 출고인데 재고보다 많이 빼려고 하면 막기
            if (!isStockIn && selectedItem.Quantity < amount)
            {
                MessageBox.Show("재고 수량보다 많이 출고할 수 없습니다.");
                return;
            }

            using var db = new AppDbContext();
            var item = db.Items.First(i => i.Id == selectedItem.Id);   // DB에서 최신 상태로 다시 가져옴

            item.Quantity += isStockIn ? amount : -amount;   // 입고면 더하고, 출고면 뺌

            db.SaveChanges();
            LoadItems();
        }
    }
}