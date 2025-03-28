using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class UnityExplorerConfig
{
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
