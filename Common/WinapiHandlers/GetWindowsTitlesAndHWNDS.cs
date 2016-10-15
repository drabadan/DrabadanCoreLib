using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DrabadanCoreLib.Common.WinapiHandlers
{    
    internal sealed class GetWindowsTitlesAndHWNDS
    {
        #region WindowHandleTests
        private class WindowHandleInfo
        {
            private delegate bool EnumWindowProc(IntPtr hwnd, IntPtr lParam);

            [DllImport("user32")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr lParam);

            private IntPtr _MainHandle;

            public WindowHandleInfo(IntPtr handle)
            {
                _MainHandle = handle;
            }

            public List<IntPtr> GetAllChildHandles()
            {
                List<IntPtr> childHandles = new List<IntPtr>();

                GCHandle gcChildhandlesList = GCHandle.Alloc(childHandles);
                IntPtr pointerChildHandlesList = GCHandle.ToIntPtr(gcChildhandlesList);

                try
                {
                    EnumWindowProc childProc = new EnumWindowProc(EnumWindow);
                    EnumChildWindows(this._MainHandle, childProc, pointerChildHandlesList);
                }
                finally
                {
                    gcChildhandlesList.Free();
                }

                return childHandles;
            }

            private bool EnumWindow(IntPtr hWnd, IntPtr lParam)
            {
                GCHandle gcChildhandlesList = GCHandle.FromIntPtr(lParam);

                if (gcChildhandlesList == null || gcChildhandlesList.Target == null)
                {
                    return false;
                }

                List<IntPtr> childHandles = gcChildhandlesList.Target as List<IntPtr>;
                childHandles.Add(hWnd);

                return true;
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(HandleRef hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(HandleRef hWnd, StringBuilder lpString, int nMaxCount);

        private static string GetWindowTextByHandle(object sender, IntPtr handle)
        {
            int capacity = GetWindowTextLength(new HandleRef(sender, handle)) * 2;
            StringBuilder stringBuilder = new StringBuilder(capacity);
            GetWindowText(new HandleRef(sender, handle), stringBuilder, stringBuilder.Capacity);
            return stringBuilder.ToString();
        }
        

        public static async Task<Process> SearchForProcessByNameAsync(string targetName)
        {
            return await Task.Run(()=> Process.GetProcesses().ToList().Find(p => p.MainWindowTitle.Contains(targetName)));
        }

        public static async Task<List<IntPtr>> GetChildWindowsHWNDAsync(Process process)
        {
            return await Task.Run(()=> new WindowHandleInfo(process.MainWindowHandle).GetAllChildHandles().ToList());
        }

        public static async Task<IList<string>> GetChildWindowsTitlesAsync(Process process)
        {
            List<string> result = new List<string>();
            var hwndsList = await GetChildWindowsHWNDAsync(process);
            var currProcess = Process.GetCurrentProcess();
            hwndsList?.ForEach(p => result.Add(GetWindowTextByHandle(currProcess, p)));
            return result;
        }

        //private async Task ProcesessTests() 
        //{
        //    var proc = Process.GetProcesses().ToList().Find(p => p.MainWindowTitle.Contains("Stealth"));
        //    var allChildWindows = new WindowHandleInfo(proc.MainWindowHandle).GetAllChildHandles().ToList();
        //    List<string> allTitles = new List<string>();
        //    allChildWindows.ForEach(w => allTitles.Add(GetWindowTextByHandle(w)));


        //    SendConsoleMessage($"You are trying to connect the script to: {allTitles[allTitles.IndexOf("Main") + 1].Split(' ').First()} character!");
        //}

        #endregion



    }
}
