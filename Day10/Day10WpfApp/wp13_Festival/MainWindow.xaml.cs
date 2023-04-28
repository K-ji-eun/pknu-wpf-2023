using MahApps.Metro.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using wp13_Festival.Logics;
using wp13_Festival.Models;

namespace wp13_Festival
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnFestival_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(TxtFestivalName.Text))
            {
                await Commons.ShowMessageAsync("검색", " 검색할 축제 정보를 입력하세요.");
                return;
            }


            if (TxtFestivalName.Text.Length <= 2)
            {
                await Commons.ShowMessageAsync("검색", "검색어를 2자이상 입력하세요.");
                return;
            }

        }

        private async void BtnReqtime_Click(object sender, RoutedEventArgs e)
        {
            string apiKey = "eah4sO3GHeKxsDRxvzwAQMU5I5zDKlGRz6WPeocoIXYce8ptdka7EIB84Lod4N%2BhPh1UtODmsrTnXKa0Dvj%2F8g%3D%3D";
            string openApiUri = $"https://apis.data.go.kr/6260000/FestivalService/getFestivalKr?serviceKey={apiKey}" + $"&pageNo=1&numOfRows=1000&resultType=JSON";
            string result = string.Empty;

            // api 실행할 객체
            WebRequest req = null;
            WebResponse res = null;
            StreamReader reader = null;

            try
            {
                req = WebRequest.Create(openApiUri);
                res = await req.GetResponseAsync();
                reader = new StreamReader(res.GetResponseStream());
                result = reader.ReadToEnd();

                Debug.WriteLine(result);
            }
            catch (Exception ex)
            {
                await Commons.ShowMessageAsync("오류", $"OpenAPI 조회오류 {ex.Message}");
            }
            var jsonResult = JObject.Parse(result);
            var status = Convert.ToInt32(jsonResult["getFestivalKr"]["header"]["code"]);

            try
            {
                if (status == 00)
                {
                    var data = jsonResult["getFestivalKr"]["item"];
                    var json_array = data as JArray;

                    var FestivalInfos = new List<FestivalInfo>();
                    foreach ( var Info in json_array ) 
                    {
                        FestivalInfo temp = new FestivalInfo
                        {
                            Id = 0,
                            UC_SEQ = Convert.ToInt32(Info["UC_SEQ"]),
                            MAIN_TITLE = Convert.ToString(Info["MAIN_TITLE"]),
                            GUGUN_NM = Convert.ToString(Info["GUGUN_NM"]),
                            MAIN_PLACET = Convert.ToString(Info["MAIN_PLACET"]),
                            CNTCT_TEL = Convert.ToString(Info["CNTCT_TEL"]),
                            HOMEPAGE_URL = Convert.ToString(Info["HOMEPAGE_URL"])
                        };
                        FestivalInfos.Add(temp);
                    }

                    this.DataContext = FestivalInfos;
                }
            }
            catch (Exception ex)
            {
                await Commons.ShowMessageAsync("오류", $"JSON 처리오류 {ex.Message}");
            }


        }

        private void BtnSaveData_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TxtFestivalName_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
