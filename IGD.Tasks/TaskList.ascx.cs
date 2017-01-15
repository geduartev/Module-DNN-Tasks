using DotNetNuke.UI.Modules;
using IGD.Tasks.Models;
using System;
using System.Collections;
using System.Linq;
using System.Web.UI.WebControls;
using DotNetNuke.Collections;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.UI.Skins;
using DotNetNuke.UI.Skins.Controls;
using System.Collections.Generic;

namespace IGD.Tasks
{
    public partial class TaskList : ModuleUserControlBase
    {
        private readonly TaskController _controller = new TaskController();
        private const string IncludedCompletedKey = "plTasks_IncludeCompletedTasks";

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //TaskController controller = new TaskController();

            if (!IsPostBack)
            {
                IEnumerable<Task> taskList = ModuleContext.Settings.GetValueOrDefault(IncludedCompletedKey, true)
                    ? _controller.GetTasks(ModuleContext.PortalSettings.UserId)
                    : _controller.GetTasks(ModuleContext.PortalSettings.UserId).Where(t => !t.IsComplete);

                DataGridTasks.DataSource = taskList;
                DataGridTasks.DataBind();
                // DataGridTasks.DataSource = controller.GetTasks(ModuleContext.PortalSettings.UserId);
                // DataGridTasks.DataBind();
            }

            hyperLinkAdd.NavigateUrl = ModuleContext.EditUrl("Edit");
        }

        protected void DeleteTask(object source, DataGridCommandEventArgs e)
        {
            try
            {
                var taskId = Convert.ToInt32(e.CommandArgument);
                var task = _controller.GetTaskById(taskId, ModuleContext.PortalSettings.UserId);
                _controller.DeleteTask(task);

                Response.Redirect(Request.RawUrl);
            }
            catch (Exception)
            {
                const string headerText = "Error";
                const string messageText = "There was error deleting the Task.<br /> Please check the Event Viewer for mor information.";
                Skin.AddModuleMessage(this, headerText, messageText, ModuleMessage.ModuleMessageType.RedError);
            }
        }
    }
}