﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Toems_Common.Dto;
using Toems_Common.Entity;

namespace Toems_FrontEnd.views.policies
{
    public partial class assignedmodules : BasePages.Policies
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                chkScript.Checked = true;
                chkCommand.Checked = true;
                chkSoftware.Checked = true;
                chkFile.Checked = true;
                chkPrinter.Checked = true;
                chkWu.Checked = true;
                PopulateGrid();
            }
        }

        protected void ddl_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        protected void PopulateGrid()
        {
            var limit = 0;
            limit = ddlLimit.Text == "All" ? int.MaxValue : Convert.ToInt32(ddlLimit.Text);

            var filter = new DtoModuleSearchFilter();
            filter.Limit = limit;
            filter.Searchstring = txtSearch.Text;
            filter.IncludePrinter = chkPrinter.Checked;
            filter.IncludeSoftware = chkSoftware.Checked;
            filter.IncludeCommand = chkCommand.Checked;
            filter.IncludeFileCopy = chkFile.Checked;
            filter.IncludeScript = chkScript.Checked;
            filter.IncludeWu = chkWu.Checked;
            var modules = Call.PolicyApi.GetAssignedModules(Policy.Id, filter);
            gvModules.DataSource = modules;
            gvModules.DataBind();

            // lblTotal.Text = gvModules.Rows.Count + " Result(s) / " + Call.PolicyApi.GetCount() + " Policy(s)";
        }

        protected void search_Changed(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        protected void gridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            PopulateGrid();

            var listModules = (List<EntityPolicyModules>)gvModules.DataSource;
            switch (e.SortExpression)
            {
                case "Name":
                    listModules = GetSortDirection(e.SortExpression) == "Desc"
                        ? listModules.OrderByDescending(h => h.Name).ToList()
                        : listModules.OrderBy(h => h.Name).ToList();
                    break;
                case "Order":
                    listModules = GetSortDirection(e.SortExpression) == "Desc"
                        ? listModules.OrderByDescending(h => h.Order).ToList()
                        : listModules.OrderBy(h => h.Order).ToList();
                    break;
                case "ModuleType":
                    listModules = GetSortDirection(e.SortExpression) == "Desc"
                        ? listModules.OrderByDescending(h => h.ModuleType).ToList()
                        : listModules.OrderBy(h => h.ModuleType).ToList();
                    break;

            }

            gvModules.DataSource = listModules;
            gvModules.DataBind();
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            ChkAll(gvModules);
        }

        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {
            var control = sender as Control;
            if (control != null)
            {
                var gvRow = (GridViewRow)control.Parent.Parent;
                var dataKey = gvModules.DataKeys[gvRow.RowIndex];
                if (dataKey != null)
                {
                    var name = gvRow.Cells[2].Text;
                    var policyModule = Call.PolicyModulesApi.Get(Convert.ToInt32(dataKey.Value));
                    policyModule.Order = Convert.ToInt32(((TextBox) gvRow.FindControl("txtOrder")).Text);
                    var result = Call.PolicyModulesApi.Put(policyModule.Id, policyModule);
                    if (result.Success) EndUserMessage = "Successfully Updated " + name;
                    else EndUserMessage = result.ErrorMessage;
                    PopulateGrid();
                }
            }
           
        }

        protected void btnRemove_OnClick(object sender, EventArgs e)
        {
            var control = sender as Control;
            if (control != null)
            {
                var gvRow = (GridViewRow)control.Parent.Parent;
                var dataKey = gvModules.DataKeys[gvRow.RowIndex];
                if (dataKey != null)
                {
                    var name = gvRow.Cells[2].Text;
                    var result = Call.PolicyModulesApi.Delete(Convert.ToInt32(dataKey.Value));
                    if (result.Success) EndUserMessage = "Successfully Removed " + name;
                    else EndUserMessage = result.ErrorMessage;
                    PopulateGrid();
                }
            }
        }

        protected void chkFilter_OnCheckedChanged(object sender, EventArgs e)
        {
            PopulateGrid();
        }
    }
}