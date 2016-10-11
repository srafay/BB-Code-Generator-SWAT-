using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SWAT_Test
{
    public partial class Form1 : Form
    {
        string applicantname = "";
        string Final="";
        double M_Rules=0;
        double m4marks = 0;
        string m4comment;
        double duelmarks = 0;
        string duelcomment;
        double M_Aim=0;
        double M_SOC=0;
        double SOC_sec;
        double M_Driving=0;
        string drivingcomment;
        string rulescomment;
        double M_Final;
        int m4numberofcounts = 0;
        int socstandingattempts = 0;
        int socrunningattempts = 0;
        public Form1()
        {
            InitializeComponent();
            this.Text = "SWAT Applicant Testing Software";
            
        }

        private void Update_rules_Click(object sender, EventArgs e)
        {
            applicantname = AN.Text;
            M_Rules = Convert.ToDouble(marks_rules.Text);
            rulescomment = com_rules.Text;
            MessageBox.Show("SWAT Rules Marks are updated now!", "Form Locked!");
            //Update_rules.Enabled = false;
        }

        private void Update_driving_Click(object sender, EventArgs e)
        {
            M_Driving = Convert.ToDouble(marks_driving.Text);
            drivingcomment = com_driving.Text;
            MessageBox.Show("Driving marks are updated now!", "Form Locked!");
            //Update_driving.Enabled = false;
        }

        private void Update_m4_Click(object sender, EventArgs e)
        {
            m4comment = "";
            int count=0;
            double damage=0 ;
            string temp="";
            if (tire1.Text.Length != 0)
            {
                if (Convert.ToDouble(carhp1.Text) >= 900)
                { damage = 0; temp = "low car damage"; goto one; }
                if (Convert.ToDouble(carhp1.Text) >= 750 )
                { damage = -0.25; temp = "medium car damage"; goto one; }
                if (Convert.ToDouble(carhp1.Text) >= 500)
                { damage = -0.5; temp = "big car damage"; goto one; }
                if (Convert.ToDouble(carhp1.Text) >= 250)
                { damage = -0.75; temp = "heavy car damage"; goto one; }
                if (Convert.ToDouble(carhp1.Text) < 250)
                { damage = -1; temp = "blown car"; goto one; }
            one:
                m4comment = "[list][*][b]First try[/b]: " + tire1.Text + "-tire(s) with " + temp;
                m4marks = Convert.ToInt32(tire1.Text)+1+damage;
                count++;
            }

            if (tire2.Text.Length != 0)
            {
                if (Convert.ToDouble(carhp2.Text) == 1000)
                { damage = 0; temp = "no car damage"; goto two; }
                if (Convert.ToDouble(carhp2.Text) >= 750)
                { damage = -0.25; temp = "medium car damage"; goto two; }
                if (Convert.ToDouble(carhp2.Text) >= 500)
                { damage = -0.5; temp = "big car damage"; goto two; }
                if (Convert.ToDouble(carhp2.Text) >= 250)
                { damage = -0.75; temp = "heavy car damage"; goto two; }
                if (Convert.ToDouble(carhp2.Text) < 250)
                { damage = -1; temp = "blown car"; goto two; }
            two:
                m4comment += Environment.NewLine;
            m4comment = m4comment + "[*][b]Second try[/b]: " + tire2.Text + "-tire(s) with " + temp;
                m4marks += Convert.ToInt32(tire2.Text) + 1 + damage;
                count++;
            }
            if (tire3.Text.Length != 0)
            {
                if (Convert.ToDouble(carhp3.Text) == 1000)
                { damage = 0; temp = "no car damage"; goto three; }
                if (Convert.ToDouble(carhp3.Text) >= 750)
                { damage = -0.25; temp = "medium car damage"; goto three; }
                if (Convert.ToDouble(carhp3.Text) >= 500)
                { damage = -0.5; temp = "big car damage"; goto three; }
                if (Convert.ToDouble(carhp3.Text) >= 250)
                { damage = -0.75; temp = "heavy car damage"; goto three; } 
                if (Convert.ToDouble(carhp3.Text) < 250)
                { damage = -1; temp = "blown car"; goto three; }
            three:
                m4comment += Environment.NewLine;
            m4comment = m4comment + "[*][b]Third try[/b]: " + tire3.Text + "-tire(s) with " + temp;
                m4marks += Convert.ToInt32(tire3.Text) + 1 + damage;
                count++;
            }
            if (tire4.Text.Length != 0)
            {
                if (Convert.ToDouble(carhp4.Text) == 1000)
                { damage = 0; temp = "no car damage"; goto four; }
                if (Convert.ToDouble(carhp4.Text) >= 750)
                { damage = -0.25; temp = "medium car damage"; goto four; }
                if (Convert.ToDouble(carhp4.Text) >= 500)
                { damage = -0.5; temp = "big car damage"; goto four; }
                if (Convert.ToDouble(carhp4.Text) >= 250)
                { damage = -0.75; temp = "heavy car damage"; goto four; } 
                if (Convert.ToDouble(carhp4.Text) < 250)
                { damage = -1; temp = "blown car"; goto four; }
            four:
                m4comment += Environment.NewLine;
            m4comment = m4comment + "[*][b]Fourth try[/b]: " + tire4.Text + "-tire(s) with " + temp;
                m4marks += Convert.ToInt32(tire4.Text) + 1 + damage;
                count++;
            }
            m4comment = m4comment + "[/list]";
            m4marks = m4marks / count;
            overall_m4.Text = Convert.ToString(m4marks);
            MessageBox.Show("M4 shooting marks are updated now!", "Form Locked!");
            //Update_m4.Enabled = false;
            m4numberofcounts = count;
        }

        private void Update_Duel_Click(object sender, EventArgs e)
        {
            duelcomment = "";
            duelmarks = 0;
            int count=0;
            string temp = "";
            if (dmarks1.Text.Length != 0)
            {
                duelmarks += Convert.ToDouble(dmarks1.Text);
                count++;
                temp = calculatehp(Convert.ToDouble(dmarks1.Text));
                duelcomment = "[*][b]In dueling[/b], ";
                duelcomment += "[list][*][b]First duel[/b]: " + temp;
            }
            if (dmarks2.Text.Length != 0)
            {
                duelmarks += Convert.ToDouble(dmarks2.Text);
                count++;
                temp = calculatehp(Convert.ToDouble(dmarks2.Text));
                duelcomment += Environment.NewLine;
                duelcomment = duelcomment + "[*][b]Second duel[/b]: " + temp; // plus here
            }

            duelcomment += "[/list]";
            duelmarks = duelmarks / count;
            overall_duel.Text = Convert.ToString(duelmarks);
            M_Aim = Math.Round ( (duelmarks + m4marks) / 2 , 3 );
            M_Aim = roundoff_custom(M_Aim);
            MessageBox.Show("Dueling marks are updated now!", "Form Locked!");
            //Update_Duel.Enabled = false;
        }

        private void Update_SOC_Click(object sender, EventArgs e)
        {
            double ssec=0;
            double rsec=0;
            int count = 0;
            if (soc_s1.Text.Length != 0)
            {
                ssec += Convert.ToDouble(soc_s1.Text);
                count++;
            }
            if (soc_s2.Text.Length != 0)
            {
                ssec += Convert.ToDouble(soc_s2.Text);
                count++;
            }
            if (soc_s3.Text.Length != 0)
            {
                ssec += Convert.ToDouble(soc_s3.Text);
                count++;
            }
            if (soc_s4.Text.Length != 0)
            {
                ssec += Convert.ToDouble(soc_s4.Text);
                count++;
            }
            ssec = ssec / count;
            socstandingattempts = count;
            count = 0;
            if (soc_r1.Text.Length != 0)
            {
                rsec += Convert.ToDouble(soc_r1.Text);
                count++;
            }
            if (soc_r2.Text.Length != 0)
            {
                rsec += Convert.ToDouble(soc_r2.Text);
                count++;
            }

            if (soc_r3.Text.Length != 0)
            {
                rsec += Convert.ToDouble(soc_r3.Text);
                count++;
            }
            if (soc_r4.Text.Length != 0)
            {
                rsec += Convert.ToDouble(soc_r4.Text);
                count++;
            }
            socrunningattempts = count;
            if (count != 0)
            {
                rsec = rsec / count;
            }
            else
            {
                rsec = ssec;
            }
       
            SOC_sec = (rsec + ssec) / 2;
            SOC_sec = roundoff_soc(SOC_sec);
            
            //M_SOC = calculatesoc ( Convert.ToString((rsec + ssec) / 2) );
            M_SOC = calculatesocNEW(Convert.ToString(SOC_sec));
            overall_soc.Text = Convert.ToString(M_SOC);
            overall_sec.Text = Convert.ToString(SOC_sec);
            MessageBox.Show("Speed of command's marks are updated now!", "Form Locked!");
            //Update_SOC.Enabled = false;
        }

        string calculatehp(double marks)
        {
            if (marks - 0.9 < 0)
                return "I killed him with almost full armour";
            if (marks - 1.9 < 0)
                return "I killed him with damaged armour (but not half)";
            if (marks - 2.4 < 0)
                return "I killed him with Half armour";
            if (marks - 2.9 < 0)
                return "I killed him with no armour (full hp)";
            if (marks - 3.9 < 0)
                return "I killed him with damaged hp (but not half hp)";
            if (marks - 4.9 < 0)
                return "I killed him with half hp";
            if (marks == 5)
                return "He killed me";
            return "";
        }

        double calculatesoc(string sec)
        {

            double marks = 5;
            double soc =  Convert.ToDouble (sec);
            for (double i = 3; i < 14; i = Math.Round (i + 0.1 , 2))
            {
                if (soc <= i)
                    return marks;
                marks =  Math.Round (marks - 0.05 , 2);
            }
            return marks;
        }

        double calculatesocNEW(string sec)
        {

            double marks = 0;
            double soc = Convert.ToDouble(sec);

                if (soc >= 9)   // checking if failed in soc
                    marks = 0;

            double temp = soc - (int)soc; // stores fractional part if any
            soc = (int)soc;

                if (soc == 8)
                    marks = 2.5;
                if (soc == 7)
                    marks = 3;
                if (soc == 6)
                    marks = 3.5;
                if (soc == 5)
                    marks = 4;
                if (soc == 4)
                    marks = 4.5;
                if (soc <= 3)
                    marks = 5;
            
            /* now calculating for fractional part */

                if (temp == 0.5)
                    marks -= 0.25;
                return marks;

          
           
        }

        double roundoff_custom(double x)
        {
            double temp;
            temp = x - (int)(x);
            if (temp >= 0.875)
            {
                temp = 1;
                goto label;
            }
            if (temp >= 0.625)
            {
                temp = 0.75;
                goto label;
            }
            if (temp >= 0.375)
            {
                temp = 0.5;
                goto label;
            }
            if (temp >= 0.125)
            {
                temp = .25;
                goto label;
            }
            if (temp < 0.125)
            {
                temp = 0;
                goto label;
            }

        label:  // label for goto

            x = (int)x + temp;
        return x;
        }

        double roundoff_soc(double x)
        {
            double temp;
            temp = x - (int)x;
            if (temp <= 0.25)
            {
                temp = 0;
                goto label;
            }
            if (temp <= 0.5)
            {
                temp = 0.5;
                goto label;
            }
            if (temp <= 0.75)
            {
                temp = 0.5;
                goto label;
            }
            if (temp <= 1)
            {
                temp = 1;
                goto label;
            }
        label:  // label for goto
        x = (int)x + temp;
        return x;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            M_Final = Math.Round ( (M_Driving + M_Aim + M_SOC) / 3 , 2); // M_Aim = m4marks duelmarks /2
            //M_Final = roundoff_custom(M_Final); // no need to fancy round off overall

            Final = "[col][b]Applicant's In-Game Name:[/b] ";
            Final = Final + applicantname + Environment.NewLine + Environment.NewLine;
            Final += "    [b]Driving Skill:[/b] ";
            Final = Final + M_Driving + "/5" + Environment.NewLine;
            Final += "    [b]Aiming and Shooting:[/b] ";
            Final = Final + M_Aim + "/5" + Environment.NewLine;
            Final += "    [b]Speed of Commands:[/b] ";
            Final = Final + M_SOC + "/5" + Environment.NewLine;
            Final += "    [b]Knowledge of S.W.A.T. Rules:[/b] ";
            Final = Final + M_Rules + "/5" + Environment.NewLine;
            Final += Environment.NewLine;
            Final = Final + "    [b]Overall:[/b] " + M_Final + "/5" + Environment.NewLine;
            Final = Final + "    [b]Notes:[/b]|[img]http://i.picresize.com/images/2016/02/04/HCG7b.png[/img][/col]";
            Final = Final + "[list][*][b]In rules[/b], " + rulescomment + Environment.NewLine;
            if (M_Rules >= 4)
            {
                Final = Final + "[*][b]In driving[/b], " + drivingcomment + Environment.NewLine;
                Final = Final + "[*][b]In the M4 part[/b], he had " + m4numberofcounts + " attempts" + Environment.NewLine;
                Final = Final + m4comment + Environment.NewLine;
                Final = Final + duelcomment + Environment.NewLine;
                Final = Final + "[*][b]In SOC[/b], he had " + socstandingattempts + " attempts for standing part and ";
                Final = Final + socrunningattempts + " attempts for the running part.";
                Final = Final + Environment.NewLine + "His average speed of commands is ";
                Final = Final + SOC_sec + " seconds.";
            }
            Final+= "[/list]";
            File.WriteAllText(applicantname+"'s Test result.txt", Final);
            MessageBox.Show("BB Code is ready, copy it from txt file", "SWAT Test result");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://ls-rcr.com/forum/viewforum.php?f=32");
        }

        private void label48_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://ls-rcr.com/forum/memberlist.php?mode=viewprofile&u=18265");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://ls-rcr.com/forum/memberlist.php?mode=viewprofile&u=18265");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://ls-rcr.com/forum/memberlist.php?mode=viewprofile&u=22496");
        }

        private void label49_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://ls-rcr.com/forum/memberlist.php?mode=viewprofile&u=22496");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://ls-rcr.com/forum/memberlist.php?mode=viewprofile&u=18265");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://ls-rcr.com/forum/memberlist.php?mode=viewprofile&u=22496");
        }

      
    }
}

