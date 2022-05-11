using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace WinEventMonitor
{
	public sealed class NotificationIcon
	{
		private NotifyIcon notifyIcon;
		private ContextMenu notificationMenu;
		
		#region Initialize icon and menu
		public NotificationIcon()
		{
			notifyIcon = new NotifyIcon();
			//Debug.WriteLine("SystemInformation.SmallIconSize=" + SystemInformation.SmallIconSize);
			notificationMenu = new ContextMenu(InitializeMenu());
			
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationIcon));
			notifyIcon.Icon = (Icon)resources.GetObject("icon" + SystemInformation.SmallIconSize.Width);
			notifyIcon.ContextMenu = notificationMenu;
#if DEBUG
			notifyIcon.Text = "Outlook Helper _DEBUG_";
#else
			notifyIcon.Text = "Outlook Helper";
#endif
		}
		
		private MenuItem[] InitializeMenu()
		{
			MenuItem[] menu = new MenuItem[] {
				new MenuItem("Exit", menuExitClick)
			};
			return menu;
		}
		#endregion
		
		
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern bool SetProcessDPIAware();
		
		#region Main - Program entry point
		[STAThread]
		public static void Main(string[] args)
		{
			if (Environment.OSVersion.Version.Major >= 6) SetProcessDPIAware();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			bool isFirstInstance;
			// Please use a unique name for the mutex to prevent conflicts with other programs
			using (Mutex mtx = new Mutex(true, "{6B18A1EB-EC1C-4fa9-8539-3A1D95814B4D}", out isFirstInstance)) {
				if (isFirstInstance)
				{
					WinEventHook evth = new WinEventHook();

					NotificationIcon notificationIcon = new NotificationIcon();
					notificationIcon.notifyIcon.Visible = true;
					Application.Run();
					notificationIcon.notifyIcon.Dispose();
				} else {
					MessageBox.Show("The application is already running");
				}
			} // releases the Mutex
		}
		#endregion
		
		#region Event Handlers
		private void menuExitClick(object sender, EventArgs e)
		{
			Application.Exit();
		}
		#endregion
	}
}
