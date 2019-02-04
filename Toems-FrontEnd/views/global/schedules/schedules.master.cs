﻿using System;
using Toems_Common;
using Toems_Common.Dto;
using Toems_Common.Entity;
using Toems_FrontEnd.BasePages;

namespace Toems_FrontEnd.views.global.schedules
{
    public partial class schedules : MasterBaseMaster
    {
        public EntitySchedule Schedule { get; set; }
        private BasePages.Global GlobalBasePage { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            GlobalBasePage = Page as BasePages.Global;
            GlobalBasePage.RequiresAuthorization(AuthorizationStrings.ScheduleRead);
            Schedule = GlobalBasePage.Schedule;
            if (Schedule != null)
            {
                Level1.Visible = false;
                Level2.Visible = true;
                btnDelete.Visible = true;

            }
            else
            {
                Level1.Visible = true;
                Level2.Visible = false;
                btnDelete.Visible = false;
            }

        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            lblTitle.Text = "Delete " + Schedule.Name + "?";
            DisplayConfirm();
        }

        protected void buttonConfirm_Click(object sender, EventArgs e)
        {
            var result = new DtoActionResult();
            result = GlobalBasePage.Call.ScheduleApi.Delete(Schedule.Id);

            if (result.Success)
            {
                PageBaseMaster.EndUserMessage = "Successfully Deleted Schedule: " + Schedule.Name;
                Response.Redirect("~/views/global/schedules/search.aspx");
            }
            else
                PageBaseMaster.EndUserMessage = result.ErrorMessage;
        }
    }
}