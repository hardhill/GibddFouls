using GibddFouls.Classes;
using GibddFouls.Models;
using GibddFouls.Properties;
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
    public partial class MainForm : Form
    {
        DbContext dbContext = DbContext.GetInstance();
        FormGraph formGraph;
        public MainForm()
        {
            InitializeComponent();
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //парамтры соединения берется из файла параметров
            DbContext.Connection = Settings.Default.dbconnect;
            //первоначально показать данные по нарушениям
            UpdateListFouls("");
            
        }

        private void bNewOwner_Click(object sender, EventArgs e)
        {
            //подготовить форму для новой записи таблицы "Владельцы"
            FormOwner formOwner = new FormOwner();
            formOwner.txtOwner.Text = txtFindOwner.Text;
            formOwner.IdOwner = 0;
            if(formOwner.ShowDialog() == DialogResult.OK)
            {
                //новую запись в таблицу владельцев автотранспорта
                dbContext.NewOwner(formOwner.txtOwner.Text);
                UpdateListOwners(txtFindOwner.Text);
            }
        }

        private void bFindOwner_Click(object sender, EventArgs e)
        {
            //поиск пользователей авто по условию
            UpdateListOwners(txtFindOwner.Text);
        }
        private void UpdateListOwners(string text)
        {
            //владельцы авто
            listBoxOwner.Items.Clear();
            List<Owner> list = dbContext.GetOwnersLike(text, true);
            foreach (var item in list)
            {
                listBoxOwner.Items.Add(item);
            }
            lblCountOwner.Text = dbContext.CountData("owners").ToString();
            lblFoundOwner.Text = list.Count().ToString();
        }
        private void UpdateListCars(string text)
        {
            listBoxCar.Items.Clear();
            List<Car> list = dbContext.GetCars(text);
            foreach (var item in list)
            {
                listBoxCar.Items.Add(item);
            }
            lblCountCar.Text = dbContext.CountData("carmodels").ToString();
            lblFoundCar.Text = list.Count().ToString();
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //смена закладок
            if(tabControl1.SelectedTab == tabPageOwners)
            {
                UpdateListOwners(txtFindOwner.Text);
            }
            else if(tabControl1.SelectedTab == tabPageCars)
            {
                //автомобили
                UpdateListCars(txtFindCar.Text);
            }
            else if(tabControl1.SelectedTab == tabPageFT)
            {
                //виды нарушений
                UpdateListFT(txtFindFT.Text);
            }
            else if(tabControl1.SelectedTab == tabPageRegCar)
            {
                //регистрация автомобилей
                UpdateListReg(txtNumber.Text);
            }
            else if(tabControl1.SelectedTab == tabPagePDDFouls)
            {
                UpdateListFouls(txtNumber1.Text);
            }
        }

        private void bNewCar_Click(object sender, EventArgs e)
        {
            FormCar formCar = new FormCar();
            formCar.txtCarname.Text = "";
            formCar.txtCaryear.Text = "";
            formCar.IdCar = 0;
            if (formCar.ShowDialog() == DialogResult.OK)
            {
                dbContext.NewCar(formCar.txtCarname.Text, formCar.txtCaryear.Text);
                UpdateListCars(txtFindCar.Text);
            }
            
        }

        private void bFindCar_Click(object sender, EventArgs e)
        {
            //поиск авто по условию
            UpdateListCars(txtFindCar.Text);
        }

        private void bNewFT_Click(object sender, EventArgs e)
        {
            FormFT formFT = new FormFT();
            formFT.Text = "";
            formFT.IdFT = 0;
            if (formFT.ShowDialog() == DialogResult.OK)
            {
                dbContext.NewFoulType(formFT.txtFT.Text);
            }
        }

        private void bFindFT_Click(object sender, EventArgs e)
        {
            UpdateListFT(txtFindFT.Text);
        }
        private void UpdateListFT(string text)
        {
            listBoxFT.Items.Clear();
            List<FoulType> foulTypes = dbContext.GetFoulsType(text);
            foreach (var item in foulTypes)
            {
                
                listBoxFT.Items.Add(item);
            }
            lblCountFT.Text = dbContext.CountData("typefouls").ToString();
            lblFoundFT.Text = foulTypes.Count().ToString();
        }
        private void UpdateListReg(string text)
        {
            listBoxReg.Items.Clear();
            List<VRegistration> regs = dbContext.GetVRegistrations(text);
            foreach(var item in regs)
            {
                listBoxReg.Items.Add(item);
            }
            lblCountReg.Text = dbContext.CountData("registration").ToString();
            lblFoundReg.Text = regs.Count().ToString();
        }
        private void UpdateListFouls(string text)
        {
            listBoxFouls.Items.Clear();
            List<VFoul> vfouls = dbContext.GetVFouls(text);
            foreach(var item in vfouls)
            {
                listBoxFouls.Items.Add(item);
            }
            lblCountFouls.Text = dbContext.CountData("fouls").ToString();
            lblFoundFouls.Text = vfouls.Count().ToString();

        }
        private void bNewNumber_Click(object sender, EventArgs e)
        {
            Registration newRegistration = new Registration();
            FormRegistration formRegistration = new FormRegistration();
            formRegistration.OwnerId = 0;
            formRegistration.CarId = 0;
            formRegistration.txtNumber.Text = "";
            if (formRegistration.ShowDialog() == DialogResult.OK)
            {
                //либо новый владелец либо находит по условию имеющегося
                int newIdOwner = dbContext.NewOwner(formRegistration.txtOwner.Text);
                Registration registration = new Registration();
                registration.CarId = formRegistration.CarId; registration.Number = formRegistration.txtNumber.Text; registration.OwnerId = newIdOwner;
                int newRegistr = dbContext.NewRegistration(registration);
            }
            UpdateListReg(txtNumber.Text);
        }

        private void bFindNum_Click(object sender, EventArgs e)
        {
            UpdateListReg(txtNumber.Text);
        }

        
        private void listBoxOwner_DoubleClick(object sender, EventArgs e)
        {
            Owner owner = (Owner)listBoxOwner.SelectedItem;
            FormOwner formOwner = new FormOwner();
            formOwner.IdOwner = owner.OwnerId;
            formOwner.txtOwner.Text = owner.OwnerName;
            if (formOwner.ShowDialog() == DialogResult.OK)
            {
                owner.OwnerName = formOwner.txtOwner.Text;
                dbContext.UpdateOwner(owner.OwnerId, owner.OwnerName);
                listBoxOwner.Items[listBoxOwner.SelectedIndex] = owner;
                
            }
            UpdateListOwners(txtFindOwner.Text);
        }

        private void listBoxCar_DoubleClick(object sender, EventArgs e)
        {
            Car car = (Car)listBoxCar.SelectedItem;
            FormCar formCar = new FormCar();
            formCar.txtCarname.Text = car.Carname;
            formCar.txtCaryear.Text = car.Caryear;
            formCar.IdCar = car.IdCar;
            if (formCar.ShowDialog() == DialogResult.OK)
            {
                car.Carname = formCar.txtCarname.Text;
                car.Caryear = formCar.txtCaryear.Text;
                dbContext.UpdateCar(car);
                listBoxCar.Items[listBoxCar.SelectedIndex] = car;
            }
            UpdateListCars(txtFindCar.Text);
        }

        private void listBoxFT_DoubleClick(object sender, EventArgs e)
        {
            FoulType foulType = (FoulType)listBoxFT.SelectedItem;
            FormFT formFT = new FormFT();
            formFT.IdFT = foulType.Id;
            formFT.txtFT.Text = foulType.Type;
            if (formFT.ShowDialog() == DialogResult.OK)
            {
                foulType.Type = formFT.txtFT.Text;
                dbContext.UpdateFoulType(foulType);
                listBoxFT.Items[listBoxFT.SelectedIndex] = foulType;
            }
            UpdateListFT(txtFindFT.Text);
        }

        private void listBoxReg_DoubleClick(object sender, EventArgs e)
        {
            VRegistration vreg = (VRegistration)listBoxReg.SelectedItem;
            int idx = listBoxReg.SelectedIndex;
            FormRegistration formRegistration = new FormRegistration();
            formRegistration.IdReg = vreg.Id;
            formRegistration.txtNumber.Text = vreg.Number;
            formRegistration.txtOwner.Text = vreg.Owner;
            formRegistration.CarId = vreg.CarId;
            formRegistration.OwnerId = vreg.OwnerId;
            var list = formRegistration.cbCars.Items;
            for(var i = 0; i < list.Count; i++)
            {
                if (list[i].ToString().Contains($"{vreg.Carmodel}\t[{vreg.Caryear}]"))
                {
                    formRegistration.cbCars.SelectedIndex = i;
                    break;
                }
            }
            
            if (formRegistration.ShowDialog() == DialogResult.OK)
            {
                int newIdOwner = dbContext.NewOwner(formRegistration.txtOwner.Text);
                Registration registration = new Registration();
                registration.CarId = formRegistration.CarId; registration.Number = formRegistration.txtNumber.Text; registration.OwnerId = newIdOwner;
                dbContext.UpdateRegistration(registration);
                
                listBoxReg.SelectedIndex = idx;
            }
            UpdateListReg(txtNumber.Text);
        }

        private void bNewFoul_Click(object sender, EventArgs e)
        {
            FormPDD formPDD = new FormPDD();
            formPDD.dateTimePicker1.Value = DateTime.Now;
            formPDD.IdFoul = 0;
            formPDD.IdTypeFoul = 0;
            formPDD.txtNumber.Text = ""; formPDD.IdReg = 0;
            
            if(formPDD.ShowDialog() == DialogResult.OK)
            {
                Foul foul = new Foul();
                foul.DtFoul = formPDD.dateTimePicker1.Value;
                foul.IdTypeFoul = formPDD.IdTypeFoul;
                foul.IdRegistr = formPDD.IdReg;
                dbContext.NewFoul(foul);
                UpdateListFouls(txtNumber.Text);
            }
        }

        private void bFindFoul_Click(object sender, EventArgs e)
        {
            UpdateListFouls(txtNumber1.Text);
        }

        private void listBoxFouls_DoubleClick(object sender, EventArgs e)
        {
           VFoul vfoul =  (VFoul)listBoxFouls.SelectedItem;
            FormPDD formPdd = new FormPDD();
            formPdd.dateTimePicker1.Value = vfoul.Date;
            formPdd.IdFoul = vfoul.Id;
            formPdd.IdReg = vfoul.IdReg;
            formPdd.IdTypeFoul = vfoul.IdTypeFoul;
            formPdd.txtNumber.Text = vfoul.Number;
            if (formPdd.ShowDialog() == DialogResult.OK)
            {
                Foul foul = new Foul();
                foul.Id = vfoul.Id;
                foul.DtFoul = formPdd.dateTimePicker1.Value;
                foul.IdTypeFoul = formPdd.IdTypeFoul;
                foul.IdRegistr = formPdd.IdReg;
                dbContext.UpdateFoul(foul);
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.KeyCode == Keys.Back && e.Shift)
            {
                dbContext.InitDb();
                MessageBox.Show("Создана структура данных", "Внимание.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }else if (e.Alt && e.KeyCode == Keys.Back)
            {
                dbContext.FillData();
                MessageBox.Show("Добавлены некоторые данные. Программа будет закрыта.", "Внимание.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }

        private void txtNumber1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                txtNumber1.Text = "";
                UpdateListFouls("");
            }
            if (e.KeyCode == Keys.Enter)
            {
                UpdateListFouls(txtNumber1.Text);
            }
        }

        private void txtNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                txtNumber.Text = "";
                UpdateListReg("");
            }else if(e.KeyCode == Keys.Enter)
            {
                UpdateListReg(txtNumber.Text);
            }
        }

        private void txtFindOwner_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                txtFindOwner.Text = "";
                UpdateListOwners("");
            }
            else if (e.KeyCode == Keys.Enter)
            {
                UpdateListOwners(txtFindOwner.Text);
            }
        }

        private void txtFindCar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                txtFindCar.Text = "";
                UpdateListCars("");
            }
            else if (e.KeyCode == Keys.Enter)
            {
                UpdateListCars(txtFindCar.Text);
            }
        }

        private void txtFindFT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                txtFindFT.Text = "";
                UpdateListFT("");
            }
            else if (e.KeyCode == Keys.Enter)
            {
                UpdateListFT(txtFindFT.Text);
            }
        }

        private void bGraph1_Click(object sender, EventArgs e)
        {
            if (formGraph == null||formGraph.IsDisposed)
            {
                formGraph = new FormGraph();
                
            }
            formGraph.foulsRep = dbContext.GetFoulsRep(); 
            formGraph.Show();
        }
    }
}
