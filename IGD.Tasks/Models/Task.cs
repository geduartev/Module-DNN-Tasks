using System.Data;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;

namespace IGD.Tasks.Models
{
    public class Task : IHydratable
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }

        public void Fill(IDataReader dr)
        {
            TaskId = Null.SetNullInteger(dr["TaskId"]);
            UserId = Null.SetNullInteger(dr["UserId"]);
            Name = Null.SetNullString(dr["Name"]);
            Description = Null.SetNullString(dr["Description"]);
            IsComplete = Null.SetNullBoolean(dr["IsComplete"]);
        }

        public int KeyID
        {
            get { return TaskId; }
            set { TaskId = value; }
        }
    }
}