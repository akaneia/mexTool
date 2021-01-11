﻿using System.ComponentModel;
using System.Reflection;

namespace System.Windows.Forms
{
    /// <summary>
    /// Wraps System.Windows.Forms.OpenFileDialog to make it present
    /// a vista-style dialog.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignTimeVisible(false)]
    public partial class OpenFolderDialog : Component
    {
        // Wrapped dialog
        System.Windows.Forms.OpenFileDialog ofd = null;

        /// <summary>
        /// Default constructor
        /// </summary>
        public OpenFolderDialog()
        {
            ofd = new System.Windows.Forms.OpenFileDialog();

            ofd.Filter = "Folders|\n";
            ofd.AddExtension = false;
            ofd.CheckFileExists = false;
            ofd.DereferenceLinks = true;
            ofd.Multiselect = false;
        }

        #region Properties

        /// <summary>
        /// Gets/Sets the initial folder to be selected. A null value selects the current directory.
        /// </summary>
        public string InitialDirectory
        {
            get { return ofd.InitialDirectory; }
            set { ofd.InitialDirectory = value == null || value.Length == 0 ? Environment.CurrentDirectory : value; }
        }

        /// <summary>
        /// Gets/Sets the title to show in the dialog
        /// </summary>
        public string Title
        {
            get { return ofd.Title; }
            set { ofd.Title = value == null ? "Select a folder" : value; }
        }

        /// <summary>
        /// Gets the selected folder
        /// </summary>
        public string SelectedPath
        {
            get { return ofd.FileName; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Shows the dialog
        /// </summary>
        /// <returns>DialogResult.OK if user presses OK, else DialogResult.Cancel</returns>
        public DialogResult ShowDialog()
        {
            return ShowDialog(IntPtr.Zero);
        }

        /// <summary>
        /// Shows the dialog
        /// </summary>
        /// <param name="hWndOwner">Handle of the control to be parent</param>
        /// <returns>DialogResult.OK if user presses OK, else DialogResult.Cancel</returns>
        public DialogResult ShowDialog(IntPtr hWndOwner)
        {
            bool flag = false;

            if (Environment.OSVersion.Version.Major >= 6)
            {
                var r = new Reflector("System.Windows.Forms");

                uint num = 0;
                Type typeIFileDialog = r.GetType("FileDialogNative.IFileDialog");
                object dialog = r.Call(ofd, "CreateVistaDialog");
                r.Call(ofd, "OnBeforeVistaDialog", dialog);

                uint options = (uint)r.CallAs(typeof(System.Windows.Forms.FileDialog), ofd, "GetOptions");
                options |= (uint)r.GetEnum("FileDialogNative.FOS", "FOS_PICKFOLDERS");
                r.CallAs(typeIFileDialog, dialog, "SetOptions", options);

                object pfde = r.New("FileDialog.VistaDialogEvents", ofd);
                object[] parameters = new object[] { pfde, num };
                r.CallAs2(typeIFileDialog, dialog, "Advise", parameters);
                num = (uint)parameters[1];
                try
                {
                    int num2 = (int)r.CallAs(typeIFileDialog, dialog, "Show", hWndOwner);
                    flag = 0 == num2;
                }
                finally
                {
                    r.CallAs(typeIFileDialog, dialog, "Unadvise", num);
                    GC.KeepAlive(pfde);
                }
            }
            else
            {
                var fbd = new FolderBrowserDialog();
                fbd.Description = this.Title;
                fbd.SelectedPath = this.InitialDirectory;
                fbd.ShowNewFolderButton = false;
                if (fbd.ShowDialog(new WindowWrapper(hWndOwner)) != DialogResult.OK) return DialogResult.Cancel;
                ofd.FileName = fbd.SelectedPath;
                flag = true;
            }

            return flag ? DialogResult.OK : DialogResult.Cancel;
        }

        #endregion
    }

    /// <summary>
    /// Creates IWin32Window around an IntPtr
    /// </summary>
    public class WindowWrapper : System.Windows.Forms.IWin32Window
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="handle">Handle to wrap</param>
        public WindowWrapper(IntPtr handle)
        {
            _hwnd = handle;
        }

        /// <summary>
        /// Original ptr
        /// </summary>
        public IntPtr Handle
        {
            get { return _hwnd; }
        }

        private IntPtr _hwnd;
    }

    /// <summary>
    /// This class is from the Front-End for Dosbox and is used to present a 'vista' dialog box to select folders.
    /// Being able to use a vista style dialog box to select folders is much better then using the shell folder browser.
    /// http://code.google.com/p/fed/
    ///
    /// Example:
    /// var r = new Reflector("System.Windows.Forms");
    /// </summary>
    public class Reflector
    {
        #region variables

        string m_ns;
        Assembly m_asmb;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ns">The namespace containing types to be used</param>
        public Reflector(string ns)
            : this(ns, ns)
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="an">A specific assembly name (used if the assembly name does not tie exactly with the namespace)</param>
        /// <param name="ns">The namespace containing types to be used</param>
        public Reflector(string an, string ns)
        {
            m_ns = ns;
            m_asmb = null;
            foreach (AssemblyName aN in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
            {
                if (aN.FullName.StartsWith(an))
                {
                    m_asmb = Assembly.Load(aN);
                    break;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Return a Type instance for a type 'typeName'
        /// </summary>
        /// <param name="typeName">The name of the type</param>
        /// <returns>A type instance</returns>
        public Type GetType(string typeName)
        {
            Type type = null;
            string[] names = typeName.Split('.');

            if (names.Length > 0)
                type = m_asmb.GetType(m_ns + "." + names[0]);

            for (int i = 1; i < names.Length; ++i)
            {
                type = type.GetNestedType(names[i], BindingFlags.NonPublic);
            }
            return type;
        }

        /// <summary>
        /// Create a new object of a named type passing along any params
        /// </summary>
        /// <param name="name">The name of the type to create</param>
        /// <param name="parameters"></param>
        /// <returns>An instantiated type</returns>
        public object New(string name, params object[] parameters)
        {
            Type type = GetType(name);

            ConstructorInfo[] ctorInfos = type.GetConstructors();
            foreach (ConstructorInfo ci in ctorInfos)
            {
                try
                {
                    return ci.Invoke(parameters);
                }
                catch { }
            }

            return null;
        }

        /// <summary>
        /// Calls method <paramref name="func"/> on object <paramref name="obj"/> passing parameters 'parameters'
        /// </summary>
        /// <param name="obj">The object on which to execute <paramref name="func"/></param>
        /// <param name="func">The function to execute</param>
        /// <param name="parameters">The parameters to pass to <paramref name="func"/></param>
        /// <returns>The result of the function invocation</returns>
        public object Call(object obj, string func, params object[] parameters)
        {
            return Call2(obj, func, parameters);
        }

        /// <summary>
        /// Calls method <paramref name="func"/> on object <paramref name="obj"/> passing parameters 'parameters'
        /// </summary>
        /// <param name="obj">The object on which to execute function <paramref name="func"/></param>
        /// <param name="func">The function to execute</param>
        /// <param name="parameters">The parameters to pass to function <paramref name="func"/></param>
        /// <returns>The result of the function invocation</returns>
        public object Call2(object obj, string func, object[] parameters)
        {
            return CallAs2(obj.GetType(), obj, func, parameters);
        }

        /// <summary>
        /// Calls method <paramref name="func"/> on object <paramref name="obj"/> which is of type <paramref name="type"/> passing parameters 'parameters'
        /// </summary>
        /// <param name="type">The type of <paramref name="obj"/></param>
        /// <param name="obj">The object on which to execute function <paramref name="func"/></param>
        /// <param name="func">The function to execute</param>
        /// <param name="parameters">The parameters to pass to function <paramref name="func"/></param>
        /// <returns>The result of the function invocation</returns>
        public object CallAs(Type type, object obj, string func, params object[] parameters)
        {
            return CallAs2(type, obj, func, parameters);
        }

        /// <summary>
        /// Calls method <paramref name="func"/> on object <paramref name="obj"/> which is of type <paramref name="type"/> passing parameters 'parameters'
        /// </summary>
        /// <param name="type">The type of <paramref name="obj"/></param>
        /// <param name="obj">The object on which to execute function <paramref name="func"/></param>
        /// <param name="func">The function to execute</param>
        /// <param name="parameters">The parameters to pass to function <paramref name="func"/></param>
        /// <returns>The result of the function invocation</returns>
        public object CallAs2(Type type, object obj, string func, object[] parameters)
        {
            MethodInfo methInfo = type.GetMethod(func, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return methInfo.Invoke(obj, parameters);
        }

        /// <summary>
        /// Returns the value of property <paramref name="prop"/> of object <paramref name="obj"/>
        /// </summary>
        /// <param name="obj">The object containing <paramref name="prop"/></param>
        /// <param name="prop">The property name</param>
        /// <returns>The property value</returns>
        public object Get(object obj, string prop)
        {
            return GetAs(obj.GetType(), obj, prop);
        }

        /// <summary>
        /// Returns the value of property <paramref name="prop"/> of object <paramref name="obj"/> which has type <paramref name="type"/>
        /// </summary>
        /// <param name="type">The type of <paramref name="obj"/></param>
        /// <param name="obj">The object containing <paramref name="prop"/></param>
        /// <param name="prop">The property name</param>
        /// <returns>The property value</returns>
        public static object GetAs(Type type, object obj, string prop)
        {
            PropertyInfo propInfo = type.GetProperty(prop, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return propInfo.GetValue(obj, null);
        }

        /// <summary>
        /// Returns an enum value
        /// </summary>
        /// <param name="typeName">The name of enum type</param>
        /// <param name="name">The name of the value</param>
        /// <returns>The enum value</returns>
        public object GetEnum(string typeName, string name)
        {
            Type type = GetType(typeName);
            FieldInfo fieldInfo = type.GetField(name);
            return fieldInfo.GetValue(null);
        }

        #endregion

    }
}