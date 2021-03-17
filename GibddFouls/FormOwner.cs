using GibddFouls.Classes;
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
    public partial class FormOwner : Form
    {
        public DbContext dbContext;
        public int IdOwner;
        public FormOwner()
        {
            InitializeComponent();
            
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            dbContext = DbContext.GetInstance();
            if (IdOwner > 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    //удаление
                    dbContext.DeleteData(IdOwner, "owners");
                    Close();
                }
            }
        }
    }
}
