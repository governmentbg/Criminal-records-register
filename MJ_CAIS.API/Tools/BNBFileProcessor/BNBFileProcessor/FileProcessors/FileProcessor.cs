
using BNBFileProcessor.ExceptionHandlers;
using BNBFileProcessor.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BNBFileProcessor.FileProcessors
{
    public class FileProcessor
    {
        private string _watcherType = null;
        private readonly bool _deleteCompletedFiles;

        public EventLog EventLog { get; }

        public FileProcessor(EventLog eventLog, string watcherType, bool deleteCompletedFiles = false)
        {
            EventLog = eventLog;
            _watcherType = watcherType;
            _deleteCompletedFiles = deleteCompletedFiles;
        }

        public async Task CopyFilesToDirectoryAsync(string[] files, string processingPath, string compleatePath, string errorPath)
        {

            if (files == null) throw new ArgumentException(nameof(files));
            if (string.IsNullOrEmpty(compleatePath)) throw new ArgumentException(nameof(compleatePath));
            if (string.IsNullOrEmpty(errorPath)) throw new ArgumentException(nameof(errorPath));

            //EventListenerErrorLogger.LogInEventLog("Getting files", EventLog);

            //StartWatch();

            var fileTasks = (from file in files   //First move file to Processing folder
                             select Task.Factory.StartNew(async () => await FileOperationHelper.CopyFileToDirectoryAsync(file, processingPath), TaskCreationOptions.AttachedToParent)
                             .Unwrap()
                             .ContinueWith(async (copyFileToRunningTask, filePathToCopy) =>
                             {
                                 var fileName = Path.GetFileName(filePathToCopy.ToString());

                                 //Get result from parent task
                                 if (copyFileToRunningTask.IsFaulted || copyFileToRunningTask.IsCanceled)
                                 {
                                     EventListenerErrorLogger.HandleExceptionAsync(copyFileToRunningTask.Exception, EventLog, fileName);
                                 }
                                 else
                                 {
                                     string processingFilePath = await copyFileToRunningTask;

                                     //Process File from Processing folder
                                     await Task.Factory.StartNew(async (filePath) => await ProcessFile(processingFilePath),
                                         processingFilePath, TaskCreationOptions.AttachedToParent).Unwrap()
                                     .ContinueWith(async (processFileTask, data) =>
                                     {
                                         string processingFilePathParam = data?.ToString();

                                         if (processFileTask.IsFaulted || processFileTask.IsCanceled)
                                         {
                                             EventListenerErrorLogger.HandleExceptionAsync(processFileTask.Exception, EventLog, fileName);

                                             await FileOperationHelper.CopyFileToDirectoryAsync(processingFilePathParam, errorPath)
                                             .ContinueWith(failedTask =>
                                             {
                                                 EventListenerErrorLogger.HandleExceptionAsync(failedTask.Exception, EventLog, fileName);
                                             }, TaskContinuationOptions.NotOnRanToCompletion);
                                         }
                                         else
                                         {
                                             //If succeeded process file move it to Compleated folder or else go move to error folder
                                             await FileOperationHelper.CopyFileToDirectoryAsync(processingFilePathParam, compleatePath)
                                               .ContinueWith(async copyFileToDirectoryTask =>
                                               {
                                                   if (copyFileToDirectoryTask.IsFaulted)
                                                   {
                                                       EventListenerErrorLogger.HandleExceptionAsync(copyFileToDirectoryTask.Exception, EventLog, fileName);
                                                   }
                                                   else
                                                   {
                                                       if (!_deleteCompletedFiles) return;

                                                       string compleatedFilePath = await copyFileToDirectoryTask;

                                                       try
                                                       {
                                                           await AsyncTaskOperationHelper.RetryOnFault(() =>
                                                           {
                                                               File.Delete(compleatedFilePath);

                                                               return Task.CompletedTask;
                                                           }, 2, () => Task.Delay(500));
                                                       }
                                                       catch (Exception ex)
                                                       {
                                                           EventListenerErrorLogger.HandleExceptionAsync(ex, EventLog, fileName);
                                                       }
                                                   }

                                               }, TaskContinuationOptions.AttachedToParent);
                                         }
                                     }, processingFilePath, TaskContinuationOptions.AttachedToParent);
                                 }
                             }, file, TaskContinuationOptions.AttachedToParent).Unwrap()).ToArray();


            //StopWatch();

            await Task.CompletedTask;
        }

        private async Task ProcessFile(string fullPath)
        {
            if (FileOperationHelper.IsFileLocked(fullPath)) await Task.FromException(new Exception(string.Format("File {0} is locked", fullPath)));

            //IEventProcessor eventProcessor = new BNBFileProcessor(new MJ_CAIS.DataAccess.CaisDbContext(),null);
            IEventProcessor eventProcessor = DIContainer.Container.Services.GetService<IBNBEventProcessor>();
            //new BNBFileProcessor(new MJ_CAIS.DataAccess.CaisDbContext(), null);
            await eventProcessor.Process(fullPath);

            await Task.CompletedTask;
        }


        #region StopWatcherHelper

        public static Stopwatch stopwatch;

        public static void StartWatch()
        {
            stopwatch = new Stopwatch();
            // Begin timing.
            stopwatch.Start();
        }

        public static void StopWatch()
        {
            // Stop timing.
            stopwatch.Stop();
            // Write result.
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
        }

        #endregion

    }
}