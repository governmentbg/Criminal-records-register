
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

using BNBFileProcessor.Configuration;
using BNBFileProcessor.Configurations.Elements;
using BNBFileProcessor.Helpers;
using BNBFileProcessor.FileWatchers;
using BNBFileProcessor.ExceptionHandlers;
using Microsoft.Extensions.Hosting;

namespace BNBFileProcessor
{
    public partial class FileMonitoring : ServiceBase
    {
        private List<Task> _tasks = new List<Task>();

        private CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private CancellationToken CancellationToken
        {
            get
            {
                return _tokenSource.Token;
            }
        }

        private EventListenerSetting _settings;

        public FileMonitoring(EventListenerSetting settings)
        {
            InitializeComponent();

            _settings = settings;
        }

        private async Task CreateWatcherDirectories(WatcherSetting watcherSettings)
        {
            try
            {
                FileOperationHelper.CreateWatcherDirectories(watcherSettings);
            }
            catch (Exception ex)
            {
                await EventListenerErrorLogger.HandleExceptionAsync(ex, eventLog);
                this.OnStop();
            }
        }

        internal void StartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();            
            Console.ReadLine();
        }


        protected async override void OnStart(string[] args)
        {
            eventLog.WriteEntry("In OnStart");

            try
            {
                foreach (WatcherSetting watcherSettings in _settings.WatcherSettingsList.Watchers)
                {
                    await CreateWatcherDirectories(watcherSettings);

                    _tasks.Add(Task.Factory.StartNew((settings) =>
                      {
                          var watcherSetting = settings as WatcherSettingsWrapper;
                          TimeWatcher timeWatcher = new TimeWatcher(watcherSetting.WatcherSettings, eventLog);
                      }, new WatcherSettingsWrapper() { WatcherSettings = watcherSettings }, CancellationToken));
                }

                await Task.WhenAll(_tasks);
            }
            catch (AggregateException ex)
            {
                foreach (var exception in ex.Flatten().InnerExceptions)
                {
                    EventListenerErrorLogger.HandleExceptionAsync(exception, eventLog);
                }
            }
            catch (Exception ex)
            {
                EventListenerErrorLogger.HandleExceptionAsync(ex, eventLog);
            }
        }

        protected override void OnShutdown()
        {
            //_tokenSource.Cancel();

            //foreach (var task in _tasks)
            //{
            //    var status = task.Status;
            //}
            eventLog.WriteEntry("In OnShutdown.");
            base.OnShutdown();
        }

        protected override void OnStop()
        {
            //_tokenSource.Cancel();

            //foreach (var task in _tasks)
            //{
            //    var status = task.Status;
            //}
            Thread.Sleep(TimeSpan.FromSeconds(10));
            eventLog.WriteEntry("In OnStop.");
            base.OnStop();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
