using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using wp09_caliburnApp.Models;

namespace wp09_caliburnApp.ViewModels
{
    public class MainViewModel : Screen
    {
        // Caliburn version업으로 변경
        private string firstName = "JiEun";

        public string FirstName { 
            get => firstName;
            set
            {
                firstName = value;
                NotifyOfPropertyChange(() => FirstName); // 속성값이 변경된걸 시스템에 알려주는 역할
                NotifyOfPropertyChange(nameof(CanClearName)); // 속성값이 변경된걸 시스템에 알려주는 역할
                NotifyOfPropertyChange(nameof(FullName)); // 속성값이 변경된걸 시스템에 알려주는 역할
            }
        }

        private string lastName = "Kwak";

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => FullName); // 변화 통보
            }
        }

        public string FullName
        {
            get => $"{LastName} {FirstName}";
        }

        // 콤보박스에 바인딩할 속성
        // 이런 곳에서는 var를 쓸 수 업음
        private BindableCollection<Person> managers = new BindableCollection<Person>();

        public BindableCollection<Person> Managers
        { 
            get => managers;
            set => managers = value;
        }

        // 콤보박스에 선택된 값을 지정할 속성
        private Person selectedManager;

        public Person SelectedManager
        {
            get => selectedManager;
            set
            {
                selectedManager = value;
                LastName = selectedManager.LastName;
                FirstName = selectedManager.FirstName;
                NotifyOfPropertyChange(nameof(SelectedManager));
            }
        }

        public MainViewModel()
        {
            // DB를 사용하면 여기서 DB접속 > 데이터 Select까지...
            Managers.Add(new Person {  FirstName = "John", LastName = "Carmack" });
            Managers.Add(new Person {  FirstName = "Steve", LastName = "Jobs" });
            Managers.Add(new Person {  FirstName = "Bill", LastName = "Gates" });
            Managers.Add(new Person {  FirstName = "Elon", LastName = "Musk" });
        }

        public void ClearName()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
        }

        // 메서드와 이름 동일하게 앞에 Can을 붙임
        public bool CanClearName
        {
            get => !(String.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName));
        }
    }
}
