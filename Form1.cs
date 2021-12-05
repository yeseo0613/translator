using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace 번역기_프로젝트
{
    public partial class Form1 : Form
    {
        bool direct = false; // false : 한국 -> 다른 언어, true : 다른 언어 -> 한국
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "https://openapi.naver.com/v1/papago/n2mt";

            WebRequest wr = WebRequest.Create(url);
            wr.Headers.Add("X-Naver-Client-ID", apiCode.client_id);
            wr.Headers.Add("X-Naver-Client-Secret", apiCode.client_secret);
            wr.Method = "POST";
            string query; // 번역할 문장
            string source; // 출발 언어
            string target; // 도착 언어

            if (direct)
            {
                // 다른 언어 -> 한국어
                if (comboBox1.Text == "영어")
                {
                    source = "en";
                }
                else if (comboBox1.Text == "일본어")
                {
                    source = "ja";
                }
                else if (comboBox1.Text == "중국어 간체")
                {
                    source = "zh-CN";
                }
                else if (comboBox1.Text == "중국어 번체")
                {
                    source = "zh-TW";
                }
                else if (comboBox1.Text == "베트남어")
                {
                    source = "vi";
                }
                else if (comboBox1.Text == "인도네시아어")
                {
                    source = "id";
                }
                else if (comboBox1.Text == "태국어")
                {
                    source = "th";
                }
                else if (comboBox1.Text == "독일어")
                {
                    source = "de";
                }
                else if (comboBox1.Text == "러시아어")
                {
                    source = "ru";
                }
                else if (comboBox1.Text == "스페인어")
                {
                    source = "es";
                }
                else if (comboBox1.Text == "이탈리아어")
                {
                    source = "it";
                }
                else
                {
                    source = "fr";
                }
                query = richTextBox2.Text;
                target = "ko";
            }
            else
            {
                // 한국어 -> 다른언어
                if (comboBox1.Text == "영어")
                {
                    target = "en";
                }
                else if (comboBox1.Text == "일본어")
                {
                    target = "ja";
                }
                else if (comboBox1.Text == "중국어 간체")
                {
                    target = "zh-CN";
                }
                else if (comboBox1.Text == "중국어 번체")
                {
                    target = "zh-TW";
                }
                else if (comboBox1.Text == "베트남어")
                {
                    target = "vi";
                }
                else if (comboBox1.Text == "인도네시아어")
                {
                    target = "id";
                }
                else if (comboBox1.Text == "태국어")
                {
                    target = "th";
                }
                else if (comboBox1.Text == "독일어")
                {
                    target = "de";
                }
                else if (comboBox1.Text == "러시아어")
                {
                    target = "ru";
                }
                else if (comboBox1.Text == "스페인어")
                {
                    target = "es";
                }
                else if (comboBox1.Text == "이탈리아어")
                {
                    target = "it";
                }
                else
                {
                    target = "fr";
                }
                query = richTextBox1.Text;
                source = "ko";
            }

            // ()안에 들어있는 것을 byteArray로 바꾸어줌
            byte[] byteDataParams = Encoding.UTF8.GetBytes("source=" + source + "&target=" + target + "&text=" + query);  // source=출발지 언어, target=목적지 언어, text=번역하고자 하는 문장 
            wr.ContentType = "application/x-www-form-urlencoded";
            wr.ContentLength = byteDataParams.Length;

            // client 에서 server 쪽으로 요청
            Stream st = wr.GetRequestStream();
            st.Write(byteDataParams, 0, byteDataParams.Length);
            st.Close();

            WebResponse wrs = wr.GetResponse();

            Stream stream = wrs.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string text = reader.ReadToEnd();
            stream.Close();
            wrs.Close();
            reader.Close();
            Console.WriteLine(text);

            JObject json = JObject.Parse(text);

            if (direct)
            {
                richTextBox1.Text = json["message"]["result"]["translatedText"].ToString();
            }
            else
            {
                richTextBox2.Text = json["message"]["result"]["translatedText"].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(direct)
            {
                // 한국어 -> 다른 언어
                button2.Text = "▶";
                direct = false;
            }
            else
            {
                // 다른 언어 -> 한국어
                button2.Text = "◀";
                direct = true;
            }
        }

    }
}
