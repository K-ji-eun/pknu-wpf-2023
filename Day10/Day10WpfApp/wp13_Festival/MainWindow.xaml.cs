using MahApps.Metro.Controls;
using MySqlConnector;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
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

        // 검색한 결과 DB(MySQL)에 저장
        private async void BtnSaveData_Click(object sender, RoutedEventArgs e)
        {
            if (GrdResult.Items.Count == 0)
            {
                await Commons.ShowMessageAsync("오류", "조회하고 저장하세요.");
                return; 
            }
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Commons.myConnString))
                {
                    if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
                    var query = @"INSERT INTO festivalin
                                            (
                                            UC_SEQ,
                                            MAIN_TITLE,
                                            GUGUN_NM,
                                            MAIN_PLACET,
                                            CNTCT_TEL,
                                            HOMEPAGE_URL)
                                            VALUES
                                            (
                                            @UC_SEQ,
                                            @MAIN_TITLE,
                                            @GUGUN_NM,
                                            @MAIN_PLACET,
                                            @CNTCT_TEL,
                                            @HOMEPAGE_URL)";

                    var insRes = 0;
                    foreach (var temp in GrdResult.Items)
                    {
                        if (temp is FestivalInfo)
                        {
                            var item = temp as FestivalInfo;

                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@UC_SEQ", item.UC_SEQ);
                            cmd.Parameters.AddWithValue("@MAIN_TITLE", item.MAIN_TITLE);
                            cmd.Parameters.AddWithValue("@GUGUN_NM", item.GUGUN_NM);
                            cmd.Parameters.AddWithValue("@MAIN_PLACET", item.MAIN_PLACET);
                            cmd.Parameters.AddWithValue("@CNTCT_TEL", item.CNTCT_TEL);
                            cmd.Parameters.AddWithValue("@HOMEPAGE_URL", item.HOMEPAGE_URL);

                            insRes += cmd.ExecuteNonQuery();
                        }
                    }
                    await Commons.ShowMessageAsync("저장", "DB 저장 성공");
                }
            }
            catch (Exception ex)
            {
                await Commons.ShowMessageAsync("오류", $"DB저장 오류!{ex.Message}");
            }
        }


        private void TxtFestivalName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSearchFestival_Click(sender, e);
            }
        }

        private async void BtnSearchFestival_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtFestivalName.Text))
            {
                await Commons.ShowMessageAsync("검색", " 검색할 축제 정보를 입력하세요.");
                return;
            }

            this.DataContext = null;
            //TxtFestivalName.Text = string.Empty;
            //string txtsearch = TxtFestivalName.Text;

            List<FestivalInfo> list  = new List<FestivalInfo>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Commons.myConnString))
                {
                    if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
                    var query = $@"SELECT Id
                                       , UC_SEQ
                                       , MAIN_TITLE
                                       , GUGUN_NM
                                       , MAIN_PLACET
                                       , CNTCT_TEL
                                       , HOMEPAGE_URL
                                    FROM festivalin
                                    WHERE MAIN_TITLE LIKE '%{TxtFestivalName.Text}%'
                                    ORDER BY Id ASC";

                    var cmd = new MySqlCommand(query, conn);
                    var adapter = new MySqlDataAdapter(cmd);
                    var dSet = new DataSet();
                    adapter.Fill(dSet, "festivalin");

                    foreach (DataRow dr in dSet.Tables["festivalin"].Rows)
                    {
                        list.Add(new FestivalInfo
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            UC_SEQ = Convert.ToInt32(dr["UC_SEQ"]),
                            MAIN_TITLE = Convert.ToString(dr["MAIN_TITLE"]),
                            GUGUN_NM = Convert.ToString(dr["GUGUN_NM"]),
                            MAIN_PLACET = Convert.ToString(dr["MAIN_PLACET"]),
                            CNTCT_TEL = Convert.ToString(dr["CNTCT_TEL"]),
                            HOMEPAGE_URL = Convert.ToString(dr["HOMEPAGE_URL"])
                        });
                    }

                    this.DataContext = list;
                }
            }
            catch(Exception ex)
            {
                await Commons.ShowMessageAsync("오류", $"DB검색 오류 {ex.Message}");
            }
        }
    }
}
