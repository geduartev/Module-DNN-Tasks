using System;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;
using DotNetNuke.Services.Log.EventLog;
using IGD.Tasks.Models;
using DotNetNuke.Services.Exceptions;

namespace IGD.Tasks.Models
{
    public class TaskController
    {
        private const string CacheKey = "PL_Tasks_{0}";
        private const CacheItemPriority CachePriority = CacheItemPriority.Default;
        private const int CacheTimeOut = 20;

        private readonly EventLogController _eventLogController = new EventLogController();

        private void AddLog(string key, string friendlyName, Task task)
        {
            EnsureLogTypeExists(key, friendlyName);

            var log = new LogInfo
            {
                LogTypeKey = key
            };

            log.LogProperties.Add(new LogDetailInfo("TaskName", task.Name));
            _eventLogController.AddLog(log);
        }

        private void EnsureLogTypeExists(string key, string friendlyName)
        {
            LogTypeInfo logType = null;
            var logTypeDictionary = _eventLogController.GetLogTypeInfoDictionary();
            if (!logTypeDictionary.TryGetValue(key, out logType))
            {
                logType = new LogTypeInfo()
                {
                    LogTypeKey = key,
                    LogTypeFriendlyName = friendlyName,
                    LogTypeDescription = string.Empty,
                    LogTypeCSSClass = "OperationSucess",
                    LogTypeOwner = "DotNetNuke.Logging.EventLogType"
                };
                _eventLogController.AddLogType(logType);

                var logTypeConfig = new LogTypeConfigInfo()
                {
                    LoggingIsActive = true,
                    LogTypeKey = key,
                    KeepMostRecent = "100",
                    NotificationThreshold = 1,
                    NotificationThresholdTime = 1,
                    NotificationThresholdTimeType = LogTypeConfigInfo.NotificationThresholdTimeTypes.Days,
                    MailFromAddress = string.Empty,
                    MailToAddress = string.Empty,
                    LogTypePortalID = "*"
                };
                _eventLogController.AddLogTypeConfigInfo(logTypeConfig);
            }
        }

        public void AddTask(Task task)
        {
            task.TaskId = DataProvider.Instance()
                .ExecuteScalar<int>("plTasks_CreateTask", task.Name, task.Description, task.IsComplete, task.UserId);

            //var log = new LogInfo
            //{
            //    LogTypeKey = EventLogController.EventLogType.ADMIN_ALERT.ToString()
            //};

            //log.LogProperties.Add(new LogDetailInfo("Action", "New Task Create"));
            //log.LogProperties.Add(new LogDetailInfo("TaskName", task.Name));

            //var controller = new EventLogController();
            //controller.AddLog(log);

            AddLog("TASK_CREATED","Task created", task);
            ClearCache(task);
        }

        private void ClearCache(Task task)
        {
            string cacheKey = string.Format(CacheKey, task.UserId);
            DataCache.RemoveCache(cacheKey);
        }

        public void DeleteTask(Task task)

        {
            try
            {
                // Para evaluar comportamiento de los exception: throw new Exception("This is just a demostration of the one exception");

                DataProvider.Instance().ExecuteNonQuery("plTasks_DeleteTask", task.TaskId);
                AddLog("TASK_DELETED", "Task deleted", task);
                ClearCache(task);
            }
            catch (Exception exc)
            {
                Exceptions.LogException(exc);
                throw new Exception("An exception was Logged when deleting the task.");
            }
        }

        public Task GetTaskById(int taskId, int userId)
        {
            // return CBO.FillObject<Task>(DataProvider.Instance().ExecuteReader("plTasks_GetTask", taskId));
            return GetTasks(userId).SingleOrDefault(t => t.TaskId == taskId);
        }

        private IList<Task> GetTasksInternal(int userId)
        {
            return CBO.FillCollection<Task>(DataProvider.Instance().ExecuteReader("plTasks_GetTasks", userId));
        } 

        public IList<Task> GetTasks(int userId)
        {
            string cacheKey = string.Format(CacheKey, userId);
            var cacheArgs = new CacheItemArgs(cacheKey, CacheTimeOut, CachePriority);
            // Sin Cache: return CBO.FillCollection<Task>(DataProvider.Instance().ExecuteReader("plTasks_GetTasks", userId));
            return CBO.GetCachedObject<IList<Task>>(cacheArgs, c => GetTasksInternal(userId));
        }

        public void UpdateTask(Task task)
        {
            DataProvider.Instance().ExecuteNonQuery("plTasks_UpdateTasks", task.TaskId, task.Name, task.Description, task.IsComplete);
            AddLog("TASK_UPDATED", "Task updated", task);
            ClearCache(task);
        }
    }
}