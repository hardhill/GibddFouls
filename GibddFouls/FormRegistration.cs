using GibddFouls.Classes;
using GibddFouls.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GibddFouls
{
    public partial class FormRegistration : Form
    {
        DbContext dbContext;
        public int IdReg;
        public int OwnerId;
        public int CarId;
        private Validator validator;
        public FormRegistration()
        {
            InitializeComponent();
            dbContext = DbContext.GetInstance();
            List<Car> cars = dbContext.GetCars("");
            foreach (var item in cars)
            {
                cbCars.Items.Add(item);
            }
            
            validator = new Validator();
        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {
            TextBox thisText = (sender as TextBox);
            validator.NumberCorrect = false;
            if(thisText.TextLength == 6){
                validator.NumberCorrect = true;
            }
            thisText.ForeColor = validator.NumberCorrect ? Color.Green : Color.Red;
            bSave.Enabled = validator.Check();
        }

        class Validator
        {
            public bool NumberCorrect = false;
            public bool OwnerCorrect = false;
            public bool CarCorrect = false;
            public bool Check()
            {
                return NumberCorrect && OwnerCorrect && CarCorrect;
            }
        }

        private void txtOwner_TextChanged(object sender, EventArgs e)
        {
            string tm = "[^a-zA-Z0-9]";
            validator.OwnerCorrect = (Regex.IsMatch(txtOwner.Text, tm) && txtOwner.TextLength > 3);
            txtOwner.ForeColor = validator.OwnerCorrect ? Color.Green : Color.Red;
            bSave.Enabled = validator.Check();
        }

        

        private void cbCars_SelectedIndexChanged(object sender, EventArgs e)
        {
            validator.CarCorrect = false;
            if (cbCars.SelectedIndex >= 0)
            {
                cbCars.Text = cbCars.Items[cbCars.SelectedIndex].ToString();
                CarId = ((Car)cbCars.SelectedItem).IdCar;
                validator.CarCorrect = true;

            }
            cbCars.ForeColor = validator.CarCorrect ? Color.Green : Color.Red;
            bSave.Enabled = validator.Check();
        }

        private void FormRegistration_Load(object sender, EventArgs e)
        {
            bSave.Enabled = validator.Check();
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            
            if (IdReg > 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    //удаление
                    dbContext.DeleteData(IdReg, "registration");
                    Close();
                }
            }
        }
    }
}
