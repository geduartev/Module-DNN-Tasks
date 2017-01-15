using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Collections;
using DotNetNuke.Entities.Modules;

namespace IGD.Tasks
{
    public partial class Settings : ModuleSettingsBase
    {
        private const string IncludedCompletedKey = "plTasks_IncludeCompletedTasks";

        public override void LoadSettings()
        {
            checkBoxIncludeComplete.Checked = ModuleSettings.GetValueOrDefault(IncludedCompletedKey, true);
        }

        public override void UpdateSettings()
        {
            var controller = new ModuleController();
            controller.UpdateModuleSetting(ModuleContext.ModuleId, IncludedCompletedKey, checkBoxIncludeComplete.Checked.ToString());
        }
    }
}