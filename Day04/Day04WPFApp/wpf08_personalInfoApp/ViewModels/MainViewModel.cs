using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf08_personalInfoApp.ViewModels
{
    internal class MainViewModel:ViewModelBase
    {
        //View에서 사용할 멤버 변수
        
        //입력 변수 
        private string inFirstName;
        private string inLastName;
        private string inEmail;
        private DateTime inDate;

        //출력 변수
        private string outFirstName;
        private string outLastName;
        private string outEmail;
        private string outDate;//생일 출력 시 문자열로

        private string outAdult;
        private string outBirthday;
        private string outZodiac;

        //입력 프로퍼티
        public string InFirstName 
        {
            get => inFirstName; 
            set
            {
                inFirstName = value;
                RaisePropertyChanged(nameof(InFirstName));
            }
        }
        public string InLastName
        {
            get => inLastName;
            set
            {
                inLastName = value;
                RaisePropertyChanged(nameof(InLastName));
            }
        }
        public string InEmail
        {
            get => inEmail;
            set
            {
                inEmail = value;
                RaisePropertyChanged(nameof(InEmail));
            }
        }
        public DateTime InDate 
        { 
            get => inDate; 
            set
            {
                inDate = value;
                RaisePropertyChanged(nameof(InDate));
            }
        }

        //출력 프로퍼티
        public string OutFirstName 
        { 
            get => outFirstName; 
            set
            {
                outFirstName = value;
                RaisePropertyChanged(nameof(OutFirstName));
            }
        }
        public string OutLastName 
        { 
            get => outLastName; 
            set
            {
                outLastName = value;
                RaisePropertyChanged(nameof(OutLastName));
            }
        }
        public string OutEmail 
        { 
            get => outEmail; 
            set
            {
                outEmail = value;
                RaisePropertyChanged(nameof(OutEmail));
            }
        }
        public string OutDate 
        { 
            get => outDate; 
            set
            {
                outDate = value;
                RaisePropertyChanged(nameof(OutDate));
            }
        }
        public string OutAdult 
        { 
            get => outAdult; 
            set
            {
                outAdult = value;
                RaisePropertyChanged(nameof(OutAdult));
            }
        }
        public string OutBirthday
        { 
            get => outBirthday; 
            set
            {
                outBirthday = value;
                RaisePropertyChanged(nameof(OutBirthday));
            }
        }
        public string OutZodiac 
        { 
            get => outZodiac; 
            set
            {
                outZodiac = value;
                RaisePropertyChanged(nameof(OutZodiac));
            }
        }
    }
}