namespace Kama.SmsService
{
    partial class KamaSmsService
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.KamaSmsServiceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.KamaSmsServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // KamaMailServiceProcessInstaller1
            // 
            this.KamaSmsServiceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.KamaSmsServiceProcessInstaller1.Password = null;
            this.KamaSmsServiceProcessInstaller1.Username = null;
            // 
            // KamaMailServiceInstaller
            // 
            this.KamaSmsServiceInstaller.DisplayName = "KamaSmsService";
            this.KamaSmsServiceInstaller.ServiceName = "KamaSmsService";
            // 
            // KamaMailService
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.KamaSmsServiceProcessInstaller1,
            this.KamaSmsServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller KamaSmsServiceProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller KamaSmsServiceInstaller;
    }
}