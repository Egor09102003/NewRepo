using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();

            userNameField.ForeColor = Color.Gray;
            userNameField.Text = "Введите имя";

            userSurnameField.ForeColor = Color.Gray;
            userSurnameField.Text = "Введите фамилию";

            loginField.ForeColor = Color.Gray;
            loginField.Text = "Введите логин";

            passField.ForeColor = Color.Gray;
            passField.Text = "Введите пароль";

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;

            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        Color col;

        private void closeButton_MouseEnter(object sender, EventArgs e)
        {
            col = closeButton.BackColor;
            closeButton.BackColor = Color.Red;
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.BackColor = col;
        }

        private void userNameField_Enter(object sender, EventArgs e)
        {
            if(userNameField.Text == "Введите имя")
            {
                userNameField.Text = "";
                userNameField.ForeColor = Color.Black;
            }
        }

        private void userNameField_Leave(object sender, EventArgs e)
        {
            if(userNameField.Text == "")
            {
                userNameField.ForeColor = Color.Gray;
                userNameField.Text = "Введите имя";
            }
        }

        private void userSurnameField_Enter(object sender, EventArgs e)
        {
            if(userSurnameField.Text == "Введите фамилию")
            {
                userSurnameField.Text = "";
                userSurnameField.ForeColor = Color.Black;
            }
        }

        private void userSurnameField_Leave(object sender, EventArgs e)
        {
            if (userSurnameField.Text == "")
            {
                userSurnameField.ForeColor = Color.Gray;
                userSurnameField.Text = "Введите фамилию";
            }
        }

        private void loginField_Enter(object sender, EventArgs e)
        {
            if (loginField.Text == "Введите логин")
            {
                loginField.Text = "";
                loginField.ForeColor = Color.Black;
            }
        }

        private void loginField_Leave(object sender, EventArgs e)
        {
            if (loginField.Text == "")
            {
                loginField.ForeColor = Color.Gray;
                loginField.Text = "Введите логин";
            }
        }

        private void passField_Enter(object sender, EventArgs e)
        {
            if (passField.Text == "Введите пароль")
            {
                passField.UseSystemPasswordChar = true;
                passField.Text = "";
                passField.ForeColor = Color.Black;
            }
        }

        private void passField_Leave(object sender, EventArgs e)
        {
            if (passField.Text == "")
            {
                passField.UseSystemPasswordChar = false;
                passField.ForeColor = Color.Gray;
                passField.Text = "Введите пароль";
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if(userNameField.Text == "Введите имя")
            {
                MessageBox.Show("Введите имя");
                return;
            }
            else if(userSurnameField.Text == "Введите фамилию")
            {
                MessageBox.Show("Введите фамилию");
                return;
            }
            else if (loginField.Text == "Введите логин")
            {
                MessageBox.Show("Введите логин");
                return;
            }
            else if (passField.Text == "Введите пароль")
            {
                MessageBox.Show("Введите пароль");
                return;
            }

            if (isUserExists())
            {
                return;
            }

            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, " +
                "`pass`, " +
                "`name`, " +
                "`surname`) " +
                "VALUES (" +
                "@login, " +
                "@password, " +
                "@name, " +
                "@surname)", db.getConnection());

            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginField.Text;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = passField.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = userNameField.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = userSurnameField.Text;

            db.openConnection();
            
            if(command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан");
            }
            else
            {
                MessageBox.Show("Аккаунт не был создан");
            }

            db.closeConnection();
        }


        public Boolean isUserExists()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` " +
                "WHERE `login` = @ul", db.getConnection());
            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = loginField.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь с таким логином уже существует, введите другой");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
