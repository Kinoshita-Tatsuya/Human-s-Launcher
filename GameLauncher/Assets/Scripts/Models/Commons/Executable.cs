using Assets.Scripts.Infrastructures;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace GameLauncher.Models.Commons
{
    /// <summary>
    /// ファイルを実行する
    /// </summary>
    /// <remarks>
    /// 起動するファイルがショートカットでない場合起動できない場合がある
    /// </remarks>
    public class Executable
    {
        public event Action<Executable> OnProcessStarted;
        public event EventHandler OnProcessEnded;

        public Executable(string exePath)
        {
            ExePath = exePath;
        }

        public void Execute()
        {
            if (Process != null) return;

            try
            {
                ExecuteProcess();
            }
            catch (Win32Exception e)
            {
                EndProcess(this,EventArgs.Empty);
                MessageBoxShower.DisplayDialog("❌Error", "ファイルパスに間違いがあります。\n" + e.Message, "OK");
            }
            catch (InvalidOperationException e)
            {
                EndProcess(this, EventArgs.Empty);
                MessageBoxShower.DisplayDialog("❌Error", "ファイルパスが存在しません。\n" + e.Message, "OK");
            }
        }

        private void ExecuteProcess()
        {
            Process = new Process();

            // ProcessのFileNameにパスを入れて起動する際は絶対パスがいる
            var fullPath = Path.GetFullPath(ExePath);

            Process.StartInfo.FileName = fullPath;
            Process.EnableRaisingEvents = true;
            Process.Exited += EndProcess;

            Process.Start();

            OnProcessStarted?.Invoke(this);
        }

        private void EndProcess(object sender, EventArgs e)
        {
            Process.Dispose();
            Process = null;

            OnProcessEnded?.Invoke(sender, e);
        }

        private string ExePath { get; set; }
        private Process Process { get; set; } = null;
    }
}
