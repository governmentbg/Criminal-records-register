using BNBFileProcessor.Configurations.Elements;
using BNBFileProcessor.ExceptionHandlers;
using BNBFileProcessor.FileProcessors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace BNBFileProcessor.FileWatchers
{
    public class TimeWatcher
    {
        private System.Diagnostics.EventLog _AppEventLog;
        private System.Threading.Timer _ThreadTimer;

        private WatcherSetting _watcherSettings;
        private FileProcessor _fileOperation;

        private int _Interval;

        private string _Name = "";
        public string Name
        {
            get { return this._Name; }
            set { this._Name = value; }
        }

        public TimeWatcher(WatcherSetting watcherSettings, System.Diagnostics.EventLog appEventLog)
        {
            _watcherSettings = watcherSettings;

            _fileOperation = new FileProcessor(appEventLog, _watcherSettings.WatcherKey, _watcherSettings.DeleteCompletedFiles);

            _Interval = _watcherSettings.ElapsedTime;

            _ThreadTimer = new System.Threading.Timer(new TimerCallback(OnThreadTimer), null, 0, Timeout.Infinite);

            this._AppEventLog = appEventLog;
        }

        private async void OnThreadTimer(object state)
        {
            try
            {
                //string[] files = System.IO.Directory.GetFiles(_watcherSettings.SourcePath, _watcherSettings.FileFilter).Take(_watcherSettings.MaxFileProcess).ToArray();

                DirectoryInfo directoryInfo = new DirectoryInfo(_watcherSettings.SourcePath);
                IEnumerable<FileInfo> filesFileInfo = directoryInfo.GetFiles(_watcherSettings.FileFilter, SearchOption.AllDirectories)
                                                 .OrderBy(f => f.LastWriteTime).Take(_watcherSettings.MaxFileProcess).ToList();

                if (filesFileInfo == null || filesFileInfo.Count() == 0) return;

                //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

                await _fileOperation.CopyFilesToDirectoryAsync(filesFileInfo.Select(x => x.FullName).ToArray(), _watcherSettings.ProcessingPath, 
                    _watcherSettings.CompleatePath, _watcherSettings.ErrorPath);
            }
            catch (AggregateException ex)
            {
                foreach (var exception in ex.Flatten().InnerExceptions)
                {
                    EventListenerErrorLogger.HandleExceptionAsync(exception, _AppEventLog);
                }
            }
            catch (Exception ex)
            {
                EventListenerErrorLogger.HandleExceptionAsync(ex, _AppEventLog);
            }
            finally
            {
                StartThreadTimeWatcher();
            }
        }

        private void StartThreadTimeWatcher()
        {
            _ThreadTimer = new System.Threading.Timer(new TimerCallback(OnThreadTimer), null, _Interval, Timeout.Infinite);
        }

        public void StopThreadTimer()
        {
            _ThreadTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }

    }
}
