﻿using System;
﻿using System;
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


public class Enemy : GameObject
{
    public String Shootmode;

    public int bulletNum;

    public int bulletEachTime;
    public int bulletRestoreLimit;
    public int bulletRestoreClock;

    public Enemy(int x, int y)
        : base(x, y)
    {
        lx = x;
        ly = 0;
        vx = 0;
        vy = 1;
        setClock();
        loadImage();
        Shootmode = "Line";
        health = 10;
    }

    protected void setClock()
    {
        //f = frame = timer interval of FixUpdate
        clock = 0;
        clockLimit = 10;//每隔 20f 發射一顆子彈
        bulletNum = 5;
        bulletEachTime = 5;//每次射擊都會有 5 發子彈
        bulletRestoreClock = 0;
        bulletRestoreLimit = 175;//每隔 bulletRestoreLimit f 進行一次射擊
        move = 0;
        moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.
    }

    protected void loadImage()
    {
        img = new System.Windows.Forms.PictureBox();
        img.Location = img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
        img.Image = Image.FromFile(Application.StartupPath + "\\assest\\player.png");
        img.BackColor = Color.Transparent;
    }

    public Boolean canShoot()
    {
        adjustTimeInterval();
        clock++;
        if (clock > clockLimit && bulletNum > 0)
        {
            clock = 0;
            bulletNum--;
            return true;
        }
        return false;
    }
    protected void adjustTimeInterval()
    {
        bulletRestoreClock++;
        if (bulletRestoreClock > bulletRestoreLimit)
        {
            bulletRestoreClock = 0;
            bulletNum = bulletEachTime;
        }
    }
    public String getShootMode()
    {
        return Shootmode;
    }

    public Vector2D getVelocity(int px, int py)
    {
        Vector2D bulletVelocity = new Vector2D(px, py);
        return bulletVelocity;
    }
}