using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf08_personalInfoApp.Logics;

namespace wpf08_personalInfoApp.Models
{
    internal class Person
    {        
        private string email;
        private DateTime date;

        public string FirstName { get; set; }//Auto Property
        public string LastName { get; set; }
        public string Email
        {
            get => email;
            set
            {
                if(Commons.IsValidEmail(value)!=true)
                {
                    throw new Exception("Invalid Email");
                }
                else
                {
                    email = value;
                }
            }
        }
        public DateTime Date 
        { 
            get => date; 
            set
            {
                var result = Commons.GetAge(value);
                if(result>120||result<0)
                {
                    throw new Exception("Invalid date");
                }
                else
                {
                    date = value;
                }
            }

        }
        public bool IsAdult
        {
            get
            {
                return Commons.GetAge(date) > 18;
            }
        }

        public bool IsBirthDay
        {
            get
            {
                return DateTime.Now.Month==date.Month&&
                    DateTime.Now.Day==date.Day;
            }
        }
        public string Zodiac
        {
            get => Commons.GetZodiac(date);
        }
        //MainWindow.xaml에서 사용자가 입력하는 부분 받을 생성자
        public Person(string firstName, string lastName, string email, DateTime date)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Date = date;
        }
    }
}
