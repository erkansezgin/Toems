﻿using System;
using System.Collections.Generic;
using System.Linq;
using Toems_Common;
using Toems_Common.Entity;

namespace Toems_FrontEnd.views.admin
{
    public partial class server : BasePages.Admin
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                PopulateForm();
            }

        }

        private void PopulateForm()
        {
            txtOrganization.Text = Call.SettingApi.GetSetting(SettingStrings.CertificateOrganization).Value;
        



        }

        protected void btnUpdateSettings_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOrganization.Text))
            {
                EndUserMessage = "Organization Must Not Be Empty";
                return;
            }
            if (!txtOrganization.Text.All(c => char.IsLetterOrDigit(c) || c == '_' || c == '-' || c == ' ' || c == '.'))
            {
                EndUserMessage = "Organization Can Only Contain Alphanumeric Characters And _ - space .";
                return;
            }
                var listSettings = new List<EntitySetting>
            {
              
                new EntitySetting
                {
                    Name = SettingStrings.CertificateOrganization,
                    Value = txtOrganization.Text,
                    Id = Call.SettingApi.GetSetting(SettingStrings.CertificateOrganization).Id
                },
            


            };


            if (Call.SettingApi.UpdateSettings(listSettings))
            {
                EndUserMessage = "Successfully Updated Settings";
            }
            else
            {
                EndUserMessage = "Could Not Update Settings";
            }
        }
    }
}