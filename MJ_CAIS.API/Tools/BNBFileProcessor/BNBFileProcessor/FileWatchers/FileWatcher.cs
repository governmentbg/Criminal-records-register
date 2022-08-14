
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
    public class FileWatcher
    {
        private FileSystemWatcher _watcher;
        private System.Diagnostics.EventLog _AppEventLog;
        private WatcherSetting _watcherSettings;
        private FileProcessor _fileOperation;

        public FileWatcher(WatcherSetting watcherSettings, System.Diagnostics.EventLog appEventLog)
        {
            _watcherSettings = watcherSettings;

            _fileOperation = new FileProcessor(appEventLog, watcherSettings.WatcherKey, watcherSettings.DeleteCompletedFiles);

            _AppEventLog = appEventLog;
            _watcher = new FileSystemWatcher();
            _watcher.Path = _watcherSettings.SourcePath;
            /* Watch for changes in LastAccess and LastWrite times, and
               the renaming of files or directories. */
            _watcher.NotifyFilter = NotifyFilters.LastWrite;
            // Only watch text files.
            _watcher.Filter = _watcherSettings.FileFilter;
            // Add event handlers.
            //_watcher.Created += OnCreated;
            _watcher.Changed += OnCreated;
            // Begin watching.
        }

        public void StartFileWatcherToWatch()
        {
            _watcher.EnableRaisingEvents = true;
        }

        public void StopFileWatcherToWatch()
        {
            _watcher.EnableRaisingEvents = false;
            _watcher.Dispose();
        }

        private async void OnCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                string fullPath = Path.Combine(_watcherSettings.SourcePath, e.Name);

                await _fileOperation.CopyFilesToDirectoryAsync(new string[] { fullPath }, _watcherSettings.ProcessingPath
                    , _watcherSettings.CompleatePath, _watcherSettings.ErrorPath);

                //Console.WriteLine(string.Format("File with name: {0}, in : {1}, operation type: {2} ", e.Name, e.FullPath, e.ChangeType));
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
        }
    }
}
