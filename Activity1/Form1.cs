using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Activity1
{
    public partial class Form1 : Form
    {
        VaccuumEnvironment env = new VaccuumEnvironment();
        Agent agent = new SimpleReflexAgent();
        int cx, cy;
        public Form1()
        {
            InitializeComponent();
            cx = 0; cy = 0;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "Creating a 2x2 world \n";
            richTextBox1.Text += env; // add the env conditions

            for(int step = 0; step < 10; step++)
            {
                var percept = env.Percept(agent);
                var action = agent.Program(percept) as string;

                env.ExecuteAction(agent, action);

                var tup = percept as Tuple<int, int, bool>;
                string locText = "(?, ?)";
                if (tup != null) locText = $"({tup.Item1}, {tup.Item2})";

                richTextBox1.Text += $"Step {step + 1}: Action = {action} | Location = {locText} | Score = {agent.Performance}\n";

                await Task.Delay(1000);
            }

            richTextBox1.Text += "Final World:\n";
            richTextBox1.Text += $"{env.ToString()}";
            richTextBox1.Text += $"FINAL PERFORMANCE SCORE: {agent.Performance}\n"; 

        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Graphics g = e.Graphics;
        }

    }
}
