﻿using BugTracker2.DataAccess;
using BugTracker2.Models;
using System;
using System.Collections.Generic;

namespace BugTracker2.Interfaces
{
    public interface IBugModel
    {
        int BugID { get; set; }
        string BugStatus { get; set; }
        string BugDescription { get; set; }
        DateTime CreatedOn { get; set; }

        public static int CreateBug(BugModel bug)
        {
            BugModel data = new()
            {
                BugStatus = "New",
                BugDescription = bug.BugDescription,
            };

            string sqlCreateBug = @$"INSERT INTO dbo.bugs (BugStatus, BugDescription) VALUES(@BugStatus, @BugDescription)";

            return SqlDataAccess.SaveData(sqlCreateBug, data);
        }

        public static List<BugModel> LoadBugs()
        {
            string sql = @"SELECT BugID, BugStatus, BugDescription, CreatedOn FROM dbo.bugs";

            return SqlDataAccess.LoadData<BugModel>(sql);
        }
    }
}
