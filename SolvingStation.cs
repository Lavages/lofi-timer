using System;
using System.Windows.Forms;

namespace LofiCubeTimer
{
    public partial class SolvingStation : Form
    {
        private DateTime startTime;
        private TimeSpan elapsedTime;

        public SolvingStation()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Start the stopwatch
            startTime = DateTime.Now - elapsedTime;
            timer1.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            // Stop the stopwatch
            timer1.Stop();
            elapsedTime = DateTime.Now - startTime;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Reset the stopwatch
            timer1.Stop();
            elapsedTime = TimeSpan.Zero;
            UpdateTimerDisplay();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Update the elapsed time
            elapsedTime = DateTime.Now - startTime;
            UpdateTimerDisplay();
        }

        private void UpdateTimerDisplay()
        {
            // Display the elapsed time
            lblTimer.Text = elapsedTime.ToString(@"mm\:ss\.ff");
        }

        private void lblTimer_Click(object sender, EventArgs e)
        {

        }
    }
}
