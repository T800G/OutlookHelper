using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Linq;
using Aras.Tools.InnovatorAdmin.Dialog;

namespace WinEventMonitor
{
	public class WinEventHook
	{
		public const String APPWNDCLASSNAME =  "rctrl_renwnd32";
		public const String APPWNDCAPTION = "Outlook";
		
		[DllImport("user32.dll")]
		public static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess,uint idThread, uint dwFlags);
		[DllImport("user32.dll")]
		public static extern bool UnhookWinEvent(IntPtr hWinEventHook);
		[DllImport("kernel32.dll", SetLastError = true)]
  		static extern uint GetCurrentThreadId();

		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		
 		EventConstants[] arrevt = {
//	 		EventConstants.EVENT_OBJECT_CREATE,
			EventConstants.EVENT_OBJECT_SHOW //,
//				EventConstants.EVENT_OBJECT_FOCUS
// 			EventConstants.EVENT_SYSTEM_FOREGROUND,
	 		};
		
		public delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
		static WinEventDelegate procDelegate = new WinEventDelegate(WinEventProc);	
   

		private IntPtr m_hook;
		
		public WinEventHook()
		{
			m_hook = IntPtr.Zero;
			m_hook = SetWinEventHook((uint)arrevt.Min(),
			                         (uint)arrevt.Max(),
			                         IntPtr.Zero,
			                         procDelegate,
			                         0, 0,
			                         (uint)(EventHookFlags.WINEVENT_OUTOFCONTEXT | EventHookFlags.WINEVENT_SKIPOWNPROCESS));

		}
		~WinEventHook()
		{
			UnhookWinEvent(m_hook);
		}

	
		static void	WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
		{
			// Did this event happen on the same thread we are on...Could be some issues if the CLR thread does not match the native thread.
			if (GetCurrentThreadId() == dwEventThread) return;
			
			if (idObject == Convert.ToInt32(ObjectIdentifiers.OBJID_WINDOW))
			{
				switch ((EventConstants)eventType)
				{
					//DebugHelper.DbgTraceWinEventName(eventType);
//					case EventConstants.EVENT_OBJECT_FOCUS:
//					{
//						Debug.WriteLine("EVENT_OBJECT_FOCUS whnd=0x" + hwnd.ToString("X") );
//					Debug.WriteLine("caption=" + Win32Helpers.GetWindowCaption(hwnd));
//					
//					}
//					break;
					
					case EventConstants.EVENT_OBJECT_SHOW:
						if (Win32Helpers.IsTopWindow(hwnd)
						    && Win32Helpers.IsWindowClass(hwnd, APPWNDCLASSNAME)
						    && Win32Helpers.GetWindowCaption(hwnd).Contains(APPWNDCAPTION))
						{
							Debug.WriteLine("caption=" + Win32Helpers.GetWindowCaption(hwnd));
							Debug.WriteLine("EVENT_OBJECT_SHOW class=" + APPWNDCLASSNAME + " whnd=0x" + hwnd.ToString("X")/*GetForegroundWindow()*/);
							
							//skip if on remote desktop
							if (System.Windows.Forms.SystemInformation.TerminalServerSession)
							{
								//Debug.WriteLine("TerminalServerSession=true");
								return;
							}
							
							//check duplicate vs extended desktop
							//skip if only one monitor or desktop duplicated on other monitors
							//Debug.WriteLine("AllScreens.Length=" + System.Windows.Forms.Screen.AllScreens.Length);
							if (System.Windows.Forms.Screen.AllScreens.Length < 2) return;
							
							//force window reposition
							Aras.Tools.InnovatorAdmin.Dialog.InteropUtil.WINDOWPLACEMENT wpl = Aras.Tools.InnovatorAdmin.Dialog.InteropUtil.GetWindowPlacement(hwnd);
							//Debug.WriteLine("wpl.showCmd =" + wpl.showCmd);							
							Win32Helpers.PostMessage(hwnd, (uint) WM.WM_SYSCOMMAND, new IntPtr((int) SysCommands.SC_MINIMIZE), IntPtr.Zero);
							Win32Helpers.PostMessage(hwnd, (uint) WM.WM_SYSCOMMAND, new IntPtr((int) SysCommands.SC_RESTORE), IntPtr.Zero);
							switch (wpl.showCmd)
							{
								case 3: //SW_MAXIMIZE
								Win32Helpers.PostMessage(hwnd, (uint) WM.WM_SYSCOMMAND, new IntPtr((int) SysCommands.SC_MAXIMIZE), IntPtr.Zero);
								break;
							}
						}
					break;	
				}
			}
		}

	} //end class

}
