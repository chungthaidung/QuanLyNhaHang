using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
namespace GUI
{
    public partial class Form_Login : Form
    {
        public EmployeeBLL employeeBLL;
        public Form_Login()
        {
            employeeBLL = new EmployeeBLL();
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }

        private void img_Min_Click_1(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Minimized;
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Minimized;
            }
        }

        private void img_Max_Click_1(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void img_Close_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
        

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string tenDN = txtUsername.Text;
            string matkhau = txtPassword.Text;

            if (string.IsNullOrEmpty(tenDN) || string.IsNullOrEmpty(matkhau))
            {
                MessageBox.Show("Please insert your username and password correctly");
            }
            else
            {
                if (employeeBLL.CheckLogin(tenDN, matkhau) == true)
                {
                    bool isAdmin = employeeBLL.CheckAdmin(tenDN);
                    int employeeID = employeeBLL.GetEmployeeID(tenDN);
                    Form_Restaurant f = new Form_Restaurant(isAdmin, employeeID);
                    this.Hide();
                    f.Closed += (s, args) => this.Close();
                    f.Show();
                }
                else
                    MessageBox.Show("Incorrect. Please try again !!");

            }
        }
        
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                string tenDN = txtUsername.Text;
                string matkhau = txtPassword.Text;

                if (string.IsNullOrEmpty(tenDN) || string.IsNullOrEmpty(matkhau))
                {
                    MessageBox.Show("Please insert your username and password correctly");
                }
                else
                {
                    if (employeeBLL.CheckLogin(tenDN, matkhau) == true)
                    {
                        bool isAdmin = employeeBLL.CheckAdmin(tenDN);
                        int employeeID = employeeBLL.GetEmployeeID(tenDN);
                        Form_Restaurant f = new Form_Restaurant(isAdmin, employeeID);
                        this.Hide();
                        f.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect. Please try again !!");
                    }


                }
            }
        }

        private void txtUsername_OnValueChanged(object sender, EventArgs e)
        {
            if (btnSignIn.BackColor == Color.FromArgb(200, 237, 230))
            {
                btnSignIn.BackColor = Color.FromArgb(18, 37, 44);
                btnSignIn.ForeColor = Color.White;
            }
        }

        private Form_Loading splashScreen = new Form_Loading();


        private void Form_Login_Load(object sender, EventArgs e)
        {
            splashScreen.Show();
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler
                 (worker_RunWorkerCompleted);
            worker.RunWorkerAsync();
            this.Visible = false;

        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            splashScreen.Close();
            this.Visible = true;
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
        }
    }
}
