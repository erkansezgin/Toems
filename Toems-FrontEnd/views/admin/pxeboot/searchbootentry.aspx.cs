﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Toems_Common.Dto;

namespace Toems_FrontEnd.views.admin.pxeboot
{
    public partial class searchbootentry : BasePages.Admin
    {
        protected void ButtonConfirmDelete_Click(object sender, EventArgs e)
        {
            var deletedCount = 0;
            foreach (GridViewRow row in gvEntries.Rows)
            {
                var cb = (CheckBox)row.FindControl("chkSelector");
                if (cb == null || !cb.Checked) continue;
                var dataKey = gvEntries.DataKeys[row.RowIndex];
                if (dataKey == null) continue;
                if (Call.CustomBootMenuApi.Delete(Convert.ToInt32(dataKey.Value)).Success)
                    deletedCount++;
            }
            EndUserMessage = "Successfully Deleted " + deletedCount + " Custom Boot Entry(s)";
            PopulateGrid();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            PopulateGrid();
        }

        protected void PopulateGrid()
        {
            var filter = new DtoSearchFilter();
            filter.Limit = 0;
            filter.SearchText = txtSearch.Text;
            gvEntries.DataSource = Call.CustomBootMenuApi.Search(filter);
            gvEntries.DataBind();

            lblTotal.Text = gvEntries.Rows.Count + " Result(s) / " + Call.CustomBootMenuApi.GetCount() +
                            " Custom Boot Entry(s)";
        }

        protected void search_Changed(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            DisplayConfirm();
        }

        protected void chkSelectAll_CheckedChanged1(object sender, EventArgs e)
        {
            ChkAll(gvEntries);
        }
    }
}