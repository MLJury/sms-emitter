namespace SmsService
{
    partial class SmsService
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
            this.SmsServiceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.SmsServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // 
            this.SmsServiceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.SmsServiceProcessInstaller1.Password = null;
            this.SmsServiceProcessInstaller1.Username = null;
            // 
            // MailServiceInstaller
            // 
            this.SmsServiceInstaller.DisplayName = "SmsService";
            this.SmsServiceInstaller.ServiceName = "SmsService";
            // 
            // MailService
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SmsServiceProcessInstaller1,
            this.SmsServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller SmsServiceProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller SmsServiceInstaller;
    }
}