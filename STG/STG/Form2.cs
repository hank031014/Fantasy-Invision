﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STG
{
    public partial class Form2 : Form
    {
        Form1 NewGame;

        public Form2()
        {
            InitializeComponent();
            //Label_HighScore.Text = "";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            NewGame = new Form1();
            NewGame.Show();
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
