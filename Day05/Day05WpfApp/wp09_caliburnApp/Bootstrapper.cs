using Caliburn.Micro;       //컬리번 마이크로안에 BootstrapperBase가 다 들어가 있습
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using wp09_caliburnApp.ViewModels;

namespace wp09_caliburnApp
{
    // Caliburn 으로 MVVM 실행할 때 주요설정 진행
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize(); // Caliburn MVVM 초기화
        }

        // 시작한 후에 초기화 진행
        protected async override void OnStartup(object sender, StartupEventArgs e)
        {
            //base.OnStartup(sender, e); // 부모클래스 실행은 주석처리
            await DisplayRootViewForAsync<MainViewModel>(); //메서드를 비동기로 유지 선택했는데 맞는지 모르겠다 추후 나비효과가 올 수 있으니 메모해두자// 내가한게 맞았다 ㅎ
        }
    }
}