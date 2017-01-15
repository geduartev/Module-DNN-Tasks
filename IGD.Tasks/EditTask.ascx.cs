using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Collections;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.UI.Modules;
using IGD.Tasks.Models;

namespace IGD.Tasks
{
    public partial class EditTask : ModuleUserControlBase
    {
        private int _taskId;
        private readonly TaskController _controller = new TaskController();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _taskId = Request.QueryString.GetValueOrDefault("TaskId", -1);
            if (_taskId > -1 && !IsPostBack)
            {
                var task = _controller.GetTaskById(_taskId, ModuleContext.PortalSettings.UserId);
                textBoxName.Text = task.Name;
                textBoxDescription.Text = task.Description;
                checkBoxIsComplete.Checked = task.IsComplete;
            }
        }
        protected void SaveTask(object sender, EventArgs e)
        {
            var task = new Task
            {
                TaskId = _taskId,
                Name = textBoxName.Text,
                Description = textBoxDescription.Text,
                IsComplete = checkBoxIsComplete.Checked,
                UserId = ModuleContext.PortalSettings.UserId
            };

            if (_taskId == -1)
            {
                _controller.AddTask(task);
            }
            if (_taskId != -1)
            {
                _controller.UpdateTask(task);
            }
            
            Response.Redirect(Globals.NavigateURL());
        }

        protected void Cancel(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL());
        }
    }
}