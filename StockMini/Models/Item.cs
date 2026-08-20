using System;

namespace StockMini.Models
{
    // 재고 품목 하나를 표현하는 클래스 (DB의 Items 테이블과 매칭됨)
    public class Item
    {
        public int Id { get; set; }              // 고유 번호 (자동 증가)
        public string Name { get; set; } = string.Empty;      // 품목명 (예: 볼펜)
        public string Category { get; set; } = string.Empty;  // 카테고리 (예: 사무용품)
        public int Quantity { get; set; }         // 현재 재고 수량
        public decimal Price { get; set; }        // 단가
        public int MinThreshold { get; set; } = 5; // 이 수량 이하면 "부족" 표시 (기본값 5)
        public DateTime CreatedAt { get; set; } = DateTime.Now; // 등록된 날짜/시간
    }
}