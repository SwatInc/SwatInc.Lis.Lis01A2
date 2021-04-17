using System;
using System.Runtime.InteropServices;

namespace SwatInc.Lis.Lis02A2
{
	public static class LISLogConsole
	{
		private const int SW_HIDE = 0;

		private const int SW_SHOW = 5;

		private const uint SC_CLOSE = 61536u;

		private const uint MF_BYCOMMAND = 0u;

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool AllocConsole();

		[DllImport("kernel32.dll")]
		private static extern IntPtr GetConsoleWindow();

		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		[DllImport("Kernel32")]
		private static extern void FreeConsole();

		[DllImport("user32.dll")]
		private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

		[DllImport("user32.dll")]
		private static extern IntPtr DeleteMenu(IntPtr hMenu, uint uPosition, uint uFlags);

		public static void ShowConsoleWindow()
		{
			IntPtr handle = GetConsoleWindow();
			if (handle == IntPtr.Zero)
			{
				AllocConsole();
				handle = GetConsoleWindow();
				IntPtr exitButton = GetSystemMenu(handle, bRevert: false);
				if (exitButton != (IntPtr)0)
				{
					DeleteMenu(exitButton, 61536u, 0u);
				}
			}
			else
			{
				ShowWindow(handle, 5);
			}
		}

		public static void CloseConsoleWindow()
		{
			IntPtr handle = GetConsoleWindow();
			if (handle != IntPtr.Zero)
			{
				FreeConsole();
			}
		}
	}
}
