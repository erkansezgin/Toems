﻿using System;
using System.Collections.Generic;
using Toems_Common;
using Toems_Common.Entity;
using Toems_FrontEnd.BasePages;

namespace Toems_FrontEnd.views.admin
{
    public partial class views_admin_security : Admin
    {
      

        protected void btnUpdateSettings_OnClick(object sender, EventArgs e)
        {
          
            var listSettings = new List<EntitySetting>
            {
              
                new EntitySetting
                {
                    Name = SettingStrings.LdapEnabled,
                    Value = Convert.ToInt16(chkldap.Checked).ToString(),
                    Id = Call.SettingApi.GetSetting(SettingStrings.LdapEnabled).Id
                },
                  new EntitySetting
                {
                    Name = SettingStrings.RequirePreProvision,
                    Value = Convert.ToInt16(chkPreProvision.Checked).ToString(),
                    Id = Call.SettingApi.GetSetting(SettingStrings.RequirePreProvision).Id
                },
                  new EntitySetting
                {
                    Name = SettingStrings.RequireResetRequests,
                    Value = Convert.ToInt16(chkResetRequest.Checked).ToString(),
                    Id = Call.SettingApi.GetSetting(SettingStrings.RequireResetRequests).Id
                },
                  new EntitySetting
                {
                    Name = SettingStrings.RequireProvisionApproval,
                    Value = Convert.ToInt16(chkProvisionApproval.Checked).ToString(),
                    Id = Call.SettingApi.GetSetting(SettingStrings.RequireProvisionApproval).Id
                },
                    new EntitySetting
                {
                    Name = SettingStrings.PreProvisionRequiresApproval,
                    Value = Convert.ToInt16(chkPreProvisionApproval.Checked).ToString(),
                    Id = Call.SettingApi.GetSetting(SettingStrings.PreProvisionRequiresApproval).Id
                },
               
                new EntitySetting
                {
                    Name = SettingStrings.IntercomKeyEncrypted,
                    Value = Guid.NewGuid().ToString("N").ToUpper() + Guid.NewGuid().ToString("N").ToUpper(),
                    Id = Call.SettingApi.GetSetting(SettingStrings.IntercomKeyEncrypted).Id
                },

              
            };

            if (!string.IsNullOrEmpty(txtKey.Text))
            {
                listSettings.Add(
                    new EntitySetting
                    {
                        Name = SettingStrings.ProvisionKeyEncrypted,
                        Value = txtKey.Text,
                        Id = Call.SettingApi.GetSetting(SettingStrings.ProvisionKeyEncrypted).Id
                    });
            }
          
          
            EndUserMessage = Call.SettingApi.UpdateSettings(listSettings) ? "Successfully Updated Settings" : "Could Not Update Settings";

         
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            chkldap.Checked = GetSetting(SettingStrings.LdapEnabled) == "1";
            chkResetRequest.Checked = GetSetting(SettingStrings.RequireResetRequests) == "1";
            chkPreProvision.Checked = GetSetting(SettingStrings.RequirePreProvision) == "1";
            chkProvisionApproval.Checked = GetSetting(SettingStrings.RequireProvisionApproval) == "1";
            chkPreProvisionApproval.Checked = GetSetting(SettingStrings.PreProvisionRequiresApproval) == "1";

        }

        protected void chkPreProvision_OnCheckedChanged(object sender, EventArgs e)
        {
            if (!chkPreProvision.Checked)
                chkPreProvisionApproval.Checked = false;
        }

        protected void chkPreProvisionApproval_OnCheckedChanged(object sender, EventArgs e)
        {
            if (!chkPreProvision.Checked && chkPreProvisionApproval.Checked)
            {
                chkPreProvisionApproval.Checked = false;
                EndUserMessage = "Pre Provision Approval Required Depends On Pre Provision Required";
            }
        }

        protected void btnGenKey_OnClick(object sender, EventArgs e)
        {
            txtKey.Text = Guid.NewGuid().ToString("N").ToUpper() + Guid.NewGuid().ToString("N").ToUpper();
        }
    }
}