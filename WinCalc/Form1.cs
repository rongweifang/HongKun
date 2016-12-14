using KunTaiServiceLibrary;
using KunTaiServiceLibrary.controllers.pushOrder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinCalc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
        public bool IsExpriredByDay(DateTime joinDate, double duration)
        {
            return DateTime.Now - joinDate > TimeSpan.FromDays(duration);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            var result = IsExpriredByDay(new DateTime(2017, 1, 1), 5);
            if (result)
            {
                this.Close();
            }
         }

        private void button1_Click(object sender, EventArgs e)
        {
            double maxValue = Convert.ToDouble(TB_maxValue.Text.Trim());//最大温度值
            double minValue = Convert.ToDouble(TB_minValue.Text);//最小温度值
            OrderAlgorithm OA = new OrderAlgorithm();
            OA.MaxValue = maxValue;
            OA.MinValue = minValue;
            OA.Area = Decimal.Parse(TB_Area.Text);
            OA.Tonnage = int.Parse(TB_Tonnage.Text);
            OA.Target = int.Parse(TB_Target.Text);
            OA.BoilerCount = int.Parse(TB_BoilerCount.Text);
            OA.Power = int.Parse(TB_Power.Text);
            //OA.Scontent = (float)dr["Scontent"];
            OA.Efficiency = Decimal.Parse(TB_Efficiency.Text);
            // OA.Calorie = (float)dr["Calorie"];
            TB_Coal.Text = OA.GetCoalTotal().ToString();
            TB_Water.Text= OA.GetWaterGL().ToString();
            TB_Ele.Text= OA.GetEleGL().ToString();
            TB_Alkali.Text= OA.GetAlkali().ToString();
            

            TB_GJ.Text = OA.GetHeadGJ().ToString();

            decimal instructTime = OA.GetRunDate();
            TB_instructTime.Text = instructTime.ToString();

            LB_H.Text = string.Format("面积（{0}）*热负荷（{1}）*小时（24）*（18-平均温度{2}/（18+21））=GJ（{3}）",OA.Area,OA.Target, (decimal)(Convert.ToDouble(maxValue) + Convert.ToDouble(minValue)) / 2,OA.GetLoadDay());
            LB_C.Text = string.Format("GJ({0})/277777.78*238900/煤燃烧值（5133）={1}/1000(T)={2}T", OA.GetLoadDay(),OA._CoalTotal(),OA.GetCoalTotal());
            LB_T.Text = string.Format("耗煤量({0})*1000(KG)/133/吨位({1})={2}",OA.GetCoalTotal(), TB_Tonnage.Text,OA.GetRunDate());
            LB_E.Text = string.Format("运行时间（{0}）*锅炉功率（{1})={2}", OA.GetRunDate(),OA.Power,OA.GetEleGL());
            LB_W.Text = string.Format("耗煤量({0})*0.14={1}", OA.GetCoalTotal(), OA.GetWaterGL());
            LB_S.Text = string.Format("硫=煤（{0}）*0.52%（煤的含硫量）={1}；碱=硫（{2}） *2.5/95%(碱的纯度)*0.2= {3}",OA.GetCoalTotal(), OA.GetCoalTotal() * 0.006M, OA.GetCoalTotal() * 0.006M, OA.GetCoalTotal() * 0.006M* 2.5M/0.95M*0.2M);
            LB_S.Text += string.Format("；盐= 耗水（{0}）*0.41= {1}", OA.GetWaterTotal(), OA.GetSalt()*45/50);
            //==========================================================================
            OA.Area = Decimal.Parse(M1.Text);//面积
            OA.Target = int.Parse(T1.Text);//热负荷
            OA.Power = Decimal.Parse(P1.Text);
            OA.CYCLEFLOW = Decimal.Parse(F1.Text);
            OA.Efficiency = Decimal.Parse(X1.Text);
            OA.PathFlow = decimal.Parse(F11.Text);
            OA.PatchPower = decimal.Parse(P11.Text.Trim());
            OA.PatchEfficiency = decimal.Parse(X11.Text.Trim());
            OA.Ratio =1m;
            G1.Text = OA.GetHeadGJ().ToString();
            W1.Text = OA.GetWaterTotal().ToString();
            H1.Text = OA.GetPumpDate(OA.GetWaterTotal()).ToString();
            E1.Text= OA.GetGuoLuEle().ToString();
           // LB_S.Text += string.Format("；盐= 耗水（{0}）*0.41= {1}", OA.GetWaterTotal(), OA.GetSalt());
            TB_Salt.Text = OA.GetSalt().ToString();
            //===========================================================================
            OA.Area = Decimal.Parse(M2.Text);//面积
            OA.Target = int.Parse(T2.Text);//热负荷
            OA.Power = Decimal.Parse(P2.Text);
            OA.CYCLEFLOW = Decimal.Parse(F2.Text);
            OA.Efficiency = Decimal.Parse(X2.Text);
            OA.PathFlow = decimal.Parse(F21.Text);
            OA.PatchPower = decimal.Parse(P21.Text.Trim());
            OA.PatchEfficiency = decimal.Parse(X21.Text.Trim());
            OA.Ratio = 1m;
            G2.Text = OA.GetHeadGJ().ToString();
            W2.Text = OA.GetWater2().ToString();
            H2.Text = OA.GetStationRunDate().ToString();
            E2.Text = OA.GetStationEle().ToString();
            //面积（{0}）*热负荷（{1}）*小时（24）*（18-平均温度{2}/（18+21））=GJ（{3}）
            //100*q({0}/4.2*0.003={1}
            LB_Y_H.Text = string.Format("面积（{0}）*热负荷（{1}）*小时（24）*（18-平均温度{2}/（18+21））/277777.78=GJ（{3}）", OA.Area,OA.Target, (decimal)(Convert.ToDouble(maxValue) + Convert.ToDouble(minValue)) / 2, OA.GetLoadDay()/ 277777.78m);
            LB_Y_W.Text = string.Format("100*q({0})/4.2*0.003={1}", OA.GetHeadGJ(), OA.GetHeadGJ()*100*0.003m/4.2m);
            LB_Y_T.Text = string.Format("补水量（{0}）/（流量（{1}）*效率（{2}））={3}",OA.GetAllWater2(),OA.CYCLEFLOW,OA.Efficiency, OA.GetAllWater2()/(OA.CYCLEFLOW* OA.Efficiency));
            LB_Y_E.Text = string.Format("时间（{0}）*[循环泵功率（{1}）*效率（{2}）+补水泵功率({3})*效率({4})]={5}",+
                + OA.GetStationRunDate(),OA.Power,OA.Efficiency,OA.PatchPower,OA.PatchEfficiency, +
                +OA.GetStationRunDate()*(OA.Power* OA.Efficiency+ OA.PatchPower* OA.PatchEfficiency));
            //===========================================================================
            OA.Area = Decimal.Parse(M3.Text);//面积
            OA.Target = int.Parse(T3.Text);//热负荷
            OA.Power = Decimal.Parse(P3.Text);
            OA.CYCLEFLOW = Decimal.Parse(F3.Text);
            OA.Efficiency = Decimal.Parse(X3.Text);
            OA.PathFlow = decimal.Parse(F31.Text);
            OA.PatchPower = decimal.Parse(P31.Text.Trim());
            OA.PatchEfficiency = decimal.Parse(X31.Text.Trim());
            OA.Ratio = 1m;
            G3.Text = OA.GetHeadGJ().ToString();
            W3.Text = OA.GetWater2().ToString();
            H3.Text = OA.GetStationRunDate().ToString();
            E3.Text = OA.GetStationEle().ToString();
            //===========================================================================
            OA.Area = Decimal.Parse(M4.Text);//面积
            OA.Target = int.Parse(T4.Text);//热负荷
            OA.Power = Decimal.Parse(P4.Text);
            OA.CYCLEFLOW = Decimal.Parse(F4.Text);
            OA.Efficiency = Decimal.Parse(X4.Text);
            OA.PathFlow = decimal.Parse(F41.Text);
            OA.PatchPower = decimal.Parse(P41.Text.Trim());
            OA.PatchEfficiency = decimal.Parse(X41.Text.Trim());
            OA.Ratio = 1m;
            G4.Text = OA.GetHeadGJ().ToString();
            W4.Text = OA.GetWater2().ToString();
            H4.Text = OA.GetStationRunDate().ToString();
            E4.Text = OA.GetStationEle().ToString();
            //===========================================================================
            OA.Area = Decimal.Parse(M5.Text);//面积
            OA.Target = int.Parse(T5.Text);//热负荷
            OA.Power = Decimal.Parse(P5.Text);
            OA.CYCLEFLOW = Decimal.Parse(F5.Text);
            OA.Efficiency = Decimal.Parse(X5.Text);
            OA.PathFlow = decimal.Parse(F51.Text);
            OA.PatchPower = decimal.Parse(P51.Text.Trim());
            OA.PatchEfficiency = decimal.Parse(X21.Text.Trim());
            OA.Ratio = 1m;
            G5.Text = OA.GetHeadGJ().ToString();
            W5.Text = OA.GetWater2().ToString();
            H5.Text = OA.GetStationRunDate().ToString();
            E5.Text = OA.GetStationEle().ToString();


        }
    }
}
