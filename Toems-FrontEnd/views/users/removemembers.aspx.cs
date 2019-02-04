﻿using System;
using System.Web.UI.WebControls;
using Toems_Common;
using Toems_Common.Dto;
using Toems_FrontEnd.BasePages;

namespace Toems_FrontEnd.views.users
{
    public partial class views_users_removemembers : Users
    {
        protected void btnRemoveSelected_OnClick(object sender, EventArgs e)
        {
            var successCount = 0;
            foreach (GridViewRow row in gvUsers.Rows)
            {
                var cb = (CheckBox) row.FindControl("chkSelector");
                if (cb == null || !cb.Checked) continue;
                var dataKey = gvUsers.DataKeys[row.RowIndex];
                if (dataKey != null)
                {
                    var user = Call.ToemsUserApi.Get(Convert.ToInt32(dataKey.Value));
                    user.UserGroupId = -1;

                    if (Call.ToemsUserApi.Put(user.Id, user).Success)
                        successCount++;
                }
            }
            EndUserMessage = "Successfully Removed " + successCount + " Users From The Group";
            PopulateGrid();
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            ChkAll(gvUsers);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RequiresAuthorization(AuthorizationStrings.Administrator);
            if (IsPostBack) return;
            PopulateGrid();
        }

        protected void PopulateGrid()
        {
            var filter = new DtoSearchFilter();
            filter.Limit = 0;
            filter.SearchText = txtSearch.Text;
            gvUsers.DataSource = Call.UserGroupApi.GetGroupMembers(ToemsUserGroup.Id, filter);
            gvUsers.DataBind();
            lblTotal.Text = gvUsers.Rows.Count + " Result(s) / " +
                            Call.UserGroupApi.GetMemberCount(ToemsUserGroup.Id) +
                            " Total User(s)";
        }

        protected void search_Changed(object sender, EventArgs e)
        {
            PopulateGrid();
        }
    }
}