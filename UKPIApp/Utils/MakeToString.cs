using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.Utils
{
    public class MakeToString
    {
        #region Các trường dữ liệu
        /*Fields 1: Dữ liệu chính cần thiết lập*/
        private double Value { get; set; }               // Giá trị dãy số
        private string StrNum { get; set; }              // Giá trị dãy số dạng string
        private int Lengths { get; set; }                // Chiều dài của dãy số

        /*Fields 2: Dữ liệu chứa các từ, cụm từ dùng cho việc chuyển đổi Chữ*/
        private readonly string[] _str = { Readzero[0], Readone[0], "hai", "ba", "bốn", Readfive[1], "sáu", "bảy", "tám", "chín" };         // Mảng đọc 0 tới 9
        private static readonly string[] Readzero = { "", "lẻ", "không trăm", "mươi", "trăm", "không trăm lẻ" };       // Mảng đọc các trường hợp của 0
        private static readonly string[] Units = { "nghìn", "triệu", "tỷ" };         // Mảng đọc bậc đơn vị
        private static readonly string[] Readone = { "một", "mười", "mốt" };        // Mảng đọc các trường hợp của 1
        private static readonly string[] Readfive = { "năm", "lăm" };  // Mảng đọc các trường hợp của 5 
        private const string Space = " "; // Khoảng trắng
        public readonly int[] BlockNum = { 0, 0, 0, 0, 0 };       // Mảng chứa dãy số sau khi cắt thành từng 3 số một. Tối đa là 5 Block
        private readonly string[] _stringBlock = { "", "", "", "", "" };     // Mảng chứa chuỗi đọc Block[i] tương ứng 1 2 3 4 5
        private readonly int[] _flagBlock = { 0, 0, 0, 0, 0 };           // Cờ hiệu xem xét Block[i] có bằng (0000). Giá trị 1 vs 0


        /*Fields 3: Các giá trị dùng cho phép thử. Không cần thiết phải tồn tại toàn cục*/
        private const int A = 10; //10      // Các biến dùng để đối chiếu, so sánh
        private const int B = 100; //100     // Không quan trọng, bạn có thể thay thế chúng theo cách của bạn
        private const int C = 1000; //1000    // Sẽ Fix  . . .
        private const int D = 1000000; //C*C
        private const int E = 1000000000; //D*C
        private const double F = 1000000000000; //D*D
        private const double G = 999999999999999; // Giá trị tối đa có thể đọc: 15 chữ số

        #endregion

        /// <Constructor>
        /// Hàm khởi tạo All in one. Giá trị cần có là dãy số bạn cần chuyển đổi 
        /// </Constructor>
        /// <param name="value">Nhập vào giá trị cần chuyển đổi</param>
        public MakeToString(Double value)
        {
            Value = value;
            StrNum = Convert.ToString(Value);
            Lengths = StrNum.Length;
        }//End Constructor

        /*
         *  Hàm xử lý đọc từng block. Gọi hàm ReadBlock để xử lý
         */

        /// <ReadBlockFirst>
        /// Hàm đọc khối Block nghìn tỷ
        /// Nếu Giá trị bằng 0 thì bỏ qua.
        /// </ReadBlockFirst>
        private void ReadBlockFirst()
        {
            if (BlockNum[0] <= 0) return;
            _flagBlock[0] = 1;
            _stringBlock[0] = ReadThisBlock(BlockNum[0]) + Space + Units[0] + Space;
        }

        /// <ReadBlockSecond>
        /// Hàm đọc Block đơn vị tỷ
        /// Nếu BlockFist không có (==0) thì đọc bình thường
        /// Ngược lại:
        ///         Áp dụng kiểu đọc
        ///         - Cộng thêm "Không trăm lẻ" nếu có 1 chữ số
        ///         - Cộng thêm "không trăm" nếu có 2 chữ số
        ///         - Đọc bình thường nếu có 3 chữ số  
        /// </ReadBlockSecond>
        private void ReadBlockSecond()
        {
            if (BlockNum[1] == 0 & BlockNum[0] != 0)
            {
                _stringBlock[1] = Space + Units[2] + Space;
            }
            if (BlockNum[1] <= 0) return;
            _flagBlock[1] = 1;
            if (_flagBlock[0] == 0)
            {
                _stringBlock[1] = ReadThisBlock(BlockNum[1]) + Space + Units[2] + Space;
            }

            if (_flagBlock[0] != 1) return;
            if (BlockNum[1] < A)
            {
                _stringBlock[1] = Readzero[5] + Space + ReadThisBlock(BlockNum[1]) + Space + Units[2] + Space;
            }
            if (BlockNum[1] < B & BlockNum[1] >= A)
            {
                _stringBlock[1] = Readzero[2] + Space + ReadThisBlock(BlockNum[1]) + Space + Units[2] + Space;
            }
            if (!(BlockNum[1] < C & BlockNum[1] >= B)) return;
            _stringBlock[1] = ReadThisBlock(BlockNum[1]) + Space + Units[2] + Space;
        }

        private void ReadBlockThird()
        {
            if (BlockNum[2] <= 0) return;
            _flagBlock[2] = 1;
            if (_flagBlock[0] == 0 & _flagBlock[1] == 0)
            {
                _stringBlock[2] = ReadThisBlock(BlockNum[2]) + Space + Units[1] + Space;
            }
            if (_flagBlock[0] != 1 && _flagBlock[1] != 1) return;
            if (BlockNum[2] < A)
            {
                _stringBlock[2] = Readzero[5] + Space + ReadThisBlock(BlockNum[2]) + Space + Units[1] + Space;
            }
            if (BlockNum[2] < B & BlockNum[2] >= A)
            {
                _stringBlock[2] = Readzero[2] + Space + ReadThisBlock(BlockNum[2]) + Space + Units[1] + Space;
            }
            if (BlockNum[2] < C & BlockNum[2] >= B)
            {
                _stringBlock[2] = ReadThisBlock(BlockNum[2]) + Space + Units[1] + Space;
            }
        }

        private void ReadBlockFourth()
        {
            if (BlockNum[3] <= 0) return;
            _flagBlock[3] = 1;
            if (_flagBlock[0] == 0 & _flagBlock[1] == 0 & _flagBlock[2] == 0)
            {
                _stringBlock[3] = ReadThisBlock(BlockNum[3]) + Space + Units[0] + Space;
            }
            if (_flagBlock[0] != 1 && _flagBlock[1] != 1 && _flagBlock[2] != 1) return;
            if (BlockNum[3] < A)
            {
                _stringBlock[3] = Readzero[5] + Space + ReadThisBlock(BlockNum[3]) + Space + Units[0] + Space;
            }
            if (BlockNum[3] < B & BlockNum[3] >= A)
            {
                _stringBlock[3] = Readzero[2] + Space + ReadThisBlock(BlockNum[3]) + Space + Units[0] + Space;
            }
            if (BlockNum[3] < C & BlockNum[3] >= B)
            {
                _stringBlock[3] = ReadThisBlock(BlockNum[3]) + Space + Units[0] + Space;
            }
        }

        private void ReadBlockFifth()
        {
            if (BlockNum[4] <= 0) return;
            _flagBlock[4] = 1;
            if (_flagBlock[0] == 0 & _flagBlock[1] == 0 & _flagBlock[2] == 0 & _flagBlock[3] == 0)
            {
                _stringBlock[4] = ReadThisBlock(BlockNum[4]);
            }
            if (_flagBlock[0] != 1 && _flagBlock[1] != 1 && _flagBlock[2] != 1 && _flagBlock[3] != 1) return;
            if (BlockNum[4] < A)
            {
                _stringBlock[4] = Readzero[5] + Space + ReadThisBlock(BlockNum[4]);
            }
            if (BlockNum[4] < B & BlockNum[4] >= A)
            {
                _stringBlock[4] = Readzero[2] + Space + ReadThisBlock(BlockNum[4]);
            }
            if (BlockNum[4] < C & BlockNum[4] >= B)
            {
                _stringBlock[4] = ReadThisBlock(BlockNum[4]);
            }
        }

        public string ReadThis()
        {
            ReadBlockFirst();
            ReadBlockSecond();
            ReadBlockThird();
            ReadBlockFourth();
            ReadBlockFifth();
            var temp = _stringBlock[0]
                    + _stringBlock[1]
                    + _stringBlock[2]
                    + _stringBlock[3]
                    + _stringBlock[4];

            return temp.Substring(0, 1).ToUpper() + temp.Substring(1, temp.Length - 1);

        }

        /// <BlockProcessing>
        /// Cắt dãy số 1 đến 15 chữ số thành các khối tương ứng. Mỗi 3 số thành một khối . . .(Block)
        /// </BlockProcessing>
        public void BlockProcessing()
        {
            if (Lengths > 15) return; //Điều kiện tiên quyết 
            #region Block 1000 Billion
            if (Value <= G & Value >= F)
            {
                //999.111.111.111.111
                BlockNum[0] = Convert.ToInt32(StrNum.Substring(0, Lengths - 12));
            }//Done
            #endregion
            #region Block Bilion
            if (Value >= E)
            {
                /*111.999.111.111.111*/
                var temp = BlockNum[0] > 0
                    ? Convert.ToString(BlockNum[0]).Length
                    : 0;
                BlockNum[1] = Convert.ToInt32(StrNum.Substring(temp, Lengths - 9 - temp));
            }//Done
            #endregion
            #region Block Million
            if (Value >= D)
            {
                /*111.111.999.111.111*/
                var temp = BlockNum[0] + BlockNum[1] > 0
                    ? Convert.ToString(StrNum.Substring(0, Lengths - 9)).Length
                    : 0;
                BlockNum[2] = Convert.ToInt32(StrNum.Substring(temp, Lengths - 6 - temp));
            }
            #endregion
            #region Block Thousand
            if (Value >= C)
            {
                /*111.111.111.999.111*/
                var temp = BlockNum[0] + BlockNum[1] + BlockNum[2] > 0
                    ? Convert.ToString(StrNum.Substring(0, Lengths - 6)).Length
                    : 0;
                BlockNum[3] = Convert.ToInt32(StrNum.Substring(temp, Lengths - 3 - temp));
            }
            #endregion
            #region Block Hundred

            if (!(Value > 0)) return;
            var temporary = BlockNum[0] + BlockNum[1] + BlockNum[2] + BlockNum[3] > 0
                ? Convert.ToString(StrNum.Substring(0, Lengths - 3)).Length
                : 0;
            BlockNum[4] = Convert.ToInt32(StrNum.Substring(temporary, Lengths - temporary));
            /*111.111.111.111.999*/
            #endregion
        }   //End BlockProcessing - Done

        /// <ReadOneNumeral>
        /// Hàm này có nhiệm vụ đọc các số 1 chữ số
        /// </ReadOneNumeral>
        /// <param name="v">Số nhập vào</param>
        /// <returns>String</returns>
        private string ReadOneNumeral(int v)
        {
            var temp = "Giá trị phải một chữ số";
            if (!(v < A & v >= 0)) return temp;
            for (var i = 0; i < A; i++)
            {
                temp = v == 5 ? Readfive[0] : _str[v]; // Đọc các số bình thường trừ 5 và 0
            }
            return temp;
        }//End Read One Numeral

        /// <ReadTwoNumeral>
        /// Hàm này có nhiệm vụ đọc các số 2 chữ số
        /// </ReadTwoNumeral>
        /// <param name="v">Số nhập vào</param>
        /// <returns>String</returns>
        private string ReadTwoNumeral(int v)
        {
            var temp = "Giá trị phải 2 chữ số !!!";
            if (!(v < A * A & v >= A)) return temp;

            #region _x0
            if (v % A == 0)    // chia hết cho mười
            {
                temp = v > A ? (v / A == 5 ? Readfive[0] : _str[v / A]) + Space + Readzero[3] : Readone[1];
            }
            #endregion
            #region _xx
            if (v % A != 0)        // không chia hết cho mười
            {
                temp = v > A + 9 & v / 10 != 5
                    ? _str[v / A] + Space + Readzero[3]
                      + Space + (v % A > 1
                          ? _str[v % A]
                          : Readone[2])
                    : v / 10 == 5 ? Readfive[0] + Space + Readzero[3]
                                    + Space + (v % 10 == 1 ? Readone[2] : _str[v % 10]) : Readone[1] + Space + _str[v % A];
            }
            #endregion

            return temp;
        }//End Read Two Numeral

        /// <ReadThreeNumeral>
        /// Hàm này có nhiệm vụ đọc các số có 3 chữ số
        /// </ReadThreeNumeral>
        /// <param name="v">Số nhập vào</param>
        /// <returns>String</returns>
        private string ReadThreeNumeral(int v)
        {
            /*Xét 3 trường hợp của số có 4 chữ số: x00 -xx0- x0x - xxx với x là các số bất kỳ khác 0*/
            var temp = "Giá trị phải 3 chữ số !";
            if (!(v >= A & v < C)) return temp;

            if (v % A == 0) //=> x00: 100 200 300 . . .
            {
                temp = (v / B == 5 ? Readfive[0] : _str[v / B]) + Space + Readzero[4];
            }
            if (v % 100 % 10 == 0 & v % 100 / 10 != 0)  //=>xx0: 150 550
            {
                temp = v / 100 == 5
                    ? Readfive[0] + Space + Readzero[4] + Space + ReadTwoNumeral(v % 100)
                    : _str[v / 100] + Space + Readzero[4] + Space + ReadTwoNumeral(v % 100);
            }
            if (v % B / A == 0 & v % B % A != 0)   //=> x0x: 101 209 409 ...
            {
                temp = (v / B == 5 ? Readfive[0] : _str[v / B])
                    + Space + Readzero[4]
                    + Space + Readzero[1]
                    + Space + ReadOneNumeral(v % 100 % 10);


            }
            if (v % B / A != 0 & v % B % A != 0)  //=> xxx: 123 846 459 ...
            {
                temp = (v / B == 5 ? Readfive[0] : _str[v / B]) + Space
                       + Readzero[4] + Space + ReadTwoNumeral(v % B);
            }

            return temp;
        }//End Read Three Numeral - Only Read 3 numeral

        /// <ReadThisBlock>
        /// Hàm này gọi 3 hàm ReadOne, ReadTwo, ReadThree để đọc một số bất kỳ 1 đến 3 chữ số
        /// </ReadThisBlock>
        /// <param name="n"></param>
        /// <returns>String</returns>
        private string ReadThisBlock(int n)
        {
            var temp = "";
            if (n > 0 & n < A)
            {
                temp = ReadOneNumeral(n);
            }
            if (n >= A & n < B)
            {
                temp = ReadTwoNumeral(n);
            }
            if (n >= B & n < C)
            {
                temp = ReadThreeNumeral(n);
            }
            return temp;
        } // End Read This Block - Read Full Blocks

    }
}
