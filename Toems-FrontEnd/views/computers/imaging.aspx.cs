﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Toems_FrontEnd.views.computers
{
    public partial class imaging : BasePages.Computers
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                PopulateForm();
        }

        private void PopulateProfiles()
        {
            if (ddlComputerImage.Text == "Select Image") return;
            PopulateImageProfilesDdl(ddlImageProfile, Convert.ToInt32(ddlComputerImage.SelectedValue));
            try
            {
                ddlImageProfile.SelectedIndex = 1;
            }
            catch
            {
                //ignore
            }
        }
        protected void ddlComputerImage_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateProfiles();
        }

        private void PopulateForm()
        {
            PopulateImagesDdl(ddlComputerImage);
            ddlComputerImage.SelectedValue = ComputerEntity.ImageId.ToString();
            PopulateProfiles();
            ddlImageProfile.SelectedValue = ComputerEntity.ImageProfileId.ToString();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            ComputerEntity.ImageId = Convert.ToInt32(ddlComputerImage.SelectedValue);
            ComputerEntity.ImageProfileId =
                Convert.ToInt32(ddlComputerImage.SelectedValue) == -1
                    ? -1
                    : Convert.ToInt32(ddlImageProfile.SelectedValue);



            var result = Call.ComputerApi.Put(ComputerEntity.Id, ComputerEntity);
            EndUserMessage = result.Success ? String.Format("Successfully Updated Computer {0}", ComputerEntity.Name) : result.ErrorMessage;
        }
    }
}