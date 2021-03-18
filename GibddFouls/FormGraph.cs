using GibddFouls.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GibddFouls
{
    public partial class FormGraph : Form
    {
        public FoulsRep foulsRep;
        public FormGraph()
        {
            InitializeComponent();
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormGraph_Load(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Series.Add("series1");
            chart1.Series[0].XValueType = ChartValueType.DateTime;
            foreach ( var item in foulsRep.Data)
            {

                double yValue = Convert.ToDouble(item.Value);
                DataPoint dataPoint = new DataPoint(item.xdata.ToOADate(), yValue);

                chart1.Series[0].Points.Add(dataPoint);
            }
            
        }
    }
}
