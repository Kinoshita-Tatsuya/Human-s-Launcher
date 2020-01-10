using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using System.Windows.Forms;

namespace Assets.Scripts.Infrastructures
{
    class MessageBoxShower
    {
        public static bool DisplayDialog(string title, string message, string ok)
        {
#if UNITY_EDITOR 
            return EditorUtility.DisplayDialog(title, message, ok);
#else
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return true;
#endif
        }
    }
}
