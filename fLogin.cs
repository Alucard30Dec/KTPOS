﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;
using KTPOS.Proccess;
using KTPOS.STAFF;

namespace KTPOS
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            int x = (this.ClientSize.Width - panelLogin.Width) / 2;
            int y = (this.ClientSize.Height - panelLogin.Height) / 2;
            panelLogin.Location = new Point(x,y);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(800, 450);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you really want to exit?", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Exit cancelled. Continue your activity ❤️.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Focus();
            }
        }

        private void btnMaxSize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMaxSize.Visible = false;
            btnMinSize.Visible = true;
        }

        private void btnMinSize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.Manual;

            // Tùy chỉnh lại vị trí nếu cần (giữ nguyên vị trí hiện tại)
            this.Location = new Point(0, 0);
            btnMinSize.Visible = false;
            btnMaxSize.Visible = true;
        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
            /*string role = "Manager";
            fStaff f = new fStaff(role);
            this.Hide();
            f.ShowDialog();*/
            string email = txtAccount.Text;
            string password = txtPass.Text;
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both email and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string role = LgAccount.Instance.LgManage(email, password);
                if (role != null)
                {
                    fStaff_F f = new fStaff_F(role);
                    this.Hide();
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please enter a valid email or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter a valid email or password." + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        

        private void btnEye_Click(object sender, EventArgs e)
        {
            if (txtPass.PasswordChar == '\0')
            {
                txtPass.PasswordChar = '\u25CF';
                btnEye.Visible = false;
                btnHide.Visible = true;
            }
        }
        private void btnHide_Click(object sender, EventArgs e)
        {
            if (txtPass.PasswordChar == '\u25CF')
            {
                txtPass.PasswordChar = '\0';
                btnHide.Visible = false;
                btnEye.Visible = true;
            }
        }
    }
}
