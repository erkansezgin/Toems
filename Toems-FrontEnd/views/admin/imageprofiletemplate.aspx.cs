﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Toems_Common.Entity;
using Toems_Common.Enum;

namespace Toems_FrontEnd.views.admin
{
    public partial class imageprofiletemplate : BasePages.Admin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlImageType.DataSource = Enum.GetNames(typeof(EnumProfileTemplate.TemplateType));
                ddlImageType.DataBind();
                DisplayForm();
            }
        }

        protected void btnUpdateSettings_OnClick(object sender, EventArgs e)
        {
            var template = new EntityImageProfileTemplate();
            template.TemplateType = (EnumProfileTemplate.TemplateType)
                Enum.Parse(typeof(EnumProfileTemplate.TemplateType), ddlImageType.SelectedValue);
            template.Name = txtProfileName.Text;
            template.Description = txtProfileDesc.Text;



            template.Kernel = ddlKernel.Text;
            template.BootImage = ddlBootImage.Text;
            template.WebCancel = chkWebCancel.Checked;
            template.TaskCompletedAction = ddlTaskComplete.Text;
            template.ChangeName = chkChangeName.Checked;
            template.SkipExpandVolumes = chkDownNoExpand.Checked;
            template.FixBcd = chkAlignBCD.Checked;
            template.RandomizeGuids = chkRandomize.Checked;
            template.FixBootloader = chkRunFixBoot.Checked;
            template.SkipNvramUpdate = chkNvram.Checked;

            if (ddlImageType.Text == EnumProfileTemplate.TemplateType.LinuxBlock.ToString() || ddlImageType.Text == EnumProfileTemplate.TemplateType.LinuxFile.ToString())
                template.PartitionMethod = ddlPartitionMethodLin.Text;
            else if (ddlImageType.Text == EnumProfileTemplate.TemplateType.WinPE.ToString())
                template.PartitionMethod = ddlPartitionMethodWin.Text;

            template.ForceStandardEfi = chkForceEfi.Checked;
            template.ForceStandardLegacy = chkForceLegacy.Checked;
            template.ForceDynamicPartitions = chkDownForceDynamic.Checked;
            template.RemoveGPT = chkRemoveGpt.Checked;
            template.SkipShrinkVolumes = chkUpNoShrink.Checked;
            template.SkipShrinkLvm = chkUpNoShrinkLVM.Checked;
            template.Compression = ddlCompAlg.Text;
            template.CompressionLevel = ddlCompLevel.Text;
            template.UploadSchemaOnly = chkSchemaOnly.Checked;
            template.SenderArguments = txtSender.Text;
            template.ReceiverArguments = txtReceiver.Text;
            template.SkipHibernationCheck = chkSkipHibernation.Checked;
            template.SkipBitlockerCheck = chkSkipBitlocker.Checked;
            var result = Call.ImageProfileTemplateApi.Put(template.Id,template);
            if (result.Success) EndUserMessage = "Successfully Updated " + template.TemplateType;
            else
            {
                EndUserMessage = "Could Not Update " + template.TemplateType;
            }
        }

        protected void chkForceDynamic_OnCheckedChanged(object sender, EventArgs e)
        {
            if (chkDownForceDynamic.Checked)
            {
                ddlPartitionMethodLin.Enabled = false;
                ddlPartitionMethodLin.Text = "Dynamic";
            }
            else
            {
                ddlPartitionMethodLin.Enabled = true;
            }
        }

        private void DisplayForm()
        {
            var template = Call.ImageProfileTemplateApi.Get((EnumProfileTemplate.TemplateType)
                    Enum.Parse(typeof(EnumProfileTemplate.TemplateType), ddlImageType.SelectedValue));

            txtProfileName.Text = template.Name;
            txtProfileDesc.Text = template.Description;

            ddlKernel.DataSource = Call.FilesystemApi.GetKernels();
            ddlBootImage.DataSource = Call.FilesystemApi.GetBootImages();
            ddlKernel.DataBind();
            ddlBootImage.DataBind();

            try
            {
                ddlKernel.Text = template.Kernel;
            }
            catch
            {
                //ignored
            }

            try
            {
                ddlBootImage.Text = template.BootImage;
            }
            catch
            {
                //ignored
            }


            chkWebCancel.Checked = Convert.ToBoolean(template.WebCancel);
            ddlTaskComplete.Text = template.TaskCompletedAction;
            chkChangeName.Checked = Convert.ToBoolean(template.ChangeName);
            chkDownNoExpand.Checked = Convert.ToBoolean(template.SkipExpandVolumes);
            chkAlignBCD.Checked = Convert.ToBoolean(template.FixBcd);
            chkRandomize.Checked = Convert.ToBoolean(template.RandomizeGuids);
            chkRunFixBoot.Checked = Convert.ToBoolean(template.FixBootloader);
            chkNvram.Checked = Convert.ToBoolean(template.SkipNvramUpdate);

            if (ddlImageType.Text == EnumProfileTemplate.TemplateType.LinuxBlock.ToString() || ddlImageType.Text == EnumProfileTemplate.TemplateType.LinuxFile.ToString())
                ddlPartitionMethodLin.Text = template.PartitionMethod;
            else if (ddlImageType.Text == EnumProfileTemplate.TemplateType.WinPE.ToString())
                ddlPartitionMethodWin.Text = template.PartitionMethod;

            chkForceEfi.Checked = Convert.ToBoolean(template.ForceStandardEfi);
            chkForceLegacy.Checked = Convert.ToBoolean(template.ForceStandardLegacy);
            chkDownForceDynamic.Checked = Convert.ToBoolean(template.ForceDynamicPartitions);
            chkRemoveGpt.Checked = Convert.ToBoolean(template.RemoveGPT);
            chkUpNoShrink.Checked = Convert.ToBoolean(template.SkipShrinkVolumes);
            chkUpNoShrinkLVM.Checked = Convert.ToBoolean(template.SkipShrinkLvm);
            ddlCompAlg.Text = template.Compression;
            ddlCompLevel.Text = template.CompressionLevel;
            chkSchemaOnly.Checked = Convert.ToBoolean(template.UploadSchemaOnly);
            txtSender.Text = template.SenderArguments;
            txtReceiver.Text = template.ReceiverArguments;
            chkSkipHibernation.Checked = Convert.ToBoolean(template.SkipHibernationCheck);
            chkSkipBitlocker.Checked = Convert.ToBoolean(template.SkipBitlockerCheck);

            LinuxAll1.Visible = false;
            LinuxAll2.Visible = false;
            LinuxAll3.Visible = false;
            LinuxAll4.Visible = false;
            LinuxAll5.Visible = false;
            LinuxAll6.Visible = false;
            LinuxBlock1.Visible = false;
            LinuxBlock2.Visible = false;
            LinuxFileWinpe1.Visible = false;
            winpe1.Visible = false;

            if (ddlImageType.Text == EnumProfileTemplate.TemplateType.LinuxBlock.ToString())
            {
                LinuxAll1.Visible = true;
                LinuxAll2.Visible = true;
                LinuxAll3.Visible = true;
                LinuxAll4.Visible = true;
                LinuxAll5.Visible = true;
                LinuxAll6.Visible = true;
                LinuxBlock1.Visible = true;
                LinuxBlock2.Visible = true;
            }
            else if (ddlImageType.Text == EnumProfileTemplate.TemplateType.LinuxFile.ToString())
            {
                LinuxAll1.Visible = true;
                LinuxAll2.Visible = true;
                LinuxAll3.Visible = true;
                LinuxAll4.Visible = true;
                LinuxAll5.Visible = true;
                LinuxAll6.Visible = true;
                LinuxFileWinpe1.Visible = true;
            }

            else if (ddlImageType.Text == EnumProfileTemplate.TemplateType.WinPE.ToString())
            {
                LinuxFileWinpe1.Visible = true;
                winpe1.Visible = true;
            }
        }

        protected void ddlImageType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayForm();
        }
    }
}