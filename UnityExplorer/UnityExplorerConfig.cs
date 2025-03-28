using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class UnityExplorerConfig
{
    public void OpenRepository()
    {
        var psi = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "https://github.com/yukieiji/UnityExplorer",
            UseShellExecute = true
        };
        System.Diagnostics.Process.Start(psi);
    }

    public void RemoverUnityExplorer(string exePath)
    {
        try
        {
            string targetFolder = Path.GetDirectoryName(exePath);
            string bepinexPath  = Path.Combine(targetFolder, "BepInEx", "plugins", "sinai-dev-UnityExplorer");

            if (Directory.Exists(bepinexPath))
            {
                Directory.Delete(bepinexPath, true);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error removing files: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
