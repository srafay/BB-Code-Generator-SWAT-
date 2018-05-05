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
        //Make a structure for an applicant
        public class ApplicantData
        {
            public string name;
            public string output;
            public enum Categories
            {
                RULES = 0,
                DRIVING,
                M4,
                DUEL,
                SOC,
                MAX_CATEGORIES
            }
            public double[] results = new double[(int)Categories.MAX_CATEGORIES];
            public string[] comments = new string[(int)Categories.MAX_CATEGORIES];

            //"overall" grades are rules, aim, soc, driving, final
            //aim is the average of m4 and duel results
            //each category has its optional comment

            public enum Attempts
            {
                STANDING = 0,
                RUNNING,
                MAX_ATTEMPTS
            }
            public int[] socAttempts = new int[(int)Attempts.MAX_ATTEMPTS];
            public int m4attempts = 0;
            public double socTime = 0;
        }

        ApplicantData applicant = new ApplicantData();

        public Form1()
        {
            InitializeComponent();
            this.Text = "SWAT Applicant Testing Software v1.1";
        }

        private void Update_rules_Click(object sender, EventArgs e)
        {
            applicant.name = AN.Text;
            applicant.results[(int)ApplicantData.Categories.RULES] = Convert.ToDouble(marks_rules.Text);
            applicant.comments[(int)ApplicantData.Categories.RULES] = com_rules.Text;
            MessageBox.Show("SWAT Rules Marks are updated now!", "Form Locked!");
            //Update_rules.Enabled = false;
        }

        private void Update_driving_Click(object sender, EventArgs e)
        {
            int number_of_deductions = checkedListBox1.CheckedIndices.Count;
            applicant.results[(int)ApplicantData.Categories.DRIVING] = Convert.ToDouble(marks_driving.Text);


            /* Deduct marks only if driving marks are > 2.5/5 */
            if (applicant.results[(int)ApplicantData.Categories.DRIVING] > 2.5)
            {
                for (int i = 0; i < number_of_deductions; ++i)
                    applicant.results[(int)ApplicantData.Categories.DRIVING] -= 0.25;
            }

            /* ////////////////////////////////////////////// */

            driving_marks_overall.Text = Convert.ToString(applicant.results[(int)ApplicantData.Categories.DRIVING]) + " / 5";
            applicant.comments[(int)ApplicantData.Categories.DRIVING] = com_driving.Text;

            if (number_of_deductions != 0)
            {
                applicant.comments[(int)ApplicantData.Categories.DRIVING] += " [list]The applicants marks were deducted due to the following reasons : " + Environment.NewLine;

                //go through each possible deduction, and adjust driving comment accordingly
                //checkedListBox1 is responsible for holding all checkboxes related to driving - see which ones are toggled there
                string temp = "";

                CheckedListBox.CheckedItemCollection _checked = checkedListBox1.CheckedItems;
                foreach (var item in _checked)
                {
                    //comment string and label text are exactly the same, except with a [*]
                    temp += "[*]" + item.ToString();
                }

                applicant.comments[(int)ApplicantData.Categories.DRIVING] += temp + "[/list]";
            }
            MessageBox.Show("Driving marks are updated now!", "Form Locked!");
            //Update_driving.Enabled = false;

        }


        private void Update_m4_Click(object sender, EventArgs e)
        {
            applicant.comments[(int)ApplicantData.Categories.M4] = "";
            int count = 0;
            double damage = 0;
            string temp = "";

            if (tire1.Text.Length != 0)
            {
                //adjust temp string according to damage
                evaluateCarDamage(ref temp);
                applicant.comments[(int)ApplicantData.Categories.M4] = "[list][*][b]First try[/b]: " + tire1.Text + "-tire(s) with " + temp;
                applicant.results[(int)ApplicantData.Categories.M4] = Convert.ToInt32(tire1.Text) + 1 + damage;
                ++count;
            }

            if (tire2.Text.Length != 0)
            {
                //adjust temp string according to damage
                evaluateCarDamage(ref temp);
                applicant.comments[(int)ApplicantData.Categories.M4] += Environment.NewLine + "[*][b]Second try[/b]: " + tire2.Text + "-tire(s) with " + temp;
                applicant.results[(int)ApplicantData.Categories.M4] += Convert.ToInt32(tire2.Text) + 1 + damage;
                ++count;
            }

            if (tire3.Text.Length != 0)
            {
                //adjust temp string according to damage
                evaluateCarDamage(ref temp);
                applicant.comments[(int)ApplicantData.Categories.M4] += Environment.NewLine + "[*][b]Third try[/b]: " + tire3.Text + "-tire(s) with " + temp;
                applicant.results[(int)ApplicantData.Categories.M4] += Convert.ToInt32(tire3.Text) + 1 + damage;
                ++count;
            }

            if (tire4.Text.Length != 0)
            {
                //adjust temp string according to damage
                evaluateCarDamage(ref temp);
                applicant.comments[(int)ApplicantData.Categories.M4] += Environment.NewLine + "[*][b]Fourth try[/b]: " + tire4.Text + "-tire(s) with " + temp;
                applicant.results[(int)ApplicantData.Categories.M4] += Convert.ToInt32(tire4.Text) + 1 + damage;
                ++count;
            }

            applicant.comments[(int)ApplicantData.Categories.M4] += "[/list]";
            applicant.results[(int)ApplicantData.Categories.M4] /= count;
            overall_m4.Text = Convert.ToString(applicant.results[(int)ApplicantData.Categories.M4]);
            MessageBox.Show("M4 shooting marks are updated now!", "Form Locked!");
            //Update_m4.Enabled = false;
            applicant.m4attempts = count;
        }

        private void Update_Duel_Click(object sender, EventArgs e)
        {
            applicant.comments[(int)ApplicantData.Categories.DUEL] = "";
            applicant.results[(int)ApplicantData.Categories.DUEL] = 0;
            int count = 0;
            string temp = "";

            if (dmarks1.Text.Length != 0)
            {
                applicant.results[(int)ApplicantData.Categories.DUEL] += Convert.ToDouble(dmarks1.Text);
                ++count;
                temp = evaluateHP(Convert.ToDouble(dmarks1.Text));
                applicant.comments[(int)ApplicantData.Categories.DUEL] = "[*][b]In dueling[/b], [list][*][b]First duel[/ b]: " + temp;
            }

            if (dmarks2.Text.Length != 0)
            {
                applicant.results[(int)ApplicantData.Categories.DUEL] += Convert.ToDouble(dmarks2.Text);
                ++count;
                temp = evaluateHP(Convert.ToDouble(dmarks2.Text));
                applicant.comments[(int)ApplicantData.Categories.DUEL] += Environment.NewLine + "[*][b]Second duel[/ b]: " + temp;
            }

            applicant.comments[(int)ApplicantData.Categories.DUEL] += "[/list]";
            applicant.results[(int)ApplicantData.Categories.DUEL] /= count;
            overall_duel.Text = Convert.ToString(applicant.results[(int)ApplicantData.Categories.DUEL]);
            //Do these later on - in a printing message
            /*
             * M_Aim = Math.Round((duelmarks + m4marks) / 2, 3);
                M_Aim = roundoff_custom(M_Aim);
             * */
            MessageBox.Show("Dueling marks are updated now!", "Form Locked!");
        }

        private void Update_SOC_Click(object sender, EventArgs e)
        {
            //first index is standing, second index is running
            double[] seconds = { 0, 0 };
            int count = 0;

            TextBox[] standing = { soc_s1, soc_s2, soc_s3, soc_s4 };

            for (int i = 0; i < standing.Length; ++i)
            {
                if (standing[i].Text.Length != 0)
                {
                    seconds[0] += Convert.ToDouble(standing[i].Text);
                    ++count;
                }
            }

            seconds[0] /= count;
            applicant.socAttempts[(int)ApplicantData.Attempts.STANDING] = count;
            count = 0;

            TextBox[] running = { soc_r1, soc_r2, soc_r3, soc_r4 };

            for (int i = 0; i < running.Length; ++i)
            {
                if (running[i].Text.Length != 0)
                {
                    seconds[1] += Convert.ToDouble(running[i].Text);
                    ++count;
                }
            }
            applicant.socAttempts[(int)ApplicantData.Attempts.RUNNING] = count;
            seconds[1] = (count != 0) ? seconds[1] / count : seconds[0];

            //calculating total time
            applicant.socTime = (seconds[0] + seconds[1]) / 2;
            roundoff_soc(ref applicant.socTime);
            //calculating soc score
            applicant.results[(int)ApplicantData.Categories.SOC] = calculatesocNEW(applicant.socTime);

            overall_soc.Text = Convert.ToString(applicant.results[(int)ApplicantData.Categories.SOC]);
            overall_sec.Text = Convert.ToString(applicant.socTime);

            MessageBox.Show("Speed of command's marks are updated now!", "Form Locked!");
            //Update_SOC.Enabled = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //This function could still use some cleanup on the whole string appending (memory intensive since it creates a new string per each appending operation) 
            double aimScore = Math.Round((applicant.results[(int)ApplicantData.Categories.DUEL] + applicant.results[(int)ApplicantData.Categories.M4]) / 2, 3);
            roundoff_custom(ref aimScore);
            double overallScore = Math.Round((applicant.results[(int)ApplicantData.Categories.DRIVING] + applicant.results[(int)ApplicantData.Categories.SOC] + aimScore) / 3, 2);

            string Final = "";

            Final = "[col][b]Applicant's In-Game Name:[/b] ";
            Final += applicant.name + Environment.NewLine + Environment.NewLine;
            Final += "    [b]Driving Skill:[/b] ";
            Final += applicant.results[(int)ApplicantData.Categories.DRIVING] + "/5" + Environment.NewLine;
            Final += "    [b]Aiming and Shooting:[/b] ";
            Final += aimScore + "/5" + Environment.NewLine;
            Final += "    [b]Speed of Commands:[/b] ";
            Final += applicant.results[(int)ApplicantData.Categories.SOC] + "/5" + Environment.NewLine;
            Final += "    [b]Knowledge of S.W.A.T. Rules:[/b] ";
            Final += applicant.results[(int)ApplicantData.Categories.RULES] + "/5" + Environment.NewLine + Environment.NewLine;
            Final += "    [b]Overall:[/b] " + overallScore + "/5" + Environment.NewLine;
            Final += "    [b]Notes:[/b]|[img]http://i.picresize.com/images/2016/02/04/HCG7b.png[/img][/col]";
            Final += "[list][*][b]In rules[/b], " + applicant.comments[(int)ApplicantData.Categories.RULES] + Environment.NewLine;
            if (applicant.results[(int)ApplicantData.Categories.RULES] >= 4)
            {
                Final += "[*][b]In driving[/b], " + applicant.comments[(int)ApplicantData.Categories.RULES] + Environment.NewLine;
                Final += "[*][b]In the M4 part[/b], he had " + applicant.m4attempts + " attempts" + Environment.NewLine;
                Final += applicant.comments[(int)ApplicantData.Categories.RULES] + Environment.NewLine;
                Final += applicant.comments[(int)ApplicantData.Categories.DUEL] + Environment.NewLine;
                Final += "[*][b]In SOC[/b], he had " + applicant.socAttempts[(int)ApplicantData.Attempts.STANDING] + " attempts for standing part and ";
                Final += applicant.socAttempts[(int)ApplicantData.Attempts.RUNNING] + " attempts for the running part.";
                Final += Environment.NewLine + "His average speed of commands is ";
                Final += applicant.socTime + " seconds.";
            }
            Final += "[/list]";
            applicant.output = Final;
            File.WriteAllText(applicant.name + "'s Test result.txt", Final);
            MessageBox.Show("BB Code is ready, copy it from txt file", "SWAT Test result");
        }

        private void evaluateCarDamage(ref string str)
        {
            switch (Convert.ToDouble(carhp1.Text))
            {
                //C# 7 feature - ranged switch statements (would've used simple if-elseif-else branch if not available)
                case double d when (d >= 900): str = "low car damage"; break;
                case double d when (d < 900 && d >= 750): str = "medium car damage"; break;
                case double d when (d < 750 && d >= 500): str = "big car damage"; break;
                case double d when (d < 500 && d >= 250): str = "heavy car damage"; break;
                //default should really be left to extraneous cases (such as invalid input), but bleh don't have time for that
                default: str = "blown car"; break;
            }
        }

        private string evaluateHP(double marks)
        {
            double[] intervals = { 0.9, 1.9, 2.4, 2.9, 3.9, 4.4, 4.9, 5 };
            string[] messages = { "I killed him with almost full armour", "I killed him with damaged armour(but not half)", "I killed him with Half armour", "I killed him with no armour (full hp)",
                                    "I killed him with damaged hp (but not half hp)", "I killed him with half hp", "I killed him with less than half hp", "He killed me"};
            
            //messages correspond to their respective marks - intervals[i] being smaller than 0 -> use that to our advantage
            for(int i=0; i < intervals.Length; ++i)
            {
                if (marks - intervals[i] < 0)
                    return messages[i];
            }
            return null;
        }

        private void roundoff_custom(ref double x)
        {
            double temp;
            temp = x - (int)x;
            switch (temp)
            {
                //C# 7 feature - ranged switch statements (would've used simple if-elseif-else branch if not available)
                case double d when (d >= 0.875): temp = 1; break;
                case double d when (d < 0.875 && d >= 0.625): temp = 0.75; break;
                case double d when (d < 0.625 && d >= 0.375): temp = 0.5; break;
                case double d when (d < 0.375 && d >= 0.125): temp = 0.25; break;
                default: temp = 0; break;
            }

            x = (int)x + temp;
        }
       

        private void roundoff_soc(ref double time)
        {
            double temp = time - (int)time;
            switch (temp)
            {
                //C# 7 feature - ranged switch statements (would've used simple if-elseif-else branch if not available)
                case double d when (d <= 0.25): temp = 0; break;
                case double d when (d > 0.25 && d <= 0.5): temp = 0.5; break;
                case double d when (d > 0.5 && d <= 0.75): temp = 0.5; break;
                case double d when (d > 0.75 && d <= 1): temp = 1; break;
            }
            time = (int)time + time;
        }

        double calculatesocNEW(double seconds)
        {

            double marks = 0;

            //failed in soc
            if (seconds >= 8)
                return marks;

            double temp = seconds - (int)seconds;
            seconds = (int)seconds;

            switch (seconds)
            {
                case 7: marks = 3; break;
                case 6: marks = 3.5; break;
                case 5: marks = 4; break;
                case 4: marks = 4.5; break;
                case 3: marks = 5; return marks;
            }

            if (temp == 0.5)
                marks -= 0.25;
            return marks;
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

