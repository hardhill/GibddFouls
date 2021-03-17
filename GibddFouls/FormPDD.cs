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

namespace GibddFouls
{
    public partial class FormPDD : Form
    {
        public int IdFoul;
        public int IdReg;
        public int IdTypeFoul;
        DbContext dbContext;
        public FormPDD()
        {
            InitializeComponent();
            dbContext = DbContext.GetInstance();
            lblRegistration.Text = "<нет данных>";
        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {
            lblRegistration.Text = "<нет данных>";
            IdReg = 0;
            if (txtNumber.TextLength == 6)
            {
                List<VRegistration> vreg = dbContext.GetVRegistrations(txtNumber.Text);
                if (vreg.Count == 1)
                {
                    lblRegistration.Text = $"ТС: {vreg[0].Carmodel} [{vreg[0].Caryear}] владелец:{vreg[0].Owner}";
                        IdReg = vreg[0].Id;
                }
            }
            CheckValid();
        }

        private void cbTypeFoul_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTypeFoul.SelectedIndex >= 0) {
                FoulType foul = (FoulType)cbTypeFoul.SelectedItem;
                IdTypeFoul = foul.Id;
            }
            else
            {
                IdTypeFoul = 0;
            }
            CheckValid();
        }
        private void CheckValid()
        {
            bSave.Enabled = IdReg != 0 && IdTypeFoul != 0;
        }

        private void FormPDD_Load(object sender, EventArgs e)
        {
            List<FoulType> ftList = dbContext.GetFoulsType("");
            foreach (var item in ftList)
            {
                cbTypeFoul.Items.Add(item);
            }
            if (IdTypeFoul == 0)
            {
                cbTypeFoul.SelectedIndex = -1;
            }
            else
            {
               for(var i = 0; i < cbTypeFoul.Items.Count; i++)
                {
                    FoulType item = (FoulType)cbTypeFoul.Items[i];
                    if (IdTypeFoul == item.Id)
                    {
                        cbTypeFoul.SelectedIndex = i;
                        break;
                    }
                }
            }
            List<VRegistration> vreg = dbContext.GetVRegistrations(txtNumber.Text);
            if (vreg.Count == 1)
            {
                lblRegistration.Text = $"ТС: {vreg[0].Carmodel} [{vreg[0].Caryear}] владелец:{vreg[0].Owner}";
                IdReg = vreg[0].Id;
            }
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            if (IdFoul > 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    //удаление
                    dbContext.DeleteData(IdFoul, "fouls");
                    Close();
                }
            }
        }
    }
}
