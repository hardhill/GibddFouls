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
    public partial class FormVoc : Form
    {
        public int SelectedId;
        public EditMode EditMode;
        public FormVoc()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           Owner owner= (Owner)listBox1.SelectedItem;
           lblValue.Text = $"{owner.OwnerId} - {owner.OwnerName}";
            SelectedId = owner.OwnerId;
        }
    }
}
