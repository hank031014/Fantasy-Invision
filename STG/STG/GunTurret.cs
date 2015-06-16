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


public class GunTurret : Enemy
{
    bool disposed = false;

    public GunTurret(int x, int y)
        : base(x, y)
    {
        lx = x;
        ly = y;
        vx = 0;
        vy = 3;
        setClock();
        loadImage();
        imgAutoSize();
        Shootmode = "Ray";
        health = 3;
    }

    protected override void setClock()
    {
        //f = frame = timer interval of FixUpdate
        clock = 0;
        clockLimit = 30;//每隔 30f 發射一顆子彈
        bulletNum = 2;
        bulletEachTime = 2;//每次射擊都會有 2 發子彈
        bulletRestoreClock = 0;
        bulletRestoreLimit = 200;//每隔 bulletRestoreLimit f 進行一次射擊
        move = 0;
        moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.
    }

    public override void Move()
    {
        lx += vx;
        ly += vy;
        if (vy > 0.15)
            vy -= 0.005;
        else
            vy = 0;
        img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
        img.BackColor = Color.Transparent;
    }

    public override Vector2D getVelocity(double px, double py)
    {
        double bux = (px - lx);
        double buy = (py - ly);
        
        //speed normaliziation.
        double normal = Math.Sqrt(Math.Pow(bux,2) + Math.Pow(buy,2));
        bux /= normal;
        buy /= normal;

        //set Velocity as 10 pixel/fs;
        bux *= 10;
        buy *= 10;

        Vector2D bulletVelocity = new Vector2D(bux, buy);
        return bulletVelocity;
    }
    //Dispose method
    protected override void Dispose(bool disposing)
   {
      if (disposed)
         return; 

      if (disposing) {
         // Free any other managed objects here.
         //
      }

      // Free any unmanaged objects here.
      //
      disposed = true;
   }

   ~GunTurret()
   {
      Dispose(false);
   }
}