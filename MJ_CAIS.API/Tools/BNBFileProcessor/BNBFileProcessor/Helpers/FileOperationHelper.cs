using BNBFileProcessor.Configurations.Elements;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BNBFileProcessor.Helpers
{
    public static class FileOperationHelper
    {
        public static void CreateWatcherDirectories(WatcherSetting watcherSettings)
        {
            var user = System.Security.Principal.WindowsIdentity.GetCurrent();
            try
            {
                if (!System.IO.Directory.Exists(watcherSettings.CompleatePath))
                {
                    System.IO.Directory.CreateDirectory(watcherSettings.CompleatePath);
                    SetPartialAccess(watcherSettings.CompleatePath, user.Name);
                }

                if (!System.IO.Directory.Exists(watcherSettings.ErrorPath))
                {
                    System.IO.Directory.CreateDirectory(watcherSettings.ErrorPath);
                    SetPartialAccess(watcherSettings.ErrorPath, user.Name);
                }

                if (!System.IO.Directory.Exists(watcherSettings.ProcessingPath))
                {
                    System.IO.Directory.CreateDirectory(watcherSettings.ProcessingPath);
                    SetPartialAccess(watcherSettings.ProcessingPath, user.Name);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static EventLog CreateEventLog(string eventLogSource, string eventLogName, long maximumKilobytes)
        {
            EventLog eventLog = new EventLog();

            if (!System.Diagnostics.EventLog.SourceExists(eventLogSource))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                   eventLogSource, eventLogName);
            }
            eventLog.Source = eventLogSource;
            eventLog.Log = eventLogName;
            eventLog.MaximumKilobytes = maximumKilobytes;
            return eventLog;
        }


        public static void DeleteEventLog(string eventLogSource, string eventLogName)
        {
            try
            {
                if (EventLog.GetEventLogs().FirstOrDefault(x => x.Log == eventLogName) != null)
                {
                    EventLog.DeleteEventSource(eventLogSource);
                    EventLog.Delete(eventLogName);
                }
            }
            catch (Exception) { throw; }
        }

        public static void SetPartialAccess(string dir, string user)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            DirectorySecurity dirSecurity = GetPartialAccessSecurity(user);

            dirSecurity.AddAccessRule(new FileSystemAccessRule(user,
               FileSystemRights.Write | FileSystemRights.Read |
               FileSystemRights.Delete | FileSystemRights.DeleteSubdirectoriesAndFiles,
               InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
               PropagationFlags.None, AccessControlType.Allow));

            dirInfo.SetAccessControl(dirSecurity);
        }

        public static void SetFullAccess(string dir, string user)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            DirectorySecurity dirSecurity = GetFullAccessSecurity(user);

            dirSecurity.AddAccessRule(new FileSystemAccessRule(user,
                FileSystemRights.FullControl,
                InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                PropagationFlags.None, AccessControlType.Allow));

            dirInfo.SetAccessControl(dirSecurity);
        }

        private static DirectorySecurity GetPartialAccessSecurity(string user)
        {
            DirectorySecurity directorySecurity = new DirectorySecurity();

            directorySecurity.AddAccessRule(new FileSystemAccessRule(user,
               FileSystemRights.Write | FileSystemRights.Read |
               FileSystemRights.Delete | FileSystemRights.DeleteSubdirectoriesAndFiles,
               InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
               PropagationFlags.None, AccessControlType.Allow));

            return directorySecurity;
        }

        private static DirectorySecurity GetFullAccessSecurity(string user)
        {
            DirectorySecurity directorySecurity = new DirectorySecurity();
            directorySecurity.AddAccessRule(new FileSystemAccessRule(user,
              FileSystemRights.FullControl,
              InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
              PropagationFlags.None, AccessControlType.Allow));

            return directorySecurity;
        }

        public static void SetAccessToInstallerUser(string dir)
        {
            // Create security identifier for Local Service user; Local Service is used for the service at installation
            SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.LocalServiceSid, null);
            DirectoryInfo di = new DirectoryInfo(dir);
            DirectorySecurity ds = di.GetAccessControl();
            // add a new file access rule w/ write/modify for all users to the directory security object

            ds.AddAccessRule(new FileSystemAccessRule(sid,
                             FileSystemRights.Write | FileSystemRights.Modify,
                             InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,   // all sub-dirs to inherit
                             PropagationFlags.None,
                             AccessControlType.Allow));                                            // Turn write and modify on
            //Apply the directory security to the directory
            di.SetAccessControl(ds);
        }

        public static async Task CopyFileAsync(string sourcePath, string destinationPath)
        {
            using (Stream source = File.Open(sourcePath, FileMode.Open))
            {
                using (Stream destination = File.Create(destinationPath))
                {
                    await source.CopyToAsync(destination);
                }
            }
        }

        public static async Task MoveFileAsync(string sourcePath, string destinationPath)
        {
            if (IsFileLocked(sourcePath)) return;

            using (Stream source = File.Open(sourcePath, FileMode.Open, FileAccess.ReadWrite, FileShare.Delete))
            {
                using (Stream destination = File.Create(destinationPath))
                {
                    await source.CopyToAsync(destination);
                }
                File.Delete(sourcePath);
            }
        }

        public static bool IsFileLocked(string filePath)
        {
            FileStream stream = null;

            try
            {
                FileInfo fileInfo = new FileInfo(filePath);

                stream = fileInfo.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        public static async Task<string> CopyFileToDirectoryAsync(string sourcePath, string targetPath)
        {
            // Use static Path methods to extract only the file name from the path.
            string fileNameWithExtension = System.IO.Path.GetFileName(sourcePath);
            string destFilePath = System.IO.Path.Combine(targetPath, fileNameWithExtension);

            //throw new Exception("CopyFileToDirectoryAsync error");
            await MoveFileAsync(sourcePath, destFilePath);

            return destFilePath;
        }


    }
}
