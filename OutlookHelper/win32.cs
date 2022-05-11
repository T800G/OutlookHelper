using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Globalization; 

namespace WinEventMonitor
{
	public enum EventConstants
        {
            EVENT_AIA_START = 0xA000,
            EVENT_AIA_END = 0xAFFF,
            EVENT_MIN = 0x00000001,
            EVENT_MAX = 0x7FFFFFFF,
            EVENT_OBJECT_ACCELERATORCHANGE = 0x8012,
            EVENT_OBJECT_CONTENTSCROLLED = 0x8015,
            EVENT_OBJECT_CREATE = 0x8000,
            EVENT_OBJECT_DEFACTIONCHANGE = 0x8011,
            EVENT_OBJECT_DESCRIPTIONCHANGE = 0x800D,
            EVENT_OBJECT_DESTROY = 0x8001,
            EVENT_OBJECT_DRAGSTART = 0x8021,
            EVENT_OBJECT_DRAGCANCEL = 0x8022,
            EVENT_OBJECT_DRAGCOMPLETE = 0x8023,
            EVENT_OBJECT_DRAGENTER = 0x8024,
            EVENT_OBJECT_DRAGLEAVE = 0x8025,
            EVENT_OBJECT_DRAGDROPPED = 0x8026,
            EVENT_OBJECT_END = 0x80FF,
            EVENT_OBJECT_FOCUS = 0x8005,
            EVENT_OBJECT_HELPCHANGE = 0x8010,
            EVENT_OBJECT_HIDE = 0x8003,
            EVENT_OBJECT_HOSTEDOBJECTSINVALIDATED = 0x8020,
            EVENT_OBJECT_IME_HIDE = 0x8028,
            EVENT_OBJECT_IME_SHOW = 0x8027,
            EVENT_OBJECT_IME_CHANGE = 0x8029,
            EVENT_OBJECT_INVOKED = 0x8013,
            EVENT_OBJECT_LIVEREGIONCHANGED = 0x8019,
            EVENT_OBJECT_LOCATIONCHANGE = 0x800B,
            EVENT_OBJECT_NAMECHANGE = 0x800C,
            EVENT_OBJECT_PARENTCHANGE = 0x800F,
            EVENT_OBJECT_REORDER = 0x8004,
            EVENT_OBJECT_SELECTION = 0x8006,
            EVENT_OBJECT_SELECTIONADD = 0x8007,
            EVENT_OBJECT_SELECTIONREMOVE = 0x8008,
            EVENT_OBJECT_SELECTIONWITHIN = 0x8009,
            EVENT_OBJECT_SHOW = 0x8002,
            EVENT_OBJECT_STATECHANGE = 0x800A,
            EVENT_OBJECT_TEXTEDIT_CONVERSIONTARGETCHANGED = 0x8030,
            EVENT_OBJECT_TEXTSELECTIONCHANGED = 0x8014,
            EVENT_OBJECT_VALUECHANGE = 0x800E,
            EVENT_SYSTEM_ALERT = 0x0002,
            EVENT_SYSTEM_ARRANGMENTPREVIEW = 0x8016,
            EVENT_SYSTEM_CAPTUREEND = 0x0009,
            EVENT_SYSTEM_CAPTURESTART = 0x0008,
            EVENT_SYSTEM_CONTEXTHELPEND = 0x000D,
            EVENT_SYSTEM_CONTEXTHELPSTART = 0x000C,
            EVENT_SYSTEM_DESKTOPSWITCH = 0x0020,
            EVENT_SYSTEM_DIALOGEND = 0x0011,
            EVENT_SYSTEM_DIALOGSTART = 0x0010,
            EVENT_SYSTEM_DRAGDROPEND = 0x000F,
            EVENT_SYSTEM_DRAGDROPSTART = 0x000E,
            EVENT_SYSTEM_END = 0x00FF,
            EVENT_SYSTEM_FOREGROUND = 0x0003,
            EVENT_SYSTEM_MENUPOPUPEND = 0x0007,
            EVENT_SYSTEM_MENUPOPUPSTART = 0x0006,
            EVENT_SYSTEM_MENUEND = 0x0005,
            EVENT_SYSTEM_MENUSTART = 0x0004,
            EVENT_SYSTEM_MINIMIZEEND = 0x0017,
            EVENT_SYSTEM_MINIMIZESTART = 0x0016,
            EVENT_SYSTEM_MOVESIZEEND = 0x000B,
            EVENT_SYSTEM_MOVESIZESTART = 0x000A,
            EVENT_SYSTEM_SCROLLINGEND = 0x0013,
            EVENT_SYSTEM_SCROLLINGSTART = 0x0012,
            EVENT_SYSTEM_SOUND = 0x0001,
            EVENT_SYSTEM_SWITCHEND = 0x0015,
            EVENT_SYSTEM_SWITCHSTART = 0x0014,
            EVENT_OEM_DEFINED_START = 0x0101,
            EVENT_OEM_DEFINED_END = 0x01FF,
            EVENT_UIA_EVENTID_START = 0x4E00,
            EVENT_UIA_EVENTID_END = 0x4EFF,
            EVENT_UIA_PROPID_START = 0x7500,
            EVENT_UIA_PROPID_END = 0x75FF,
            
			EVENT_OBJECT_UNCLOAKED = 0x8018,
			EVENT_OBJECT_CLOAKED = 0x8017

        };
	
        [Flags]
        public enum EventHookFlags
        {
            WINEVENT_OUTOFCONTEXT = 0x0000,
            WINEVENT_SKIPOWNTHREAD = 0x0001,
            WINEVENT_SKIPOWNPROCESS = 0x0002,
            WINEVENT_INCONTEXT = 0x0004
        };
        
        public enum CommonObjectIDs
        {
	        CHILDID_SELF = 0,
			INDEXID_OBJECT = 0,
			INDEXID_CONTAINER = 0        
        };
        	
        public enum ObjectIdentifiers: long
        {
        	OBJID_WINDOW        = (long)0x00000000,
			OBJID_SYSMENU       = (long)0xFFFFFFFF,
			OBJID_TITLEBAR      = (long)0xFFFFFFFE,
			OBJID_MENU          = (long)0xFFFFFFFD,
			OBJID_CLIENT        = (long)0xFFFFFFFC,
			OBJID_VSCROLL       = (long)0xFFFFFFFB,
			OBJID_HSCROLL       = (long)0xFFFFFFFA,
			OBJID_SIZEGRIP      = (long)0xFFFFFFF9,
			OBJID_CARET         = (long)0xFFFFFFF8,
			OBJID_CURSOR        = (long)0xFFFFFFF7,
			OBJID_ALERT      	= (long)0xFFFFFFF6,
			OBJID_SOUND        	= (long)0xFFFFFFF5,
			OBJID_QUERYCLASSNAMEIDX = (long)0xFFFFFFF4,
			OBJID_NATIVEOM      = (long)0xFFFFFFF0
        };
        
        // http://www.pinvoke.net/default.aspx/Enums/WindowsMessages.html
		public enum WM : uint
		{
			WM_SYSCOMMAND = 0x0112,
		}
        
		// http://www.pinvoke.net/default.aspx/Enums/SysCommands.html
		public enum SysCommands : int
		{
		    SC_SIZE = 0xF000,
		    SC_MOVE = 0xF010,
		    SC_MINIMIZE = 0xF020,
		    SC_MAXIMIZE = 0xF030,
		    SC_NEXTWINDOW = 0xF040,
		    SC_PREVWINDOW = 0xF050,
		    SC_CLOSE = 0xF060,
		    SC_VSCROLL = 0xF070,
		    SC_HSCROLL = 0xF080,
		    SC_MOUSEMENU = 0xF090,
		    SC_KEYMENU = 0xF100,
		    SC_ARRANGE = 0xF110,
		    SC_RESTORE = 0xF120,
		    SC_TASKLIST = 0xF130,
		    SC_SCREENSAVE = 0xF140,
		    SC_HOTKEY = 0xF150,
		    //#if(WINVER >= 0x0400) //Win95
		    SC_DEFAULT = 0xF160,
		    SC_MONITORPOWER = 0xF170,
		    SC_CONTEXTHELP = 0xF180,
		    SC_SEPARATOR = 0xF00F,
		    //#endif /* WINVER >= 0x0400 */
		
		    //#if(WINVER >= 0x0600) //Vista
		    SCF_ISSECURE = 0x00000001,
		    //#endif /* WINVER >= 0x0600 */
		
		    /*
		        * Obsolete names
		        */
		    SC_ICON = SC_MINIMIZE,
		    SC_ZOOM = SC_MAXIMIZE,
		}
		
		
		// http://www.pinvoke.net/default.aspx/Enums/ShowWindowCommand.html
		enum ShowWindowCommands
		{
		    /// <summary>
		    /// Hides the window and activates another window.
		    /// </summary>
		    Hide = 0,
		    /// <summary>
		    /// Activates and displays a window. If the window is minimized or 
		    /// maximized, the system restores it to its original size and position.
		    /// An application should specify this flag when displaying the window 
		    /// for the first time.
		    /// </summary>
		    Normal = 1,
		    /// <summary>
		    /// Activates the window and displays it as a minimized window.
		    /// </summary>
		    ShowMinimized = 2,
		    /// <summary>
		    /// Maximizes the specified window.
		    /// </summary>
		    Maximize = 3, // is this the right value?
		                  /// <summary>
		                  /// Activates the window and displays it as a maximized window.
		                  /// </summary>       
		    ShowMaximized = 3,
		    /// <summary>
		    /// Displays a window in its most recent size and position. This value 
		    /// is similar to <see cref="Win32.ShowWindowCommand.Normal"/>, except 
		    /// the window is not activated.
		    /// </summary>
		    ShowNoActivate = 4,
		    /// <summary>
		    /// Activates the window and displays it in its current size and position. 
		    /// </summary>
		    Show = 5,
		    /// <summary>
		    /// Minimizes the specified window and activates the next top-level 
		    /// window in the Z order.
		    /// </summary>
		    Minimize = 6,
		    /// <summary>
		    /// Displays the window as a minimized window. This value is similar to
		    /// <see cref="Win32.ShowWindowCommand.ShowMinimized"/>, except the 
		    /// window is not activated.
		    /// </summary>
		    ShowMinNoActive = 7,
		    /// <summary>
		    /// Displays the window in its current size and position. This value is 
		    /// similar to <see cref="Win32.ShowWindowCommand.Show"/>, except the 
		    /// window is not activated.
		    /// </summary>
		    ShowNA = 8,
		    /// <summary>
		    /// Activates and displays the window. If the window is minimized or 
		    /// maximized, the system restores it to its original size and position. 
		    /// An application should specify this flag when restoring a minimized window.
		    /// </summary>
		    Restore = 9,
		    /// <summary>
		    /// Sets the show state based on the SW_* value specified in the 
		    /// STARTUPINFO structure passed to the CreateProcess function by the 
		    /// program that started the application.
		    /// </summary>
		    ShowDefault = 10,
		    /// <summary>
		    ///  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread 
		    /// that owns the window is not responding. This flag should only be 
		    /// used when minimizing windows from a different thread.
		    /// </summary>
		    ForceMinimize = 11
		}
		
	public class Win32Helpers
	{
		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName,int nMaxCount);
		
		[DllImport("user32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
		public static extern IntPtr GetParent(IntPtr hWnd);
		
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		static extern int GetWindowText(int hWnd, StringBuilder title, int size);

		public static String GetWindowCaption(IntPtr hWnd)
		{
			StringBuilder title = new StringBuilder(256);
			if (GetWindowText((int)hWnd, title, 256)>0) return title.ToString();
			return String.Empty;
		}
		public static bool IsTopWindow(IntPtr hWnd)
		{
			return (GetParent(hWnd) == IntPtr.Zero);
		}
			
		public static bool IsWindowClass(IntPtr hWnd, string clsName)
		{
		    // Pre-allocate 256 characters, since this is the maximum class name length.
		    StringBuilder ClassName = new StringBuilder(256);
		    int nRet = GetClassName(hWnd, ClassName, ClassName.Capacity);
		    if(nRet != 0)
		    {
		        return (string.Compare(ClassName.ToString(), clsName, true, CultureInfo.InvariantCulture) == 0);
		    }
		    else
		    {
		        return false;
		    }
		}
		
		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
	
	};
};