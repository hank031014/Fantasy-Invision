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


public class Player : GameObject
{
    private int SLR;//S for stand,L for Left,R for Right.
    private int ImageClock, ImageClockLimit;
    private Boolean OP;
    private int OPclock, OPclockLimit;
    private Image[] S, L, R;
    private int So, Lo, Ro;//Image order for S,L,R;Used in Function ChangeImage();
    private int OPEndTime;
    private int Attack;
    private int AEEndTime;

    public Player(int x, int y)
        : base(x, y)
    {
        OPEndTime = -1;
        AEEndTime = -1;

        health = 50;
        lx = x;
        ly = y;
        //f = frame = timer interval of FixUpdate 
        clock = 0;
        OP = false;
        Attack = 1;
        clockLimit = 7;//每隔 10f 有一發子彈
        ImageClock = 0;
        ImageClockLimit = 10;
        move = 0;
        moveLimit = 0;//每隔 1f 可以移動 p+vx,p+vy.
        vx = 0;
        vxupLimit = 3;//x軸速度在 3~-3
        vxdownLimit = -3;
        vy = 0;
        vyupLimit = 3;//y軸速度在 3~-3
        vydownLimit = -3;
        setPlayerImage();
        img = new System.Windows.Forms.PictureBox();
        img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
        img.Image = S[1];
        img.BackColor = Color.Transparent;
        imgAutoSize();
        ResetImageOrder();
    }

    public void setOPEndTime(int InStartTime, int InRemainTime)
    {
        OPEndTime = InStartTime + InRemainTime;
    }

    public int getOPEndTime()
    {
        return OPEndTime;
    }

    public bool IsOP2(int NowTime)
    {
        if (OPEndTime == NowTime){
            OPEndTime = -1;
            return false;
        }
        else if (OPEndTime == -1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void setOPClock(int clock)
    {
        OP = true;
        OPclockLimit = clock;
    }

    public Boolean isOP()
    {
        if (OP)
        {
            OPclock++;
            if(OPclock > OPclockLimit)
            {
                OP = false;
                OPclock = 0;
            }
            return OP;
        }
        return false;
    }

    private void setPlayerImage()
    {
        S = new Image[9];
        L = new Image[3];
        R = new Image[3];
        String s;
        int i;
        for(i=1;i<9;i++)
        {
            s = "\\assest\\Player\\playerS" + i + ".png";
            S[i] = Image.FromFile(Application.StartupPath + s);
        }
        for(i=1;i<3;i++)
        {
            s = "\\assest\\Player\\playerL" + i + ".png";
            L[i] = Image.FromFile(Application.StartupPath + s);
        }
        for (i=1;i<3;i++)
        {
            s = "\\assest\\Player\\playerR" + i + ".png";
            R[i] = Image.FromFile(Application.StartupPath + s);
        }
    }

    public void addV(int ax, int ay)
    {
        if (ax != 0)
        {
            if (ax > 0 && vx < vxupLimit)
                vx += ax;
            if (ax < 0 && vx > vxdownLimit)
                vx += ax;
        }
        else
        {
            if (ay > 0 && vy < vyupLimit)
                vy += ay;
            if (ay < 0 && vy > vydownLimit)
                vy += ay;
        }
    }

    public void setSLR(int mode)
    {
        SLR = mode;
        ResetImageOrder();
    }

    private void ResetImageOrder()
    {
        ImageClock = ImageClockLimit;
        So = 1;
        Lo = 1;
        Ro = 1;
    }

    public void ChangeImage()
    {
        ImageClock++;
        if (ImageClock > ImageClockLimit)
        {
            ImageClock = 0;
            switch (SLR)
            {
                case 0:

                    if (So > 8)
                        So = 1;
                    img.Image = S[So];
                    So++;
                    break;
                case 1:
                    if (Lo > 2)
                        Lo = 1;
                    img.Image = L[Lo];
                    Lo++;

                    break;
                case 2:
                    if (Ro > 2)
                        Ro = 1;
                    img.Image = R[Ro];
                    Ro++;
                    break;
            }
        }
    }

    public override void Move()
    {
        if (lx + vx > 0 && lx + vx < 590)
            lx += vx;
        if (ly + vy > 0 && ly + vy < 570)
            ly += vy;
        img.Location = new Point(Convert.ToInt32(lx), Convert.ToInt32(ly));
        //img.BackColor = Color.Transparent;
        img.SetBounds((int)lx + 4, (int)ly + 5, 17, 30);
    }

    public void setHP(int blood)
    {
        health += blood;
    }

    public int getHP()
    {
        return health;
    }

    public void setAttackEnhance(int StartTime, int RemainTime, int InputAttack)
    {
        AEEndTime = StartTime + RemainTime;
        Attack = InputAttack;
    }

    public int getAttack()
    {
        return Attack;
    }

    public void setAttack()
    {
        Attack = 1;
    }

    public Boolean IsAttackEnhancing(int NowTime)
    {
        if (AEEndTime == NowTime)
        {
            AEEndTime = -1;
            return false;
        }
        else if (AEEndTime == -1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}