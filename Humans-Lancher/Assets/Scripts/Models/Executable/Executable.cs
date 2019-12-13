using System.ComponentModel;
using System.Diagnostics;
using UnityEditor;

namespace HumansLancher.Models.Executable
{
    public class Executable : AbstractExecutable
    {
        public Executable(string exePath) : base(exePath) 
        {
        }

        public override void Execute()
        {
            //他のプロセスが起動中なら実行しない
            if (proc != null) return;
            try
            {
                ExecuteProcess();
            }
            catch (Win32Exception e)
            {
                EditorUtility.DisplayDialog("❌Error", "ファイルパスに間違いがあります。\n" + e.Message, "OK");
            }
        }

        private void ExecuteProcess()
        {
            using (proc = new Process())
            {
                //起動したいファイルのパス
                proc.StartInfo.FileName = ExePath;

                //別プロセス終了時の処理を行うようにするフラグをオン
                proc.EnableRaisingEvents = true;

                //実際の終了処理
                proc.Exited += (s, e) =>
                {
                    proc.Dispose();
                    proc = null;
                };

                //実行
                proc.Start();
            }
        }

        Process proc { get; set; } = null;
    }
}
