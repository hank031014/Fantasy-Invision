﻿﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Diagnostics;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectSound;

public class EnemyBullet : GameObject
{
    //public double lx, ly;
    public int middleX, middleY;
    public int radius;
    private string MoveMode;
    public double angle;

    public EnemyBullet(int x, int y)
        : base(x, y)
    {
        lx = x;
        ly = y;
        //f = frame = timer interval of FixUpdate 
        move = 0;
        moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.
        vx = 0;
        vy = 5;
        img = new System.Windows.Forms.PictureBox();
        img.Location = img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
        img.Image = Image.FromFile(Application.StartupPath + "\\assest\\Bullet_black.BMP");
        img.BackColor = Color.Transparent;

        MoveMode = "";
        radius = 100;
        angle = 0;
    }

    public void circleMove()
    {
        middleY++;
        lx = Convert.ToInt32(middleX + radius * Math.Cos(angle += 0.05));
        ly = Convert.ToInt32(middleY + radius * Math.Sin(angle += 0.05));
        img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
        img.BackColor = Color.Transparent;
    }

    public void setXY(double ox, double oy)
    {
        middleX = (int)ox + 20;
        middleY = (int)oy + 20;
    }

    public void setMoveMode(string outstr)
    {
        MoveMode = outstr;
    }

    public string getMoveMode()
    {
        return MoveMode;
    }
}