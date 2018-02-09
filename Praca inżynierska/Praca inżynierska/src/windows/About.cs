using log4net;
using PI.src.general;
using PI.src.helpers;
using PI.src.localization.windows;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace PI.src.windows
{
    public partial class About : Form
    {
        private static readonly ILog log = LogManager.GetLogger( MethodBase.GetCurrentMethod().DeclaringType );
        private volatile bool IsFormShown;
        private AppTimer timer;

        public About()
        {
            InitializeComponent();
            IsFormShown = false;
            timer = new AppTimer();
            timer.OnCount += new EventHandler<OnCountEventArgs>( OnTimerCount );
            LocalizeWindow();
            UpdateUiBySettings();
            timer.Start();
        }

        private void UpdateUiBySettings()
        {
            uiTech_DotNet2_TxtBx.Text = SystemInfos.TryGetDotNetFrameworkVersion();
            uiTech_LogPath2_TxtBx.Text = "NotImplemented";
            uiMeta_Ver2_TxtBx.Text = SystemInfos.TryGetAssemblyVersion();
            uiMeta_Com2_TxtBx.Text = SystemInfos.TryGetCompanyName();
            uiOth_Ctr2_TxtBx.Text = "0:00:00:00";
            uiOth_Repo2_TxtBx.Text = "https://www.github.com/vego1mar/PI/releases";
        }

        private void LocalizeWindow()
        {
            AboutStrings names = new AboutStrings();
            Text = names.Form.Text.GetString();

            // Ui: Technical informations
            uiTech_Title_TxtBx.Text = names.Ui.TechnicalTitle.GetString();
            uiTech_DotNet1_TxtBx.Text = names.Ui.TechnicalDotNet.GetString();
            uiTech_LogPath1_TxtBx.Text = names.Ui.TechnicalLogPath.GetString();

            // Ui: Assembly metadata
            uiMeta_Title_TxtBx.Text = names.Ui.MetadataTitle.GetString();
            uiMeta_Ver1_TxtBx.Text = names.Ui.MetadataVersion.GetString();
            uiMeta_Com1_TxtBx.Text = names.Ui.MetadataCompany.GetString();

            // Ui: Other
            uiOth_Title_TxtBx.Text = names.Ui.OtherTitle.GetString();
            uiOth_Ctr1_TxtBx.Text = names.Ui.OtherCounter.GetString();
            uiOth_Repo1_TxtBx.Text = names.Ui.OtherRepository.GetString();
            uiOth_Promo1_TxtBx.Text = names.Ui.OtherPromoter1.GetString();
            uiOth_Promo2_TxtBx.Text = names.Ui.OtherPromoter2.GetString();
        }

        protected virtual void OnTimerCount( object sender, OnCountEventArgs e )
        {
            UpdateUiByTimerCount( e.Time );
        }

        private void UpdateUiByTimerCount( string time )
        {
            try {
                if ( IsFormShown ) {
                    BeginInvoke( (MethodInvoker) delegate { uiOth_Ctr2_TxtBx.Text = time; } );
                }
            }
            catch ( ObjectDisposedException ex ) {
                log.Error( MethodBase.GetCurrentMethod().Name + '(' + time + ')', ex );
            }
            catch ( InvalidOperationException ex ) {
                log.Error( MethodBase.GetCurrentMethod().Name + '(' + time + ')', ex );
            }
            catch ( Exception ex ) {
                log.Fatal( MethodBase.GetCurrentMethod().Name + '(' + time + ')', ex );
            }
        }

        #region Event handlers

        private void OnShown( object sender, EventArgs e )
        {
            LocalizeWindow();
            IsFormShown = true;
            ui_TblLay.Select();
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + (sender as Form).Name + ')' );
        }

        private void OnFormClosed( object sender, FormClosedEventArgs e )
        {
            IsFormShown = false;
            log.Info( MethodBase.GetCurrentMethod().Name + '(' + (sender as Form).Name + ',' + e.CloseReason + ')' );
        }

        #endregion
    }
}
