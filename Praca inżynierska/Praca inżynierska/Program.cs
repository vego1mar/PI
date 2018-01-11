using System.Windows.Forms;
using System.Reflection;
using System;
using log4net;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo( "PITests" )]
[assembly: log4net.Config.XmlConfigurator( Watch = true )]

namespace PI
{
    static class Program
    {
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            log.Info( "Application started." );
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new MainWindow() );
            log.Info( "Application exited." );
        }
    }
}
