using Microsoft.Extensions.Hosting;

namespace BNBFileProcessor
{
    partial class FileMonitoring : IHostLifetime
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
            this.eventLog = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog)).BeginInit();
            // 
            // eventLog
            // 
            this.eventLog.Log = "TL_BNB_Event_Listener_Log";
            this.eventLog.Source = "TL_BNB_Event_Listener";
            // 
            // FileMonitoring
            // 
            this.AutoLog = false;
            this.CanShutdown = true;
            this.ServiceName = "BNB Event Listener";
            ((System.ComponentModel.ISupportInitialize)(this.eventLog)).EndInit();

        }

        #endregion

        public System.Diagnostics.EventLog eventLog;
    }
}
