using System.Windows;
using StockMini.Models;

namespace StockMini
{
    public partial class AddItemWindow : Window
    {
        public AddItemWindow()
        {
            InitializeComponent();
        }

        // "저장" 버튼 눌렀을 때 실행되는 코드
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // 입력창에 적은 값들을 가져와서 새 Item 객체 만들기
            var newItem = new Item
            {
                Name = NameBox.Text,
                Category = CategoryBox.Text,
                Quantity = int.Parse(QuantityBox.Text),   // 문자를 숫자로 변환
                Price = decimal.Parse(PriceBox.Text)       // 문자를 소수로 변환
            };

            // DB에 저장하기
            using var db = new AppDbContext();
            db.Items.Add(newItem);   // 새 품목을 추가할 목록에 넣음
            db.SaveChanges();        // 실제로 DB에 반영 (여기서 진짜 저장됨)

            this.Close();   // 저장 끝나면 이 입력창 닫기
        }
    }
}