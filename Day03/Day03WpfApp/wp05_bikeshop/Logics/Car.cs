using System.Windows.Media;

namespace wp05_bikeshop.Logics
{
    internal class Car : Notifier // 값이 바뀌는 걸 인지하여 처리
    {
        private string names {  get; set; }   // Auto Property
        public string Names
        {
            get => names;
            // 프로퍼티 변경하는 것
            set
            {
                names = value;
                OnPropertyChanged("Names"); // Names 프로퍼티가 바뀜!!!
            }
        }
        private double speed { get; set; }
        public double Speed { 
            get => speed;
            set
            {
                speed = value;
                OnPropertyChanged(nameof(Speed));   // Speed
            }
        }
        public Color Colorz { get; set; }
        //public Human Driver { get; set; }
    }
}
