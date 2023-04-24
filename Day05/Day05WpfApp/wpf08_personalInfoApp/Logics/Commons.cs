using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace wpf08_personalInfoApp.Logics
{
    internal class Commons
    {
        public static bool IsValidEmail(string email)
        {
            //이메일 형식 체크
            //Regular Expression
            var strPattern = @"^([0-9a-zA-Z]+)@([0-9a-zA-Z]+)(\.[0-9a-zA-Z]+){1,}$";
            return Regex.IsMatch(email, strPattern); // 패턴 만족 시 true, 아니면 false return
        }
        public static int GetAge(DateTime value)
        {
            //입력된 날짜로 나이 계산
            int middle;
            if(DateTime.Now.Month<value.Month||DateTime.Now.Month==value.Month
                && DateTime.Now.Day<value.Day)
            {
                middle = DateTime.Now.Year - value.Year - 1;//생일 안지남
            }
            else
            {
                middle=DateTime.Now.Year-value.Year;//생일지남
            }
            return middle;
        }
        public static string GetZodiac(DateTime value)
        {
            //입력된 생일로 12지신 리턴
            int reminder = value.Year % 12;
            switch(reminder)
            {
                case 4:
                    return "쥐띠";
                case 5:
                    return "소띠";
                case 6:
                    return "호랑이띠";
                case 7:
                    return "토끼띠";
                case 8:
                    return "용띠";
                case 9:
                    return "뱀띠";
                case 10:
                    return "말띠";
                case 11:
                    return "양띠";
                case 0:
                    return "원숭이띠";
                case 1:
                    return "닭띠";
                case 2:
                    return "개띠";
                case 3:
                    return "돼지띠";
                default:
                    return "잡띠";
            }
        }
    }
}
