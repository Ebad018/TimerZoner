using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace TimeZoner.Interop;

public static class DesktopInterop
{
    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    
    static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
    const uint SWP_NOSIZE = 0x0001;
    const uint SWP_NOMOVE = 0x0002;
    const uint SWP_NOACTIVATE = 0x0010;
    const int WM_WINDOWPOSCHANGING = 0x0046;

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWPOS
    {
        public IntPtr hwnd;
        public IntPtr hwndInsertAfter;
        public int x;
        public int y;
        public int cx;
        public int cy;
        public uint flags;
    }

    public static void PinToDesktop(Window window)
    {
        var hwnd = new WindowInteropHelper(window).Handle;
        
        // Initial push to bottom
        SetWindowPos(hwnd, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
        
        // Hook to intercept window position changes
        HwndSource source = HwndSource.FromHwnd(hwnd);
        source.AddHook(new HwndSourceHook(WndProc));
    }

    private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        if (msg == WM_WINDOWPOSCHANGING)
        {
            var ptr = Marshal.PtrToStructure(lParam, typeof(WINDOWPOS));
            if (ptr != null)
            {
                WINDOWPOS wp = (WINDOWPOS)ptr;
                // Ensure the window doesn't try to go above HWND_BOTTOM
                wp.hwndInsertAfter = HWND_BOTTOM;
                Marshal.StructureToPtr(wp, lParam, false);
            }
        }
        return IntPtr.Zero;
    }
}
