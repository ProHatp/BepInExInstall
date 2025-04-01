using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BepInExInstall
{
    public static class UIManager
    {
        public static void ShowError(string message, string title = "Erro")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowInfo(string message, string title = "Informação")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void UpdateProgress(MetroFramework.Controls.MetroProgressBar progressBar, Label statusLabel, int progress, string statusText = null)
        {
            if (progressBar.InvokeRequired || statusLabel.InvokeRequired)
            {
                progressBar.Invoke(new Action(() => UpdateProgress(progressBar, statusLabel, progress, statusText)));
                return;
            }

            progressBar.Value = progress;
            if (!string.IsNullOrEmpty(statusText))
                statusLabel.Text = statusText;
        }

        public static void ShowProgress(MetroFramework.Controls.MetroProgressBar progressBar, Label statusLabel, string initialText = "")
        {
            progressBar.Visible = true;
            statusLabel.Visible = true;
            progressBar.Value = 0;
            statusLabel.Text = initialText;
        }

        public static void HideProgress(MetroFramework.Controls.MetroProgressBar progressBar, Label statusLabel)
        {
            progressBar.Visible = false;
            statusLabel.Visible = false;
            progressBar.Value = 0;
        }
    }
}
